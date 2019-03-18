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

    TavolsagFigyelo tolatasFigyeles;
    TavolsagFigyelo eloreFigyeles;

    float rovidVektorhossz = 0.5f;
    float hosszuVektorhossz = 0f;

    private void Start()
    {
        Eltolas = new Vector3(5, 1.5f, 0);
        hosszuVektorhossz = Eltolas.x;

        Cursor.visible = false;

        tolatasFigyeles = new TavolsagFigyelo(transform.position, rovidVektorhossz);
        eloreFigyeles = new TavolsagFigyelo(transform.position, -hosszuVektorhossz);
        kameraAllapot = Pozicio.alap;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = jatekosTranszformacio.position + Eltolas;

        tolatasFigyeles.setSugarOrigin(kameraTest.position);
        eloreFigyeles.setSugarOrigin(kameraTest.position);

        tolatasFigyeles.rajzolFigyelo(Color.white);
        eloreFigyeles.rajzolFigyelo();

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
