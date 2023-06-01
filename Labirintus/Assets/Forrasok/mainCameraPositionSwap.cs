using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCameraPositionSwap : MonoBehaviour
{
    private Transform behindHitTr;

    private Transform mainCameraTr;
    private Transform jatekosTr;
    private bool simaNezetE = true;
    bool utkozneE = false;
    int idozito = 0;
    int hatarIdozito = 800;
    Vector3 kameraSimaPozicio = new Vector3();
    Vector3 kameraTopPozicio;
    Vector3 iranyJatekosfele;

    // Start is called before the first frame update
    void Start()
    {
        iranyJatekosfele = new Vector3(0f, 0f, 0f);
        mainCameraTr = this.transform.parent.gameObject.transform;
        behindHitTr = mainCameraTr.parent.gameObject.transform.GetChild(4);
        jatekosTr = mainCameraTr.parent.gameObject.transform;
        Vector3 kameraSimaPozicio = mainCameraTr.position;
        mainCameraTr.position = kameraSimaPozicio;
    }

    // Update is called once per frame
    void Update()
    {
        if(simaNezetE == false) 
        {
            if (idozito >= hatarIdozito)
            {
                valtSima();
            }
        }

        Physics.Linecast(behindHitTr.position, jatekosTr.position, out RaycastHit hitInfo);
        if (hitInfo.collider.name == "JatekosKocka")
        {
            Debug.DrawLine(behindHitTr.position, jatekosTr.position, Color.blue);
            utkozneE = false;

            if (idozito <= hatarIdozito)
            {
                idozito++;
            }
        }
        else
        {
            Debug.DrawLine(behindHitTr.position, jatekosTr.position, Color.red);
            utkozneE = true;
        }
        Debug.Log(idozito);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("A kamera most nem lát");
        if (simaNezetE && idozito >= hatarIdozito)
        {
            valtTop();
        }
    }

    void valtSima()
    {
        mainCameraTr.localRotation = Quaternion.Euler(0f, -90f, 0f);
        //mainCameraTr.position = kameraSimaPozicio;
        simaNezetE = true;
    }

    void valtTop()
    {
        mainCameraTr.Translate(0f, 0.8f, 2.2f);
        mainCameraTr.localRotation = Quaternion.Euler(60f, -90f, 0f);
        idozito = 0;
        simaNezetE = false;
    }

}
