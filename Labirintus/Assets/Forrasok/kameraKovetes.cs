using Assets.Model;
using System;
using System.Collections.Generic;
using UnityEngine;


public class kameraKovetes : MonoBehaviour
{
    public Transform jatekosTranszformacio;
    public GameObject foKamera;
    public GameObject valtoKamera;
    public Vector3 Eltolas;
    enum Pozicio { alap, kozeli, felul }
    Pozicio kameraAllapot;

    VektorSugar tolatasFigyeles;
    VektorSugar eloreFigyeles;

    float rovidVektorhossz = 0.5f;
    float hosszuVektorhossz = 0f;
    float tolatasRadarVisszah = -2f;

    float normalKameraMagassag = 1.5f;

    private void Start()
    {
        Eltolas = new Vector3(5f, normalKameraMagassag, 0);
        hosszuVektorhossz = Eltolas.x;

        Cursor.visible = false;

        foKamera.SetActive(true);

        tolatasFigyeles = new VektorSugar(transform.position, 5f);
        eloreFigyeles = new VektorSugar(transform.position, -hosszuVektorhossz);
        kameraAllapot = Pozicio.alap;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("c") && transform.position.y + Input.GetAxis("Mouse Y") < 9f && transform.position.y + Input.GetAxis("Mouse Y") > 1.2f)
        {
            Eltolas.Set(Eltolas.x, Eltolas.y + Input.GetAxis("Mouse Y"), Eltolas.z);
        }
        else if (Input.GetKey("r"))
        {
            Eltolas.Set(5f, normalKameraMagassag, 0);
        }


        transform.position = jatekosTranszformacio.position + Eltolas;
        tolatasFigyeles.setSugarOrigin(valtoKamera.transform.position, tolatasRadarVisszah);
        eloreFigyeles.setSugarOrigin(valtoKamera.transform.position);

        if (kameraAllapot == Pozicio.alap && tolatasFigyeles.utkozikEX())
        {
            kameraValtKozeli();
        }
        else if (kameraAllapot == Pozicio.kozeli && !tolatasFigyeles.utkozikEX())
        {
            kameraValtAlapPoz();
        }
        else if (eloreFigyeles.utkozikEX())
        {
            kameraValtFelul();
        }
        else if (!tolatasFigyeles.utkozikEX() && kameraAllapot == Pozicio.felul)
        {
            kameraValtKozeli();
        }


        tolatasFigyeles.rajzolFigyelo(Color.white);
        eloreFigyeles.rajzolFigyelo();

    }

    private void kameraValtFelul()
    {
        Eltolas.y = 5f;
        Eltolas.x = 0.5f;
        tolatasFigyeles.setSugarHosszX(rovidVektorhossz - tolatasRadarVisszah);
        transform.rotation = Quaternion.Euler(60f, -90f, 0);
        kameraAllapot = Pozicio.felul;
    }

    void kameraValtAlapPoz()
    {
        Eltolas.y = normalKameraMagassag;
        Eltolas.x = 5f;
        tolatasFigyeles.setSugarHosszX(rovidVektorhossz -tolatasRadarVisszah);
        transform.rotation = Quaternion.Euler(0, -90f, 0);
        kameraAllapot = Pozicio.alap;
    }

    void kameraValtKozeli()
    {
        Eltolas.y = normalKameraMagassag;
        Eltolas.x = 2f;
        tolatasFigyeles.setSugarHosszX(hosszuVektorhossz - tolatasRadarVisszah);
        transform.rotation = Quaternion.Euler(0, -90f, 0);
        kameraAllapot = Pozicio.kozeli;
    }

}
