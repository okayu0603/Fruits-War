using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private float time = 0f; //time
    private bool SetActiveFlag = false; //�_�������ł��̃t���O
    public float span = 0.5f; //�_�ł̊Ԋu

    [SerializeField] GameObject ClicktoStart;

    void Update() //�e�L�X�g�̓_��(Click to Start!)
    {
        time += Time.deltaTime; //�b���J�E���g

        if(SetActiveFlag == false) //0.5�b����
        {
            ClicktoStart.SetActive(false);
            if(time > span)
            {
                time = 0f;
                SetActiveFlag = true;
            }
        }
        else if(SetActiveFlag == true) //0.5�b�_��
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
