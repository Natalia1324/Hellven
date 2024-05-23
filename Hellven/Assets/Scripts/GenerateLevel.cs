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

    private bool[,] map;
    private Dictionary<int, TileBase> tileConfigurations = new Dictionary<int, TileBase>();

    void Start()
    {
        InitializeTileConfigurations();
        GenerateTerrain();
    }

    void InitializeTileConfigurations()
    {
        // NESW -> 8421 -> 15
        for (int i = 0; i < 16; i++)
        {
            tileConfigurations.Add(i, tileTypes[i]);
        }
    }

        void GenerateTerrain()
        {
            map = new bool[2 * mapWidth + 1, 2 * mapHeight + 1];

            for (int x = -mapWidth; x <= mapWidth; x++)
                for (int y = -mapHeight; y <= mapHeight; y++)
                    if (x * x < y * y + radius * radius * 1.9)
                        map[x + mapWidth, y + mapHeight] = true;
                    else if (x * x < y * y + radius * radius * 2.1 && UnityEngine.Random.Range(0, 2) == 0)
                        map[x + mapWidth, y + mapHeight] = true;
                    else
                        map[x + mapWidth, y + mapHeight] = false;

            tilemap.SetTile(new Vector3Int(            0,             0, 0), GetFromSouranding( map[1, 0], map[0, 1],                        0,                         0 ) ) ;
            tilemap.SetTile(new Vector3Int(            0, mapHeight * 2, 0), GetFromSouranding( map[1, 0],         0,                        0, map[0, mapHeight * 2 - 1] ) ) ;
            tilemap.SetTile(new Vector3Int( mapWidth * 2,             0, 0), GetFromSouranding(         0, map[0, 1], map[mapWidth * 2 - 1, 0],                         0 ) ) ;
            tilemap.SetTile(new Vector3Int( mapWidth * 2, mapHeight * 2, 0), GetFromSouranding(         0,         0, map[mapWidth * 2 - 1, 0], map[0, mapHeight * 2 - 1] ) ) ;

            for (int x = 1; x < 2 * mapWidth; x++)
            {
                tilemap.SetTile(new Vector3Int(x, 0, 0), GetFromSouranding(map[x,1], map[x+1, 0], 0, map[x-1, 0]));
                tilemap.SetTile(new Vector3Int(x, mapHeight * 2, 0), GetFromSouranding(0, map[x + 1, mapHeight * 2], map[x, mapHeight * 2 - 1], map[x - 1, mapHeight * 2]));
            }

            for (int y = 1; y < 2 * mapHeight; y++)
            {

            }

            for (int x = 1; x < 2 * mapWidth; x++)
            {
                for (int y = 1; y < 2 * mapHeight; y++)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), GetTileFromCoords(x, y));
                }
            }
        }

        TileBase GetFromSouranding(int N, int S, int W, int E)
        {
            return tileConfigurations[N * 8 + S * 4 + W * 2 + E]
        }

        TileBase GetTileFromCoords(int x, int y)
        {
            int N = map[x, y + 1] ? 1 : 0;
            int S = map[x, y - 1] ? 1 : 0;
            int W = map[x - 1, y] ? 1 : 0;
            int E = map[x + 1, y] ? 1 : 0;

            return GetFromSouranding( N * 8 + S * 4 + W * 2 + E );
        }
    }
