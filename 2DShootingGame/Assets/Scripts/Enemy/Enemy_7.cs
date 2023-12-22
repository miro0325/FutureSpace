using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_7 : MonoBehaviour
{
    // Start is called before the first frame update
    bool isDelay = false;

    public float speed = 1f;

    public Sprite bulletImage;
    Quaternion rotation;

    bool isShootDelay = false;


    void Start()
    {
        Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotation = Quaternion.Euler(0, 0, z + 90);
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if(!isDelay)
        {
            StartCoroutine(Shot());
            StartCoroutine(delay());
        }
    }

    

    IEnumerator delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(2.5f);
        isDelay = false;
    }

    IEnumerator Shot()
    {
        for (int i = 0; i < 3; i++)
        {

            DefaultBullet bullet = ObjectPool.GetObject(2);
            bullet.isTarget = false;
            bullet.GetComponent<SpriteRenderer>().sprite = bulletImage;
            bullet.transform.position = transform.position;
            bullet.transform.rotation = rotation;
            bullet.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
            bullet.damage = 2;

            bullet.transform.localScale = new Vector3(1, 1, 1);
            bullet.speed = 12;
            yield return new WaitForSeconds(0.2f);
        }
    }

}
