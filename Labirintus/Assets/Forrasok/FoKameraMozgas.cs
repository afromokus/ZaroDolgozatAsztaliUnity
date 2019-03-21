using System.Collections.Generic;
using UnityEngine;
using Assets.Model;

public class FoKameraMozgas : MonoBehaviour
{
    public Transform foKameraTransform;

    Vector3 eltolasKozeli;
    Vector3 eltolasFelul;
    enum Pozicio { alap, kozeli, felul }

    Pozicio kameraAllapot;

    VektorSugar figyeloKameraEloreX;
    VektorSugar kijovetelFigyelo;
    VektorSugar balraFigyelo;

    Vector3 balraFigyeloEltolas;

    float tolatasRadarVisszah = -1f;
    float rovidVektorHossz = 0.5f;
    private Vector3 eltolasAlap;

    private void Start()
    {
        eltolasAlap = new Vector3(5f, 1.5f, 0f);

        eltolasKozeli = new Vector3(2f, 1.5f, 0f);
        eltolasFelul = new Vector3(-2f, 1.5f, 0f);

        balraFigyeloEltolas = new Vector3(-0.5f, 0, 0);

        valtKameraAlaphelyzetbe();
        kameraAllapot = Pozicio.alap;

        figyeloKameraEloreX = new VektorSugar(transform.position, rovidVektorHossz + tolatasRadarVisszah);
        kijovetelFigyelo = new VektorSugar(transform.position, 5f);
        balraFigyelo = new VektorSugar(transform.position, new Vector3(0f, 0f, 2f));

        transform.position = transform.parent.position + eltolasAlap;

    }

    // Update is called once per frame
    void Update()
    {

        figyeloKameraEloreX.setSugarOrigin(transform.position);
        kijovetelFigyelo.setSugarOrigin(transform.position);
        balraFigyelo.setSugarOrigin(transform.position + balraFigyeloEltolas);

        if (kameraAllapot == Pozicio.kozeli && !kijovetelFigyelo.utkozikEX())
        {
            valtKameraAlaphelyzetbe();
            kameraAllapot = Pozicio.alap;
        }
        else if (kameraAllapot == Pozicio.alap && figyeloKameraEloreX.utkozikEX())
        {
            valtKameraKozeliNezet();
            kameraAllapot = Pozicio.kozeli;
        }
        else if (kameraAllapot == Pozicio.kozeli && figyeloKameraEloreX.utkozikEX())
        {
            transform.position = transform.position + eltolasFelul;
            transform.LookAt(transform.parent);
            kameraAllapot = Pozicio.felul;
        }


        figyeloKameraEloreX.rajzolFigyelo(Color.white);
        kijovetelFigyelo.rajzolFigyelo();
        balraFigyelo.rajzolFigyelo(Color.red);

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

}
