using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    
    public GameObject bullet;

    private Queue<DefaultBullet> poolQueue = new Queue<DefaultBullet>();


    [SerializeField]
    private GameObject playerBullet;

    private Queue<DefaultBullet> poolQueue2 = new Queue<DefaultBullet>();

    private Queue<DefaultBullet> poolQueue3 = new Queue<DefaultBullet>();

    void Awake()
    {
        instance = this;
        Initialize(40);
    }

    DefaultBullet CreateNewBullet(int select)
    {
        if (select == 1)
        {
            var newBullet = Instantiate(playerBullet, transform).GetComponent<DefaultBullet>();
            newBullet.gameObject.SetActive(false);
            return newBullet;
        }
        else if (select == 2)
        {
            var newBullet = Instantiate(bullet, transform).GetComponent<DefaultBullet>();
            newBullet.gameObject.SetActive(false);
            return newBullet;

        }
        else return null;

    }

    

    void Initialize(int count)
    {
        for(int i = 0; i < count; i++)
        {
            poolQueue.Enqueue(CreateNewBullet(2));
        }
        for (int i = 0; i < count; i++)
        {
            poolQueue2.Enqueue(CreateNewBullet(1));
        }
    }

    public static DefaultBullet GetObject(int select)
    {
        if(select == 1)
        {
            if (instance.poolQueue2.Count > 0)
            {
                var obj = instance.poolQueue2.Dequeue();
                    obj.transform.SetParent(null);
                    obj.gameObject.SetActive(true);
                DefaultBullet bullet = obj;
                
                return obj;
            }
            else
            {
                var newObj = instance.CreateNewBullet(1);
                newObj.transform.SetParent(null);
                newObj.gameObject.SetActive(true);
                DefaultBullet bullet = newObj;
               
                return newObj;
            }
        }
        else if(select == 2)
        {

            if(instance.poolQueue.Count > 0 )
            {
                var obj = instance.poolQueue.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                DefaultBullet bullet = obj;
               

                return obj;
            } else
            {
                var newObj = instance.CreateNewBullet(2);
                newObj.transform.SetParent(null);
                newObj.gameObject.SetActive(true);
                DefaultBullet bullet = newObj;
                
                return newObj;
            }
        }
        else return null;
    }



    public static void ReturnObject(DefaultBullet obj, int select)
    {
        
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        Debug.Log("Return");
        if (select == 1)
        {
            instance.poolQueue2.Enqueue(obj);
        }
        else if (select == 2)
        {
            obj.GetComponent<SpriteRenderer>().sprite = instance.bullet.GetComponent<SpriteRenderer>().sprite;
            obj.GetComponent<CircleCollider2D>().radius = instance.bullet.GetComponent<CircleCollider2D>().radius;
            instance.poolQueue.Enqueue(obj);

        }
        else return;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
