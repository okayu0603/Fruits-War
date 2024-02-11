using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip DestroySE;
    AudioSource audioSource;
    private GameObject[] enemyObject;
    private int EnemyCount;
    private bool StartEnemyCountFlag = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() //�G���j���ɔ��j����炷
    {
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy");

        if (StartEnemyCountFlag == true) //�ŏ��̓G�̐��𐔂��ĕۑ�
        {
            EnemyCount = enemyObject.Length;
            StartEnemyCountFlag = false;
        }

        if (enemyObject.Length == EnemyCount - 1) //�G�̐�����̌������特��炷
        {
            audioSource.PlayOneShot(DestroySE);
            EnemyCount--;
        }
    }
}
