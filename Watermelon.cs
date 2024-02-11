using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : EnemyBase
{
    [SerializeField] float Rad;
    [SerializeField] float Angle = 0;
    [SerializeField] GameObject bullet;

    public override void Shoot()
    {
        Rad = Angle * Mathf.PI / 180f;
        Vector3 shootposition = new Vector3(transform.position.x + Mathf.Cos(Rad), transform.position.y + Mathf.Sin(Rad), 0); //オブジェクトの中心から半径1離れた場所から発射
        Instantiate(bullet, shootposition, Quaternion.identity);
        Angle += 30;

        if(Angle > 360)
        {
            Angle = 30;
        }
    }
}
