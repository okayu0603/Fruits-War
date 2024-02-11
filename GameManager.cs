using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject NextStageButton;
    [SerializeField] Text LimitBulletText;
    [SerializeField] GameObject GameOverText;
    [SerializeField] GameObject GameClearText;
    [SerializeField] GameObject Ink1;
    [SerializeField] GameObject Ink2;
    [SerializeField] GameObject Ink3;
    [SerializeField] GameObject HP3;
    [SerializeField] GameObject HP2;
    [SerializeField] GameObject HP1;

    [SerializeField] AudioClip DestroySE;
    [SerializeField] AudioClip InkSE;
    [SerializeField] AudioClip GameClearSE;
    [SerializeField] AudioClip GameOverSE;

    AudioSource audioSource;

    public int hp = 3; //�v���C���[��HP
    public int LimitBullet = 20; //�v���C���[�̎c�e��
    public float StageChangeMinutes = 3f; //�X�e�[�W�J�ڂɂ����鎞��

    public bool IsCountDown { get; set; }
    private bool StageChangeFlag = true;
    public static int StageChangeCount = 0; //���̃X�e�[�W�ɂ����߂邽�߂̃J�E���g��
    private bool Flag = true; //��񂾂����S���Ɏ��s���邽�߂̊�
    private bool GameClearFlag = true; //�Q�[���N���A�̔��f
    private bool GameOverFlag = true; //�Q�[���I�[�o�[�̔��f

    private TextMeshProUGUI countDownText;
    private GameObject[] enemyObject;
    private GameObject[] bulletObject;
    private GameObject[] bullet2Object;
    private GameObject[] bullet3Object;
    private GameObject[] bullet4Object;
    private GameObject[] bullet5Object;
    private GameObject[] bullet6Object;
    private GameObject[] playerbullet;

    // Start is called before the first frame update
    void Start()
    {
        LimitBulletText.text = LimitBullet.ToString();
        audioSource = GetComponent<AudioSource>(); //�����ɂ��Ă���I�[�f�B�I�\�[�X�̎擾
        countDownText = GameObject.Find("CountDownText").GetComponent<TextMeshProUGUI>();
        IsCountDown = true;
        StartCoroutine(CountDown());
    }

    private void FixedUpdate()
    {
        playerbullet = GameObject.FindGameObjectsWithTag("bullet");
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
        bulletObject = GameObject.FindGameObjectsWithTag("Enemy_Bullet"); //�z��I�ȕϐ�
        bullet2Object = GameObject.FindGameObjectsWithTag("Enemy_Bullet2");
        bullet3Object = GameObject.FindGameObjectsWithTag("Enemy_Bullet3");
        bullet4Object = GameObject.FindGameObjectsWithTag("Cherry_Bullet1");
        bullet5Object = GameObject.FindGameObjectsWithTag("Cherry_Bullet2");
        bullet6Object = GameObject.FindGameObjectsWithTag("Watermelon_Bullet1");

        PlayerManager playerManager = FindObjectOfType<PlayerManager>();

        if (enemyObject.Length == 0 && GameClearFlag) //��ʏ�̓G�����Ȃ��Ȃ�����Q�[���N���A
        {
            GameClearText.SetActive(true);
            GameOverFlag = false; //�Q�[���N���A���ɃQ�[�����[�΂̏��������Ȃ�
            if (StageChangeFlag == true) //��ʏ�Ɏc�����e�̏���
            {
                StageChangeFlag = false;
                foreach (GameObject obj in bulletObject) //��ʏ�Ɏc�����e�̔j�� foreach�͔z��̏����Ɏg��
                {
                    Destroy(obj);
                }
                foreach (GameObject obj2 in bullet2Object)
                {
                    Destroy(obj2);
                }
                foreach (GameObject obj3 in bullet3Object)
                {
                    Destroy(obj3);
                }
                foreach (GameObject obj4 in bullet4Object)
                {
                    Destroy(obj4);
                }
                foreach (GameObject obj5 in bullet5Object)
                {
                    Destroy(obj5);
                }
                foreach (GameObject obj6 in bullet6Object)
                {
                    Destroy(obj6);
                }

                audioSource.Stop();
                audioSource.PlayOneShot(GameClearSE);
                NextStageButton.SetActive(true);
            }
        }

        
        if (LimitBullet <= 0 && playerbullet.Length <= 0 && Flag == true && GameOverFlag) //�c�e��0���v���C���[�̒e����ʂ���������玀�S
        {
            playerManager.PlayerDeath();
            audioSource.Stop();
            audioSource.PlayOneShot(DestroySE);
            GameOverText.SetActive(true);
            audioSource.PlayOneShot(GameOverSE);
            Invoke("RestartScene", 7f);
            Flag = false; //1�񂾂����s
            GameOverFlag = !GameOverFlag;
        }
    }

    public void Plum1MISS() //Plum1����̍U���ɂ���
    {
        Renderer rend1 = Ink1.GetComponent<Renderer>();
        Renderer rend2 = Ink2.GetComponent<Renderer>();
        Renderer rend3 = Ink3.GetComponent<Renderer>();
        if (hp == 3)
        {
            rend1.material.color = new Color(0.533f, 0.152f, 0.745f, 1f); //�C���N�̐F�̎w��
            DisplayInk();
        }
        else if (hp == 2)
        {
            rend2.material.color = new Color(0.533f, 0.152f, 0.745f, 1f);
            DisplayInk();
        }
        else if (hp == 1)
        {
            rend3.material.color = new Color(0.533f, 0.152f, 0.745f, 1f);
            DisplayInk();
        }
        else if (hp < 1)
        {
            DisplayInk();
        }
    }

    public void Plum2MISS() //Plum2����̍U���ɂ���
    {
        Renderer rend1 = Ink1.GetComponent<Renderer>();
        Renderer rend2 = Ink2.GetComponent<Renderer>();
        Renderer rend3 = Ink3.GetComponent<Renderer>();

        if (hp == 3)
        {
            rend1.material.color = new Color(0.152f, 0.156f, 0.745f, 1);
            DisplayInk();
        }
        else if (hp == 2)
        {
            rend2.material.color = new Color(0.152f, 0.156f, 0.745f, 1);
            DisplayInk();
        }
        else if (hp == 1)
        {
            rend3.material.color = new Color(0.152f, 0.156f, 0.745f, 1);
            DisplayInk();

        }
        else if (hp < 1)
        {
            DisplayInk();
        }
    }

    public void Plum3MISS() //Plum3����̍U���ɂ���
    {
        Renderer rend1 = Ink1.GetComponent<Renderer>();
        Renderer rend2 = Ink2.GetComponent<Renderer>();
        Renderer rend3 = Ink3.GetComponent<Renderer>();

        if (hp == 3)
        {
            rend1.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
            DisplayInk();
        }
        else if (hp == 2)
        {
            rend2.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
            DisplayInk();
        }
        else if (hp == 1)
        {
            rend3.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
            DisplayInk();
        }
        else if (hp < 1)
        {
            DisplayInk();
        }
    }

    public void Cherry1MISS() //Cherry1����̍U���ɂ���
    {
        Renderer rend1 = Ink1.GetComponent<Renderer>();
        Renderer rend2 = Ink2.GetComponent<Renderer>();
        Renderer rend3 = Ink3.GetComponent<Renderer>();

        if (hp == 3)
        {
            rend1.material.color = new Color(1f, 0f, 0.407f, 1f);
            DisplayInk();
        }
        else if (hp == 2)
        {
            rend2.material.color = new Color(1f, 0f, 0.407f, 1f);
            DisplayInk();
        }
        else if (hp == 1)
        {
            rend3.material.color = new Color(1f, 0f, 0.407f, 1f);
            DisplayInk();
        }
        else if (hp < 1)
        {
            DisplayInk();
        }
    }

    public void Cherry2MISS() //Cherry2����̍U���ɂ���
    {
        Renderer rend1 = Ink1.GetComponent<Renderer>();
        Renderer rend2 = Ink2.GetComponent<Renderer>();
        Renderer rend3 = Ink3.GetComponent<Renderer>();

        if (hp == 3)
        {
            rend1.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
            DisplayInk();
        }
        else if (hp == 2)
        {
            rend2.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
            DisplayInk();
        }
        else if (hp == 1)
        {
            rend3.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
            DisplayInk();
        }
        else if (hp < 1)
        {
            DisplayInk();
        }
    }

    public void Cherry3MISS() //Cherry3����̍U���ɂ���
    {
        Renderer rend1 = Ink1.GetComponent<Renderer>();
        Renderer rend2 = Ink2.GetComponent<Renderer>();
        Renderer rend3 = Ink3.GetComponent<Renderer>();

        if (hp == 3)
        {
            rend1.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
            DisplayInk();
        }
        else if (hp == 2)
        {
            rend2.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
            DisplayInk();
        }
        else if (hp == 1)
        {
            rend3.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
            DisplayInk();
        }
        else if (hp < 1)
        {
            DisplayInk();
        }
    }

    public void Shoot() //�v���C���[�̒e����
    {
        LimitBullet--; //�c�e���̌���
        LimitBulletText.text = LimitBullet.ToString();

        if(LimitBullet < 0)
        {
            LimitBullet = 0;
            LimitBulletText.text = LimitBullet.ToString();
        }
    }

    public void RestartScene() //�Q�[���I�[�o�[�̂��Ƃ̃��X�^�[�g
    {
        UnityEngine.SceneManagement.Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    private IEnumerator CountDown() //�Q�[���X�^�[�g���̃J�E���g�_�E��
    {
        countDownText.text = SceneManager.GetActiveScene().name;

        yield return new WaitForSeconds(1f);
        countDownText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownText.text = "Start";
        IsCountDown = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(countDownText);
    }

    private void DisplayInk()
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if (hp == 3)
        {
            hp--;
            Ink1.SetActive(true); //�C���N�̕\��
            HP3.SetActive(false); //HP1����
            audioSource.PlayOneShot(InkSE); //�C���N���Đ�
        }
        else if(hp == 2)
        {
            hp--;
            Ink2.SetActive(true);
            HP2.SetActive(false);
            audioSource.PlayOneShot(InkSE);
        }
        else if(hp == 1)
        {
            hp--;
            Ink3.SetActive(true);
            HP1.SetActive(false);
            audioSource.PlayOneShot(InkSE);
        }
        else if(hp < 1 && GameOverFlag)
        {
            GameClearFlag = false; //�Q�[���I�[�o�[���ɃQ�[���N���A�̏��������Ȃ�
            playerManager.PlayerDeath();
            audioSource.Stop();
            audioSource.PlayOneShot(DestroySE);
            GameOverText.SetActive(true);
            audioSource.PlayOneShot(GameOverSE);
            Invoke("RestartScene", 7f);
        }
    }
}
