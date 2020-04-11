using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/Create Enemy Data", order = 1)]
public class Enemies : ScriptableObject
{
    [SerializeField] GameObject enemyType1 = null;
    [SerializeField] GameObject enemyType2 = null;
    public GameObject EnemyType1
    {
        get
        {
            return enemyType1;
        }
    }
    public GameObject EnemyType2
    {
        get
        {
            return enemyType2;
        }
    }
}
