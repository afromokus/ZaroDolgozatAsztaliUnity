using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JatekosCollision : MonoBehaviour
{
    public GameObject joPasi;

    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;

    bool mehetElorePasi = false;

    private void FixedUpdate()
    {
        if (mehetElorePasi) 
        {
            joPasi.transform.Translate(0f,0f,0.03f);
            if (joPasi.transform.position.x > -41f) 
            {
                mehetElorePasi = false;
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("alvasjel")) 
        {
            A.SetActive(false);
            B.SetActive(false);
            C.SetActive(false);
            D.SetActive(false);
            E.SetActive(false);

            mehetElorePasi = true;
        }
    }
}
