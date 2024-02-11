using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] GameObject upWatermelon;
    [SerializeField] GameObject downWatermelon;
    [SerializeField] GameObject Ink1;
    [SerializeField] GameObject Ink2;
    [SerializeField] GameObject Ink3;

    private bool Flag = true;

    private void Update()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        Vector2 position = new Vector2(transform.position.x + 3, transform.position.y);
        Vector2 position2 = new Vector2(transform.position.x - 3, transform.position.y);

        Renderer rend1 = Ink1.GetComponent<Renderer>();
        Renderer rend2 = Ink2.GetComponent<Renderer>();
        Renderer rend3 = Ink3.GetComponent<Renderer>();

        rend1.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
        rend2.material.color = new Color(0.745f, 0.152f, 0.188f, 1);
        rend3.material.color = new Color(0.745f, 0.152f, 0.188f, 1);

        if (gameManager.IsCountDown)
        {
            return;
        }

        if (Flag)
        {
            Instantiate(upWatermelon, position, Quaternion.identity);
            Instantiate(downWatermelon, position2, Quaternion.identity);
            Destroy(this.gameObject);
            Ink1.SetActive(true);
            Ink2.SetActive(true);
            Ink3.SetActive(true);
            Flag = !Flag;
        }
    }
}
