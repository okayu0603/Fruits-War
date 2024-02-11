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
    private void Update() //“GŒ‚”j‚É”š”j‰¹‚ğ–Â‚ç‚·
    {
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy");

        if (StartEnemyCountFlag == true) //Å‰‚Ì“G‚Ì”‚ğ”‚¦‚Ä•Û‘¶
        {
            EnemyCount = enemyObject.Length;
            StartEnemyCountFlag = false;
        }

        if (enemyObject.Length == EnemyCount - 1) //“G‚Ì”‚ªˆê‘ÌŒ¸‚Á‚½‚ç‰¹‚ğ–Â‚ç‚·
        {
            audioSource.PlayOneShot(DestroySE);
            EnemyCount--;
        }
    }
}
