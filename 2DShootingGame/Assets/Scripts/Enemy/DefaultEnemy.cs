using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    Stat stat;
    public float speed = 2f;

    private bool isDelay = false;

    public GameObject bullet;

    void Start()
    {
        stat = GetComponent<Stat>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, z + 90);
        transform.rotation = rotation;
        Shot();
    }

    void Shot()
    {
        if (!isDelay)
        {
            DefaultBullet b = ObjectPool.GetObject(2);
            b.isTarget = true;
            b.speed =
            ObjectPool.instance.bullet.GetComponent<DefaultBullet>().speed;
            b.transform.GetComponent<CircleCollider2D>().radius = ObjectPool.instance.bullet.GetComponent<CircleCollider2D>().radius;
            b.transform.localScale = new Vector3(1, 1, 1);
            b.transform.position = transform.position;
            b.damage = 1;
            b.transform.rotation = Quaternion.identity;
            b.isEnemyBullet = true;
            b.SetTarget();
            b.damage = b.damage * stat.damage;
            StartCoroutine(delay());
        }
    }

   

    
    

    IEnumerator delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(1f);
        isDelay = false;
    }

    
}
