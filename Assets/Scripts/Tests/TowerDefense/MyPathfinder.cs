using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class MyPathfinder : IPathFinder
{
    List<Vector2Int> map;
    public MyPathfinder(List<Vector2Int> _Accessables)
    {
        map = _Accessables;
    }
    public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        for (int i = 0; i < map.Count; i++)
        {
            path.Add(new Vector2Int());
        }
        return path;
    }
}
