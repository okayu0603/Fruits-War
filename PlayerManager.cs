using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    public float speed = 0.01f; //プレイヤーの移動速度
    public int BulletCount = 20; //残弾数
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

    private void FixedUpdate() //上下左右で移動，wasdで発射
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
        Vector2 position = this.transform.position;
        //プレイヤーの中心から距離1離れたところから発射
        Vector2 position2 = new Vector2(position.x, position.y + 0.75f);
        Vector2 position3 = new Vector2(position.x - 0.75f, position.y);
        Vector2 position4 = new Vector2(position.x, position.y - 0.75f);
        Vector2 position5 = new Vector2(position.x + 0.75f, position.y);

        if (gameManager.IsCountDown) //カウントダウン中は動かない
        {
            return;
        }

        if(enemyObject.Length == 0) //ゲームクリア時にプレイヤーを動かなくさせる
        {
            return;
        }

        //プレイヤーの移動
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
        //プレイヤーの弾発射
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
        if(BulletCount < 0) //ライフが0未満になったら死亡
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

    private void OnCollisionEnter2D(Collision2D collision) //衝突判定
    {
        Vector2 position = this.transform.position;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (collision.gameObject.tag == "Enemy_Bullet") //Plum-1の敵
        {
            rb.velocity = Vector2.zero; //弾に当たると一旦速度0になる
            gameManager.Plum1MISS();
        }
        else if (collision.gameObject.tag == "Enemy_Bullet2") //Plum-2の敵
        {
            rb.velocity = Vector2.zero;
            gameManager.Plum2MISS();
        }
        else if (collision.gameObject.tag == "Enemy_Bullet3") //Plum-3の敵
        {
            rb.velocity = Vector2.zero;
            gameManager.Plum3MISS();
        }
        if (collision.gameObject.tag == "Cherry_Bullet1") //Cherry-1の敵
        {
            rb.velocity = Vector2.zero;
            gameManager.Cherry1MISS();
        }
        else if (collision.gameObject.tag == "Cherry_Bullet2") //Cherry-2の敵
        {
            rb.velocity = Vector2.zero;
            gameManager.Cherry2MISS();
        }
        else if (collision.gameObject.tag == "Cherry_Bullet3") //Cherry-3の敵
        {
            rb.velocity = Vector2.zero;
            gameManager.Cherry3MISS();
        }
        else if (collision.gameObject.tag == "Watermelon_Bullet1") //Cherry-3の敵
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

    public void PlayerDeath() //プレイヤー死亡時
    {
        Instantiate(PlayerDeathEffect, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
