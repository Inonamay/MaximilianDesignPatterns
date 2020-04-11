using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/Create Tower", order = 2)]
public class Tower : ScriptableObject
{
    [SerializeField] GameObject projectile = null;
    [SerializeField] float shootCooldown = 1;

    public GameObject Projectile
    {
        get
        {
            return projectile;
        }
    }
    public float ShootCooldown
    {
        get
        {
            return shootCooldown;
        }
    }
}
