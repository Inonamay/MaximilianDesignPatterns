using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 10;
    int currentHitPoints = 0;
    public int HitPoints
    {
        get
        {
            return currentHitPoints;
        }
        set
        {
            if(value <= 0)
            {
                Death();
                return;
            }
            currentHitPoints = value;
        }
    }
    private void Start()
    {
        currentHitPoints = maxHitPoints;
    }
    public void Death()
    {
        currentHitPoints = maxHitPoints;
        gameObject.SetActive(false);
    }
}
