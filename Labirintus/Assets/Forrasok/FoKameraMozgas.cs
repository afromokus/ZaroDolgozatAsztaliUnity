using System.Collections.Generic;
using UnityEngine;
using Assets.Model;
using UnityEngine.UI;
using System;

public partial class FoKameraMozgas : MonoBehaviour
{
    public Transform foKameraTransform;
    public Camera kameraFpLatas;
    public GameObject kameraFpMaga;
    public Text targyakSzovege;
    public GameObject bevitelObj;
    public InputField bevitel;

    public Transform kutyaTransform;
    public Rigidbody kutyaRigidBody;

    public GameObject csontParent;
    public GameObject kovekParent;
    public GameObject buzaParent;


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

    

    string figyeltTargy = "";

    List<string> karakterTulajdonok = new List<string>();
    private bool felvehetoKovekreNez;
    private bool felvehetoBuzaraNez;
    private bool kutyaraNez;

    string beirtParancs;
    string uzenet;

    bool megjelenitUzenetet = false;
    bool kutyaKovet = false;

    Vector3 kutyaEltolas = new Vector3(3f,-0.5f,0f);
    private Vector3 kutyaTukrozottEltolas = new Vector3(-3f, -0.5f, 0f);
    private float jatekosHelyeZ;
    private float jatekosHelyeX;
    private Transform jatekosTransf;
    public GameObject holgy;
    private Vector3 holgyEredetiHely;

    private void Start()
    {
        
        //
        kutyaRigidBody.freezeRotation = true;
        kutyaRigidBody = null;
        jatekosTransf = transform.parent;

        holgyEredetiHely = holgy.transform.position;


        bevitelObj.SetActive(false);
        targyakSzovege.text = "";
        

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

    void megjelenikUzenet(string kiir)
    {
        uzenet = kiir;
        megjelenitUzenetet = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (megjelenitUzenetet)
        {
            targyakSzovege.text = uzenet;
            System.Threading.Thread.Sleep(1000);
            megjelenitUzenetet = false;
        }
        if (Cursor.visible == true)
        {
            bevitel.placeholder.GetComponent<Text>().text = "Mit teszel?";
            if (Input.GetKey(KeyCode.Return) && bevitel.text != "")
            {
                if (bevitel.text.Length < 20)
                {
                    beirtParancs = bevitel.text.ToLower();
                }
                else
                {
                    beirtParancs = bevitel.text.Substring(0, 20).ToLower();
                }
                if (kutyaraNez)
                {
                    if (beirtParancs.Contains("ad") && beirtParancs.Contains("csont"))
                    {

                        if (karakterTulajdonok.Contains("Csont"))
                        {
                            megjelenikUzenet("Csont átadva!");
                            kutyaKovet = true;
                        }
                        else
                        {
                            megjelenikUzenet("Tárgy nincs felvéve!");
                        }
                    }
                    else if (beirtParancs.Contains("ad"))
                    {
                        megjelenikUzenet("Ad - Mit?");
                    }
                    else if (beirtParancs.Contains("csont"))
                    {
                        megjelenikUzenet("Csont - Mi történjék vele?");
                    }
                }
                Cursor.visible = false;
                bevitel.text = "";
                bevitelObj.SetActive(false);
                targyakSzovege.text = uzenet;

            }
        }
        else
        {
            RaycastHit hami = new RaycastHit();

            ray = kameraFpLatas.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hami, 2.8f))
            {
                figyeltTargy = hami.collider.name;

                /*if (figyeltTargy != "Talaj" && !figyeltTargy.Contains("Fal"))
                {
                    Debug.Log(figyeltTargy);
                }*/

                if (megjelenitUzenetet)
                {
                }
                else if (figyeltTargy == "csontHit")
                {
                    targyakSzovege.text = "Csont";
                    felvehetoCsontraNez = true;
                }
                else if (figyeltTargy.Contains("koHit"))
                {
                    targyakSzovege.text = "Kövek";
                    felvehetoKovekreNez = true;
                }
                else if (figyeltTargy == "buzaWatch")
                {
                    targyakSzovege.text = "Búza";
                    felvehetoBuzaraNez = true;
                }
                else if (figyeltTargy == "kutyaHit")
                {
                    targyakSzovege.text = "Kutya";
                    kutyaraNez = true;
                }
                else
                {
                    targyakSzovege.text = "";
                    felvehetoCsontraNez = false;
                    felvehetoKovekreNez = false;
                    felvehetoBuzaraNez = false;
                    kutyaraNez = false;
                }
            }

            if (Input.GetKey("e"))
            {
                if (felvehetoCsontraNez)
                {
                    karakterTulajdonok.Add("Csont");
                    felvehetoCsontraNez = false;
                    csontParent.SetActive(false);
                }
                else if (felvehetoKovekreNez)
                {
                    karakterTulajdonok.Add("Kövek");
                    felvehetoKovekreNez = false;
                    kovekParent.SetActive(false);
                }
                else if (felvehetoBuzaraNez && karakterTulajdonok.Contains("Vágókesztyű"))
                {
                    karakterTulajdonok.Add("Kövek");
                    felvehetoBuzaraNez = false;
                    buzaParent.SetActive(false);
                }
                else if (kutyaraNez)
                {
                    Cursor.visible = true;
                    bevitelObj.SetActive(true);
                }

            }


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
                    Input.GetAxis("Mouse Y") * 2, 270f, 0f);
            }
        }
        holgyTamadJatekost();
    }

    private void Update()
    {
        if (kutyaKovet)
        {
            
            if (transform.rotation.y < 0.25f)
            {
                kutyaTransform.position = transform.parent.position + kutyaEltolas;
                kutyaTransform.localRotation = Quaternion.Euler(0f, 270f, 0f);
            }
            else
            {
                kutyaTransform.position = transform.parent.position + kutyaTukrozottEltolas;
            }
        }


    }

    private void holgyTamadJatekost()
    {
        //-39, -25
        jatekosHelyeZ = transform.parent.localPosition.z;
        jatekosHelyeX = jatekosTransf.position.x;
        if (jatekosHelyeZ < -25 && jatekosHelyeZ > -39 || (jatekosHelyeZ < -39 && jatekosHelyeX < -25))
        {
            holgy.transform.LookAt(jatekosTransf.position);
            holgy.transform.Translate(0f, 0f, 0.08f);
        }
        else
        {
            holgy.transform.LookAt(holgyEredetiHely);
            holgy.transform.Translate(0f, 0f, 0.08f);
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
