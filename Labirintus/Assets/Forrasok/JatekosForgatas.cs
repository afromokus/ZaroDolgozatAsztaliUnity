using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JatekosForgatas : MonoBehaviour {

    float forgatasiSebesseg = 5f;
    Vector3 forgatasVektor;

    // Use this for initialization
    void Start () {
        forgatasVektor = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (Cursor.visible == false && FoKod.jatekosMozoghatE)
        {
            forgatasVektor.y += forgatasiSebesseg * Input.GetAxis("Mouse X");

            transform.eulerAngles = forgatasVektor;
        }

	}
}
