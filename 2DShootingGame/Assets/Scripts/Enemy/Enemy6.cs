using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : MonoBehaviour
{
    // Start is called before the first frame update

    bool isDelay = false;

    public float speed = 1f;

    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDelay)
        {
            Shot();
            StartCoroutine(delay());
        }
        Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, z + 90);
        transform.rotation = rotation;  
    }

    void Shot()
    {
        float radius = 1;
        Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, z + 90);
        


        for (int i = 0; i < 360; i+=360 / 10)
        {
            Vector2 position = transform.position + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i  * Mathf.Deg2Rad) * radius, 0);
            DefaultBullet defaultBullet = ObjectPool.GetObject(2);
            defaultBullet.isTarget = false;
            defaultBullet.speed =
            ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
            defaultBullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
            defaultBullet.transform.localScale = new Vector3(1, 1, 1);
            defaultBullet.transform.rotation = rotation;
            defaultBullet.transform.position = position;
           defaultBullet.damage = 1;


        }
       
    }

    IEnumerator delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(3f);
        isDelay = false;
    }
}
