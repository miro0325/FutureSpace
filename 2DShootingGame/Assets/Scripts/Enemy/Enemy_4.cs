using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1f;

    public GameObject bullet;

    bool isDelay = false;

    void Start()
    {
        
    }

    


    // Update is called once per frame
    void Update()
    {
        Shot();
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);

    }

    void Shot()
    {
        if (isDelay)
        {
            return;
        }
        StartCoroutine(Delay());
        float radius = 45;
        float amount = radius / (3 - 1);
        float z = radius / -2f;

        for (int i = 0; i < 3; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, z);
           Instantiate(bullet, transform.position, rotation);
            
            z += amount;
        }


    }

    IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(2f);
        isDelay = false;
    }
}
