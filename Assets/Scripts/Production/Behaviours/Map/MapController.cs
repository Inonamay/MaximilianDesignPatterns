using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] Map mapDataObject;
    [SerializeField] int[,] mapValues;
    [SerializeField] List<GameObject> mapObjects = new List<GameObject>();
    [SerializeField] List<Vector2Int> path;
    [SerializeField] Enemies enemyDataObject;
    [SerializeField] Dictionary<TileType, GameObject> sortingObjects = new Dictionary<TileType, GameObject>();
    [SerializeField] float timeBetweenEnemies = 0.1f;
    bool hasInitialized;
    public float TimeBetweenEnemies
    {
        get
        {
            return timeBetweenEnemies;
        }
        set
        {
            if(value >= 0)
            {
                timeBetweenEnemies = value;
            }
        }
    }
    public Enemies EnemyDataObject
    {
        get
        {
            return enemyDataObject;
        }
        set
        {
            if (value != null)
            {
                if (value.EnemyType1 == null)
                {
                    Debug.LogError("No Type 1 Enemy assigned!");
                    return;
                }
            }
            enemyDataObject = value;
        }

    }
    public List<Vector2Int> Path
    {
        get
        {
            return path;
        }
    }
    public Map mapData
    {
        get
        {
            return mapDataObject;
        }
        set
        {
            if (value != null)
            {
                if (value.FileAsset == null)
                {
                    Debug.LogError("No mapfile assigned in the object!");
                    return;
                }
                if (value.PrefabsAndTypes.Length != TileMethods.TypeAmounts)
                {
                    Debug.LogWarning("Wrong amount of prefabs in the object!");
                }
            }
            mapDataObject = value;
        }
    }
    public void Initialize()
    {
        if(enemyController == null)
        {
            hasInitialized = false;
        }
        if (!hasInitialized)
        {
            if(enemyController == null)
            {
                enemyController = new EnemyController(gameObject.transform , enemyDataObject, MapReader.GetEnemies(mapDataObject, enemyDataObject), path);
                enemyController.TimeBetween = timeBetweenEnemies;
            }
            hasInitialized = true;
        }
       
    }
    [ExecuteAlways]
    public void ResetMap()
    {
        for (int i = 0; i < mapObjects.Count; i++)
        {
#if UNITY_EDITOR
            DestroyImmediate(mapObjects[i]);
#else
            Destroy(mapObjects[i]);
#endif

            mapObjects[i] = null;
        }
       
        mapObjects.Clear();
        path.Clear();
        if (sortingObjects != null)
        {
            foreach (KeyValuePair<TileType, GameObject> item in sortingObjects)
            {
                DestroyImmediate(item.Value);
            }
            sortingObjects.Clear();
        }
    }
    [ExecuteAlways]
    public void CreateMap()
    {
        mapValues = MapReader.ReadMap(mapDataObject);
        BuildMap(mapValues);
        path = MapReader.GeneratePath(mapValues);
    }
    // Start is called before the first frame update
    void Awake()
    {
        if(mapObjects.Count == 0)
        {
            CreateMap();
        }
        Initialize();
    }

    [ExecuteAlways]
    public void BuildMap(int[,] mapValues)
    {
        ResetMap();
        Dictionary<TileType, GameObject> prefabs = new Dictionary<TileType, GameObject>();
        sortingObjects = new Dictionary<TileType, GameObject>();
        foreach (MapData data in mapDataObject.PrefabsAndTypes)
        {
            prefabs.Add(data.type, data.prefab);
            sortingObjects.Add(data.type, new GameObject(data.type.ToString()));
            sortingObjects[data.type].transform.parent = transform;
        }
        for (int x = 0; x < mapValues.GetLength(0); x++)
        {
            GameObject block;
            for (int y = 0; y < mapValues.GetLength(1); y++)
            {
                int number = mapValues[x, y];
                TileType type = TileMethods.TypeById[number];
                if (!prefabs.ContainsKey(type))
                { continue; }
                block = Instantiate(prefabs[type]);
                mapObjects.Add(block);
                block.transform.parent = sortingObjects[type].transform;
                block.transform.localPosition = Vector3.right * x * 2 + Vector3.forward * y * 2;
            }
        }
       
    }
    [ExecuteAlways]
    private void OnDestroy()
    {
        ResetMap();
    }
}
