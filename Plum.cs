using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plum : EnemyBase
{
    [SerializeField] GameObject bullet1;
    [SerializeField] GameObject bullet2;
    [SerializeField] GameObject bullet3;
    [SerializeField] GameObject bullet4;
    public override void Shoot() //“G‚Ì’e
    {
        float x = UnityEngine.Random.Range(-1f, 1f);
        Vector2 position = transform.position;
        Vector2 vector = new Vector2(position.x + 1, position.y);
        Vector2 vector2 = new Vector2(position.x, position.y + 1);
        Vector2 vector3 = new Vector2(position.x - 1, position.y);
        Vector2 vector4 = new Vector2(position.x, position.y - 1);

        Debug.Log(position);

        if (-1 <= x && x < -0.5)
        {
            Instantiate(bullet1, vector, Quaternion.identity);
        }
        else if (-0.5 <= x && x < 0)
        {
            Instantiate(bullet2, vector2, Quaternion.identity);
        }
        else if (0 <= x && x < 0.5)
        {
            Instantiate(bullet3, vector3, Quaternion.identity);
        }
        else if (0.5 <= x && x < 1)
        {
            Instantiate(bullet4, vector4, Quaternion.identity);
        }
    }
}
