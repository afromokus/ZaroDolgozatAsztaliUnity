using UnityEngine;

public class CollisionAlvasjel : MonoBehaviour
{
    public GameObject alvasJel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "JatekosKocka")
        {
            FoKod.mehetEAlvasJel = false;
            alvasJel.SetActive(false);
        }
    }
}
