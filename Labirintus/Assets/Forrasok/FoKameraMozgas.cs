using System.Collections.Generic;
using UnityEngine;
using Assets.Model;

public class FoKameraMozgas : MonoBehaviour
{
    Vector3 eltolasAlap;
    Vector3 eltolasKozeli;
    enum Pozicio { alap, kozeli, felul}

    Pozicio kameraAllapot;

    VektorSugar tolatasFigyelo;
    VektorSugar jobbraFigyelo;

    float tolatasRadarVisszah = -1f;
    float rovidVektorHossz = 0.5f;



    private void Start()
    {
        eltolasAlap = new Vector3(5f, 1.5f, 0);
        kameraAllapot = Pozicio.kozeli;

        tolatasFigyelo = new VektorSugar(transform.position, rovidVektorHossz + tolatasRadarVisszah);
        jobbraFigyelo = new VektorSugar(transform.position, new Vector3(0f, 0f, rovidVektorHossz + tolatasRadarVisszah));

    }

    // Update is called once per frame
    void Update()
    {
        tolatasFigyelo.setSugarOrigin(transform.position);
        jobbraFigyelo.setSugarOrigin(transform.position);

        if (kameraAllapot == Pozicio.kozeli)
        {
            transform.position = transform.parent.position + eltolasAlap;
            kameraAllapot = Pozicio.alap;
        }

        if (tolatasFigyelo.utkozikE())
        {
            Debug.Log("Hátul egy fal!");
        }
        else if (jobbraFigyelo.utkozikE())
        {
            Debug.Log("Jobbra egy fal!");
        }

        tolatasFigyelo.rajzolFigyelo(Color.white);
        jobbraFigyelo.rajzolFigyelo(Color.red);

    }
}
