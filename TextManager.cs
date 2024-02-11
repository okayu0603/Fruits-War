using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private float time = 0f; //time
    private bool SetActiveFlag = false; //点灯か消滅かのフラグ
    public float span = 0.5f; //点滅の間隔

    [SerializeField] GameObject ClicktoStart;

    void Update() //テキストの点滅(Click to Start!)
    {
        time += Time.deltaTime; //秒数カウント

        if(SetActiveFlag == false) //0.5秒消滅
        {
            ClicktoStart.SetActive(false);
            if(time > span)
            {
                time = 0f;
                SetActiveFlag = true;
            }
        }
        else if(SetActiveFlag == true) //0.5秒点灯
        {
            ClicktoStart.SetActive(true);
            if (time > span)
            {
                time = 0f;
                SetActiveFlag = false;
            }
        }
    }
}
