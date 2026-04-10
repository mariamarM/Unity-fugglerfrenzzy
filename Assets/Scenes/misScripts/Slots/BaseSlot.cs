using UnityEngine;

public enum SlotType
{
    Board,
    Bench
}

public abstract class BaseSlot : MonoBehaviour
{
    public Vector2Int gridPosition;
    public SlotType slotType;
    public bool isOccupied = false;
    public GameObject currentUnit;
    public Color originalColor;
    
    public virtual bool CanPlaceUnit(Team unitTeam)
    {
        
        return !isOccupied;
    }
    
    public virtual void PlaceUnit(GameObject unit, UnitDraggable draggable)
    {
        isOccupied = true;
        currentUnit = unit;
        draggable.currentSlot = this;
        unit.transform.position = transform.position;
    }
    
    public virtual void RemoveUnit()
    {
        isOccupied = false;
        currentUnit = null;
    }
    
    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }
}