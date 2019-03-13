using System.Collections.Generic;
using UnityEngine;

public class kameraKovetes : MonoBehaviour
{
    public Transform jatekosTranszformacio;
    public Vector3 eltolas;


    // Update is called once per frame
    void Update()
    {
        transform.position = jatekosTranszformacio.position + eltolas;
    }
}
