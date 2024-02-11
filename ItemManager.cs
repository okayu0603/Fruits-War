using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] Transform itemareaA;
    [SerializeField] Transform itemareaB;

    private float timer;

    public void Update() //画面上にランダムにアイテム生成
    {
        timer += Time.deltaTime;

        if(timer > 10f)
        {
            float x = Random.Range(itemareaA.position.x, itemareaB.position.x);
            float y = Random.Range(itemareaA.position.y, itemareaB.position.y);
            Instantiate(item, new Vector2(x, y), Quaternion.identity);
            timer = 0;
        }

    }

    private void FixedUpdate()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager.IsCountDown) //カウントダウン中は動かない
        {
            return;
        }
    }
}
