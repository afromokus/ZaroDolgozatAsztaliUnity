using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JatekosCollision : MonoBehaviour
{
    public GameObject joPasi;
    public GameObject holgyKesztyuNelkul;
    public GameObject kesztyu;

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

    Vector3 vektorPasiMellett;
    Vector3 vektorSator;

    int idozito = 0;
    int varakozPasiIdo = 300;

    bool mehetElorePasi = false;
    //sátor elõtt
    private bool pasiPoz1 = false;
    //sátorban
    private bool pasiPoz0 = true;
    bool joPasiAlszik = true;
    private int elalvasIdo = 400;
    private bool pasiVarHolgyre = false;
    private bool pasiEsHolgyEgyütt = false;

    private void Start()
    {
        holgyKesztyuNelkul.SetActive(false);
        vektorSator = new Vector3(-44f,0f,-44.65f);
        kesztyu.SetActive(false);
    }

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
        if (pasiPoz1 && !pasiVarHolgyre)
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
                    if (!pasiEsHolgyEgyütt)
                    {
                        idozito = 0;
                    }
                }
            }
            else
            {
                idozito++;
            }
        }
        if (pasiPoz0 && !joPasiAlszik && !mehetElorePasi && !pasiEsHolgyEgyütt) 
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

        if (pasiPoz1 && holgyKesztyuNelkul.transform.position.x < -35 && holgyKesztyuNelkul.transform.position.z < -40 && !FoKod.HolgySzerelmesE)
        {
            FoKod.HolgySzerelmesE = true;
            holgyKesztyuNelkul.SetActive(true);
            kesztyu.SetActive(true);
            pasiVarHolgyre = true;
            vektorPasiMellett = new Vector3(joPasi.transform.position.x, joPasi.transform.position.y, joPasi.transform.position.z - 2);
        }

        if (pasiVarHolgyre) 
        {
            if (holgyKesztyuNelkul.transform.position.z > -46.4f && holgyKesztyuNelkul.transform.position.x > -40.8f)
            {
                holgyKesztyuNelkul.transform.LookAt(vektorPasiMellett);
                holgyKesztyuNelkul.transform.Translate(0, 0, Time.deltaTime * 1f);
            }
            else 
            {
                holgyKesztyuNelkul.transform.LookAt(vektorSator);
                pasiVarHolgyre = false;
                pasiEsHolgyEgyütt = true;
            }
        }

        if (pasiEsHolgyEgyütt && idozito >= varakozPasiIdo && holgyKesztyuNelkul.transform.position.x > -46f) 
        {
            holgyKesztyuNelkul.transform.Translate(0, 0, 0.03f);
        }

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
