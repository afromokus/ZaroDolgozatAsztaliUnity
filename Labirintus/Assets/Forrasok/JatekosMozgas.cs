using System.Collections.Generic;
using UnityEngine;
using Assets.Model;
using System;

public class JatekosMozgas : MonoBehaviour
{

    public Animator jatekosAnimator;

    public Rigidbody jatekosTest;
    public GameObject buzaWatch;

    public static bool JatekosMegutveE = false;

    float eroElore = 1f;
    float eroOldalra = 1.5f;

    public static float sebesseg = 1f;
    float normalTempo = 5f;
    float maxSebesseg;
    float a = 0.01f;

    bool jatekosUtkozikE = false;

    private bool jatekosElore = false, jatekosHatra = false;
    private float jatekosZ;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Hitbox")
        {
            JatekosMegutveE = true;
        }
    }

    /*private void OnCollisionEnter(Collision col)
    {
        if (col.collider.name == "buzaWatch")
        {
            buzaWatch.SetActive(false);
            Debug.Log("búza megütve");
        }
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((sebesseg < 1.5f && sebesseg > 1.4f) && jatekosAnimator.GetCurrentAnimatorStateInfo(0).IsName("Haladas") &&
                                                (jatekosAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 >= 0.98f))
        {
            jatekosAnimator.Play("Idle", 0);
            jatekosAnimator.speed = 0.6f;
        }

        /*if (!buzaWatch.active && !(jatekosTest.position.z > -43.6f && jatekosTest.position.z < -41.55f &&
                jatekosTest.position.x > -26.5f && jatekosTest.position.x < -23.15f))
        {
            buzaWatch.SetActive(true);
            Debug.Log("búza kijőve");
        }*/

        if (Cursor.visible == false && FoKod.jatekosMozoghatE)
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

        /*Physics.IgnoreCollision(jatekosTest.GetComponent<Collider>(), buzaWatch.GetComponent<Collider>(),
            false);*/

    }

    private void utkozes()
    {
        //Debug.Log(sebesseg);

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
            valtAnimSebesseg();
        }

    }

    private void valtAnimSebesseg()
    {
        if (sebesseg < 2)
        {
            jatekosAnimator.speed = sebesseg / 3;
        }
        else if (sebesseg > 5)
        {
            jatekosAnimator.speed = sebesseg / 8;
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

            if (jatekosAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                jatekosAnimator.Play("Haladas");
            }

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

        valtAnimSebesseg();

    }
}
