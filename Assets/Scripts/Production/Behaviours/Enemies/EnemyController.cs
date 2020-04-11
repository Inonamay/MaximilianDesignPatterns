using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
public class EnemyController
{
    List<Vector2Int> path;
    GameObjectPool enemiesType1;
    GameObjectPool enemiesType2;
    List<GameObject> activeEnemies = new List<GameObject>();
    Enemies enemyData;
    Transform parent;
    MapEnemiesData[] waveData;
    int currentWave = 0;
    Vector3 startPoint;
    float timeBetweenEnemies = 0.1f;
    public float TimeBetween
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
    public Enemies EnemyData
    {
        get
        {
            return enemyData;
        }
        set
        {
            if(value != null)
            {
                if(value.EnemyType1 == null)
                {
                    Debug.LogError("No Type 1 Enemy assigned!");
                    return;
                }
            }
            enemyData = value;
        }
    }
    public List<Vector2Int> Path
    {
        get
        {
            return path;
        }
        set
        {
            if(value != null)
            {
                if (value.Count > 0)
                {
                    path = value;
                }
            }
           
        }
    }
    public void SpawnNextWave()
    {
        currentWave++;
        if(currentWave < waveData.Length)
        {
            SpawnWave(currentWave);
        }
        
    }
    public void RemoveActiveEnemy(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }
        if(activeEnemies.Count == 0)
        {
            SpawnNextWave();
        }
    }
    public void SpawnWave(int index)
    {
        if(index >= waveData.Length)
        {
            return;
        }
        currentWave = index;
        int enemies = waveData[index].waveInfo[UnitType.Standard];
        for (int i = 0; i < enemies; i++)
        {
            CreateEnemy(i * timeBetweenEnemies);
        }
        enemies = waveData[index].waveInfo[UnitType.Big];
        for (int i = 0; i < enemies; i++)
        {
            CreateEnemy(i * timeBetweenEnemies, UnitType.Big);
        }
    }
    public EnemyController(Transform _parent, Enemies _enemyData, MapEnemiesData[] _waveData, List<Vector2Int> _path , bool startImmediatly = true)
    {
        enemyData = _enemyData;
        parent = _parent;
        waveData = _waveData;
        path = _path;
        enemiesType1 = new GameObjectPool(10, enemyData.EnemyType1, 1, parent);
        enemiesType2 = new GameObjectPool(10, enemyData.EnemyType2, 1, parent); 
        startPoint = new Vector3(path[0].x * 2, 1, path[0].y * 2);
        if (startImmediatly)
        {
            SpawnWave(0);
        }
    }
    ~EnemyController()
    {
        enemiesType1.Dispose();
    }
    public void CreateEnemy(float sleepTime = 0f, UnitType type = UnitType.Standard )
    {
        GameObject enemyInstance;
        if (type == UnitType.Standard)
        {
            enemyInstance = enemiesType1.Rent(true);
        }
        else
        {
            enemyInstance = enemiesType2.Rent(true);
        }
        
        EnemyNavigation nav = enemyInstance.GetComponent<EnemyNavigation>();
        if (nav)
        {
            nav.SetPath(path);
            nav.SleepTime += sleepTime;
            nav.Controller = this;
        }
        activeEnemies.Add(enemyInstance);
        enemyInstance.transform.localPosition = parent.TransformPoint(startPoint);
    }
}
