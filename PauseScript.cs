using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    private bool PauseFlag = false;
    [SerializeField] GameObject PauseText;
    [SerializeField] GameObject CountDownText;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (PauseFlag)
            {
                Time.timeScale = 0f; //ポーズ中
                PauseText.SetActive(true); //ポーズ画面の表示
                CountDownText.SetActive(false); //カウントダウン非表示
            }
            else
            {
                Time.timeScale = 1f; //再開
                PauseText.SetActive(false);
                CountDownText.SetActive(true);
            }
            PauseFlag = !PauseFlag; //ONOFFの切り替え
        }
    }
}
