using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    List<Vector2Int> path;
    int indexAlongPath = 0;
    [SerializeField] float speed = 5;
    [SerializeField] float initialSleepTime = 0f;
    [SerializeField] int damage = 1;
    float currentSpeed = 5;
    float currentSleepTime = 0;
    float sleepTimer = 0;
    EnemyController controller;
    public EnemyController Controller
    {
        get
        {
            return controller;
        }
        set
        {
            controller = value;
        }
    }
    public float SleepTime
    {
        get
        {
            return currentSleepTime;
        }
        set
        {
            if(value < 0)
            {
                currentSleepTime = 0;
            }
            else
            {
                currentSleepTime = value;
            }
        }
    }
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            
            if(value < 1)
            {
                currentSpeed = 0;
            }
            else
            {
                currentSpeed = value;
            }
        }
    }
    // Start is called before the first frame update
    public void SetPath(List<Vector2Int> _path)
    {
        path = _path;
    }
    private void OnDisable()
    {
        indexAlongPath = 0;
        sleepTimer = 0;
        currentSleepTime = initialSleepTime;
        currentSpeed = speed;
        controller.RemoveActiveEnemy(gameObject);
    }
    private void Awake()
    {
        currentSleepTime = initialSleepTime;
    }
    private void Update()
    {
        if(sleepTimer < currentSleepTime)
        {
            sleepTimer += Time.deltaTime;
            return;
        }
        if (indexAlongPath < path.Count)
        {
            Vector3 point = new Vector3(path[indexAlongPath].x * 2, 1, path[indexAlongPath].y * 2);
            transform.localPosition = Vector3.Lerp(transform.localPosition, point, Time.deltaTime * currentSpeed);
            if (Vector3.Distance(transform.localPosition, point) < 0.5f)
            {
                indexAlongPath++;
            }
            if(indexAlongPath == path.Count - 1)
            {
                Player.HitPoints -= damage;
                gameObject.SetActive(false);
            }
        }
       
       
    }
}
