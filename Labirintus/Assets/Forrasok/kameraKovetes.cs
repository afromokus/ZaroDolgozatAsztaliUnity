using System.Collections.Generic;
using UnityEngine;

public class kameraKovetes : MonoBehaviour
{
    public Transform jatekosTranszformacio;
    public Rigidbody kameraTest;
    public Vector3 Eltolas;
    Ray tolatasFigyeles;
    RaycastHit elkerulObj;
    enum Pozicio { alap, kozeli, felul }
    Pozicio kameraAllapot;
    Vector3 figyeloVektor;

    private void Start()
    {
        kameraTest.freezeRotation = true;

        Eltolas = new Vector3(5, 1.5f, 0);
        figyeloVektor = new Vector3(0.2f, 0, 0);

        Cursor.visible = false;

        tolatasFigyeles = new Ray(transform.position, figyeloVektor);
        kameraAllapot = Pozicio.alap;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = jatekosTranszformacio.position + Eltolas;
        tolatasFigyeles.origin = kameraTest.position;

        if (Physics.Raycast(tolatasFigyeles, 0.2f) && kameraAllapot == Pozicio.alap)
        {
            Debug.Log("Nekiment!");
            Eltolas.x = 2f;
            figyeloVektor.x = 5f;
            kameraAllapot = Pozicio.kozeli;
        }
        else if (!Physics.Raycast(tolatasFigyeles, 5f) && kameraAllapot == Pozicio.kozeli)
        {
            Debug.Log("Kijöhet!");
            Eltolas.x = 5f;
            figyeloVektor.x = 0.2f;
            kameraAllapot = Pozicio.alap;
        }

        Debug.DrawLine(tolatasFigyeles.origin, tolatasFigyeles.origin + figyeloVektor, Color.red);

    }

}
