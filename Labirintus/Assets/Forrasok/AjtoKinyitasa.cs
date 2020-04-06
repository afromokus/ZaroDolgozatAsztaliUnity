using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FoKameraMozgas : MonoBehaviour
{

    public GameObject zartAjto;

    void ajtoNyitas()
    {
        if (zartAjto.active)
        {
            zartAjto.SetActive(false);
            zartAjto.GetComponent<Animation>().Play();
        }
    }

}
