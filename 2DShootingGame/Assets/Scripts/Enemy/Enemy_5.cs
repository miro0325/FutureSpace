using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5 : MonoBehaviour
{
    // Start is called before the first frame update
    Stat stat;

    public GameObject bullet;

    bool isDelay = false;

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

    public void Death()
    {
        CircleShot(15);
    }

    void CircleShot(int count)
    {
        for (int i = 0; i < 360; i += 360 / count)
        {
            DefaultBullet b = ObjectPool.GetObject(2);
            b.damage = 2;
            b.transform.position = transform.position;
            b.transform.rotation = Quaternion.Euler(0, 0, i);
            b.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
            b.damage = 1;
            b.isEnemyBullet = true;
            b.isTarget = false;
            b.transform.localScale = new Vector3(1, 1, 1);
            b.speed =
            ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;


            b.damage = stat.damage * b.damage;
        }
    }

    void Shot()
    {
        if (isDelay)
        {
            return;
        }
        StartCoroutine(delay());
        float radius = 60;
        float amount = radius / (8 - 1);
        float z = radius / -2f;

        for (int i = 0; i < 8; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, z);
            Instantiate(bullet, transform.position, rotation);

            z += amount;
        }


    }

    IEnumerator delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(3f);
        isDelay = false;
    }
}
