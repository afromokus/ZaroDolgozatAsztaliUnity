using Assets.Model;
using System.Collections.Generic;
using UnityEngine;


public class kameraKovetes : MonoBehaviour
{
    public Transform jatekosTranszformacio;
    public Transform kameraTest;
    public Vector3 Eltolas;
    enum Pozicio { alap, kozeli, felul }
    Pozicio kameraAllapot;
    Vector3 figyeloVektorHatra;

    TavolsagFigyelo tolatasFigyeles;

    float rovidVektorhossz = 0.5f;
    float hosszuVektorhossz;

    private void Start()
    {
        Eltolas = new Vector3(5, 1.5f, 0);
        hosszuVektorhossz = Eltolas.x;
        figyeloVektorHatra = new Vector3(rovidVektorhossz, 0, 0);

        Cursor.visible = false;

        tolatasFigyeles = new TavolsagFigyelo(transform.position, figyeloVektorHatra);
        kameraAllapot = Pozicio.alap;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = jatekosTranszformacio.position + Eltolas;
        tolatasFigyeles.setSugarOrigin(kameraTest.position);

        if (kameraAllapot == Pozicio.alap && tolatasFigyeles.utkozikE())
        {
            kameraValtAlapPoz();
        }
        else if (kameraAllapot == Pozicio.kozeli && !tolatasFigyeles.utkozikE())
        {
            kameraValtKozeli();
        }

        tolatasFigyeles.rajzolFigyelo();

    }

    void kameraValtAlapPoz()
    {
        Eltolas.x = 2f;
        tolatasFigyeles.setSugarHossz(hosszuVektorhossz);
        kameraAllapot = Pozicio.kozeli;
    }

    void kameraValtKozeli()
    {
        Eltolas.x = 5f;
        tolatasFigyeles.setSugarHossz(rovidVektorhossz);
        kameraAllapot = Pozicio.alap;
    }

}
