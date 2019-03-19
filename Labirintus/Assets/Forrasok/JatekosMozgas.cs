using System.Collections.Generic;
using UnityEngine;
using Assets.Model;

public class JatekosMozgas : MonoBehaviour
{

    public Rigidbody jatekosTest;
    float eroElore = -200;
    float eroOldalra = 150;
    bool jatekosJobbra = false, jatekosBalra = false, jatekosElore = false, jatekosHatra = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        VektorSugar jatekosSugar = new VektorSugar(transform.position, new Vector3(5f, 1f, 0f));

        jatekosSugar.rajzolFigyelo();

        mozgasVizsgalat();

        if (jatekosElore) { jatekosTest.AddForce(eroElore * Time.deltaTime, 0, 0); }
        if (jatekosHatra) { jatekosTest.AddForce(-eroElore * Time.deltaTime, 0, 0); }
        if (jatekosJobbra) { jatekosTest.AddForce(0, 0, eroOldalra * Time.deltaTime); }
        if (jatekosBalra) { jatekosTest.AddForce(0, 0, -eroOldalra * Time.deltaTime); }

    }

    void mozgasVizsgalat()
    {
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            if (jatekosHatra)
            {
                jatekosHatra = false;
                System.Threading.Thread.Sleep(50);
            }
            else
            {
                jatekosElore = true;
            }
        }
        else if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            if (jatekosElore)
            {
                jatekosElore = false;
                System.Threading.Thread.Sleep(50);
            }
            else
            {
                jatekosHatra = true;
            }
        }
        else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            if (jatekosBalra)
            {
                jatekosBalra = false;
                System.Threading.Thread.Sleep(50);
            }
            else
            {
                jatekosJobbra = true;
            }
        }
        else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            if (jatekosJobbra)
            {
                jatekosJobbra = false;
                System.Threading.Thread.Sleep(50);
            }
            else
            {
                jatekosBalra = true;
            }
        }
    }

}
