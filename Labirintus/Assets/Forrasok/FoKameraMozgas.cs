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
        eltolasAlap = new Vector3(5f, 1.5f, 0f);

        eltolasKozeli = new Vector3(2f, 1.5f, 0f);
        eltolasFelul = new Vector3(0f, 3f, 0f);

        balraFigyeloEltolas = new Vector3(-0.5f, 0, 0);

        valtKameraAlaphelyzetbe();
        kameraAllapot = Pozicio.alap;

        figyeloKameraEloreX = new VektorSugar(transform.position, -2f);
        kijovetelFigyelo = new VektorSugar(transform.position, 7f);

        figyeloKameraEloreXTukrozott = new VektorSugar(transform.position, 2f);
        kijovetelFigyeloTukrozott = new VektorSugar(transform.position, -7f);

        transform.position = transform.parent.position + eltolasAlap;

    }

    // Update is called once per frame
    void Update()
    {        
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
            figyeloKameraEloreX.setSugarOrigin(transform.position);
            kijovetelFigyelo.setSugarOrigin(transform.position, -2f);
                
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
        else if (figyeloAllapot == VektorSugarAllapot.tukrozott)
        {
            figyeloKameraEloreXTukrozott.setSugarOrigin(transform.position);
            kijovetelFigyeloTukrozott.setSugarOrigin(transform.position, 2f);

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
    }

    void valtKameraKozeliNezet()
    {
        transform.localPosition = eltolasKozeli;
        transform.LookAt(transform.parent);
    }

    void valtKameraFelulNezet()
    {
        transform.localPosition = eltolasFelul;
        transform.LookAt(transform.parent);
    }

}
