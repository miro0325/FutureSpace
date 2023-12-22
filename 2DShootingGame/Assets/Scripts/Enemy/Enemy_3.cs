using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : MonoBehaviour
{
    // Start is called before the first frame update
    float dir = 1;

    public float speed = 1f;

    public GameObject bullet;

    bool isDelay = false;
    
    void Start()
    {
        SetDir();
    }

    void SetDir()
    {
        dir = dir * -1;
        Invoke("SetDir", 1f);
    }


    // Update is called once per frame
    void Update()
    {
        Shot();
        Move();
    }

    private void Move()
    {
        transform.Translate((Vector2.down + (Vector2.right * dir)) * Time.deltaTime * speed);

    }

    void Shot()
    {
        if(isDelay)
        {
            return;
        }
        StartCoroutine(Delay());
        float radius = 60;
        float amount = radius / (5 - 1);
        float z = radius / -2f;

        for (int i = 0; i < 5; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, z);
            DefaultBullet newObj = ObjectPool.GetObject(2);
            newObj.speed =
            ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
            newObj.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
            newObj.transform.localScale = new Vector3(1, 1, 1);
            newObj.isTarget = false;
            newObj.damage = 1;
            newObj.isEnemyBullet = true;
            newObj.transform.position = transform.position;
            newObj.transform.rotation = rotation;
            
            
            z += amount;
        }

        
    }

    IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(1.8f);
        isDelay = false;
    }
}
