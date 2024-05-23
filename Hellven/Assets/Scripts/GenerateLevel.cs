using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{
    public GlobalData globalData;

    public Tilemap tilemap;
    public TileBase[] tileTypes;

    public int mapWidth = 100;
    public int mapHeight = 100;
    public float radius = 50f;

    private bool[,] map;
    private Dictionary<int, TileBase> tileConfigurations = new Dictionary<int, TileBase>();

    private Dictionary<int, TileBase> debugTiles = new Dictionary<int, TileBase>();

    void Start()
    {
        InitializeTileConfigurations();
        GenerateMap();
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

    void GenerateMap()
    {
        mapWidth = 5 + globalData.level + globalData.stage * 10;
        mapHeight = 5 + globalData.level + globalData.stage * 10;
        radius = 4 + globalData.level + globalData.stage * 10;


        map = new bool[2 * mapWidth + 1, 2 * mapHeight + 1];

        for (int x = -mapWidth; x <= mapWidth; x++)
            for (int y = -mapHeight; y <= mapHeight; y++)
                if (x * x + y * y < radius * radius)
                    map[x + mapWidth, y + mapHeight] = true;
/*                else if (x * x + y * y < radius * radius)
                    map[x + mapWidth, y + mapHeight] = true;*/
                else
                    map[x + mapWidth, y + mapHeight] = false;
    }

    void GenerateTerrain()
    {
        // Set corner tiles
        setTile(-mapWidth, -mapHeight, GetFromSouranding(map[1, 0], map[0, 1], false, false));
        setTile(-mapWidth,  mapHeight, GetFromSouranding(map[1, 0], false, false, map[0, mapHeight * 2 - 1]));
        setTile( mapWidth, -mapHeight, GetFromSouranding(false, map[0, 1], map[mapWidth * 2 - 1, 0], false));
        setTile( mapWidth,  mapHeight, GetFromSouranding(false, false, map[mapWidth * 2 - 1, 0], map[0, mapHeight * 2 - 1]));

        // Set edge tiles
        for (int x = 1; x < 2 * mapWidth; x++)
        {
            setTile(x - mapWidth, -mapHeight, GetFromSouranding(map[x, 1], map[x + 1, 0], false, map[x - 1, 0]));
            setTile(x - mapWidth, mapHeight, GetFromSouranding(false, map[x + 1, mapHeight * 2], map[x, mapHeight * 2 - 1], map[x - 1, mapHeight * 2]));
        }

        for (int y = 1; y < 2 * mapHeight; y++)
        {
            setTile(-mapWidth, y - mapHeight, GetFromSouranding(map[0, y + 1], map[1, y], map[0, y - 1], false));
            setTile(mapWidth, y - mapHeight, GetFromSouranding(map[mapWidth * 2, y + 1], false, map[mapWidth * 2 - 1, y], map[mapWidth * 2, y - 1]));
        }

        // Set inner tiles
        for (int x = 1; x < 2 * mapWidth; x++)
        {
            for (int y = 1; y < 2 * mapHeight; y++)
            {
                setTile(x - mapWidth, y - mapHeight, GetTileFromCoords(x, y));
            }
        }
    }

    void setTile(int x, int y, TileBase tile)
    {
        tilemap.SetTile(new Vector3Int(x, y, 0), tile);
    }

    TileBase GetFromSouranding(bool N, bool E, bool S, bool W)
    {
        return tileConfigurations[(N ? 8 : 0) + (E ? 4 : 0) + (S ? 2 : 0) + (W ? 1 : 0)];
    }

    TileBase GetTileFromCoords(int x, int y)
    {
        return map[x, y] ?
            GetFromSouranding(
            map[x, y + 1],
            map[x + 1, y],
            map[x, y - 1],
            map[x - 1, y]
        ) : tileConfigurations[0];
    }
}