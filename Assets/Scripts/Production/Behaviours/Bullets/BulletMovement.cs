using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] float lifeSpan = 1;
    [SerializeField] OnHitEffect onHitEffect = null;
    Transform target;
   
    public Transform Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }
    float life;
    void Update()
    {
        if(life > 0)
        {
            life -= Time.deltaTime;
        }
        else if(life != -1)
        {
            gameObject.SetActive(false);
        }
        
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, target.position) < 1)
        {
            onHitEffect.OnHit(target.gameObject.GetComponent<EnemyHealth>());
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        life = lifeSpan;
    }
}
