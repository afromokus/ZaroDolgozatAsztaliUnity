using System.Collections.Generic;
using UnityEngine;
using Assets.Model;
using System;

public class JatekosMozgas : MonoBehaviour
{

    public Rigidbody jatekosTest;
    float eroElore = 1f;
    float eroOldalra = 1.5f;

    float sebesseg = 1f;
    float normalTempo = 5f;
    float maxSebesseg;
    float a = 0.01f;

    bool jatekosUtkozikE = false;

    bool jatekosElore = false, jatekosHatra = false;

    private void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Cursor.visible == false)
        {
            mozgasVizsgalat();

            if (jatekosElore && !jatekosUtkozikE)
            {
                transform.Translate(-eroElore * Time.deltaTime * sebesseg, 0, 0);
                gyorsulas();
            }
            if (jatekosHatra && !jatekosUtkozikE)
            {
                transform.Translate(eroElore * Time.deltaTime * sebesseg, 0, 0);
                gyorsulas();
            }
        }
        else
        {
            lassulas();
            jatekosElore = false;
            jatekosHatra = false;
        }

    }

    private void utkozes()
    {
        Debug.Log(sebesseg);

        if (sebesseg > 1f)
        {
            sebesseg -= 2f;
        }

        if (sebesseg <= 1.5f)
        {
            jatekosElore = false;
            jatekosHatra = false;
        }
    }

    private void gyorsulas()
    {
        if (sebesseg < maxSebesseg)
        {
            sebesseg += a;
        }

    }

    void mozgasVizsgalat()
    {

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            maxSebesseg = 10f;
            a = 0.02f;
        }
        else
        {
            a = 0.01f;
            maxSebesseg = normalTempo;
            if (sebesseg > normalTempo)
            {
                sebesseg = normalTempo;
            }
        }

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
            transform.Translate(0, 0, eroOldalra * Time.deltaTime * sebesseg);
        }
        else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(0, 0, -eroOldalra * Time.deltaTime * sebesseg);
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
