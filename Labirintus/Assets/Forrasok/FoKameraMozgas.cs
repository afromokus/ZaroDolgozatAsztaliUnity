using System.Collections.Generic;
using UnityEngine;
using Assets.Model;

public class FoKameraMozgas : MonoBehaviour
{
    Vector3 eltolasAlap;
    Vector3 eltolasKozeli;
    Vector3 eltolasFelul;
    enum Pozicio { alap, kozeli, felul}

    Pozicio kameraAllapot;

    VektorSugar figyeloKameraEloreX;
    VektorSugar jobbraFigyelo;

    Vector3 forgatasNormal;
    Vector3 forgatasLefele;

    float tolatasRadarVisszah = -1f;
    float rovidVektorHossz = 0.5f;



    private void Start()
    {
        eltolasAlap = new Vector3(5f, 1.5f, 0f);
        eltolasKozeli = new Vector3(-3f, 0f, 0f);
        eltolasFelul = new Vector3(-2f, 1.5f, 0f);

        forgatasNormal = new Vector3(0f, -90f, 0f);
        forgatasLefele = new Vector3(80f, -90f, 0f);

        kameraAllapot = Pozicio.alap;

        figyeloKameraEloreX = new VektorSugar(transform.position, rovidVektorHossz + tolatasRadarVisszah);
        jobbraFigyelo = new VektorSugar(transform.position, new Vector3(0f, 0f, 2f));
        
        transform.position = transform.parent.position + eltolasAlap;
        transform.eulerAngles = forgatasNormal;

    }

    // Update is called once per frame
    void Update()
    {
        figyeloKameraEloreX.setSugarOrigin(transform.position);
        jobbraFigyelo.setSugarOrigin(transform.position);

        if (figyeloKameraEloreX.utkozikEX())
        {
            Debug.Log("ütközik");
        }

        if (jobbraFigyelo.utkozikE(2f))
        {
            Debug.Log("ütközik oldalt (jobbra)");
        }

        if (kameraAllapot == Pozicio.alap && figyeloKameraEloreX.utkozikEX())
        {
            transform.position = transform.position + eltolasKozeli;
            transform.eulerAngles = forgatasNormal;
            kameraAllapot = Pozicio.kozeli;
        }
        else if (kameraAllapot == Pozicio.kozeli && figyeloKameraEloreX.utkozikEX())
        {
            transform.position = transform.position + eltolasFelul;
            transform.eulerAngles = forgatasLefele;
            kameraAllapot = Pozicio.felul;
        }

        figyeloKameraEloreX.rajzolFigyelo(Color.white);
        jobbraFigyelo.rajzolFigyelo(Color.red);

    }
}
