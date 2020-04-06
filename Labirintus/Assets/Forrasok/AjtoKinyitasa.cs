using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FoKod : MonoBehaviour
{

    public GameObject zartAjto;

    void ajtoNyitas()
    {
        zartAjto.SetActive(false);
        ajtoAnimacio.SetActive(true);
    }

}
