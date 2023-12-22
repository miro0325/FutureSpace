using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static WaveManager Instance { get; set; }

    public List<Stat> enemyList = new List<Stat>();

    public int stage = 0;

    public int wave = 1;

    bool isEmpty = true;

    int curWave;

    public int enemyAmount = 0;

    public GameObject[] enemys;
    public GameObject[] enemys2;
    public GameObject[] enemys3;
    public GameObject[] enemys4;
    public GameObject[] enemys5;
    public GameObject[] enemys6;
    public GameObject[] enemys7;
    public GameObject[] enemys8;

    public GameObject middleBoss;

    

    public GameObject[] enemyBoss = new GameObject[2];

    public Transform[] stageEnemyPos;

    public Transform[] stageEnemyPos_2;

    public Transform[] stageEnemyPos_3;

    public Transform[] stageEnemyPos_4;

    public Transform[] stage2EnemyPos;

    public Transform[] stage2EnemyPos_2;

    public Transform[] stage2EnemyPos_3;

    public Transform[] stage2EnemyPos_4;

    public Transform enemyBossSpawnPos;

    public List<Transform> curEnemy = new List<Transform>();

    bool isMoving = false;

    void Awake()
    {
        Instance = this;
        StartCoroutine(SpawnDelay());
        
        stage = 1;
        wave = 0;
        
        
        curWave = 0;
        enemyAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(enemyAmount == 0 && !isEmpty)
        {
            isEmpty = true;
            StartCoroutine(Delay());
            
        }
        if(!isMoving)
        {
            //EnemyMove();
            
        }
    }

   
    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(3f);
        SpawnEnemy();
    }

    void EnemyMove()
    {
        switch(curWave)
        {
            case 1:
                for(int i = 0; i < stageEnemyPos.Length; i++)
                {
                    curEnemy[i].position = Vector2.Lerp(curEnemy[i].position, stageEnemyPos[i].position, Time.deltaTime * 2f);
                    if(Vector2.Distance(curEnemy[i].position, stageEnemyPos[i].position) < 0.7f)
                    {
                        curEnemy[i] = ObjectPool.instance.transform;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < stageEnemyPos_2.Length; i++)
                {
                    curEnemy[i].position = Vector2.Lerp(curEnemy[i].position, stageEnemyPos_2[i].position, Time.deltaTime * 2f);
                    if (Vector2.Distance(curEnemy[i].position, stageEnemyPos_2[i].position) < 0.7f)
                    {
                        curEnemy[i] = ObjectPool.instance.transform;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < stage2EnemyPos.Length; i++)
                {
                    curEnemy[i].position = Vector2.Lerp(curEnemy[i].position, stage2EnemyPos[i].position, Time.deltaTime * 2f);
                    if (Vector2.Distance(curEnemy[i].position, stage2EnemyPos[i].position) < 0.7f)
                    {
                        curEnemy[i] = ObjectPool.instance.transform;
                    }
                }
                break;
            case 4:
                for (int i = 0; i < stage2EnemyPos_2.Length; i++)
                {
                    curEnemy[i].position = Vector2.Lerp(curEnemy[i].position, stage2EnemyPos_2[i].position, Time.deltaTime * 2f);
                    if (Vector2.Distance(curEnemy[i].position, stage2EnemyPos_2[i].position) < 0.7f)
                    {
                        curEnemy[i] = ObjectPool.instance.transform;
                    }
                }
                break;


        }
    }

    public void ResetStage(int index)
    {
        if(index == 1)
        {
            isEmpty = true;
            StopCoroutine(SpawnDelay());
            stage = 1;
            foreach (Stat stat in enemyList)
            {
                if(stat != null)
                {

                    if (stat.gameObject.activeSelf)
                        stat.Damage(9999);
                        
                        
                }
            }
            enemyList.Clear();
            StopCoroutine(SpawnDelay());
            wave = 0;
            enemyAmount = 0;
            
            curWave = 0;
            StartCoroutine(SpawnDelay());
        } else if(index == 2)
        {
            isEmpty = true;
            stage = 2;
            StopCoroutine(SpawnDelay());
            foreach (Stat stat in enemyList)
            {
                if (stat != null)
                {
                    if (stat.gameObject.activeSelf)
                        stat.Damage(9999);
                        
                }
            }
            enemyList.Clear();
            StopCoroutine(SpawnDelay());
            wave = 7;
            enemyAmount = 0;
            
            curWave = 7;
            StartCoroutine(SpawnDelay());
        }
    }


    IEnumerator Move(Transform t, Transform target)
    {
        Vector2 targetPos = target.position;
        Vector2 originPos = t.position;
        float distance = 0f;
        while (Vector2.Distance(t.position, targetPos) > 0.1f)
        {
            t.position = Vector2.Lerp(originPos, targetPos, distance * 1);
            distance += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator InvOff()
    {
        Transform[] enemys  = curEnemy.ToArray();
        yield return new WaitForSeconds(0.5f);
        foreach(Transform enemy in enemys)
        {
            enemy.GetComponent<Stat>().invincibility = false;
        }
    }

    IEnumerator StopMove()
    {
        yield return new WaitForSeconds(2f);
        isMoving = true;

    }

    IEnumerator Delay()
    {
        wave++;
        curWave = wave;
        if(wave == 7)
        {
            ChangeStage();
        }
        yield return new WaitForSeconds(2f);
        curEnemy.Clear();
        SpawnEnemy();
        if(wave == 3)
        {
            GameManager.BossHPBar();
            GameManager.SetBoss(0);
        } else if(wave == 6)
        {
            GameManager.BossHPBar();
            GameManager.SetBoss(1);
        }
        else if (wave == 9)
        {
            GameManager.BossHPBar();
            GameManager.SetBoss(0);
        }

        else if (wave == 12)
        {
            GameManager.BossHPBar();
            GameManager.SetBoss(2);
        }
        else if (wave == 13)
        {
            GameManager.GameClear();

        }

        isEmpty = false;
    }


    void ChangeStage()
    {
        stage++;
        
    }

    void SpawnEnemy()
    {
        foreach (Stat stat in enemyList)
        {
            if (stat != null)
            {
                if (stat.gameObject.activeSelf)
                    stat.Damage(9999);

            }
        }
        enemyList.Clear();
        enemyAmount = 0;
        switch (stage)
        {
            case 1:
                {
                    if (wave < 3)
                    {

                        int rnd = Random.Range(0, 2);
                        if (rnd == 0)
                        {
                            
                            for (int i = 0; i < stageEnemyPos.Length; i++)
                            {

                                Stat enemy = Instantiate(enemys[i], stageEnemyPos[i].position + new Vector3(0, 10, 0), Quaternion.identity).GetComponent<Stat>();
                                enemy.invincibility = true;
                                enemy.SetHPValue(enemy.GetHPValue() * (1 + ((wave -1) * 0.25f)));
                                curEnemy.Add(enemy.transform);
                                enemyList.Add(enemy);
                                StartCoroutine(Move(enemy.transform, stageEnemyPos[i]));
                                enemyAmount++;
                            }
                        }
                        else
                        {
                            
                            for (int i = 0; i < stageEnemyPos_2.Length; i++)
                            {
                                Stat stat = Instantiate(enemys2[i], stageEnemyPos_2[i].position + new Vector3(0, 10, 0), Quaternion.identity).GetComponent<Stat>();
                                stat.invincibility = true;
                                stat.SetHPValue(stat.GetHPValue() * (1 + ((wave - 1) * 0.25f)));
                                enemyList.Add(stat);
                                curEnemy.Add(stat.transform);
                                StartCoroutine(Move(stat.transform, stageEnemyPos_2[i]));
                                enemyAmount++;
                            }
                        }
                    }
                    else if (wave == 3)
                    {
                        
                        Stat stat = Instantiate(middleBoss, enemyBossSpawnPos.position + new Vector3(0,10,0), Quaternion.Euler(0,0,90)).GetComponent<Stat>();
                        enemyList.Add(stat);
                        stat.SetHPValue(stat.GetHPValue() * (1 + ((wave - 1) * 0.25f)));
                        enemyAmount++;
                        StartCoroutine(Move(stat.transform, enemyBossSpawnPos));

                    }
                    else if (wave > 3 && wave < 6)
                    {
                        int rnd = Random.Range(0, 2);
                        if (rnd == 0)
                        {
                            
                            for (int i = 0; i < stageEnemyPos_3.Length; i++)
                            {

                                Stat enemy = Instantiate(enemys3[i], stageEnemyPos_3[i].position + new Vector3(0, 10, 0), Quaternion.identity).GetComponent<Stat>();
                                enemy.invincibility = true;
                                enemy.SetHPValue(enemy.GetHPValue() * (1 + ((wave - 1) * 0.25f)));
                                enemyList.Add(enemy);
                                curEnemy.Add(enemy.transform);
                                StartCoroutine(Move(enemy.transform, stageEnemyPos_3[i]));
                                enemyAmount++;
                            }
                        }
                        else
                        {
                            
                            for (int i = 0; i < stageEnemyPos_4.Length; i++)
                            {
                                Stat stat = Instantiate(enemys4[i], stageEnemyPos_4[i].position + new Vector3(0, 10, 0), Quaternion.identity).GetComponent<Stat>();
                                stat.invincibility = true;
                                stat.SetHPValue(stat.GetHPValue() * (1 + ((wave - 1) * 0.25f)));
                                curEnemy.Add(stat.transform);
                                enemyList.Add(stat);
                                StartCoroutine(Move(stat.transform, stageEnemyPos_4[i]));
                                enemyAmount++;
                            }
                        }
                    }
                    else if(wave == 6)
                    {
                        Stat stat = Instantiate(enemyBoss[0], enemyBossSpawnPos.position + new Vector3(0, 10, 0), Quaternion.Euler(0, 0, 180)).GetComponent<Stat>();

                        enemyAmount++;
                        StartCoroutine(Move(stat.transform, enemyBossSpawnPos));
                        enemyList.Add(stat);
                    }

                    break;
                }
            case 2:
                {
                    if(wave < 9)
                    {

                        int rnd = Random.Range(0, 2);
                        if (rnd == 0)
                        {

                       
                            for (int i = 0; i < stage2EnemyPos.Length; i++)
                            {
                                Stat stat = Instantiate(enemys3[i], stage2EnemyPos[i].position + new Vector3(0, 3, 0), Quaternion.identity).GetComponent<Stat>();
                                stat.invincibility = true;
                                stat.SetHPValue(stat.GetHPValue() * (1 + ((wave - 1) * 0.3f)));
                                enemyList.Add(stat);
                                curEnemy.Add(stat.transform);
                                StartCoroutine(Move(stat.transform, stage2EnemyPos[i]));
                                enemyAmount++;
                            }
                        }
                        else
                        {
                       
                            for (int i = 0; i < stage2EnemyPos_2.Length; i++)
                            {
                                Stat stat = Instantiate(enemys4[i], stage2EnemyPos_2[i].position + new Vector3(0, 3, 0), Quaternion.identity).GetComponent<Stat>();
                                stat.invincibility = true;
                                stat.SetHPValue(stat.GetHPValue() * (1 + ((wave - 1) * 0.3f)));
                                enemyList.Add(stat);
                                curEnemy.Add(stat.transform);
                                StartCoroutine(Move(stat.transform, stage2EnemyPos_2[i]));
                                enemyAmount++;
                            }
                        }   
                    } else if(wave == 9)
                    {
                        Stat stat = Instantiate(middleBoss, enemyBossSpawnPos.position + new Vector3(0, 10, 0), Quaternion.Euler(0, 0, 90)).GetComponent<Stat>();
                        enemyList.Add(stat);
                        stat.SetHPValue(stat.GetHPValue() * (1 + ((wave - 1) * 0.3f)));
                        enemyAmount++;
                        StartCoroutine(Move(stat.transform, enemyBossSpawnPos));
                    } else if(wave > 9 && wave <12)
                    {
                        int rnd = Random.Range(0, 2);
                        if (rnd == 0)
                        {


                            for (int i = 0; i < stage2EnemyPos_3.Length; i++)
                            {
                                Stat stat = Instantiate(enemys3[i], stage2EnemyPos_3[i].position + new Vector3(0, 3, 0), Quaternion.identity).GetComponent<Stat>();
                                stat.invincibility = true;
                                stat.SetHPValue(stat.GetHPValue() * (1 + ((wave - 1) * 0.3f)));
                                enemyList.Add(stat);
                                curEnemy.Add(stat.transform);
                                enemyAmount++;
                                StartCoroutine(Move(stat.transform, stage2EnemyPos_3[i]));
                            }
                        }
                        else
                        {

                            for (int i = 0; i < stage2EnemyPos_4.Length; i++)
                            {
                                Stat stat = Instantiate(enemys4[i], stage2EnemyPos_4[i].position + new Vector3(0, 3, 0), Quaternion.identity).GetComponent<Stat>();
                                stat.invincibility = true;
                                stat.SetHPValue(stat.GetHPValue() * (1 + ((wave - 1) * 0.3f)));
                                enemyList.Add(stat);
                                curEnemy.Add(stat.transform);
                                enemyAmount++;
                                StartCoroutine(Move(stat.transform, stage2EnemyPos_4[i]));
                            }
                        }
                    } else if(wave == 12)
                    {

                        Stat stat = Instantiate(enemyBoss[1], enemyBossSpawnPos.position + new Vector3(0, 10, 0), Quaternion.Euler(0, 0, 180)).GetComponent<Stat>();

                        enemyAmount++;
                        StartCoroutine(Move(stat.transform, enemyBossSpawnPos));
                        enemyList.Add(stat);
                    }

                    break;
                }
        }
        StartCoroutine(StopMove());
        StartCoroutine(InvOff());
        isEmpty = false;
    }


}
