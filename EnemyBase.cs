using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class EnemyBase : MonoBehaviour
{
    private float timer = 0f; //�^�C�}�[
    private float nextMoveTime;
    private Vector3 moveDirection;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveInterval;
    [SerializeField] float hp;
    [SerializeField] float interval; //�e�������Ԃ̊Ԋu
    [SerializeField] GameObject EnemyDeathEffect;
    [SerializeField] AudioClip EnemyDamageSE;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeMoveDirection(); //�����̈ړ������������_���ɐݒ�
    }

    private void Update()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager.IsCountDown) //�J�E���g�_�E�����͓����Ȃ�
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= interval) //interval(2s)�𒴂����
        {
            Shoot(); //�e����
            timer = 0f; //�^�C�}�[�̃��Z�b�g
        }
        if (Time.time >= nextMoveTime) //�ړ�������ς���^�C�~���O�ɂȂ�����A�V���������ɕύX
        {
            ChangeMoveDirection();
        }

        // �ړ�����
        Move();
    }

    private void ChangeMoveDirection() //�G�̈ړ�����
    {
        float horizontal = UnityEngine.Random.Range(-1f, 1f); //���������̃����_��
        float vertical = UnityEngine.Random.Range(-1f, 1f); //���������̃����_��
        moveDirection = new Vector3(horizontal, vertical, 0).normalized; //xy�����̑傫��0�`1�̃x�N�g����g�ݍ��킹�ĕ�����ݒ�

        nextMoveTime = Time.time + moveInterval; //�ړ�������ς��鎞�����v�Z
    }

    private void Move() //�G�̓���
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime); //�ړ������Ƒ��x�������Ĉړ�
    }

    public virtual void Shoot()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision) //�G�̏Փ˔���
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (collision.gameObject.tag == "Enemy_Bullet" || collision.gameObject.tag == "Enemy_Bullet2" || collision.gameObject.tag == "Enemy_Bullet3") //�v����
        {
            rb.velocity = Vector2.zero; //�e�ɂ�����Ƒ��x0�ɂȂ�
        }
        else if (collision.gameObject.tag == "Cherry_Bullet1" || collision.gameObject.tag == "Cherry_Bullet2" || collision.gameObject.tag == "Cherry_Bullet3") //�`�F���[
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
            hp--; //�G��HP����
            audioSource.PlayOneShot(EnemyDamageSE, 0.5f); //�G���_���[�W���󂯂�����SE
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

    public void EnemyDeath() //�G�̎��S��
    {
        Instantiate(EnemyDeathEffect, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
