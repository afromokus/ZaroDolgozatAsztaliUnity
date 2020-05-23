using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JatekosElet : MonoBehaviour
{

    float hp = 100;
    Vector3 eletVonalMeret;
    public Transform jatekosKockaTranszform;

    private void Start()
    {
        eletVonalMeret = new Vector3(1, 1, 1);
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mergezoGazSerules();
        utesSerulesEll();
    }

    private void utesSerulesEll()
    {
        if (hp >= 0)
        {
            if (JatekosMozgas.JatekosMegutveE)
            {
                hp -= 70;
                eletVonalMeret.x = hp / 100;
                transform.localScale = eletVonalMeret;
                JatekosMozgas.JatekosMegutveE = false;
            }
        }
    }

    void mergezoGazSerules()
    {
        if (hp >= 0)
        {
            if (FoKod.serulEJatekos)
            {
                eletKezeles(true);
            }
            else
            {
                eletKezeles(false);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void eletKezeles(bool serulE)
    {
        if (hp >= 0)
        {
            if (serulE)
            {
                hp -= 1.5f;
            }
            else if (hp < 100)
            {
                hp += 0.2f;
            }

            if (hp > 100)
            {
                hp = 100;
            }

            eletVonalMeret.x = hp / 100;
            transform.localScale = eletVonalMeret;
        }
    }

}
