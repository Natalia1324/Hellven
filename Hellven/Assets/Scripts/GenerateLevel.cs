using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] tileTypes;

    public int mapWidth = 10;
    public int mapHeight = 10;
    public float radius = 5f;

    private Dictionary<int, TileBase> tileConfigurations = new Dictionary<int, TileBase>();

    void Start()
    {
        InitializeTileConfigurations();
        GenerateTerrain();
    }

    void InitializeTileConfigurations()
    {
        // NSWE -> 1111 -> 15
        for(int i = 0; i < 16; i++)
        {
            tileConfigurations.Add(i, tileTypes[i]);
        }
    }

    void GenerateTerrain()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                int configuration = GetTileConfiguration(x, y);
                TileBase tile = tileConfigurations[configuration];
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    int GetTileConfiguration(int x, int y)
    {
        return 15;
    }
}
