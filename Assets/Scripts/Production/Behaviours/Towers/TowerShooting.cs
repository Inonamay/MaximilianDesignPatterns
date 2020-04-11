using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
public enum FireMode
{
    First, Last
}
public class TowerShooting : MonoBehaviour
{
    List<EnemyHealth> enemiesInRange = new List<EnemyHealth>();
    [SerializeField] Tower stats = null;
    [SerializeField] GameObject rotatorPart = null;
    Coroutine shooting;
    [SerializeField] FireMode fireMode = FireMode.First;
    GameObjectPool bullets;
    const string shootFunction = "Shooting";
    public FireMode FireModeConfig
    {
        get
        {
            return fireMode;
        }
        set
        {
            fireMode = value;
        }
    }

    public void Shoot()
    {
        bool foundTarget = false;
        EnemyHealth target = enemiesInRange[0]; 
        while (!foundTarget)
        {
            target = enemiesInRange[0];
            switch (fireMode)
            {
                case FireMode.Last: target = enemiesInRange[enemiesInRange.Count - 1]; break;
                default: break;
            }
            if (target.isActiveAndEnabled)
            {
                foundTarget = true;
            }
            else
            {
                enemiesInRange.Remove(target);
            }
            if (enemiesInRange.Count == 0)
            {
                return;
            }
        }
        GameObject bullet = bullets.Rent(true);
        bullet.transform.position = rotatorPart.transform.position;
        bullet.transform.rotation = rotatorPart.transform.rotation;
        BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
        bulletMovement.Target = target.transform;
    }
    IEnumerator Shooting()
    {
        while(enemiesInRange.Count > 0)
        {
            Shoot();
            yield return new WaitForSeconds(stats.ShootCooldown);
        }
        StopShooting();
    }
    private void Awake()
    {
        bullets = new GameObjectPool(2, stats.Projectile, 1, transform);
    }
    void StartShooting()
    {
        shooting = StartCoroutine(shootFunction);
    }
    void StopShooting()
    {
        StopCoroutine(shooting);
    }
    private void Update()
    {
        if (rotatorPart != null && enemiesInRange.Count > 0)
        {
            rotatorPart.transform.rotation = Quaternion.LookRotation((enemiesInRange[0].transform.position - transform.position).normalized, Vector3.up);
        }
        else if (rotatorPart != null)
        {
            rotatorPart.transform.rotation = Quaternion.Lerp(rotatorPart.transform.rotation, new Quaternion(0,0,0,1), Time.deltaTime * 4);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.attachedRigidbody != null)
        {
            EnemyHealth enemy = other.attachedRigidbody.gameObject.GetComponent<EnemyHealth>();
            if (enemy)
            {
                enemiesInRange.Add(enemy);
                if (enemiesInRange.Count == 1)
                {
                    StartShooting();
                }
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            EnemyHealth enemy = other.attachedRigidbody.gameObject.GetComponent<EnemyHealth>();
            if (enemy && enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }
}
