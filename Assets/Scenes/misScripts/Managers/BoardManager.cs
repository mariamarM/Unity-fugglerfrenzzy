using UnityEngine;
using System.Collections;
public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    public int rows = 4;
    public int columns = 8;
    public float cellSize = 1.5f;
    public Vector3 boardCenter;
    public int playerColumns = 4;
    public GameObject boardSlotPrefab;
    
    private BoardSlot[,] slots;
    
    void Awake() { Instance = this; CreateBoard(); }
    
    void CreateBoard()
    {
        slots = new BoardSlot[rows, columns];
        Vector3 startPos = boardCenter - new Vector3((columns-1) * cellSize / 2, 0, (rows-1) * cellSize / 2);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 pos = startPos + new Vector3(col * cellSize, 0, row * cellSize);
                GameObject slotGO = Instantiate(boardSlotPrefab, pos, Quaternion.identity, transform);
                BoardSlot slot = slotGO.GetComponent<BoardSlot>();
                slot.gridPosition = new Vector2Int(col, row);
                slot.assignedTeam = col < playerColumns ? Team.Player : Team.Enemy;
                slots[row, col] = slot;
                slotGO.layer = LayerMask.NameToLayer("Board");
            }
        }
    }
}