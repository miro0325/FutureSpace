
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiddleBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public int pattern = 1;

     Stat stat;

    public static MiddleBoss Instance { get; set; }

    bool isDelay = false;

    public float maxHP;
    
    void Awake()
    {
        Instance = this;
        stat = GetComponent<Stat>();
        maxHP = stat.GetHPValue();
    }

    
    

    // Update is called once per frame
    void Update()
    {
        Pattern();
    }
    
    void Pattern()
    {
        if(!isDelay)
        {

            switch(pattern)
            {
                case 1:
                    StartCoroutine(Pattern_1());
                    isDelay = true;
                    pattern++;
                    break;
                case 2:
                    StartCoroutine(Pattern_2());
                    isDelay = true;
                    pattern++;
                    break;
                case 3:
                    StartCoroutine(Pattern_3());
                    isDelay = true;
                    pattern = 1;
                    break;
           
            
            }
        }
    }

    IEnumerator Pattern_1()
    {
        for(int i = 0; i < 3; i++)
        {
            float radius = 60;
            int count = Random.Range(-1, 3);
            float amount = radius / (12 + count- 1);
            float z = radius / -2f;
            for (int j = 0; j < 12 + count; j++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, z);
                DefaultBullet newObj = ObjectPool.GetObject(2);
                newObj.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                newObj.isTarget = false;
                newObj.transform.localScale = new Vector3(1, 1, 1);
                newObj.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                newObj.isEnemyBullet = true;
                newObj.transform.position = transform.position;
                newObj.transform.rotation = rotation;
                newObj.damage = 1;
                Debug.Log(newObj.gameObject);
                z += amount;
            }
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(Delay());
    }

    

    IEnumerator Pattern_2()
    {
        for(int j = 0; j < 4; j++)
        {
            
            for (int i = 0; i < 360; i += 360 / 24)
            {
                DefaultBullet b = ObjectPool.GetObject(2);
                b.isTarget = false;
                b.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                b.transform.localScale = new Vector3(1, 1, 1);
                b.transform.rotation = Quaternion.Euler(0, 0, i);
                b.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                b.isEnemyBullet = true;
                b.transform.position = transform.position;
                b.damage = 1;
                yield return new WaitForSeconds(0.05f);
                

            }
        }
        StartCoroutine(Delay());
    }


    IEnumerator Pattern_3()
    {
        for(int i = 0; i < 6; i++)
        {

            float radius = 2;
            Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
            float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, z + 90);



            for (int j = 0; j < 360; j += 360 / 18)
            {
                Vector2 position = transform.position + new Vector3(Mathf.Cos(j * Mathf.Deg2Rad) * radius, Mathf.Sin(j * Mathf.Deg2Rad) * radius, 0);
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
            yield return new WaitForSeconds(0.7f);
        }
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        
       
        yield return new WaitForSeconds(3f);
        isDelay = false;
    }
    
}
