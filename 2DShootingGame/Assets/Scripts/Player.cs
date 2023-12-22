using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public static Player Instance { get; private set; }
    
    Rigidbody2D rigid;
    public GameObject defaultBullet;

    [SerializeField]
    ParticleSystem shield;

    public float speed = 1;

    bool isDelay = false;

    float bulletDelay = 0.2f;

    Transform shieldPos;
    Stat stat;

    public BulletType type = BulletType.Default;

    public enum BulletType
    {
        Default, Lazer, Mine, Beam
    }

    public int lvl = 1;

  

    public void Shield()
    {
        shieldPos = Instantiate(shield, transform.position, Quaternion.identity).transform;
        stat = GetComponent<Stat>();
    }
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Shot();
        SetLvlValue();
        if(shieldPos != null)
        {
            shieldPos.position = transform.position;
        }
       

    }

    void SetLvlValue()
    {
        if(type == BulletType.Default)
            bulletDelay = 0.2f - (0.02f * (lvl -1));
    }

    void PlayerMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rigid.velocity = new Vector2 (x, y).normalized * speed;

    }

    void Shot()
    {
        if(Input.GetMouseButton(0) && !isDelay)
        {
            switch(type)
            {
                case BulletType.Default:
                    if(lvl == 1)
                    {
                        DefaultBullet b =ObjectPool.GetObject(1);
                        b.transform.rotation = Quaternion.identity;
                        b.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                        b.isEnemyBullet = false;

                    } else if(lvl == 2)
                    {
                        for(int i = -1; i < 2; i+=2)
                        {
                            DefaultBullet b = ObjectPool.GetObject(1);
                            b.transform.rotation = Quaternion.identity;
                            b.transform.position = new Vector3(transform.position.x + i * 0.2f, transform.position.y, 0);
                            b.isEnemyBullet = false;
                        }
                    } else if(lvl == 3)
                    {
                        for (int i = -1; i < 2; i++)
                        {
                            DefaultBullet b = ObjectPool.GetObject(1);
                            b.transform.rotation = Quaternion.identity;
                            b.transform.position = new Vector3(transform.position.x + i * 0.2f, transform.position.y, 0);
                            b.isEnemyBullet = false;
                        }
                    }
                    StartCoroutine(delay());
                    
                break;
            }

        }
    }

    IEnumerator delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(bulletDelay);
        isDelay = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" )
        {
            if(!stat.invincibility)
            {
                float damage = collision.GetComponent<Stat>().damage;
                collision.GetComponent<Stat>().Damage(999);
                
                stat.Damage(damage);

            }
            
        }
    }
}
