using System.Collections.Generic;
using UnityEngine;
using Assets.Model;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public partial class FoKod : MonoBehaviour
{
    List<GameObject> halalFejek = new List<GameObject>();

    public Transform foKameraTransform;
    public Camera kameraFpLatas;
    public GameObject kameraFpMaga;
    public Text targyakSzovege;
    public GameObject bevitelObj;
    public InputField bevitel;

    public Transform kutyaTransform;
    public GameObject pasi;

    public GameObject csontParent;
    public GameObject kovekParent;
    public GameObject buzaParent;
    public GameObject ajtoAnimacio;
    public GameObject kutya;
    public GameObject halalFej;

    int i = 0;

    bool felvehetoCsontraNez = false;
    bool csengoreNez = false;
    bool urraNez = false;

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
    public static bool kutyaMutat = false;

    Vector3 kutyaEltolas = new Vector3(3f,-0.5f,0f);
    private Vector3 kutyaTukrozottEltolas = new Vector3(-3f, -0.5f, 0f);
    private float jatekosHelyeZ;
    private float jatekosHelyeX;
    private Transform jatekosTransf;
    public GameObject holgy;
    private Vector3 holgyEredetiHely;
    private bool ajtotLattaMar = true;//false;
    private bool urKutyaraRakerdezett;
    private bool urLezarva;
    private bool satorraNez;
    private bool pasiKijonE;
    private bool helikopterreNez;
    private bool jatekosTogyreNez;

    UzenetMegjelenito uzMegjel;
    private int uzIdo = 201;
    private Animation[] animaciok;
    private bool kutyaElsoHelyenVanE = false;
    private bool kutyaMasodikHelyenVanE = false;
    private bool kutyaMegyMasodikHelyre;

    private void Start()
    {

        bevitelObj.SetActive(false);

        figyeloKameraEloreXTukrozott = new VektorSugar(transform.position, 2f);

        figyeloKameraEloreX = new VektorSugar(transform.position, -2f);
        kijovetelFigyelo = new VektorSugar(transform.position, 7f);
        kijovetelFigyeloTukrozott = new VektorSugar(transform.position, -7f);

        uzMegjel = new UzenetMegjelenito(5, targyakSzovege);
        uzMegjel.megjelenitUzenetet("Nyami");

        ajtoAnimacio.SetActive(false);
        
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
        
        BiztonsagosUtvonal biztUt = new BiztonsagosUtvonal();
        List<float> szomszedosKoord = biztUt.getSzomszedosKoordinatak();
        i = 0;

        for (i = 0; i < szomszedosKoord.Count - 1; i += 2)
        {
            GameObject halalFejMasolat = Instantiate(halalFej);
            halalFejMasolat.name = "halalFej" + i/2;

            halalFejElhelyezes(halalFejMasolat, szomszedosKoord[i], szomszedosKoord[i + 1]);
            halalFejek.Add(halalFejMasolat);
        }

        /*foreach (double[] koordinataTomb in biztUt.getSzomszedosKoordinatak())
        {
            GameObject halalFejMasolat = Instantiate(halalFej);
            halalFejMasolat.name = "halalFej" + i;

            koordinatak = biztUt.getKoordinatak()[i];

            halalFejElhelyezes(halalFejMasolat, koordinatak);
            i++;
        }*/

    }

    private void halalFejElhelyezes(GameObject halalFej, float kordX, float kordZ)
    {
        halalFej.transform.position = new Vector3(kordX, 1, kordZ);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*if (!kutyaElsoHelyenVanE && !kutyaMasodikHelyenVanE)
        {
            kutyafutElsoHelyre(0.16f, 2);
        }
        else if(kutyaElsoHelyenVanE)
        {
            kutyafutMasodikHelyre(0.16f);
        }*/

        if (kutyaMegyMasodikHelyre)
        {
            kutyafutMasodikHelyre(0.2f);
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
                if (kutyaraNez && !kutyaMutat)
                {
                    if (beirtParancs.Contains("ad") && beirtParancs.Contains("csont"))
                    {

                        if (karakterTulajdonok.Contains("Csont"))
                        {
                            uzMegjel.megjelenitUzenetet("Csont átadva.");
                            uzIdo = 0;
                            karakterTulajdonok.Remove("Csont");
                            kutyaMutat = true;
                            kutya.GetComponent<Animator>().Play("Futas", 0);
                        }
                        else
                        {
                            uzMegjel.megjelenitUzenetet("Tárgy nincs felvéve!");
                            uzIdo = 0;
                        }
                    }
                    else if (beirtParancs.Contains("ad"))
                    {
                        uzMegjel.megjelenitUzenetet("Ad - Mit?");
                        uzIdo = 0;
                    }
                    else if (beirtParancs.Contains("csont"))
                    {
                        uzMegjel.megjelenitUzenetet("Csont - Mi történjék vele?");
                        uzIdo = 0;
                    }
                    else
                    {
                        uzMegjel.megjelenitUzenetet("Ismeretlen parancs");
                        uzIdo = 0;
                    }
                }

                if (urraNez && !urLezarva)
                {
                    urBeszel();
                    if (!urKutyaraRakerdezett)
                    {
                        if (beirtParancs.Contains("elkér"))
                        {
                            urKutyaraRakerdezett = true;
                            uzMegjel.megjelenitUzenetet("Persze, odaadom, de mondja csak uram," +
                                " mi lesz a kutyával?");
                            uzIdo = 0;
                        }
                    }
                    else
                    {
                        if ((beirtParancs.Contains("ad") || beirtParancs.Contains(" lehet")) && 
                            !beirtParancs.Contains("nem") || (beirtParancs.Contains("Vigyázzon") && 
                            (beirtParancs.Contains(" rá") || beirtParancs.Contains("kutyára"))))
                        {
                            uzMegjel.megjelenitUzenetet("Persze, szívesen felnevelem," +
                                " együtt fog élni velünk!");
                            uzIdo = 0;
                            urLezarva = true;
                        }
                        else
                        {
                            uzMegjel.megjelenitUzenetet("Nem szeretném, ha kivinné oda…");
                            uzIdo = 0;
                        }
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
                else if (figyeltTargy == "csengoHit")
                {
                    targyakSzovege.text = "Csengő";
                    csengoreNez = true;
                }
                else if (figyeltTargy == "urHit")
                {
                    targyakSzovege.text = "Mylord";
                    urraNez = true;
                }
                else if (figyeltTargy == "satorHit")
                {
                    targyakSzovege.text = "Sátor";
                    satorraNez = true;
                }
                else if (figyeltTargy == "heliHit")
                {
                    targyakSzovege.text = "Helikopter roncs";
                    helikopterreNez = true;
                }
                else if (figyeltTargy == "kecskeHit")
                {
                    targyakSzovege.text = "Kecske";
                }
                else if (figyeltTargy == "togyHit")
                {
                    targyakSzovege.text = "Kecsketőgy";
                    jatekosTogyreNez = true;
                }
                else
                {
                    targyakSzovege.text = "";
                    felvehetoCsontraNez = false;
                    felvehetoKovekreNez = false;
                    felvehetoBuzaraNez = false;
                    kutyaraNez = false;
                    csengoreNez = false;
                    urraNez = false;
                    satorraNez = false;
                    helikopterreNez = false;
                    jatekosTogyreNez = false;
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
                    if (!kutyaMutat)
                    {
                        Cursor.visible = true;
                        bevitelObj.SetActive(true);
                    }
                    else if (kutyaElsoHelyenVanE)
                    {
                        kutya.GetComponent<Animator>().Play("Futas", 0);
                        kutyaMegyMasodikHelyre = true;
                    }
                }
                else if (csengoreNez)
                {
                    ajtoNyitas();
                }
                else if (urraNez)
                {
                    if (ajtotLattaMar)
                    {
                        Cursor.visible = true;
                        bevitelObj.SetActive(true);
                    }
                    else
                    {
                        urBeszel();
                    }
                }
                else if (satorraNez)
                {
                    pasiKijonE = true;
                }
                else if (helikopterreNez)
                {
                    uzMegjel.megjelenitUzenetet("Vas felvéve, hozzáadva a tárgykahoz");
                    karakterTulajdonok.Add("vas");
                }
                else if (jatekosTogyreNez)
                {
                    megfejesiKiserlet();
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
        pasiMozog();
        //holgyTamadJatekost();

        if (!ajtotLattaMar)
        {
            if (jatekosHelyeZ > 25 && jatekosHelyeX > -25)
            {
                ajtotLattaMar = true;
            }
        }

        kiirUzenet();

    }

    private void kutyafutMasodikHelyre(float sebesseg)
    {
        float fordulas = 1f;
        if (kutyaTransform.position.z < -16)
        {
            kutyaTransform.rotation = Quaternion.Euler(0f, 340f, 0f);
            kutyaTransform.Translate(0f, 0f, sebesseg);
        }
        else
        {
            kutyaTransform.rotation = Quaternion.Euler(0f, 90f, 0f);
            kutya.GetComponent<Animator>().Play("Idle", 0);
            kutyaElsoHelyenVanE = false;
            kutyaMegyMasodikHelyre = false;
            kutyaMasodikHelyenVanE = true;
        }

    }

    private void kiirUzenet()
    {
        if (uzIdo <= 200)
        {
            targyakSzovege.text = uzMegjel.Uzenet;

            uzIdo++;
            //Debug.Log(uzIdo);
        }
        else
        {
            System.Threading.Thread.Sleep(uzMegjel.Idotart);
        }
    }

    private void megfejesiKiserlet()
    {
        throw new NotImplementedException();
    }

    private void urBeszel()
    {
        if (ajtotLattaMar)
        {
            uzMegjel.megjelenitUzenetet("Kinyitni az ajtót? Régen nem járt már arra senki. Már csak a " +
                "fémtokja maradt meg,a kulcsot három évszázada senki nem látta.");
            uzIdo = -450;
        }
        else
        {
            uzMegjel.megjelenitUzenetet("Jó napot!");
            uzIdo = 0;
        }
    }

    private void Update()
    {
        if (kutyaMutat)
        {
            if (!kutyaElsoHelyenVanE && !kutyaMasodikHelyenVanE)
            {
                kutyafutElsoHelyre(0.2f, 2);
            }
        }


    }

    private void kutyafutElsoHelyre(float sebesseg, int fordulasY)
    {
        float fordulas = 0.15f;
        if (kutyaTransform.position.x < 35)
        {
            kutyaTransform.Translate(0f, 0f, sebesseg);

            if (kutyaTransform.rotation.y < 0.7f)
            {
                kutyaTransform.Rotate(0f, fordulas, 0f);
            }
        }
        else
        {
            kutyaTransform.rotation = Quaternion.Euler(0f, 270, 0f);
            kutya.GetComponent<Animator>().Play("Idle", 0);
            kutyaElsoHelyenVanE = true;
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
