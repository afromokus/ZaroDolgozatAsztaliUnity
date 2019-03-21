using System.Collections.Generic;
using UnityEngine;
using Assets.Model;

public class FoKameraMozgas : MonoBehaviour
{
    public Transform foKameraTransform;

    enum Pozicio { alap, kozeli, felul}
    enum VektorSugarAllapot { normal, tukrozott}

    VektorSugarAllapot figyeloAllapot;

    Pozicio kameraAllapot;

    VektorSugar figyeloKameraEloreX;
    VektorSugar kijovetelFigyelo;


    VektorSugar figyeloKameraEloreXTukrozott;
    VektorSugar kijovetelFigyeloTukrozott;

    Vector3 balraFigyeloEltolas;

    Vector3 eltolasKozeli;
    Vector3 eltolasFelul;
    private Vector3 eltolasAlap;

    private void Start()
    {
        eltolasAlap = new Vector3(2.5f, 2.0f, 0f);
        eltolasKozeli = new Vector3(2f, 2.5f, 0f);
        eltolasFelul = new Vector3(0f, 3f, 0f);

        balraFigyeloEltolas = new Vector3(-0.5f, 0, 0);

        valtKameraAlaphelyzetbe();
        kameraAllapot = Pozicio.alap;

        figyeloKameraEloreX = new VektorSugar(transform.position, -2f);
        kijovetelFigyelo = new VektorSugar(transform.position, 7f);

        figyeloKameraEloreXTukrozott = new VektorSugar(transform.position, 2f);
        kijovetelFigyeloTukrozott = new VektorSugar(transform.position, -7f);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        figyeloKameraEloreX.setSugarOrigin(transform.position);
        kijovetelFigyelo.setSugarOrigin(transform.position, -2f);

        figyeloKameraEloreXTukrozott.setSugarOrigin(transform.position);
        kijovetelFigyeloTukrozott.setSugarOrigin(transform.position, 2f);
        if (transform.parent.localRotation.y > 0.5f)
        {
            figyeloAllapot = VektorSugarAllapot.tukrozott;
        }
        else
        {
            figyeloAllapot = VektorSugarAllapot.normal;
        }

        if (figyeloAllapot == VektorSugarAllapot.normal)
        {
                
            if (kameraAllapot == Pozicio.kozeli && !kijovetelFigyelo.utkozikEX() && !figyeloKameraEloreX.utkozikEX())
            {
                valtKameraAlaphelyzetbe();
                kameraAllapot = Pozicio.alap;
            }
            else if ((kameraAllapot == Pozicio.alap && figyeloKameraEloreX.utkozikEX()) ||
                kameraAllapot == Pozicio.felul && !kijovetelFigyelo.utkozikEX())
            {
                valtKameraKozeliNezet();
                kameraAllapot = Pozicio.kozeli;
            }
            else if (kameraAllapot == Pozicio.kozeli && figyeloKameraEloreX.utkozikEX())
            {
                valtKameraFelulNezet();
                kameraAllapot = Pozicio.felul;
            }
        }

        if (figyeloAllapot == VektorSugarAllapot.tukrozott)
        {

            if (kameraAllapot == Pozicio.kozeli && !kijovetelFigyeloTukrozott.utkozikEX() && !figyeloKameraEloreXTukrozott.utkozikEX())
            {
                valtKameraAlaphelyzetbe();
                kameraAllapot = Pozicio.alap;
            }
            else if ((kameraAllapot == Pozicio.alap && figyeloKameraEloreXTukrozott.utkozikEX()) ||
                kameraAllapot == Pozicio.felul && !kijovetelFigyeloTukrozott.utkozikEX())
            {
                valtKameraKozeliNezet();
                kameraAllapot = Pozicio.kozeli;
            }
            else if (kameraAllapot == Pozicio.kozeli && figyeloKameraEloreXTukrozott.utkozikEX())
            {
                valtKameraFelulNezet();
                kameraAllapot = Pozicio.felul;
            }
        }


        figyeloKameraEloreXTukrozott.rajzolFigyelo(Color.white);
        kijovetelFigyeloTukrozott.rajzolFigyelo();

    }

    void valtKameraAlaphelyzetbe()
    {
        transform.localPosition = eltolasAlap;
        transform.LookAt(transform.parent);
        transform.localRotation = Quaternion.Euler(0f, 270f, 0f);
    }

    void valtKameraKozeliNezet()
    {
        transform.localPosition = eltolasKozeli;
        transform.LookAt(transform.parent);
        transform.localRotation = Quaternion.Euler(0f, 270f, 0f);
    }

    void valtKameraFelulNezet()
    {
        transform.localPosition = eltolasFelul;
        transform.LookAt(transform.parent);
        transform.localRotation = Quaternion.Euler(90f, 270f, 0f);
    }

}
