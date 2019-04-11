using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JatekosElet : MonoBehaviour
{

    int hp = 100;
    Vector3 eletVonalMeret;
    public Transform jatekosKockaTranszform;

    private void Start()
    {
        eletVonalMeret = new Vector3(1, 1, 1);
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp >= 0)
        {
            if (jatekosKockaTranszform.position.x < 27 && jatekosKockaTranszform.position.x > -42 && jatekosKockaTranszform.position.z > -25 && jatekosKockaTranszform.position.z < -11)
            {
                hp--;
                Debug.Log(hp);
            }

            eletVonalMeret.x = hp / 100;
            transform.localScale = eletVonalMeret;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

}
