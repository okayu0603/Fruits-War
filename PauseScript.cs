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
                Time.timeScale = 0f; //�|�[�Y��
                PauseText.SetActive(true); //�|�[�Y��ʂ̕\��
                CountDownText.SetActive(false); //�J�E���g�_�E����\��
            }
            else
            {
                Time.timeScale = 1f; //�ĊJ
                PauseText.SetActive(false);
                CountDownText.SetActive(true);
            }
            PauseFlag = !PauseFlag; //ONOFF�̐؂�ւ�
        }
    }
}
