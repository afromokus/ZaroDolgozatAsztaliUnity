using System.Collections.Generic;
using UnityEngine;
using Assets.Model;
using UnityEngine.UI;

public class FoKameraMozgas : MonoBehaviour
{
    public Transform foKameraTransform;
    public Camera kameraFpLatas;
    public GameObject kameraFpMaga;
    public Text targyakSzovege;

    public GameObject csontParent;

    bool felvehetoCsontraNez = false;

    enum Pozicio { alap, kozeli, felul }
    enum VektorSugarAllapot { normal, tukrozott }

    Ray ray;

    VektorSugarAllapot figyeloAllapot;

    Pozicio kameraAllapot;

    VektorSugar figyeloKameraEloreX;
    VektorSugar kijovetelFigyelo;


    VektorSugar figyeloKameraEloreXTukrozott;
    VektorSugar kijovetelFigyeloTukrozott;

    Vector3 balraFigyeloEltolas;

    Vector3 eltolasKozeli;
    Vector3 eltolasFelul;
    private Vector3 eltolasAlap;

    List<string> karakterTulajdonok;

    private void Start()
    {
        targyakSzovege.text = "";

        karakterTulajdonok = new List<string>();

        kameraFpMaga.SetActive(false);
        ray = kameraFpLatas.ScreenPointToRay(Input.mousePosition);

        eltolasAlap = new Vector3(2.5f, 0.6f, 0f);
        eltolasKozeli = new Vector3(2f, 0.71f, 0f);
        eltolasFelul = new Vector3(0f, 1f, 0f);

        balraFigyeloEltolas = new Vector3(-0.5f, 0, 0);

        valtKameraAlaphelyzetbe();
        kameraAllapot = Pozicio.alap;

        figyeloKameraEloreX = new VektorSugar(transform.position, -2f);
        kijovetelFigyelo = new VektorSugar(transform.position, 7f);

        figyeloKameraEloreXTukrozott = new VektorSugar(transform.position, 2f);
        kijovetelFigyeloTukrozott = new VektorSugar(transform.position, -7f);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit hami = new RaycastHit();

        ray = kameraFpLatas.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray,out hami, 2.8f))
        {
            if (hami.collider.name != "Talaj" && !hami.collider.name.Contains("Fal"))
            {
                Debug.Log(hami.collider.name);
            }

            if (hami.collider.name == "csontHit")
            {
                targyakSzovege.text = "Csont";
                felvehetoCsontraNez = true;
            }
            else
            {
                targyakSzovege.text = "";
                felvehetoCsontraNez = false;
            }
        }

        if (Input.GetKey("e") && felvehetoCsontraNez)
        {
            karakterTulajdonok.Add("Csont");
            targyakSzovege.text = "Felvette";
            csontParent.SetActive(false);
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.green, 2.8f);

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        figyeloKameraEloreX.setSugarOrigin(transform.position);
        kijovetelFigyelo.setSugarOrigin(transform.position, -2f);

        figyeloKameraEloreXTukrozott.setSugarOrigin(transform.position);
        kijovetelFigyeloTukrozott.setSugarOrigin(transform.position, 2f);
        if (transform.parent.localRotation.y > 0.5f)
        {
            figyeloAllapot = VektorSugarAllapot.tukrozott;
        }
        else
        {
            figyeloAllapot = VektorSugarAllapot.normal;
        }

        if (figyeloAllapot == VektorSugarAllapot.normal)
        {

            if (kameraAllapot == Pozicio.kozeli && !kijovetelFigyelo.utkozikEX() && !figyeloKameraEloreX.utkozikEX())
            {
                valtKameraAlaphelyzetbe();
                kameraAllapot = Pozicio.alap;
            }
            else if ((kameraAllapot == Pozicio.alap && figyeloKameraEloreX.utkozikEX()) ||
                kameraAllapot == Pozicio.felul && !kijovetelFigyelo.utkozikEX())
            {
                valtKameraKozeliNezet();
                kameraAllapot = Pozicio.kozeli;
            }
            else if (kameraAllapot == Pozicio.kozeli && figyeloKameraEloreX.utkozikEX())
            {
                valtKameraFelulNezet();
                kameraAllapot = Pozicio.felul;
            }
        }

        if (figyeloAllapot == VektorSugarAllapot.tukrozott)
        {

            if (kameraAllapot == Pozicio.kozeli && !kijovetelFigyeloTukrozott.utkozikEX() && !figyeloKameraEloreXTukrozott.utkozikEX())
            {
                valtKameraAlaphelyzetbe();
                kameraAllapot = Pozicio.alap;
            }
            else if ((kameraAllapot == Pozicio.alap && figyeloKameraEloreXTukrozott.utkozikEX()) ||
                kameraAllapot == Pozicio.felul && !kijovetelFigyeloTukrozott.utkozikEX())
            {
                valtKameraKozeliNezet();
                kameraAllapot = Pozicio.kozeli;
            }
            else if (kameraAllapot == Pozicio.kozeli && figyeloKameraEloreXTukrozott.utkozikEX())
            {
                valtKameraFelulNezet();
                kameraAllapot = Pozicio.felul;
            }
        }


        figyeloKameraEloreXTukrozott.rajzolFigyelo(Color.white);
        kijovetelFigyeloTukrozott.rajzolFigyelo();

        if (kameraAllapot == Pozicio.alap)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x -
                Input.GetAxis("Mouse Y") * 2,  270f, 0f);
        }

    }

    void valtKameraAlaphelyzetbe()
    {
        transform.localPosition = eltolasAlap;
        transform.LookAt(transform.parent);
        transform.localRotation = Quaternion.Euler(0f, 270f, 0f);
    }

    void valtKameraKozeliNezet()
    {
        transform.localPosition = eltolasKozeli;
        transform.LookAt(transform.parent);
        transform.localRotation = Quaternion.Euler(0f, 270f, 0f);
    }

    void valtKameraFelulNezet()
    {
        transform.localPosition = eltolasFelul;
        transform.LookAt(transform.parent);
        transform.localRotation = Quaternion.Euler(90f, 270f, 0f);
    }

}
