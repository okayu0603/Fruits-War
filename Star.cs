using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] GameObject Ink1;
    [SerializeField] GameObject Ink2;
    [SerializeField] GameObject Ink3;

    private void Update()
    {
        if(Ink1 == null || Ink2 == null || Ink3 == null)
        {
            Ink1 = GameObject.Find("Ink1");
            Ink2 = GameObject.Find("Ink2");
            Ink3 = GameObject.Find("Ink3");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EliminateInk();
        }
    }

    public void EliminateInk() //âÊñ è„ÇÃÉCÉìÉNÇè¡ãé
    {
        if(Ink1 != null)
        {
            Ink1.SetActive(false);
        }
        else if (Ink2 != null)
        {
            Ink2.SetActive(false);
        }
        else if (Ink3 != null)
        {
            Ink3.SetActive(false);
        }
        Destroy(this.gameObject);
    }
}
