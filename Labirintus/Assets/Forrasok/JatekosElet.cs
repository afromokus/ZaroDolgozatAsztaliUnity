using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JatekosElet : MonoBehaviour
{

    float hp = 100;
    Vector3 eletVonalMeret;
    public Transform jatekosKockaTranszform;

    private void Start()
    {
        eletVonalMeret = new Vector3(1, 1, 1);
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!FoKod.kutyaKovet)
        {
            mergezoGazSerules();
        }
    }

    void mergezoGazSerules()
    {
        if (hp >= 0)
        {
            if (jatekosKockaTranszform.position.x < 27 && jatekosKockaTranszform.position.x > -42 && jatekosKockaTranszform.position.z > -25 && jatekosKockaTranszform.position.z < -11)
            {
                eletKezeles(true);
            }
            else
            {
                eletKezeles(false);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void eletKezeles(bool serulE)
    {
        if (serulE)
        {
            hp -= 1.5f;
        }
        else if(hp < 100)
        {
            hp += 0.5f;
        }

        if (hp > 100)
        {
            hp = 100;
        }

        eletVonalMeret.x = hp / 100;
        transform.localScale = eletVonalMeret;
    }

}
