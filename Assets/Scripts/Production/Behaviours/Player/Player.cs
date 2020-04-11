using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    [SerializeField] int maxHitPoints = 20;
    static int hitPoints;
    [SerializeField] int startingMoney = 10;
    int money = 0;
    public static int HitPoints
    {
        get
        {
            return hitPoints;
        }
        set
        {
            if(value < 1)
            {
                GameOver();
            }
            else
            {
                hitPoints = value;
            }
        }
    }
    private void Awake()
    {
        money = startingMoney;
        hitPoints = maxHitPoints;
    }
    public static void GameOver()
    {
        Debug.Log("Dead");
    }
}
