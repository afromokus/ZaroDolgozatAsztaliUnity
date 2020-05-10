using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KepHalvanyulas : MonoBehaviour
{

    public SpriteRenderer kep;
    public Transform kutyaTransform;

    float lathatosag = 0f;
    bool latszodjonE = false;

    // Start is called before the first frame update
    void Start()
    {
        kep.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(Vector3.Distance(this.transform.position, kutyaTransform.position));
        if (Vector3.Distance(this.transform.position, kutyaTransform.position) < 5)
        {
            latszodjonE = true;
        }

        if (latszodjonE)
        {
            elojovetel();
        }
        else
        {
            eltunes();
        }
    }

    void eltunes()
    {
        if (lathatosag > 0f)
        {
            lathatosag -= 0.02f;
        }

        kep.color = new Color(1f, 1f, 1f, lathatosag);
    }

    void elojovetel()
    {
        if (lathatosag < 1f)
        {
            lathatosag += 0.007f;
        }
        else
        {
            if (Vector3.Distance(this.transform.position, kutyaTransform.position) > 5)
            {
                latszodjonE = false;
            }
        }

        kep.color = new Color(1f, 1f, 1f, lathatosag);
    }

}
