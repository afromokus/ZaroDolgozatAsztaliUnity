﻿using System.Collections.Generic;
using UnityEngine;
using Assets.Model;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public partial class FoKod : MonoBehaviour
{
    List<GameObject> halalFejek = new List<GameObject>();

    public Transform foKameraTransform;
    public Transform SzellemMeshTransf;
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
    public GameObject atadandoCsont;

    public GameObject holgy;
    public GameObject holgyUtes;

    int i = 0;
    int koponyaszam = 0;

    bool felvehetoCsontraNez = false;
    bool csengoreNez = false;
    bool urraNez = false;
    bool holgyKovessenE = false;

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
    Vector3 kutyaCel;

    public Animator jatekosAnimator;
    public Animator holgyAnimator;

    string figyeltTargy = "";

    List<string> karakterTulajdonok = new List<string>();
    List<float> biztonsagosPontok;

    private bool felvehetoKovekreNez;
    private bool felvehetoBuzaraNez;
    private bool kutyaraNez;

    string beirtParancs;
    string uzenet = "";

    bool megjelenitUzenetet = false;
    private bool kutyaElindultE = false;

    Vector3 kutyaEltolas = new Vector3(3f,-0.5f,0f);
    private Vector3 kutyaTukrozottEltolas = new Vector3(-3f, -0.5f, 0f);
    private float jatekosHelyeZ;
    private float jatekosHelyeX;
    private Transform jatekosTransf;
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
    BiztonsagosUtvonal biztUt;
    public static bool serulEJatekos;
    private bool kutyaSetalGazban;
    private bool kutyaMegerkezettE = false;
    private bool csontAtadvaE;
    private bool holgyHelyenVanE = true;
    private bool holgyJatekosMelletVanE = false;
    private bool holgyElindultE = false;
    private int pihenesHolgy = 50;

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
        
        jatekosValtUnityStatusz(SzellemMeshTransf);

        jatekosAnimator.Play("Idle", 0);

        biztUt = new BiztonsagosUtvonal();
        List<float> szomszedosKoord = biztUt.getSzomszedosKoordinatak();
        i = 0;

        for (i = 0; i < szomszedosKoord.Count - 1; i += 2)
        {
            GameObject halalFejMasolat = Instantiate(halalFej);
            halalFejMasolat.name = "halalFej" + i/2;

            halalFejElhelyezes(halalFejMasolat, szomszedosKoord[i], szomszedosKoord[i + 1]);
            halalFejek.Add(halalFejMasolat);
        }

        //kutyaElindultE = true;
        kutyaCel = new Vector3(28,kutyaTransform.position.y,-18);
        biztonsagosPontok = biztUt.lekerBiztonsagosKoord();
    }

    private void jatekosValtUnityStatusz(Transform SzellemMeshT)
    {
        SzellemMeshT.localPosition = new Vector3(-0.1f, -0.38f, 0);
        SzellemMeshT.localRotation = Quaternion.Euler(0, 270, 0);
        SzellemMeshT.localScale = new Vector3(1.4f, 0.65f, 2.125f);
    }

    private void halalFejElhelyezes(GameObject halalFej, float kordX, float kordZ)
    {
        halalFej.transform.position = new Vector3(kordX, 1, kordZ);
    }

    private void setalKutyaGazban()
    {
        try
        {
            if (koponyaszam < biztonsagosPontok.Count + 1)
            {
                if (kutyaTransform.position.x - 0.1f > kutyaCel.x)
                {
                    kutyaTransform.position = Vector3.MoveTowards(kutyaTransform.position, kutyaCel, 0.06f);
                }
                else
                {
                    kutyaCel.x = biztonsagosPontok[koponyaszam];
                    kutyaCel.z = biztonsagosPontok[koponyaszam + 1];
                    kutyaTransform.LookAt(kutyaCel);
                    koponyaszam += 2;
                }
            }
            else
            {
                kutyaTransform.rotation = Quaternion.Euler(0f, 90f, 0f);
                kutyaMasodikHelyenVanE = false;
                kutyaMegerkezettE = true;
                kutyaSetalGazban = false;
                kutya.GetComponent<Animator>().Play("Idle", 0);
            }
        }
        catch
        {
            if (kutyaTransform.position.z + 0.5f < -5f)
            {
                kutyaCel.x = -46f;
                kutyaCel.z = -5f;
                kutyaTransform.LookAt(kutyaCel);
                kutyaTransform.position = Vector3.MoveTowards(kutyaTransform.position, kutyaCel, 0.06f);
            }
            else
            {
                kutyaTransform.rotation = Quaternion.Euler(0f, 90f, 0f);
                kutyaMasodikHelyenVanE = false;
                kutyaMegerkezettE = true;
                kutyaSetalGazban = false;
                kutya.GetComponent<Animator>().Play("Idle", 0);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (holgyKovessenE == false)
        {
            if (jatekosTransf.position.z > -40f && Vector3.Distance(holgy.transform.position, jatekosTransf.position) < 10f)
            {
                holgyKovessenE = true;
            }
        }

        if (csontAtadvaE)
        {
            //Debug.Log(jatekosAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (jatekosAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.68f)
            {
                uzMegjel.megjelenitUzenetet("Csont átadva.");
                uzIdo = 0;
                karakterTulajdonok.Remove("Csont");
                kutyaElindultE = true;
                kutya.GetComponent<Animator>().Play("Futas", 0);
                csontAtadvaE = false;
            }
        }

        /*if (!kutyaElsoHelyenVanE && !kutyaMasodikHelyenVanE)
        {
            //kutyafutElsoHelyre(0.16f, 2);
        }
        else if (kutyaElsoHelyenVanE)
        {
            kutyafutMasodikHelyre(0.16f);
        }
        else if (kutyaMasodikHelyenVanE)
        {
        }*/

        jatekosHelyeZ = (float)Math.Round(transform.parent.localPosition.z, 1);
        jatekosHelyeX = (float)Math.Round(jatekosTransf.position.x, 1);

        if (biztUt.serulEJatekos(jatekosHelyeX, jatekosHelyeZ, kutyaElindultE))
        {
            serulEJatekos = true;
        }
        else
        {
            serulEJatekos = false;
        }

        if (kutyaMegyMasodikHelyre)
        {
            kutyafutMasodikHelyre(0.2f);
        }

        if (kutyaSetalGazban)
        {
            setalKutyaGazban();
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
                if (kutyaraNez && !kutyaElindultE)
                {
                    if (beirtParancs.Contains("ad") && beirtParancs.Contains("csont"))
                    {
                        if (karakterTulajdonok.Contains("Csont"))
                        {
                            jatekosAnimator.Play("Atadas", 0);
                            atadandoCsont.SetActive(true);

                            csontAtadvaE = true;
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
                Debug.Log(figyeltTargy);

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
                    if (!kutyaElindultE)
                    {
                        Cursor.visible = true;
                        bevitelObj.SetActive(true);
                    }
                    else if (kutyaElsoHelyenVanE)
                    {
                        kutya.GetComponent<Animator>().Play("Futas", 0);
                        kutyaMegyMasodikHelyre = true;
                    }
                    else if (kutyaMasodikHelyenVanE)
                    {
                        kutyaTransform.rotation = Quaternion.Euler(0f, -90f, 0f);
                        kutya.GetComponent<Animator>().Play("Seta", 0);
                        kutyaSetalGazban = true;
                        kutyaCel.z = biztonsagosPontok[koponyaszam + 1];
                        kutyaTransform.LookAt(kutyaCel);
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

        if (holgyKovessenE)
        {
            holgyTamadJatekost();
        }

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

    private void LateUpdate()
    {
        if (!kutyaMegerkezettE)
        {
            if (kutyaElindultE)
            {
                if (!kutyaElsoHelyenVanE && !kutyaMasodikHelyenVanE)
                {
                    kutyafutElsoHelyre(0.2f, 2);
                }
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
        if (Math.Abs(holgy.transform.position.z - holgyEredetiHely.z) < 0.1f)
        {
            if (holgyElindultE)
            {
                if (!holgyHelyenVanE)
                {
                    holgyAnimator.Play("VisszaMozgas");
                }
            }
            holgyHelyenVanE = true;
            holgy.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else
        {
            holgyHelyenVanE = false;
        }

        if (Vector3.Distance(jatekosTransf.position, holgy.transform.position) < 3.5f)
        {
            holgyJatekosMelletVanE = true;
        }
        else
        {
            holgyJatekosMelletVanE = false;
        }
        //-39, -25
        jatekosHelyeZ = transform.parent.localPosition.z;
        jatekosHelyeX = jatekosTransf.position.x;
        if (jatekosHelyeZ < -25 && jatekosHelyeZ > -39 || (jatekosHelyeZ < -39 && jatekosHelyeX < -25))
        {
            if (!holgyElindultE)
            {
                holgyElindultE = true;
            }

            if (!holgyJatekosMelletVanE)
            {
                holgy.transform.LookAt(jatekosTransf.position);
                holgy.transform.Translate(0f, 0f, 0.08f);
                holgyAnimator.Play("Mozgas");
            }
            else
            {
                //Debug.Log(pihenesHolgy);
                if (pihenesHolgy >= 50)
                {
                    holgy.transform.GetChild(0).gameObject.SetActive(false);
                    holgyUtes.SetActive(true);
                    holgyUtes.transform.LookAt(new Vector3(jatekosTransf.position.x, -0.1f, jatekosTransf.position.z));
                    if (holgyUtes.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.45f &&
                            holgyUtes.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.85f)
                    {
                        holgyUtes.transform.GetChild(3).gameObject.GetComponent<BoxCollider>().enabled = true;
                    }
                    else
                    {
                        holgyUtes.transform.GetChild(3).gameObject.GetComponent<BoxCollider>().enabled = false;
                    }

                    if (holgyUtes.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
                    {
                        holgyUtes.SetActive(false);
                        holgy.transform.GetChild(0).gameObject.SetActive(true);
                        holgyAnimator.Play("Idle");
                        pihenesHolgy = 0;
                    }
                }
                else
                {
                    pihenesHolgy++;
                }
            }
        }
        else
        {
            if (!holgyHelyenVanE)
            {
                holgy.transform.LookAt(holgyEredetiHely);
                holgy.transform.Translate(0f, 0f, 0.08f);
            }
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
