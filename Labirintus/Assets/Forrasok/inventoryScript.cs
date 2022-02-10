using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryScript : MonoBehaviour
{

    public Canvas InventoryCanv;

    // Start is called before the first frame update
    void Start()
    {
        InventoryCanv.enabled = true;//false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i")) 
        {
            if (InventoryCanv.enabled)
            {
                InventoryCanv.enabled = false;
            }
            else 
            {
                InventoryCanv.enabled = true;
            }
        }
    }
}
