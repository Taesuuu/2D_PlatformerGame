using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("-----GameObject-----")]
    public GameObject[] endObjects;
    public GameObject[] boxObjects;
    public GameObject Player;

    [Header("-----Stages-----")]
    public GameObject[] Stages;

    [Header("-----Int & Float-----")]
    public int stageKeyNum;
    public int keyCount;
    public int playerHp;

    [Header("-----UI obejct-----")]
    public Text keyCountText;
    public Text stageText;
    public Image[] hps;

    private Vector2 startPos;
    private int stageIndex;

    // Start is called before the first frame update
    void Start()
    {
        stageIndex = 0;
        playerHp = 3;
        stageText.text = "Stage " + (stageIndex + 1);
        startPos = new Vector2(Player.transform.position.x, Player.transform.position.y);
        for(int i = 0; i < Stages.Length; i++)
        {
            Stages[i].SetActive(false);
            endObjects[i].SetActive(false);
        }

        Stages[stageIndex].SetActive(true);
        GetStageKeyNum();
        keyCountText.text = keyCount + " / " + stageKeyNum;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void touchTrap()
    {
        Debug.Log("밟았다");
        Player.transform.position = startPos;
        playerHp--;
        hps[playerHp % 2].color = new Color(1, 1, 1, 0.3f);
        if(playerHp == 0)
        {
            GameOver();
        }
    }

    public void stageClear()
    {
        Stages[stageIndex].SetActive(false);
        Player.transform.position = startPos;
        stageIndex++;
        keyCount = 0;
        if (stageIndex == Stages.Length)
        {
            GameOver();
            return;
        }
        GetStageKeyNum();
        Stages[stageIndex].SetActive(true);
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        keyCountText.text = keyCount + " / " + stageKeyNum;
        stageText.text = "Stage " + (stageIndex + 1);
    }

    public void GetKeyOjbect()
    {
        keyCount++;
        keyCountText.text = keyCount + " / " + stageKeyNum;
        if (keyCount == stageKeyNum)
        {
            endObjects[stageIndex].SetActive(true);
        }
    }

    public void GetHPobject()
    {
        hps[playerHp % 2].color = new Color(1, 1, 1, 1f);
        if(playerHp == 3 )
        {
            return;
        }
        else
        {
            playerHp++;
        }
        
    }

    public void GameOver()
    {
        //게임 종료
        for(int y = 0; y < hps.Length; y++)
        {
            hps[y].color = new Color(1, 0, 0, 0);
        }
        for(int i = 0; i < Stages.Length; i++)
        {
            Stages[i].SetActive(false);
        }

        Debug.Log("게임 종료");


        // 클로징 화면 출력
    }

    void GetStageKeyNum()
    {
        switch (stageIndex)
        {
            case 0:
                stageKeyNum = 3;
                break;
            case 1:
                stageKeyNum = 4;
                break;
            case 2:
                stageKeyNum = 5;
                break;
        }
    }

    public void RandomBoxtouch()
    {
        boxObjects[stageIndex].SetActive(true);
    }
}
