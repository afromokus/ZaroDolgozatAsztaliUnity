﻿using Assets.Model;
using System;
using System.Collections.Generic;
using UnityEngine;


public class kameraKovetes : MonoBehaviour
{
    public Transform jatekosTranszformacio;
    public Transform kameraTest;
    public Vector3 Eltolas;
    enum Pozicio { alap, kozeli, felul }
    Pozicio kameraAllapot;

    TavolsagFigyelo tolatasFigyeles;
    TavolsagFigyelo eloreFigyeles;

    float rovidVektorhossz = 0.5f;
    float hosszuVektorhossz = 0f;
    float tolatasRadarVisszah = -2f;

    float normalKameraMagassag = 1.5f;

    private void Start()
    {
        Eltolas = new Vector3(5f, normalKameraMagassag, 0);
        hosszuVektorhossz = Eltolas.x;

        Cursor.visible = false;

        tolatasFigyeles = new TavolsagFigyelo(transform.position, rovidVektorhossz - tolatasRadarVisszah);
        eloreFigyeles = new TavolsagFigyelo(transform.position, -hosszuVektorhossz);
        kameraAllapot = Pozicio.alap;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = jatekosTranszformacio.position + Eltolas;

        tolatasFigyeles.setSugarOrigin(kameraTest.position, tolatasRadarVisszah);
        eloreFigyeles.setSugarOrigin(kameraTest.position);

        if (kameraAllapot == Pozicio.alap && tolatasFigyeles.utkozikE())
        {
            kameraValtKozeli();
        }
        else if (kameraAllapot == Pozicio.kozeli && !tolatasFigyeles.utkozikE())
        {
            kameraValtAlapPoz();
        }
        else if (eloreFigyeles.utkozikE())
        {
            kameraValtFelul();
        }
        else if (!tolatasFigyeles.utkozikE() && kameraAllapot == Pozicio.felul)
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
        tolatasFigyeles.setSugarHossz(rovidVektorhossz - tolatasRadarVisszah);
        transform.rotation = Quaternion.Euler(60f, -90f, 0);
        kameraAllapot = Pozicio.felul;
    }

    void kameraValtAlapPoz()
    {
        Eltolas.y = normalKameraMagassag;
        Eltolas.x = 5f;
        tolatasFigyeles.setSugarHossz(rovidVektorhossz -tolatasRadarVisszah);
        transform.rotation = Quaternion.Euler(0, -90f, 0);
        kameraAllapot = Pozicio.alap;
    }

    void kameraValtKozeli()
    {
        Eltolas.y = normalKameraMagassag;
        Eltolas.x = 2f;
        tolatasFigyeles.setSugarHossz(hosszuVektorhossz - tolatasRadarVisszah);
        transform.rotation = Quaternion.Euler(0, -90f, 0);
        kameraAllapot = Pozicio.kozeli;
    }

}
