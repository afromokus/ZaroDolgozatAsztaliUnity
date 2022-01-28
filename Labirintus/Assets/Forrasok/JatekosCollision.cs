using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JatekosCollision : MonoBehaviour
{
    public GameObject joPasi;

    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;

    private Vector3 helyA = new Vector3();
    private Vector3 helyB = new Vector3();
    private Vector3 helyC = new Vector3();
    private Vector3 helyD = new Vector3();
    private Vector3 helyE = new Vector3();

    int idozito = 0;
    int varakozPasiIdo = 300;

    bool mehetElorePasi = false;
    private bool pasiPoz1 = false;
    private bool pasiPoz0 = true;
    bool joPasiAlszik = true;
    private int elalvasIdo = 400;

    private void FixedUpdate()
    {
        if (mehetElorePasi) 
        {
            joPasi.transform.Translate(0f,0f,0.03f);
            if (joPasi.transform.position.x > -41f)
            {
                mehetElorePasi = false;
                pasiPoz0 = false;
                pasiPoz1 = true;
            }
        }
        if (pasiPoz1)
        {
            if (idozito >= varakozPasiIdo) 
            {
                if (joPasi.transform.position.x > -44)
                {
                    joPasi.transform.Translate(0f, 0f, -0.03f);
                }
                else
                {
                    pasiPoz0 = true;
                    pasiPoz1 = false;
                    idozito = 0;
                }
            }
            else
            {
                idozito++;
            }
        }
        if (pasiPoz0 && !joPasiAlszik && !mehetElorePasi) 
        {
            if(idozito >= elalvasIdo) 
            {
                joPasiAlszik = true;
                A.SetActive(true);
                B.SetActive(true);
                C.SetActive(true);
                D.SetActive(true);
                E.SetActive(true);

                visszaAllitAlvasJelA();
                visszaAllitAlvasJelB();
                visszaAllitAlvasJelC();
                visszaAllitAlvasJelD();
                visszaAllitAlvasJelE();

                idozito = 0;
            }
            idozito++;
        }

        Debug.Log(idozito);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("alvasjel")) 
        {
            A.SetActive(false);
            B.SetActive(false);
            C.SetActive(false);
            D.SetActive(false);
            E.SetActive(false);

            mehetElorePasi = true;
            joPasiAlszik = false;
            idozito = 0;

        }
    }
    private void visszaAllitAlvasJelA()
    {
        helyA.Set(-41f, 1.6f, A.transform.position.z);
        A.transform.position = helyA;
    }
    private void visszaAllitAlvasJelB()
    {
        helyB.Set(-40f, 2.58f, B.transform.position.z);
        B.transform.position = helyB;
    }
    private void visszaAllitAlvasJelC()
    {
        helyC.Set(-39.85f, 2.2f, C.transform.position.z);
        C.transform.position = helyC;
    }
    private void visszaAllitAlvasJelD()
    {
        helyD.Set(-38f, 1.92f, D.transform.position.z);
        D.transform.position = helyD;
    }
    private void visszaAllitAlvasJelE()
    {
        helyE.Set(-38.8f, 1.76f, E.transform.position.z);
        E.transform.position = helyE;
    }
}
