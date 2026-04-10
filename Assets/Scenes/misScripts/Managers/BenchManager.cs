using UnityEngine;
using System.Collections;
public class BenchManager : MonoBehaviour
{
    public static BenchManager Instance;
    public int benchSize = 9;
    public float cellSize = 1.2f;
    public Vector3 benchStartPos;
    public GameObject benchSlotPrefab;
    
    private BenchSlot[] slots;
    
    void Awake() { Instance = this; CreateBench(); }
    
    void CreateBench()
    {
        slots = new BenchSlot[benchSize];
        for (int i = 0; i < benchSize; i++)
        {
            Vector3 pos = benchStartPos + new Vector3(i * cellSize, 0, 0);
            GameObject slotGO = Instantiate(benchSlotPrefab, pos, Quaternion.identity, transform);
            BenchSlot slot = slotGO.GetComponent<BenchSlot>();
            slot.gridPosition = new Vector2Int(i, 0);
            slots[i] = slot;
            slotGO.layer = LayerMask.NameToLayer("Bench");
        }
    }
    
    public BenchSlot GetEmptySlot()
    {
        foreach (var slot in slots)
            if (!slot.isOccupied) return slot;
        return null;
    }
}