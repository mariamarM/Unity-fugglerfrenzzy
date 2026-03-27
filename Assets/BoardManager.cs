using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int width = 8;
    public int height = 8;
    public float tileSize = 1f;

    void Start()
    {
        Debug.Log("Tablero iniciado");
        
    }

    void Update()
    {

    }

    public Vector3 GetTilePosition(int x, int z)
    {
        return new Vector3(x * tileSize, 0, z * tileSize);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x * tileSize, 0, z * tileSize);
                Gizmos.DrawWireCube(pos, new Vector3(tileSize, 0, tileSize));
            }
        }
    }
}