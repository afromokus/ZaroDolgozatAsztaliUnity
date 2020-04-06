
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FoKameraMozgas : MonoBehaviour
{

    int pasiSzamlalo = 0;
    Vector3 kint = new Vector3(-42f, 0f, -44.57f);
    Vector3 bent = new Vector3(-44f, 0f, -44.57f);
    

    private void pasiMozog()
    {
        if (pasiKijonE)
        {
            pasi.transform.SetPositionAndRotation(kint, pasi.transform.rotation);
            pasiKijonE = false;
        }
        else if(pasi.transform.position.x == -42f)
        {
            pasiSzamlalo++;
            if (pasiSzamlalo == 300)
            {
                pasi.transform.SetPositionAndRotation(bent, pasi.transform.rotation);
            }
        }
    }

}

