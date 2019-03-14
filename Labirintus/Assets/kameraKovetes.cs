using System.Collections.Generic;
using UnityEngine;

public class kameraKovetes : MonoBehaviour
{
    public Transform jatekosTranszformacio;
    public Rigidbody kameraTest;
    public Vector3 eltolas;

    public Vector3 nullVector;

    private void Start()
    {
        eltolas.x = 5;
        eltolas.y = 1.5f;
        eltolas.z = 0;

        nullVector = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = jatekosTranszformacio.position + eltolas;
    }

    private void OnCollisionEnter(Collision utkozesKamera)
    {
        if (utkozesKamera.collider.tag == "Fal")
        {
            kameraTest.freezeRotation = true;
        }
    }
}
