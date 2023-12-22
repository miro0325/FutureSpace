using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    protected virtual void GetItem()
    {
        Destroy(gameObject);

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Return")
        {
            Destroy(gameObject);
        }  else if(collision.gameObject == Player.Instance.gameObject)
        {
            GetItem();
        }
    }




}
