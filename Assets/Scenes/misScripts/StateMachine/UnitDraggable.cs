using UnityEngine;
using System.Collections;

public class UnitDraggable : MonoBehaviour
{
    [Header("Configuración")]
    public Team unitTeam;
    public float snapSpeed = 15f;
    public float dragHeight = 2f;
    
    [HideInInspector]
    public BaseSlot currentSlot; // Puede ser BoardSlot o BenchSlot
    
    private bool isDragging = false;
    private Vector3 dragOffset;
    private BaseSlot targetSlot;
    private Vector3 originalPosition;
    private Camera mainCamera;
    private Collider unitCollider;
    private Rigidbody rb;
    
    // Máscaras de capas según contexto
    private int boardLayerMask;
    private int benchLayerMask;
    
    void Start()
    {
        mainCamera = Camera.main;
        unitCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        
        boardLayerMask = LayerMask.GetMask("Board");
        benchLayerMask = LayerMask.GetMask("Bench");
        
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        
        gameObject.layer = LayerMask.NameToLayer("Unit");
    }
    
    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            transform.position = mousePosition + dragOffset;
            DetectSlotUnderMouse();
        }
        else if (targetSlot != null)
        {
            // Efecto imán
            transform.position = Vector3.Lerp(transform.position, targetSlot.transform.position, snapSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetSlot.transform.position) < 0.05f)
            {
                transform.position = targetSlot.transform.position;
                CompletePlacement();
            }
        }
    }
    
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = dragHeight;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
    
    void DetectSlotUnderMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
     //esto es lo de boarc para q sepa que hay una casilla debapo del raton 
        if (Physics.Raycast(ray, out hit, 100f, boardLayerMask))
        {
            BoardSlot slot = hit.collider.GetComponent<BoardSlot>();
            if (slot != null && slot.CanPlaceUnit(unitTeam))
            {
                SetTargetSlot(slot);
                return;
            }
        }
        
        // lo mismo en bench y q use el collider para detectar blabla
        if (Physics.Raycast(ray, out hit, 100f, benchLayerMask))
        {
            BenchSlot slot = hit.collider.GetComponent<BenchSlot>();
            if (slot != null && slot.CanPlaceUnit(unitTeam))
            {
                SetTargetSlot(slot);
                return;
            }
        }
        
        ClearTargetSlot();
    }
    
    void SetTargetSlot(BaseSlot slot)
    {
        if (targetSlot == slot) return;
        ClearTargetSlot();
        targetSlot = slot;
        HighlightSlot(targetSlot, true);
    }
    
    void ClearTargetSlot()
    {
        if (targetSlot != null)
        {
            HighlightSlot(targetSlot, false);
            targetSlot = null;
        }
    }
    
    void HighlightSlot(BaseSlot slot, bool highlight)
    {
        Renderer renderer = slot.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = highlight ? Color.green : slot.originalColor;
        }
    }
    
    void CompletePlacement()
    {
        // quitas el elemento dentro del slot 
        if (currentSlot != null)
        {
            currentSlot.RemoveUnit();
        }
        
        //esto es par colocarf el nueo
        targetSlot.PlaceUnit(gameObject, this);
        currentSlot = targetSlot;
        targetSlot = null;
        
        // Congelar físicamente
        // if (rb != null)
        // {
        //     rb.isKinematic = true;
        //     rb.constraints = RigidbodyConstraints.FreezeAll;
        // }
        
        // esto hace quE Deshabilitar arrastre si no queremos que se muevan después
        // enabled = false;
    }
    
    public void StartDrag()
    {
        if (currentSlot != null)
        {
            currentSlot.RemoveUnit();
            currentSlot = null;
        }
        
        isDragging = true;
        dragOffset = transform.position - GetMouseWorldPosition();
        originalPosition = transform.position;
        
        if (unitCollider != null) unitCollider.enabled = true;
    }
    
    public void EndDrag()
    {
        isDragging = false;
        
        if (targetSlot == null)
        {
            StartCoroutine(ReturnToOriginal());
        }
    }
    
    IEnumerator ReturnToOriginal()
    {
        float elapsed = 0;
        Vector3 startPos = transform.position;
        while (elapsed < 0.3f)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, originalPosition, elapsed / 0.3f);
            yield return null;
        }
        transform.position = originalPosition;
    }
    
    void OnMouseDown()
    {
        StartDrag();
    }
    
    void OnMouseUp()
    {
        EndDrag();
    }
}