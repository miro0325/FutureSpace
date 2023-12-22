using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    private Vector3 targetPos;

    public enum BulletType
    {
        Default, Lazer
    }
    public BulletType type;

    public bool isTarget = false;

    public bool isEnemyBullet = false;

    public float speed = 10;

    public float damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Enemy" || collision.tag == "Boss" || collision.tag == "Extra")  && !isEnemyBullet)
        {
            Stat stat = collision.GetComponent<Stat>();
            stat.Damage(damage);
            StopAllCoroutines();
            if(type == BulletType.Default)  
                ReturnBullet(1);
            else 
                Destroy(gameObject);

        } else if(collision.gameObject == Player.Instance.gameObject && isEnemyBullet) {
            Stat stat = collision.GetComponent<Stat>();
            stat.Damage(damage);
            StopAllCoroutines();
            ReturnBullet(2);
        } 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Return")
        {
            if (type == BulletType.Default)
            {
                if (isEnemyBullet)
                {
                    ReturnBullet(2);
                }
                else
                {
                    ReturnBullet(1);
                }
            }
            else if (type == BulletType.Lazer)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget()
    {

        if(isTarget)
        {
            if(isEnemyBullet)
            {
                target = Player.Instance.transform;
                targetPos = target.position;
                Vector2 dir = (target.position - transform.position).normalized;
                float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, z - 90);
                transform.rotation = rotation;
            } else
            {
                GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
                float distance = 99999;
                foreach(var enemy in enemys)
                {
                    if(Vector2.Distance(enemy.transform.position, Player.Instance.transform.position) < distance)
                    {
                        target = enemy.transform;
                        distance = Vector2.Distance(enemy.transform.position, Player.Instance.transform.position);
                    }
                }
                targetPos = target.position;
                Vector2 dir = (target.position - transform.position).normalized;
                float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, z - 90);
                transform.rotation = rotation;
            }
        } 
    }

    void Start()
    {
        SetTarget();
        
    }

   

    

    void ReturnBullet(int select)
    {
        if(type == BulletType.Default)
            ObjectPool.ReturnObject(this, select);
        else if(type == BulletType.Lazer)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTarget)
        {
            if(isEnemyBullet)
            {
                transform.Translate(Vector2.down * Time.deltaTime * speed);
            } else
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
            }
        } else
        {
            
            transform.Translate(Vector2.up * speed * Time.deltaTime);
             
        }  
    }
}
