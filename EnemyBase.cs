using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class EnemyBase : MonoBehaviour
{
    private float timer = 0f; //タイマー
    private float nextMoveTime;
    private Vector3 moveDirection;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveInterval;
    [SerializeField] float hp;
    [SerializeField] float interval; //弾を撃つ時間の間隔
    [SerializeField] GameObject EnemyDeathEffect;
    [SerializeField] AudioClip EnemyDamageSE;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeMoveDirection(); //初期の移動方向をランダムに設定
    }

    private void Update()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager.IsCountDown) //カウントダウン中は動かない
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= interval) //interval(2s)を超えると
        {
            Shoot(); //弾発射
            timer = 0f; //タイマーのリセット
        }
        if (Time.time >= nextMoveTime) //移動方向を変えるタイミングになったら、新しい方向に変更
        {
            ChangeMoveDirection();
        }

        // 移動処理
        Move();
    }

    private void ChangeMoveDirection() //敵の移動方向
    {
        float horizontal = UnityEngine.Random.Range(-1f, 1f); //水平方向のランダム
        float vertical = UnityEngine.Random.Range(-1f, 1f); //垂直方向のランダム
        moveDirection = new Vector3(horizontal, vertical, 0).normalized; //xy方向の大きさ0～1のベクトルを組み合わせて方向を設定

        nextMoveTime = Time.time + moveInterval; //移動方向を変える時刻を計算
    }

    private void Move() //敵の動き
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime); //移動方向と速度をかけて移動
    }

    public virtual void Shoot()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision) //敵の衝突判定
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (collision.gameObject.tag == "Enemy_Bullet" || collision.gameObject.tag == "Enemy_Bullet2" || collision.gameObject.tag == "Enemy_Bullet3") //プラム
        {
            rb.velocity = Vector2.zero; //弾にあたると速度0になる
        }
        else if (collision.gameObject.tag == "Cherry_Bullet1" || collision.gameObject.tag == "Cherry_Bullet2" || collision.gameObject.tag == "Cherry_Bullet3") //チェリー
        {
            rb.velocity = Vector2.zero;
        }
        else if (collision.gameObject.tag == "Watermelon_Bullet1")
        {
            rb.velocity = Vector2.zero;
        }
        else if (collision.gameObject.tag == "bullet")
        {
            rb.velocity = Vector2.zero;
            hp--; //敵のHP減少
            audioSource.PlayOneShot(EnemyDamageSE, 0.5f); //敵がダメージを受けた時のSE
            if (hp == 0)
            {
                EnemyDeath();
            }
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void EnemyDeath() //敵の死亡時
    {
        Instantiate(EnemyDeathEffect, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
