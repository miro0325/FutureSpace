using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void GetItem()
    {
        Player.Instance.GetComponent<Stat>().Heal(3);
        base.GetItem();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        base.OnTriggerEnter2D(collision);
    }
}
