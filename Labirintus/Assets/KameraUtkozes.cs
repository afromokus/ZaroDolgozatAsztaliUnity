using UnityEngine;

public class KameraUtkozes : MonoBehaviour
{

    private void OnCollisionEnter(Collision utkozesInfo)
    {
        if (utkozesInfo.collider.name.Contains("Fal"))
        {
            Debug.Log("Ütközés fallal!");
        }
    }

}
