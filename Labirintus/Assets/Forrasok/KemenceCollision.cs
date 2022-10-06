using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KemenceCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        if (FoKod.lerakomod)
        {
            this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (FoKod.lerakomod)
        {
            this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
