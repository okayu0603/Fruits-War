using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WaterMelonBullet : MonoBehaviour
{
    private static float Angle = 0;
    private static float rad;
    public float Force = 0f;
    private int ReflectionCount = 0;
    Vector3 vec;

    public void Start()
    {
        rad = Angle * Mathf.PI / 180f; //ラジアンに変換
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        Vector3 bullet = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f); //単位円の外側に向けたベクトル
        vec = Force * (bullet);
        Angle += 30; //30°刻み
        rigidbody2D.AddForce(vec);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Enemy_Bullet" || collision.gameObject.tag == "Enemy_Bullet2" || collision.gameObject.tag == "Enemy_Bullet3")
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Cherry_Bullet1" || collision.gameObject.tag == "Cherry_Bullet2" || collision.gameObject.tag == "Cherry_Bullet3") //チェリー
        {
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Watermelon_Bullet1")
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            ReflectionCount++;
            if (ReflectionCount == 3)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
