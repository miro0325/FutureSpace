using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stat : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp = 100;
    public float damage = 1;

    bool isDeath = false;   

    public int score = 0;
    [SerializeField]
    ParticleSystem deathParticle;

    SpriteRenderer spriteRenderer;

    public float maxHp;

    public bool invincibility = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHp = hp;
    }

    public float GetHP
    {
        get { return hp; }
    }

    public float GetHPValue()
    {
        return hp;
    }

    public void SetHPValue(float value)
    {
        hp = value;
        maxHp = hp;
    }
    

    

    public float GetMaxHPValue()
    {
        return maxHp;
    }


    // Update is called once per frame
    void Update()
    {
        if(Player.Instance.gameObject == gameObject)
        {
            if(hp > 10)
            {
                hp = 10;
            }
        }
    }

    public void Heal(int value)
    {
        hp += value;
    }

    public void Damage(float damage)
    {
        if (!invincibility)
        {
            hp -= damage;
        }
        
        if(this.gameObject == Player.Instance.gameObject && !invincibility)
        {
            if (hp <= 0)
            {
                hp = 0;
                StopAllCoroutines();
                Death();
                return;
            }
            
            invincibility = true;
            StartCoroutine(Damaged());
            StartCoroutine(inv());
        }
        if(invincibility)
        {
            return;
        }
        if(hp <= 0)
        {
            hp = 0;
            StopAllCoroutines();
            Death();
            return;
            
        }
        StopCoroutine(Damaged());
        StartCoroutine(Damaged());
    }

    void Death()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        GameManager.AddScore(score);
        if(Player.Instance.gameObject == this.gameObject)
        {
            GameManager.GameOver();
        } 
        try
        {
            this.gameObject.GetComponent<Enemy_5>().Death();
        } catch(NullReferenceException)
        {
            Debug.Log("Dont have Death Attack System");
        }
        if(this.gameObject.tag == "Enemy")
        {
            if(!isDeath)
            {
                isDeath = true;
                int random = UnityEngine.Random.Range(0, 100);
                if(random < 31)
                {
                    int r = UnityEngine.Random.Range(0, GameManager.Instance.items.Length);
                    GameObject item = GameManager.SpawnItem(r);
                    
                    item.transform.position = transform.position;
                }
                WaveManager.Instance.enemyAmount--;
            }
        } else if(this.gameObject.tag == "Boss")
        {
            if(!isDeath)
            {
                isDeath = true;
                int r = UnityEngine.Random.Range(0, GameManager.Instance.items.Length);
                GameObject item = GameManager.SpawnItem(r);
                item.transform.position = transform.position;
                
                WaveManager.Instance.enemyAmount--;
                GameManager.BossHPBarOff();
            }
        } else if(this.gameObject.tag == "Extra")
        {
            
            if(WaveManager.Instance.wave == 6)
            {

                Boss_1.Instance.turrets.Remove(transform);
            } else if(WaveManager.Instance.wave == 12)
            {
                Boss_2.Instance.turrets.Remove(transform);
            }
        }
        this.gameObject.SetActive(false);
    }

    IEnumerator Damaged()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    IEnumerator inv()
    {
        Player.Instance.Shield();
        yield return new WaitForSeconds(3f);
        invincibility = false;
    }
}
