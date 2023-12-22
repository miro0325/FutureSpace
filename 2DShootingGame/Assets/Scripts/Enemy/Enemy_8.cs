using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_8 : MonoBehaviour
{
    // Start is called before the first frame update
    Stat stat;

    public GameObject bullet;

    Quaternion rotation;

    bool isDelay = false;

    public Sprite bulletImage;

    int dir = 0;

    public float speed = 1f;

    void Start()
    {
        stat = GetComponent<Stat>();
        SetDir();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDelay)
        {
            Shot();
            StartCoroutine(delay());
        }
        Move();
        Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotation = Quaternion.Euler(0, 0, z + 90);
        transform.rotation = rotation;

    }

    void SetDir()
    {
        dir = Random.Range(-1, 2);
        Invoke("SetDir", 3f);
    }

    private void Move()
    {
        RaycastHit2D[] hits;
        transform.Translate(Vector2.right * dir * Time.deltaTime * speed);
        Debug.DrawRay(transform.position, Vector2.right * dir * 1.5f, Color.green, 0.3f);
        hits = Physics2D.RaycastAll(transform.position, Vector2.right * dir, 1.5f);
        foreach (RaycastHit2D hit in hits)
        {

            if (hit.collider != null && hit.collider.tag == "Wall")
            {
                Debug.Log("Hit");
                dir = dir * -1;
            }
        }
    }

    

    

    void Shot()
    {
        if (isDelay)
        {
            return;
        }
        StartCoroutine(delay());
        DefaultBullet bullet = ObjectPool.GetObject(2);
        bullet.isTarget = false;
        bullet.GetComponent<SpriteRenderer>().sprite = bulletImage;
        bullet.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        bullet.transform.position = transform.position;
        bullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
        bullet.transform.rotation = rotation;
        bullet.damage = 3;
        bullet.GetComponent<CircleCollider2D>().radius = 0.29f;
        bullet.speed = 6;


    }

    IEnumerator delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(3f);
        isDelay = false;
    }

    

           
            
        

}
