using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 *  점수 관리
 *  스테이지 관리 
 */


public class GameManager : MonoBehaviour
{
    public int StagePoint;
    public int TotalPoint;
    public int StageIndex;

    public PlayerController PlayerInfo;
    public GameObject[] Stages;

    public Image[] UIHealths;
    public Text UIPoint;
    public Text UIStage;
    public GameObject RestartButton;

    void Start()
    {
        RestartButton.SetActive(false);

        StageIndex = 0;
        StagePoint = 0;
        TotalPoint = 0;

        foreach (var Stage in Stages)
        {
            Stage.SetActive(false);
        }

        Stages[StageIndex].SetActive(true);
        SpawnPlayer(Vector2.zero);

        PlayerInfo._Property.Hp = 3;
        UIStage.text = "Stage " + (StageIndex + 1).ToString();
        OnHealthChanged(3);
    }

    void Update()
    {
        UIPoint.text = (TotalPoint + StagePoint).ToString();    
    }

    public void NextStage()
    {
        if (StageIndex < Stages.Length - 1)
        {
            ++StageIndex;

            Stages[StageIndex - 1].SetActive(false);
            Stages[StageIndex].SetActive(true);

            PlayerInfo._Property.Hp = 3;

            SpawnPlayer(Vector2.zero);
            UIStage.text = "Stage " + (StageIndex + 1).ToString();
        }
        else
        {
            // 게임 Clear
            OnGameClear();
        }

        TotalPoint += StagePoint;
        StagePoint = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerInfo.AddHealth(-1);

            if (PlayerInfo._Property.Hp > 0)
            {
                SpawnPlayer(Vector2.zero);
            }
         }
    }

    public void SpawnPlayer(Vector2 Pos)
    {
        PlayerInfo._Rigid.velocity = Vector2.zero;

        PlayerInfo.transform.position = new Vector3(Pos.x, Pos.y, -1);
    }

    public void OnPlayerDeath()
    {
        Time.timeScale = 0; // 시간 멈춤

        RestartButton.SetActive(true);
    }

    public void OnGameClear()
    {
        Time.timeScale = 0; // 시간 멈춤

        Text btnText = RestartButton.GetComponentInChildren<Text>();
        btnText.text = "Game Clear";

        RestartButton.SetActive(true);
    }

    public void OnHealthChanged(int NowHealth)
    { 
        Debug.Log(NowHealth);

        foreach(var UIHealth in UIHealths)
        {
            UIHealth.color = new Color(1, 1, 1, 0.2f);
        }

        for(int HealthIndex = 0; HealthIndex < NowHealth; ++HealthIndex)
        {
            UIHealths[HealthIndex].color = new Color(1, 1, 1, 1);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
