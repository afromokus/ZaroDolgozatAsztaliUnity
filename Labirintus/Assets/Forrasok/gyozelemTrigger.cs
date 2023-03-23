using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyozelemTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "JatekosKocka") 
        {
            StartSequenceGyozelem();
        }
    }

    private void StartSequenceGyozelem()
    {
        Debug.Log("YaaaaY!");
        Kilepes();
    }

    public void Kilepes()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
