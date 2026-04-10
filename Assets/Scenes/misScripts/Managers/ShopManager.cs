using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject unitPrefab;  // Arrastra el prefab de la unidad aquí
    
    void Update()
    {
        // Presiona la tecla B para comprar
        if (Input.GetKeyDown(KeyCode.B))
        {
            BuyUnit();
        }
    }
    
    public void BuyUnit()
    {
        // Buscar un espacio vacío en el banquillo
        BenchSlot emptySlot = FindObjectOfType<BenchManager>().GetEmptySlot();
        
        if (emptySlot == null)
        {
            Debug.Log("Banquillo lleno");
            return;
        }
        
        // Crear la unidad
        GameObject newUnit = Instantiate(unitPrefab);
        UnitDraggable draggable = newUnit.GetComponent<UnitDraggable>();
        draggable.unitTeam = Team.Player;
        
        // Colocarla en el banquillo
        emptySlot.PlaceUnit(newUnit, draggable);
        draggable.currentSlot = emptySlot;
    }
}