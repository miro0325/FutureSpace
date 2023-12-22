using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2 : MonoBehaviour
{
    // Start is called before the first frame update
    public int pattern = 1;

    public static Boss_2 Instance { get; private set; }

    public List<Transform> turrets = new List<Transform>();

    bool isDelay = false;

    bool shootBeam = false;

    public bool isDestroy = false;

    

   

    public Sprite BigBullet;

    

    public Transform turret;

    public float speed = 1f;
    public int dir;

    public bool isMove = false;

    void Awake()
    {
        Instance = this;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        Pattern();

        if (isMove)
        {
            //Patter01Move();
        }
        if (turrets.Count == 0 && !isDestroy && !turret.gameObject.activeSelf)
        {
            isDestroy = true;
            pattern = 1;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (!shootBeam)
        {
            for (int i = 0; i < turrets.Count; i++)
            {

                Vector2 dir = (Player.Instance.transform.position - turrets[i].position).normalized;
                float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, z - 90);
                turrets[i].rotation = rotation;
            }

            Vector2 dr = (Player.Instance.transform.position - turret.position).normalized;
            float zz = Mathf.Atan2(dr.y, dr.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, zz - 90);
            turret.rotation = rot;

        }

    }

    void Patter01Move()
    {
        RaycastHit2D[] hits;
        transform.Translate(Vector2.up * dir * Time.deltaTime * speed);
        Debug.DrawRay(transform.position, Vector2.right * dir * 4f, Color.green, 0.3f);
        hits = Physics2D.RaycastAll(transform.position, Vector2.right * dir, 4f);
        foreach (RaycastHit2D hit in hits)
        {

            if (hit.collider != null && hit.collider.tag == "Wall")
            {
                Debug.Log("Hit");
                dir = dir * -1;
            }
        }
    }

    IEnumerator Pattern_01()
    {
       
        yield return new WaitForSeconds(0.5f);
        isMove = true;

        dir = Random.Range(-1, 2);
        List<DefaultBullet> bullets = new List<DefaultBullet>();

        for (int j = 0; j < 5; j++)
        {

            bullets.Clear();
            
            yield return new WaitForSeconds(0.2f);
            int random = Random.Range(-5, 6);
            for (int i = 0; i < 15; i++)
            {
                Vector3 pos = transform.position + new Vector3(-15 / 2 + i + (random/10), -0.7f);

                DefaultBullet defaultBullet = ObjectPool.GetObject(2);
                defaultBullet.isTarget = false;
                defaultBullet.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                defaultBullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                defaultBullet.transform.localScale = new Vector3(1, 1, 1);

                defaultBullet.transform.rotation = Quaternion.identity;
                defaultBullet.transform.position = pos;
                defaultBullet.damage = 1;

            }
            yield return new WaitForSeconds(0.5f);

        }
        
        yield return new WaitForSeconds(0.5f);
        isMove = false;
        Debug.Log("1");
        StartCoroutine(Delay());



    }

    IEnumerator Pattern_02()
    {

        for (int j = 0; j < 15; j++)
        {

            int angle = Random.Range(-45, 45);
            for (int i = 0; i < 360; i += 360 / 60)
            {
                DefaultBullet defaultBullet = ObjectPool.GetObject(2);
                defaultBullet.isTarget = false;
                defaultBullet.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                defaultBullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                defaultBullet.transform.localScale = new Vector3(1, 1, 1);

                defaultBullet.transform.rotation = Quaternion.identity;
                defaultBullet.transform.position = transform.position;
                defaultBullet.damage = 1;
                defaultBullet.transform.rotation = Quaternion.Euler(0, 0, i + angle);
                yield return null;
            }
            yield return new WaitForSeconds(0.45f);
        }
        StartCoroutine(Delay());
    }

    IEnumerator Pattern_03()
    {
        for (int j = 0; j < 9; j++)
        {
            int random = Random.Range(-15, 15);
            for (int i = 0; i < 360; i += 360 / 60)
            {
                DefaultBullet b = ObjectPool.GetObject(2);
                b.isTarget = false;
                b.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                b.transform.localScale = new Vector3(1, 1, 1);
                b.transform.rotation = Quaternion.Euler(0, 0, 90 + i + random);
                b.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                b.isEnemyBullet = true;
                b.transform.position = transform.position;
                b.damage = 1;
                DefaultBullet a = ObjectPool.GetObject(2);
                a.isTarget = false;
                a.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                a.transform.localScale = new Vector3(1, 1, 1);
                a.transform.rotation = Quaternion.Euler(0, 0, 90 - i + random);
                a.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                a.isEnemyBullet = true;
                a.transform.position = transform.position;
                a.damage = 1;
                DefaultBullet c = ObjectPool.GetObject(2);
                c.isTarget = false;
                c.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                c.transform.localScale = new Vector3(1, 1, 1);
                c.transform.rotation = Quaternion.Euler(0, 0, 180 + i + random);
                c.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                c.isEnemyBullet = true;
                c.transform.position = transform.position;
                c.damage = 1;
                DefaultBullet d = ObjectPool.GetObject(2);
                d.isTarget = false;
                d.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                d.transform.localScale = new Vector3(1, 1, 1);
                d.transform.rotation = Quaternion.Euler(0, 0, 180 - i + random);
                d.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                d.isEnemyBullet = true;
                d.transform.position = transform.position;
                d.damage = 1;
                yield return null;

            }
            yield return new WaitForSeconds(0.3f);

        }
        StartCoroutine(Delay());

    }

    void Pattern()
    {
        if (!isDestroy)
        {
            if (!isDelay)
            {

                switch (pattern)
                {
                    case 1:
                        isDelay = true;
                        pattern++;
                        StartCoroutine(Pattern_1());
                        break;
                    case 2:
                        isDelay = true;
                        pattern++;
                        StartCoroutine(Pattern_1());
                        StartCoroutine(Pattern_2());
                        break;

                    case 3:
                        isDelay = true;
                        pattern = 1;
                        StartCoroutine(Pattern_3());
                        break;

                }
            }
        }
        else
        {
            if (!isDelay)
            {

                switch (pattern)
                {
                    case 1:
                        isDelay = true;
                        pattern++;
                        StartCoroutine(Pattern_02());

                        break;
                    case 2:
                        isDelay = true;
                        pattern++;
                        StartCoroutine(Pattern_01());
                        break;

                    case 3:
                        isDelay = true;
                        pattern = 1;
                        StartCoroutine(Pattern_03());
                        break;

                }
            }
        }
    }

    IEnumerator Pattern_1()
    {

        for (int k = 0; k < 3; k++)
        {

            for (int j = 0; j < turrets.Count; j++)
            {
                float radius = 60;
                float amount = radius / (5 - 1);
                float z = radius / -2f;

                for (int i = 0; i < 5; i++)
                {
                    Vector2 dir = (Player.Instance.transform.position - turrets[j].GetChild(1).position).normalized;
                    float a = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, z + a + 90);
                    DefaultBullet defaultBullet = ObjectPool.GetObject(2);
                    defaultBullet.isTarget = false;
                    defaultBullet.speed =
                    ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                    defaultBullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                    defaultBullet.transform.localScale = new Vector3(1, 1, 1);
                  
                    
                    
                    defaultBullet.transform.rotation = rotation;
                    defaultBullet.transform.position = turrets[j].GetChild(1).position;
                    defaultBullet.damage = 1;

                    z += amount;
                }

                
            }
            yield return new WaitForSeconds(0.75f);
        }

        StartCoroutine(Delay());
    }

    IEnumerator Pattern_2()
    {
        if(turret.gameObject.activeSelf)
        {

            for(int i = 0; i < 5; i++)
            {

                DefaultBullet bullet = ObjectPool.GetObject(2);
                Vector2 dir = (Player.Instance.transform.position - turret.GetChild(1).position).normalized;
                float a = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bullet.isTarget = false;
                bullet.GetComponent<SpriteRenderer>().sprite = BigBullet;
                bullet.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                bullet.transform.position = turret.transform.GetChild(1).position;
                bullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                bullet.transform.rotation = Quaternion.Euler(0,0,a + 90);
                bullet.damage = 3;
                bullet.GetComponent<CircleCollider2D>().radius = 0.29f;
                bullet.speed = 6;
                yield return new WaitForSeconds(0.5f);
            }

        }
        
    }

    IEnumerator Pattern_3()
    {

        if(turret.gameObject.activeSelf)
        {

            shootBeam = true;
            float r = 0;
            while(r < 1 )
            {
                SpriteRenderer spriteRenderer = turret.GetComponent<SpriteRenderer>();
                SpriteRenderer render = turret.GetChild(0).GetComponent<SpriteRenderer>();
                render.color = Color.Lerp(Color.white, Color.red, r);
                r += Time.deltaTime;
            
                spriteRenderer.color = Color.Lerp(Color.white, Color.red, r);
                //render.color = new Color(255, r, r);
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
            for (int k = 0; k < 3; k++)
            {

                    int random = Random.Range(-15, 15);
                    float radius = 90;
                    float amount = radius / (10 - 1);
                    float z = radius / -2f;

                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 dir = (Player.Instance.transform.position - turret.GetChild(1).position).normalized;
                         float a = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, z + a + 90);
                       DefaultBullet bullet = ObjectPool.GetObject(2);
                        bullet.isTarget = false;
                        bullet.GetComponent<SpriteRenderer>().sprite = BigBullet;
                        bullet.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        bullet.transform.position = turret.transform.GetChild(1).position;
                        bullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                        bullet.transform.rotation =  Quaternion.Euler(0, 0, turret.transform.rotation.z + z); ;
                        bullet.damage = 3;
                        bullet.GetComponent<CircleCollider2D>().radius = 0.29f;
                        bullet.speed = 6;
                

                        z += amount;
                    }


            
                yield return new WaitForSeconds(0.75f);
            }
            yield return new WaitForSeconds(0.1f);
        
            r = 0;
            while (r < 1)
            {
                SpriteRenderer spriteRenderer = turret.GetComponent<SpriteRenderer>();
                SpriteRenderer render = turret.GetChild(0).GetComponent<SpriteRenderer>();
                render.color = Color.Lerp(Color.red, Color.white, r);
                r += Time.deltaTime;

                spriteRenderer.color = Color.Lerp(Color.red, Color.white, r);
                //render.color = new Color(255, r, r);
                yield return null;
            }
            shootBeam = false;
        }
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.5f);
        isDelay = false;
        shootBeam = false;

    }
}
