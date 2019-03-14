using System.Collections.Generic;
using UnityEngine;

public class kameraKovetes : MonoBehaviour
{
    public Transform jatekosTranszformacio;
    public Rigidbody kameraTest;
    public Vector3 Eltolas;

    public Vector3 nullVector;
    private Vector3 utkozesUtaniEltolas;
    private Vector3 normalEltolas;

    int utkozesSzamlalo = 0;

    private void Start()
    {
        normalEltolas = new Vector3(5, 1.5f, 0);

        nullVector = new Vector3(0, 0, 0);

        utkozesUtaniEltolas = new Vector3(1.5f, 1.5f, 0);

        Eltolas = normalEltolas;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = jatekosTranszformacio.position + Eltolas;
        if (utkozesSzamlalo == 0)
        {
            Debug.Log("Kamera mehet vissza!");
        }
    }

    private void OnCollisionEnter(Collision utkozesKamera)
    {
        if (utkozesKamera.collider.tag == "Fal")
        {
            kameraTest.freezeRotation = true;
            Eltolas = utkozesUtaniEltolas;
            Debug.Log("Ütközés :\t" + utkozesSzamlalo);
            utkozesSzamlalo++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (utkozesSzamlalo > 0)
        {
            utkozesSzamlalo--;
        }
    }

}
