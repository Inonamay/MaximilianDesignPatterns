using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct MapData
{
    public TileType type;
    public GameObject prefab;

}
[System.Serializable]
public struct MapEnemiesData
{
    public Dictionary<UnitType, int> waveInfo;
}

public static class MapReader 
{
    public static List<Vector2Int> GeneratePath(int[,] mapValues)
    {
        List<Vector2Int> walkable = new List<Vector2Int>();
        Vector2Int start = Vector2Int.zero;
        Vector2Int end = Vector2Int.zero;
        for (int x = 0; x < mapValues.GetLength(0); x++)
        {
            for (int y = 0; y < mapValues.GetLength(1); y++)
            {
                int number = mapValues[x, y];
                TileType type = TileMethods.TypeById[number];
                if (TileMethods.IsWalkable(type))
                {
                    walkable.Add(new Vector2Int(x, y));
                }
                switch (type)
                {
                    case TileType.Start: start = new Vector2Int(x, y); break;
                    case TileType.End: end = new Vector2Int(x, y); break;
                    default: break;
                }
            }
        }
        AI.IPathFinder pathFinder = new AI.Dijkstra(walkable);
        walkable = (List<Vector2Int>)pathFinder.FindPath(start, end);
        return walkable;
    }
    public static MapEnemiesData[] GetEnemies(Map mapDataObject, Enemies enemyDataObject)
    {
        string file = mapDataObject.FileAsset.text.Split('#')[1];
        string[] waves = file.Split('\n');

        string[] amount;
        MapEnemiesData[] mapWaves = new MapEnemiesData[waves.Length - 1];
        for (int x = 1; x < mapWaves.Length + 1; x++)
        {
            amount = waves[x].Split(' ');
            mapWaves[x - 1].waveInfo = new Dictionary<UnitType, int>();
            for (int y = 0; y < amount.Length; y++)
            {
                UnitType type = UnitType.Standard;
                amount[y] = amount[y].Trim();
                if(y == 1)
                {
                    type = UnitType.Big;
                }
                mapWaves[x - 1].waveInfo.Add(type, int.Parse(amount[y]));
            }
        }
        return mapWaves;
    }
    public static int[,] ReadMap(Map mapDataObject)
    {
        string map = mapDataObject.FileAsset.text.Split('#')[0];
        string[] row = map.Split('\n');
        
        char[] columns = row[0].ToCharArray();
        int[,] mapValues = new int[row.Length - 1, columns.Length];
        for (int x = 0; x < row.Length - 1; x++)
        { 
            columns = row[x].ToCharArray();
            for (int y = 0; y < columns.Length; y++)
            {
                mapValues[x, y] = (int)char.GetNumericValue(columns[y]);
            }
        }
        return mapValues;
    }
   

}
