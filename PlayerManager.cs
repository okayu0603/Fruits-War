using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    public float speed = 0.01f; //�v���C���[�̈ړ����x
    public int BulletCount = 20; //�c�e��
    private bool SpeedDownFlag;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet_down;
    [SerializeField] GameObject bullet_left;
    [SerializeField] GameObject bullet_right;
    [SerializeField] GameObject PlayerDeathEffect;
    [SerializeField] AudioClip audioClip;
    AudioSource audioSource;
    private GameObject[] enemyObject;

    bool itemJudge = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate() //�㉺���E�ňړ��Cwasd�Ŕ���
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
        Vector2 position = this.transform.position;
        //�v���C���[�̒��S���狗��1���ꂽ�Ƃ��납�甭��
        Vector2 position2 = new Vector2(position.x, position.y + 0.75f);
        Vector2 position3 = new Vector2(position.x - 0.75f, position.y);
        Vector2 position4 = new Vector2(position.x, position.y - 0.75f);
        Vector2 position5 = new Vector2(position.x + 0.75f, position.y);

        if (gameManager.IsCountDown) //�J�E���g�_�E�����͓����Ȃ�
        {
            return;
        }

        if(enemyObject.Length == 0) //�Q�[���N���A���Ƀv���C���[�𓮂��Ȃ�������
        {
            return;
        }

        //�v���C���[�̈ړ�
        if (Input.GetKey("left"))
        {
            position.x -= speed;
        }
        if (Input.GetKey("right"))
        {
            position.x += speed;
        }
        if (Input.GetKey("up"))
        {
            position.y += speed;
        }
        if (Input.GetKey("down"))
        {
            position.y -= speed;
        }
        //�v���C���[�̒e����
        if (Input.GetKeyDown("w"))
        {
            if(BulletCount >= 1)
            {
                BulletCount--;
                gameManager.Shoot();
                Instantiate(bullet, position2, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown("a"))
        {
            if(BulletCount >= 1)
            {
                BulletCount--;
                gameManager.Shoot();
                Instantiate(bullet_left, position3, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown("s"))
        {
            if(BulletCount >= 1)
            {
                BulletCount--;
                gameManager.Shoot();
                Instantiate(bullet_down, position4, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown("d"))
        {
            if(BulletCount >= 1)
            {
                BulletCount--;
                gameManager.Shoot();
                Instantiate(bullet_right, position5, Quaternion.identity);
            }
        }
        if(BulletCount < 0) //���C�t��0�����ɂȂ����玀�S
        {
            PlayerDeath();
        }
        if(itemJudge == true)
        {
            audioSource.PlayOneShot(audioClip);
            itemJudge = false;
        }
        this.transform.position = position;

    }

    private void OnCollisionEnter2D(Collision2D collision) //�Փ˔���
    {
        Vector2 position = this.transform.position;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (collision.gameObject.tag == "Enemy_Bullet") //Plum-1�̓G
        {
            rb.velocity = Vector2.zero; //�e�ɓ�����ƈ�U���x0�ɂȂ�
            gameManager.Plum1MISS();
        }
        else if (collision.gameObject.tag == "Enemy_Bullet2") //Plum-2�̓G
        {
            rb.velocity = Vector2.zero;
            gameManager.Plum2MISS();
        }
        else if (collision.gameObject.tag == "Enemy_Bullet3") //Plum-3�̓G
        {
            rb.velocity = Vector2.zero;
            gameManager.Plum3MISS();
        }
        if (collision.gameObject.tag == "Cherry_Bullet1") //Cherry-1�̓G
        {
            rb.velocity = Vector2.zero;
            gameManager.Cherry1MISS();
        }
        else if (collision.gameObject.tag == "Cherry_Bullet2") //Cherry-2�̓G
        {
            rb.velocity = Vector2.zero;
            gameManager.Cherry2MISS();
        }
        else if (collision.gameObject.tag == "Cherry_Bullet3") //Cherry-3�̓G
        {
            rb.velocity = Vector2.zero;
            gameManager.Cherry3MISS();
        }
        else if (collision.gameObject.tag == "Watermelon_Bullet1") //Cherry-3�̓G
        {
            rb.velocity = Vector2.zero;
            gameManager.Cherry3MISS();
        }
        else if(collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Enemy")
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            itemJudge = true;
            Debug.Log(itemJudge);
        }
    }

    public void PlayerDeath() //�v���C���[���S��
    {
        Instantiate(PlayerDeathEffect, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
