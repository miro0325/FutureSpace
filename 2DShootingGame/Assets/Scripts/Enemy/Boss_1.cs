using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : MonoBehaviour
{
    // Start is called before the first frame update
    public int pattern = 1;

    public static Boss_1 Instance { get; private set; }

    public List<Transform> turrets =new List<Transform>();

    bool isDelay = false;

    bool shootBeam = false;

    public bool isDestroy = false;

    public GameObject AlertBlock;

    public GameObject Lazer;

    public Sprite BigBullet;

    public Sprite Beam;

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

        if(isMove)
        {
            Patter01Move();
        }
        if(turrets.Count == 0 && !isDestroy)
        {
            isDestroy = true;
            pattern = 1;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        if(!shootBeam)
        {

            for(int i = 0; i < turrets.Count; i++)
            {
                
                Vector2 dir = (Player.Instance.transform.position - turrets[i].position).normalized;
                float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, z - 90);
                turrets[i].rotation = rotation;
            }
            
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
        for(int i = 0; i < 91; i++)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180 + i);
            yield return new WaitForSeconds(0.01f);  
        }
        yield return new WaitForSeconds(0.5f);
        isMove = true;
        
        dir = Random.Range(-1, 2);
        List<DefaultBullet> bullets = new List<DefaultBullet>();

        for(int j = 0; j < 5; j++)
        {

            bullets.Clear();
            for (int i = 0; i < 10; i++)
            {
                Vector3 pos = transform.position + new Vector3(-15 / 2 + i * 1.5f, -0.7f);

                DefaultBullet defaultBullet = ObjectPool.GetObject(2);
                defaultBullet.isTarget = false;
                defaultBullet.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                defaultBullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                defaultBullet.transform.localScale = new Vector3(1, 1, 1);
                
               
                defaultBullet.transform.position = pos;
                defaultBullet.damage = 1;

                bullets.Add(defaultBullet);
            
            }

            yield return new WaitForSeconds(0.1f);

            foreach (var item in bullets)
            {
                var dir = (Player.Instance.transform.position - item.transform.position);
                float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, z + 90);

                item.transform.rotation = rotation;
            

            
            }
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 10; i++)
            {
                Vector3 pos = transform.position + new Vector3(-10 / 2 + i, -0.7f);

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
            yield return new WaitForSeconds(1.5f);

        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 91; i++)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270 - i);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        isMove = false;
        Debug.Log("1");
        StartCoroutine(Delay());



    }

    IEnumerator Pattern_02()
    {
        
        for(int j = 0; j < 15; j++)
        {

            int angle = Random.Range(0, 90);
            for (int i = 0; i < 360; i += 360 / 30)
            {
                DefaultBullet bullet = ObjectPool.GetObject(2);
                bullet.isTarget = false;
                bullet.GetComponent<SpriteRenderer>().sprite = BigBullet;
                bullet.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                bullet.transform.position = transform.position;
                bullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                bullet.transform.rotation = Quaternion.Euler(0,0,i + angle);
                bullet.damage = 3;
                bullet.GetComponent<CircleCollider2D>().radius = 0.29f;
                bullet.speed = 6;
            }
            yield return new WaitForSeconds(0.3f);
        }
        StartCoroutine(Delay());
    }

    IEnumerator Pattern_03()
    {
        for (int j = 0; j < 7; j++)
        {

            for (int i = 0; i < 360; i += 360 / 45)
            {
                DefaultBullet b = ObjectPool.GetObject(2);
                b.isTarget = false;
                b.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                b.transform.localScale = new Vector3(1, 1, 1);
                b.transform.rotation = Quaternion.Euler(0, 0, 90 +i);
                b.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                b.isEnemyBullet = true;
                b.transform.position = transform.position;
                b.damage = 1;
                DefaultBullet a = ObjectPool.GetObject(2);
                a.isTarget = false;
                a.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                a.transform.localScale = new Vector3(1, 1, 1);
                a.transform.rotation = Quaternion.Euler(0, 0, 90-i);
                a.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                a.isEnemyBullet = true;
                a.transform.position = transform.position;
                a.damage = 1;
                yield return new WaitForSeconds(0.05f);
                DefaultBullet c = ObjectPool.GetObject(2);
                c.isTarget = false;
                c.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                c.transform.localScale = new Vector3(1, 1, 1);
                c.transform.rotation = Quaternion.Euler(0, 0, 180 + i);
                c.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                c.isEnemyBullet = true;
                c.transform.position = transform.position;
                c.damage = 1;
                DefaultBullet d = ObjectPool.GetObject(2);
                d.isTarget = false;
                d.speed =
                ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                d.transform.localScale = new Vector3(1, 1, 1);
                d.transform.rotation = Quaternion.Euler(0, 0, 180- i);
                d.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                d.isEnemyBullet = true;
                d.transform.position = transform.position;
                d.damage = 1;


            }
            
        }
        StartCoroutine(Delay());

    }

    void Pattern()
    {
        if(!isDestroy)
        {
            if(!isDelay)
            {

                switch(pattern)
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
                        break;

                    case 3:
                        isDelay = true;
                        pattern = 1;
                        StartCoroutine(Pattern_2());
                        break;

                }
            }
        } else
        {
            if (!isDelay)
            {

                switch (pattern)
                {
                    case 1:
                        isDelay = true;
                        pattern++;
                        StartCoroutine(Pattern_01());
                        break;
                    case 2:
                        isDelay = true;
                        pattern++;
                        StartCoroutine(Pattern_02());
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
        
            for(int k = 0; k < 7; k++)
            {

                for(int j = 0; j < turrets.Count; j++)
                {
                    DefaultBullet defaultBullet = ObjectPool.GetObject(2);
                    defaultBullet.isTarget = false;
                    defaultBullet.speed =
                    ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
                    defaultBullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
                    defaultBullet.transform.localScale = new Vector3(1, 1, 1);
                    Vector2 dir = (Player.Instance.transform.position - turrets[j].GetChild(0).position).normalized;
                    float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, z + 90);
                    defaultBullet.transform.rotation = rotation;
                    defaultBullet.transform.position = turrets[j].GetChild(0).position;
                    defaultBullet.damage = 2;
                }
                    yield return new WaitForSeconds(0.1f);
            }
        
        StartCoroutine(Delay());
    }

    IEnumerator Pattern_2()
    {
        SpriteRenderer[] spriteRenderers = new SpriteRenderer[2];
        shootBeam = true;
        for(int i = 0; i < turrets.Count; i++)
        {
            if (turrets[i] != null)
            {

                    spriteRenderers[i] = Instantiate(AlertBlock, turrets[i].GetChild(0)).GetComponent<SpriteRenderer>();
                Vector2 dir = (Player.Instance.transform.position - turrets[i].position).normalized;
                float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, z + 90);
            
                spriteRenderers[i].transform.rotation = rotation;
                spriteRenderers[i].transform.localPosition = new Vector3(0, 23.5f, 0);
            }
        }
        for(int i = 0; i < 5; i++)
        {
            for(int j =0;  j < turrets.Count; j++)
            {
                if(turrets[j] !=null)
                    spriteRenderers[j].color = new Color(spriteRenderers[j].color.r, spriteRenderers[j].color.g, spriteRenderers[j].color.b, 0.2f);
            }
            yield return new WaitForSeconds(0.1f);
            for (int j = 0; j < turrets.Count; j++)
            {
                if (turrets[j] != null)
                    spriteRenderers[j].color = new Color(spriteRenderers[j].color.r, spriteRenderers[j].color.g, spriteRenderers[j].color.b, 1f);
            }
            yield return new WaitForSeconds(0.1f);

        }
        for (int j = 0; j < turrets.Count; j++)
        {
            if (turrets[j] != null)
                spriteRenderers[j].color = new Color(spriteRenderers[j].color.r, spriteRenderers[j].color.g, spriteRenderers[j].color.b, 0f);
        }
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < turrets.Count; i++)
        {
            if (turrets[i] != null)
            {

                spriteRenderers[i].color = new Color(255, 255, 255, 1f);
                spriteRenderers[i].gameObject.AddComponent<Beam>().ShootBeam();
            }

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
