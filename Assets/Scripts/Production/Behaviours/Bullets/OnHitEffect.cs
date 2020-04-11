using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DamageType
{
    Single, AOE
}
public enum DamageEffect
{
    None, Slow
}
[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/Create Bullet", order = 3)]
public class OnHitEffect : ScriptableObject
{
    [SerializeField] DamageType damageType = DamageType.Single;
    [SerializeField, Tooltip("Leave empty if damage type is not AOE")] float explosionRadius = 0;
    [SerializeField, Tooltip("Leave empty if damage type is not AOE")] LayerMask enemyLayers;
    [SerializeField] int damage = 1;
    [SerializeField] DamageEffect effect = DamageEffect.None;
    [SerializeField] float effectDuration = 1;
    public void OnHit(EnemyHealth enemy)
    {
        List<EnemyHealth> targets = new List<EnemyHealth>();
        
        if(damageType == DamageType.Single)
        {
            enemy.HitPoints -= damage;
            targets.Add(enemy);
        }
        else
        {
            
            Collider[] area = Physics.OverlapSphere(enemy.transform.position, explosionRadius, enemyLayers);
            for (int i = 0; i < area.Length; i++)
            {
                EnemyHealth tempEnemy = area[i].attachedRigidbody?.gameObject.GetComponent<EnemyHealth>();
                if(tempEnemy != null)
                {
                    tempEnemy.HitPoints -= damage;
                    targets.Add(tempEnemy);
                }
            }
        }
        switch (effect)
        {
            case DamageEffect.None:
                break;
            case DamageEffect.Slow:
                ApplySlow(targets);
                break;
            default:
                break;
        }
    }
    void ApplySlow(List<EnemyHealth> targets)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            EnemyEffects target = targets[i].GetComponent<EnemyEffects>();
            if (target)
            {
                target.Slow(effectDuration);
            }
        }
    }
    public void OnHit(Vector3 point)
    {
        if(damageType == DamageType.AOE)
        {
            List<EnemyHealth> targets = new List<EnemyHealth>();
            Collider[] area = Physics.OverlapSphere(point, explosionRadius, enemyLayers);
            for (int i = 0; i < area.Length; i++)
            {
                EnemyHealth tempEnemy = area[i].gameObject.GetComponent<EnemyHealth>();
                if (tempEnemy != null)
                {
                    tempEnemy.HitPoints -= damage;
                    targets.Add(tempEnemy);
                }
            }
            if (effect == DamageEffect.Slow)
            {
                ApplySlow(targets);
            }
        }
        
    }
}
