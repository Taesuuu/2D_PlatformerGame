using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("-----GameObject-----")]
    public GameObject[] endObjects;
    public GameObject Player;

    [Header("-----Stages-----")]
    public GameObject[] Stages;

    [Header("-----Int & Float-----")]
    public int stageKeyNum;
    public int keyCount;


    private Vector2 startPos;
    private int stageIndex;

    // Start is called before the first frame update
    void Start()
    {
        stageIndex = 0;
        startPos = new Vector2(Player.transform.position.x, Player.transform.position.y);
        for(int i = 0; i < Stages.Length; i++)
        {
            Stages[i].SetActive(false);
            endObjects[i].SetActive(false);
        }

        Stages[stageIndex].SetActive(true);
        GetStageKeyNum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void touchTrap()
    {
        Debug.Log("밟았다");
        Player.transform.position = startPos;
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
    }

    public void GetKeyOjbect()
    {
        keyCount++;
        if(keyCount == stageKeyNum)
        {
            endObjects[stageIndex].SetActive(true);
        }
    }

    public void GameOver()
    {
        //게임 종료
        Debug.Log("게임 종료");
        Player.GetComponent<Rigidbody2D>().gravityScale = 0;

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
}
