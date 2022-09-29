using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KemenceCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
