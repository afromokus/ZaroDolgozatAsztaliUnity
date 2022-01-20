using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JatekosCollision : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("alvasjel")) 
        {
            A.SetActive(false);
            B.SetActive(false);
            C.SetActive(false);
            D.SetActive(false);
            E.SetActive(false);
        }
    }
}
