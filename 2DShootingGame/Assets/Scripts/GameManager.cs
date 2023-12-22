
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int score;

    

    Player player;

    public InputField inputField;

    public GameObject HPBar;
    public Slider HPSlider;

    public GameObject gameOverUI;

    public GameObject rankingUI;

    public Text scoreText;
    public Text hpText;
    public Text enemyText;
    public Text stageText;
    public Text waveText;

    public Text RankText;

    public Text gameOverText;

    int boss;

    public Text overScoreText;

    public static GameManager Instance { get; private set; }

    public GameObject[] items;

    public Image Fade;

    public GameObject pObj;

    public bool start = false;
    
    public static GameObject SpawnItem(int index)
    {
        return Instantiate(Instance.items[index], Instance.transform.position, Quaternion.identity);
    }

    void Awake()
    {
        if(Instance == null)
        {

            Instance = this;
        } else 
        {
            Destroy(this.gameObject);
        }
        score = 0;

        player = Player.Instance;
        pObj.SetActive(false);
        Fade.gameObject.SetActive(true);
        StartCoroutine(FadeScreen());

    }

    IEnumerator FadeScreen()
    {
        float a = 1;
        while (Fade.color.a > 0)
        {
            Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, a);
            a -= Time.deltaTime * 1;
            yield return null;
        }
        pObj.SetActive(true);
        start = true;
        Fade.gameObject.SetActive(false);
    }

    public void GoToTitle()
    {
        StartCoroutine(FadeScreenOn());
    }

    IEnumerator FadeScreenOn()
    {
        float a = 0;
        Fade.gameObject.SetActive(true);
        while (Fade.color.a < 1)
        {
            Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, a);
            a += Time.deltaTime * 1;
            yield return null;
        }
        
        start = false;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }


    public void SetEnemyText()
    {
        enemyText.text = "Enemy Left : " + WaveManager.Instance.enemyAmount;
        stageText.text = "Stage : " + WaveManager.Instance.stage;
        int w = WaveManager.Instance.wave;
        waveText.text = "Wave : " + (w+1); 
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            Cheat();
            SetUIText();
            SetEnemyText();
        }
        if(HPBar != null)
        {

            if(HPBar.activeSelf )
            {
                UpdateHPBar();
            }
        }
    }

    public static void AddScore(int value)
    {
        if(Instance.start)
            score += value;
    }

    public static void SetBoss(int index)
    {
        Instance.boss = index;
    }

    float GetBossValue()
    {
        switch(boss)
        {
            case 0:
                Stat stat = MiddleBoss.Instance.gameObject.GetComponent<Stat>();
                return stat.GetHPValue() / stat.GetMaxHPValue();
                break;
            case 1:
                 
                Stat st = Boss_1.Instance.gameObject.GetComponent<Stat>();
                return st.GetHPValue() / st.GetMaxHPValue();
                break;

            case 2:
                Stat s = Boss_2.Instance.gameObject.GetComponent<Stat>();
                return s.GetHPValue() / s.GetMaxHPValue();
                break;
            default:
                return 0;
                break;
        }
    }

    void Cheat()
    {
       if(Input.GetKeyDown(KeyCode.F1))
        {
            if(!Player.Instance.GetComponent<Stat>().invincibility)
                Player.Instance.GetComponent<Stat>().invincibility = true;
            else
                Player.Instance.GetComponent<Stat>().invincibility = false;
            Player.Instance.Shield();
        } else if(Input.GetKeyDown(KeyCode.F2))
        {
            if(WaveManager.Instance.enemyList.Count == 0)
            {
                return;
            }
            foreach (Stat stat in WaveManager.Instance.enemyList)
            {
                
                if (stat != null)
                {

                    if (stat.gameObject.activeSelf)
                        stat.Damage(9999);
                }
            }
            WaveManager.Instance.enemyAmount = 0;
            WaveManager.Instance.enemyList.Clear();
        } else if(Input.GetKeyDown(KeyCode.F3))
        {
            WaveManager.Instance.ResetStage(1);
            
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            WaveManager.Instance.ResetStage(2);

        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            StartCoroutine(FadeScreenOn());
        } else if(Input.GetKeyDown(KeyCode.F6))
        {
            
            SetRank();
        } else if(Input.GetKeyDown(KeyCode.F7))
        {
            SpawnItem(0).transform.position = Player.Instance.transform.position; 
        } else if(Input.GetKeyDown(KeyCode.F8))
        {
            SpawnItem(1).transform.position = Player.Instance.transform.position;
        }

    }

    private void UpdateHPBar()
    {
        
        HPSlider.value = GetBossValue();
    }

    public static void BossHPBarOff()
    {
        Instance.HPBar.SetActive(false);
    }

    public static void BossHPBar()
    {
        Instance.HPBar.SetActive(true);
        Stat stat = MiddleBoss.Instance.gameObject.GetComponent<Stat>();
        Instance.HPSlider.value = stat.GetHPValue() / stat.GetMaxHPValue();

    }

    void SetUIText()
    {
        scoreText.text = "Score : " + score;
        hpText.text = "HP : " + Player.Instance.gameObject.GetComponent<Stat>().GetHPValue();
    }

    public static void GameOver()
    {
        Instance.SetUIText();
        Instance.start = false;
        Instance.StartCoroutine(Instance.delay());
        Instance.SetUIText();
        Instance.SetRankUI();
        Instance.gameOverText.text = "Game Over";
    }

    public static void GameClear()
    {
        Instance.start = false;
        Instance.gameOverText.text = "Game Clear";
        Instance.StartCoroutine(Instance.delay());
        Instance.SetRankUI();

    }

    public void SetRankUI()
    {
        string rank = "";
        if(score <= 20000)
        {
            rank = "D";
        } else if(score <= 40000)
        {
            rank = "C";
        } else if(score <= 60000)
        {
            rank = "B";
        } else if(score <= 80000) {
            rank = "A";
        } else if(score <= 100000)
        {
            rank = "S";
        } else if(score > 100000)
        {
            rank="SS";
        }
        RankText.text = "RANK : " + rank;



    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        Instance.gameOverUI.SetActive(true);
        Instance.overScoreText.text = "점수 : " + score;

    }

    public void SaveScore()
    {
        if (inputField.text == "" || inputField.text == null )
        {
            return;
        }
        gameOverUI.SetActive(false);
        SaveData saveData = SaveSystem.Load("data");
        if(saveData.rankingName.Contains(inputField.text))
        {

            for(int i = 0; i < saveData.rankingScore.Count; i++)
            {
                if(saveData.rankingName[i] == inputField.text)
                {
                    if(saveData.rankingScore[i] < score)
                    {
                        saveData.rankingScore [i] = score;
                    }
                }
            }
        } else
        {
            saveData.rankingScore.Add(score);
            saveData.rankingName.Add(inputField.text);
        }
        
        SaveSystem.Save(saveData, "data");
        SetRank();
    }

    void SetRank()
    {
        
        SaveData saveData = SaveSystem.Load("data");

        foreach(var item in saveData.rankingScore)
        {
            Debug.Log(item);
        }

        int[] rankScores = new int[10] ;
        for(int i = 0; i < rankScores.Length; i++)
        {
            rankScores [i] = -1;
        }
        string[] rankNames = new string[10];
        int sel = 0;
        if(saveData != null && saveData.rankingScore.Count >0)
        {

            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j < saveData.rankingScore.Count; j++)
                {
                    if(rankScores[i] == -1)
                    {
                        rankScores[i] = saveData.rankingScore [j];
                        rankNames[i] = saveData.rankingName[j];
                        sel = j;
                    } else
                    {
                    
                        if(rankScores[i] < saveData.rankingScore[j])
                        {
                        
                            rankScores[i] = saveData.rankingScore[j];
                            rankNames[i] = saveData.rankingName[j];
                            sel = j;
                        }
                    }
                
                }
                saveData.rankingScore[sel] = -1;
                saveData.rankingName[sel] = "None";
            }
        }
        rankingUI.SetActive(true);
        for(int i = 0; i < rankScores.Length; i++)
        {
            Debug.Log(rankNames[i] + " " + rankScores[i] + " " + (i+1));
            if(rankScores[i] == -1)
            {
                rankingUI.transform.GetChild(i + 1).GetComponent<Text>().text = (i + 1) + ". 없음";
            } else
            {

                rankingUI.transform.GetChild(i + 1).GetComponent<Text>().text = (i + 1) + ". " + rankNames[i] + "      점수 : " + rankScores[i];
            }
        }
    }


}
