using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RankingUI;
    public GameObject howToPlay;
    public Image Fade;

    bool isStart = false;

    bool isRank = false;
    
    
    public static UIManager Instance { get; set; }

    void Awake()
    {

        if(Instance == null)
        {
            Instance = this;
        } else
        {
            bool value = Instance.isRank;
            Destroy(Instance.gameObject);
            Instance = this;
            if(value)
            {

                isStart = false;
                Ranking();
            }
            isRank = false;
        }
        

        StartCoroutine(FadeScreenOff());
        
    
    }


    public void GameStart()
    {
        Fade.gameObject.SetActive(true);
        StartCoroutine(FadeScreen());
    }

    void GoToRanking()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F6))
        {
            //GoToRanking();
            //isRank = true;
        }
        
        if(Input.GetKeyDown(KeyCode.Escape) && !isStart)
        {
            if(RankingUI.activeSelf)
            {
                RankingUI.SetActive(false);
            } else if(howToPlay.activeSelf)
            {
                howToPlay.SetActive(false);
            }
        }
    }

    public IEnumerator FadeScreen()
    {
        
        float a = 0;
        while(Fade.color.a < 1)
        {
            Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, a);
            a += Time.deltaTime * 1;
            yield return null;
        }
        isStart = true;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        

    }

  

    IEnumerator FadeScreenOff()
    {
        float a = 1;
        Fade.gameObject.SetActive(true);
        while (Fade.color.a > 0)
        {
            Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, a);
            a -= Time.deltaTime * 1;
            yield return null;
        }
        Fade.gameObject.SetActive(false);


    }

    public void Exit()
    {
        
        Application.Quit();

    }

    public void HowToPlay()
    {
        
            howToPlay.SetActive(true);
       
    }

    public void Ranking()
    {
        if (!RankingUI.activeSelf)
        {
            
            RankingUI.SetActive(true);
            SaveData saveData = SaveSystem.Load("data");
            
           

            int[] rankScores = new int[10];
            for (int i = 0; i < 10; i++)
            {
                rankScores[i] = -1;
            }
            string[] rankNames = new string[10];
            int sel = 0;
            if(saveData != null && saveData.rankingScore.Count > 0)
            {

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < saveData.rankingScore.Count; j++)
                    {
                        if (rankScores[i] == -1)
                        {
                            rankScores[i] = saveData.rankingScore[j];
                            rankNames[i] = saveData.rankingName[j];
                            sel = j;
                        }
                        else
                        {

                            if (rankScores[i] < saveData.rankingScore[j])
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
            for (int i = 0; i < 10; i++)
            {
                Debug.Log(rankNames[i] + " " + rankScores[i] + " " + (i + 1));
                if (rankScores[i] == -1)
                {
                    RankingUI.transform.GetChild(i + 1).GetComponent<Text>().text = (i + 1) + ". 없음";
                }
                else
                {

                    RankingUI.transform.GetChild(i + 1).GetComponent<Text>().text = (i + 1) + ". " + rankNames[i] + "      점수 : " + rankScores[i];
                }
            }
        }
        
    }
}
