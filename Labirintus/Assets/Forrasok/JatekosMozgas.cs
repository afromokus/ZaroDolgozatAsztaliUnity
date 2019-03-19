﻿using System.Collections.Generic;
using UnityEngine;
using Assets.Model;
using System;

public class JatekosMozgas : MonoBehaviour
{

    public Rigidbody jatekosTest;
    float eroElore = -1f;
    float eroOldalra = 0.8f;

    float sebesseg = 1f;
    float maxSebesseg = 3f;

    bool jatekosJobbra = false, jatekosBalra = false, jatekosElore = false, jatekosHatra = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mozgasVizsgalat();

        if (jatekosElore)
        {
            transform.Translate(-eroElore * Time.deltaTime * sebesseg, 0, 0);
            gyorsulas();
        }
        if (jatekosHatra)
        {
            transform.Translate(eroElore * Time.deltaTime * sebesseg, 0, 0);
            gyorsulas();
        }
        if (jatekosJobbra)
        {
            transform.Translate(0, 0, -eroOldalra * Time.deltaTime * sebesseg);
        }
        if (jatekosBalra)
        {
            transform.Translate(0, 0, eroOldalra * Time.deltaTime * sebesseg);
        }

    }

    private void gyorsulas()
    {
        if (sebesseg < maxSebesseg)
        {
            sebesseg += 0.01f;
            Debug.Log(sebesseg);
        }

    }

    void mozgasVizsgalat()
    {
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            if (jatekosHatra)
            {
                lassulas();

                if (sebesseg <= 1.5f && sebesseg > 1f)
                {
                    jatekosHatra = false;
                    System.Threading.Thread.Sleep(50);
                }
            }
            else
            {
                jatekosElore = true;
            }
        }
        else if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            if (jatekosElore)
            {
                lassulas();

                if (sebesseg <= 1.5f && sebesseg > 1f)
                {
                    jatekosElore = false;
                    System.Threading.Thread.Sleep(50);
                }
            }
            else
            {
                jatekosHatra = true;
            }
        }
        else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            if (jatekosBalra)
            {
                jatekosBalra = false;
                System.Threading.Thread.Sleep(50);
            }
            else
            {
                jatekosJobbra = true;
            }
        }
        else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            if (jatekosJobbra)
            {
                jatekosJobbra = false;
                System.Threading.Thread.Sleep(50);
            }
            else
            {
                jatekosBalra = true;
            }
        }
    }

    private void lassulas()
    {
        if (sebesseg > 1.5f)
        {
            sebesseg -= 0.1f;
        }

        if (sebesseg < 1f)
        {
            sebesseg = 1f;
        }

    }
}
