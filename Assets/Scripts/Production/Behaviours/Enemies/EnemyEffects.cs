using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffects : MonoBehaviour
{
    [SerializeField] float effectEffiency = 1;
    float slowEffectTime = 0;
    bool isSlowed = false;
    public void Slow(float time)
    {
        EnemyNavigation nav = gameObject.GetComponent<EnemyNavigation>();
        if (nav)
        {
            nav.Speed *= 0.5f;
            slowEffectTime = time * effectEffiency;
            isSlowed = true;
        }
    }
    private void Update()
    {
        if(slowEffectTime > 0)
        {
            slowEffectTime -= Time.deltaTime;
        }
        else if (isSlowed)
        {
            EnemyNavigation nav = gameObject.GetComponent<EnemyNavigation>();
            if (nav)
            {
                nav.Speed = nav.Speed;
                isSlowed = false;
            }
        }
    }
}
