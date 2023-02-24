using System.Collections.Generic;
using UnityEngine;
using Assets.Model;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using Random = System.Random;

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
    public GameObject fejoJatekos;
    public GameObject jatekosAnimaltObj;

    public Transform kutyaTransform;
    public GameObject pasi;

    public GameObject kemenceHit;
    public GameObject tejesVodor;
    public GameObject vodorObj;
    public GameObject csontObj;
    public GameObject kovekObj;
    public GameObject buzaObj;
    public GameObject ajtoAnimacio;
    public GameObject kutya;
    public GameObject halalFej;
    public GameObject atadandoCsont;
    public GameObject togyHitObj;
    public GameObject AlvasJelA;
    public GameObject AlvasJelB;
    public GameObject AlvasJelC;
    public GameObject AlvasJelD;
    public GameObject AlvasJelE;
    public GameObject holgy;
    public GameObject holgyUtes;
    public GameObject kesztyuObj;
    public GameObject malomko;
    public GameObject parasztHit;
    public GameObject ToldiParasztHit;

    public AudioSource hangCsengo;

    public Image csontKep;
    public Image koKep;
    public Image kesztyuKep;
    public Image buzaKep;
    public Image vodorKep;
    public Image tejesVodorKep;
    public Image kenyerKep;
    public Image vasKep;
    public Image femTokKep;
    public Image kulcsKep;
    RectTransform rt;

    int i = 0;
    int koponyaszam = 0;

    public static bool jatekosMozoghatE = true;
    bool felvehetoCsontraNez = false;
    bool csengoreNez = false;
    bool urraNez = false;
    bool holgyKovessenE = false;
    bool barmikorFelnyithatoE = false;
    private int idozito = 0;
    public static bool lerakomod = false;
    int idozitoParasztAtfog = 0;

    public static bool bevitelObjActive { get; set; }

    private float sebessegAlvasJel = 0.02f;

    private float valtozasAlvasJelA = 0.026f;
    private float valtozasAlvasJelB = 0.026f;
    private float valtozasAlvasJelC = 0.026f;
    private float valtozasAlvasJelD = 0.026f;
    private float valtozasAlvasJelE = 0.026f;

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
    Vector3 helyAlvasJelA = new Vector3();
    Vector3 helyAlvasJelB = new Vector3();
    Vector3 helyAlvasJelC = new Vector3();
    Vector3 helyAlvasJelD = new Vector3();
    Vector3 helyAlvasJelE = new Vector3();

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
    private bool ajtotLattaMar = true; //false;
    private bool urKutyaraRakerdezett;
    private bool urLezarva;
    private bool satorraNez;
    private bool pasiKijonE;
    private bool helikopterreNez;
    private bool jatekosTogyreNez;
    private bool jatekosKesztyureNez = false;

    UzenetMegjelenito uzMegjel;
    private int uzIdo = 201;
    private bool tejAtadvaE;
    private bool kenyerAtadvaE;
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
    private bool parasztAtengedE = false;
    private int pihenesHolgy = 50;
    private bool vodorreNez;
    private bool kemencereNez = false;
    private bool tejesVodorreNez;
    private bool inputEngedelyezes = true;
    private int inputSzamlalo = 0;

    private Random rnd = new Random();
    public static bool mehetEAlvasJel = true;

    public static bool HolgySzerelmesE = false;
    private bool kesztyutJatekosFelhuztaE = false;
    private bool parasztraNez = false;
    private bool beolvasztasE = false;

    private void Start()
    {
        csontKep.transform.GetChild(0).gameObject.SetActive(false);
        koKep.transform.GetChild(0).gameObject.SetActive(false);
        kesztyuKep.transform.GetChild(0).gameObject.SetActive(false);
        buzaKep.transform.GetChild(0).gameObject.SetActive(false);
        vodorKep.transform.GetChild(0).gameObject.SetActive(false);
        tejesVodorKep.transform.GetChild(0).gameObject.SetActive(false);
        kenyerKep.transform.GetChild(0).gameObject.SetActive(false);
        vasKep.transform.GetChild(0).gameObject.SetActive(false);
        femTokKep.transform.GetChild(0).gameObject.SetActive(false);
        kulcsKep.transform.GetChild(0).gameObject.SetActive(false);

        karakterTulajdonok.Add("Kövek");
        hozzaadTargyatInventoryhoz(koKep);
        //karakterTulajdonok.Add("Csont");
        //hozzaadTargyatInventoryhoz(csontKep);
        //karakterTulajdonok.Add("Tejes Vödör");
        //karakterTulajdonok.Add("Búza");
        //hozzaadTargyatInventoryhoz(buzaKep);
        //karakterTulajdonok.Add("kesztyű");
        //hozzaadTargyatInventoryhoz(kesztyuKep);
        //karakterTulajdonok.Add("nyami");
        karakterTulajdonok.Add("Vasdarab");
        hozzaadTargyatInventoryhoz(vasKep);
        karakterTulajdonok.Add("Tejes Vödör");
        hozzaadTargyatInventoryhoz(tejesVodorKep);
        karakterTulajdonok.Add("Kenyér");
        hozzaadTargyatInventoryhoz(kenyerKep);
        //karakterTulajdonok.Add("Kulcs");
        //hozzaadTargyatInventoryhoz(kulcsKep);

        bevitelObj.SetActive(false);
        bevitelObjActive = false;
        fejoJatekos.SetActive(false);
        tejesVodor.SetActive(false);

        figyeloKameraEloreXTukrozott = new VektorSugar(transform.position, 2f);

        figyeloKameraEloreX = new VektorSugar(transform.position, -2f);
        kijovetelFigyelo = new VektorSugar(transform.position, 7f);
        kijovetelFigyeloTukrozott = new VektorSugar(transform.position, -7f);

        uzMegjel = new UzenetMegjelenito(5, targyakSzovege);
        uzMegjel.megjelenitUzenetet("Nyami");

        ajtoAnimacio.SetActive(false);
        
        jatekosTransf = transform.parent;
        holgyEredetiHely = holgy.transform.position;

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
        /*FejoJatekos.transform.localPosition.Set(jatekosTransf.localPosition.x, FejoJatekos.transform.localPosition.y, jatekosTransf.localPosition.x);
        FejoJatekos.SetActive(false);*/
    }

    private void athuzKepet(Image kep)
    {
        kep.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (kenyerAtadvaE && tejAtadvaE) 
        {
            parasztAtengedE = true;
        }

        if (parasztAtengedE) 
        {
            if (idozitoParasztAtfog < 80 && malomko.transform.rotation.y < 0.94f)
            {
                malomko.transform.Rotate(0f, 0.35f, 0f);
                malomko.transform.Translate(0f, 0f, 0.0035f);
                //malomkő elfordítva
            }
            else 
            {
                parasztHit.SetActive(false);
                //akadály eltüntetve
                if (idozitoParasztAtfog < 100)
                {
                    idozitoParasztAtfog++;
                }
                else 
                {
                    //Paraszt átfogott a malomkövön
                    if (ToldiParasztHit.transform.position.x < -19)
                    {
                        ToldiParasztHit.transform.Translate(0.03f, 0f, 0.01f);
                        malomko.transform.Rotate(0f,0f,0.4f);
                    }
                }

            }
        }

        if (idozito <= 5)
        {
            idozito++;
        }
        else 
        {
            barmikorFelnyithatoE = false;
        }
        if (mehetEAlvasJel == true)
        {
            ZbetukRoptetese();
        }

        if (holgyKovessenE == false)
        {
            if (jatekosTransf.position.z > -40f && Vector3.Distance(holgy.transform.position, jatekosTransf.position) < 10f)
            {
                holgyKovessenE = true;
            }
        }

        if (fejoJatekos.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            fejoJatekos.SetActive(false);
            jatekosAnimaltObj.SetActive(true);
            jatekosMozoghatE = true;
        }

       if (lerakomod)
        {
            if (Input.GetMouseButton(0) && kemenceHit.GetComponentInChildren<MeshRenderer>().enabled)
            {
                kemenceHit.transform.parent = null;
                kemenceHit.GetComponent<BoxCollider>().isTrigger = false;
                kemenceHit.GetComponent<Rigidbody>().useGravity = true;
                athuzKepet(koKep);
                lerakomod = false;
                barmikorFelnyithatoE = false;
            }
        }
        

        if (csontAtadvaE)
        {
            //Debug.Log(jatekosAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (jatekosAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.68f)
            {
                uzMegjel.megjelenitUzenetet("Csont átadva.");
                uzIdo = 0;
                //karakterTulajdonok.Remove("Csont");
                athuzKepet(csontKep);
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

        //ha megnyomom az entert amikor a játék épp nem áll
        if (Cursor.visible == false && !kemencereNez && Input.GetKey(KeyCode.Return) && !kutyaraNez && !urraNez && !bevitelObjActive && !parasztraNez && !barmikorFelnyithatoE)//return = enter
        {
            Cursor.visible = true;
        }

        if (Cursor.visible == true)
        {
            if (!beolvasztasE)
            {
                bevitel.placeholder.GetComponent<Text>().text = "Mit teszel?";
            }
            bevitelObj.SetActive(true);
            bevitelObj.GetComponent<InputField>().Select();
            bevitelObjActive = true;
            if (Input.GetKey(KeyCode.Return)/*Return = enter*/ && bevitel.text != "")
            {
                if (!beolvasztasE)
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
                        if (beirtParancs.Contains("ad") && beirtParancs.Contains(" csont"))
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
                        Cursor.visible = false;
                        bevitel.text = "";
                        bevitelObj.SetActive(false);
                        bevitelObjActive = false;
                        targyakSzovege.text = uzenet;
                    }
                    else if (parasztraNez)
                    {
                        if (bevitel.text.Length < 40)
                        {
                            beirtParancs = bevitel.text.ToLower();
                        }
                        else
                        {
                            beirtParancs = bevitel.text.Substring(0, 40).ToLower();
                        }
                        if (beirtParancs.Contains("ad") && beirtParancs.Contains(" tej") && beirtParancs.Contains(" keny"))
                        {

                            athuzKepet(tejesVodorKep);

                            tejAtadvaE = true;
                            jatekosAnimator.Play("Atadas", 0);

                            athuzKepet(kenyerKep);
                            kenyerAtadvaE = true;

                            uzMegjel.megjelenitUzenetet("Tej és kenyér átadva.");
                            uzIdo = 0;


                        }
                        else if (beirtParancs.Contains("ad") && beirtParancs.Contains(" tej"))
                        {
                            if (karakterTulajdonok.Contains("Tejes Vödör"))
                            {
                                //jatekosAnimator.Play("Atadas", 0);

                                uzMegjel.megjelenitUzenetet("Tej átadva.");
                                uzIdo = 0;

                                athuzKepet(tejesVodorKep);

                                tejAtadvaE = true;

                            }
                            else
                            {
                                uzMegjel.megjelenitUzenetet("Tárgy nincs felvéve!");
                                uzIdo = 0;
                            }
                        }
                        else if (beirtParancs.Contains("ad") && beirtParancs.Contains(" keny"))
                        {
                            if (karakterTulajdonok.Contains("Kenyér"))
                            {
                                jatekosAnimator.Play("Atadas", 0);

                                uzMegjel.megjelenitUzenetet("Kenyér átadva.");
                                uzIdo = 0;

                                athuzKepet(kenyerKep);
                                kenyerAtadvaE = true;

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
                        else
                        {
                            uzMegjel.megjelenitUzenetet("Ismeretlen parancs");
                            uzIdo = 0;
                        }
                        Cursor.visible = false;
                        System.Threading.Thread.Sleep(100);
                        bevitel.text = "";
                        bevitelObj.SetActive(false);
                        bevitelObjActive = false;
                        targyakSzovege.text = uzenet;
                    }
                    else if (kemencereNez)
                    {
                        if (bevitel.text.Length < 90)
                        {

                            beirtParancs = bevitel.text.ToLower();

                            Debug.Log("A beírt parancs:\t" + beirtParancs);

                            if (karakterTulajdonok.Contains("Vasdarab") && !beolvasztasE)
                            {
                                //if (karakterTulajdonok.Contains("kulcs"))
                                {
                                    if (beirtParancs.Contains("beolvaszt") && beirtParancs.Contains(" vas"))
                                    {
                                        Debug.Log("beolvasztás");
                                        uzMegjel.megjelenitUzenetet("És?");
                                        uzIdo = 0;
                                        beolvasztasE = true;
                                        bevitel.text = "";
                                        bevitel.placeholder.GetComponent<Text>().text = "Beolvasztod a vasat. Mit teszel vele?";


                                    }
                                    else
                                    {
                                        //if (!beirtParancs.Contains("kulcs") && karakterTulajdonok.Contains("fémtok"))
                                        {
                                            uzMegjel.megjelenitUzenetet("Ismeretlen parancs! (kemencérenéz beolvasztás)");
                                            bevitel.text = "";
                                            uzIdo = 0;
                                            bevitelObj.SetActive(false);
                                            bevitelObjActive = false;
                                            Cursor.visible = false;
                                        }
                                    }
                                }
                            }
                            else if (karakterTulajdonok.Contains("Búza"))
                            {
                                if (beirtParancs.Contains("búzából") || beirtParancs.Contains("búzát"))
                                {
                                    //Debug.Log("Búza észlelve!");
                                    if (beirtParancs.Contains("kenyeret"))
                                    {

                                        //Debug.Log("Kenyér észlelve!");
                                        if (beirtParancs.Contains("sütö") || beirtParancs.Contains("készítek"))
                                        {
                                            //Debug.Log("Elkészítés észlelve!");
                                            athuzKepet(buzaKep);
                                            //karakterTulajdonok.Remove("Búza");
                                            karakterTulajdonok.Add("Kenyér");
                                            hozzaadTargyatInventoryhoz(kenyerKep);

                                            uzMegjel.megjelenitUzenetet("Kenyeret készítettél a búzából.");
                                            uzIdo = 0;
                                            bevitelObj.SetActive(false);
                                            bevitelObjActive = false;
                                            Cursor.visible = false;
                                        }

                                    }
                                }
                            }
                            else
                            {
                                uzMegjel.megjelenitUzenetet("Tárgy nincs felvéve!");
                                uzIdo = 0;
                                bevitelObj.SetActive(false);
                                bevitelObjActive = false;
                                Cursor.visible = false;
                            }

                        }
                    }
                    else if (urraNez && !urLezarva)
                    {

                        if (!urKutyaraRakerdezett)
                        {
                            bevitelObj.SetActive(true);
                            bevitelObjActive = true;
                            beirtParancs = bevitel.text.ToLower();
                            if (beirtParancs.Contains("elkér") || (beirtParancs.Contains("kér") && beirtParancs.Contains("szépen")))
                            {
                                urKutyaraRakerdezett = true;
                                uzMegjel.megjelenitUzenetet("Persze, odaadom.");
                                //uzMegjel.megjelenitUzenetet("Persze, odaadom, de mondja csak uram," +
                                //    " mi lesz a kutyával?");
                                uzIdo = 0;
                                karakterTulajdonok.Add("vas öntőforma");//fémtok
                                hozzaadTargyatInventoryhoz(femTokKep);
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
                        Cursor.visible = false;
                        bevitel.text = "";
                        beirtParancs = "";
                        bevitelObj.SetActive(false);
                        bevitelObjActive = false;
                        targyakSzovege.text = uzenet;
                    }
                    else if (felvehetoBuzaraNez)
                    {
                        beirtParancs = bevitel.text.ToLower();

                        if (bevitel.text.Length <= 50)
                        {
                            beirtParancs = bevitel.text.ToLower();
                        }
                        else
                        {
                            beirtParancs = bevitel.text.Substring(0, 50).ToLower();
                        }

                        if ((beirtParancs.Contains("felvesz") || beirtParancs.Contains("felhúz")) && beirtParancs.Contains("kesztyűt"))
                        {
                            if (karakterTulajdonok.Contains("kesztyű"))
                            {
                                if (beirtParancs.Contains("levágom") && beirtParancs.Contains("búzát"))
                                {
                                    uzMegjel.megjelenitUzenetet("Levágtad a kesztyűvel a búzát");
                                    uzIdo = 0;
                                    athuzKepet(kesztyuKep);
                                    //karakterTulajdonok.Remove("kesztyű");
                                    buzaObj.SetActive(false);
                                    karakterTulajdonok.Add("Búza");
                                    hozzaadTargyatInventoryhoz(buzaKep);
                                }
                                else
                                {
                                    uzMegjel.megjelenitUzenetet("Felhúztad a kesztyűt");
                                    uzIdo = 0;
                                    athuzKepet(kesztyuKep);
                                    //karakterTulajdonok.Remove("kesztyű");
                                    kesztyutJatekosFelhuztaE = true;
                                }
                            }
                            else
                            {
                                uzMegjel.megjelenitUzenetet("Tárgy nincs felvéve!");
                                uzIdo = 0;
                            }
                        }
                        else
                        {
                            uzMegjel.megjelenitUzenetet("Ismeretlen parancs! (Kesztyű)");
                            uzIdo = 0;
                        }

                        bevitel.text = "";
                        beirtParancs = "";
                        Cursor.visible = false;
                        bevitelObj.SetActive(false);
                        bevitelObjActive = false;
                        barmikorFelnyithatoE = true;
                        idozito = 0;

                    }
                    else
                    {
                        beirtParancs = bevitel.text.ToLower();

                        if (beirtParancs.Contains("kemencét") && beirtParancs.Contains("kövek"))
                        {
                            if (beirtParancs.Length <= 35)
                            {
                                if (karakterTulajdonok.Contains("Kövek"))
                                {
                                    kemenceHit.SetActive(true);
                                    kemenceHit.GetComponentInChildren<MeshRenderer>().enabled = true;

                                    lerakomod = true;
                                }
                                else
                                {
                                    uzMegjel.megjelenitUzenetet("Tárgy nincs felvéve!");
                                    uzIdo = 0;
                                }
                                Cursor.visible = false;
                                bevitel.text = "";
                                bevitelObj.SetActive(false);
                                bevitelObjActive = false;
                                barmikorFelnyithatoE = true;
                                idozito = 0;
                            }
                            else
                            {
                                uzMegjel.megjelenitUzenetet("Ismeretlen parancs! (kemencekő)");
                                uzIdo = 0;
                            }
                        }
                        else
                        {
                            kesztyuFelveteleSzoveggel();

                            bevitel.text = "";
                            beirtParancs = "";
                            Cursor.visible = false;
                            bevitelObj.SetActive(false);
                            bevitelObjActive = false;
                            barmikorFelnyithatoE = true;
                            idozito = 0;
                        }
                    }
                }
                else if(beolvasztasE == true)
                {

                    if (bevitel.text.Length < 70)
                    {
                        beirtParancs = bevitel.text.ToLower();
                    }
                    else
                    {
                        beirtParancs = bevitel.text.Substring(0, 70).ToLower();
                    }

                    if (beirtParancs.Contains("kulcs") && beirtParancs.Contains("készítek") && beirtParancs.Contains("fémtok"))
                    {
                        if (karakterTulajdonok.Contains("vas öntőforma"))
                        {
                            uzMegjel.megjelenitUzenetet("Elkészítetted a kulcsot!");
                            uzIdo = 0;

                            karakterTulajdonok.Add("kulcs");
                            hozzaadTargyatInventoryhoz(kulcsKep);
                            athuzKepet(vasKep);
                            athuzKepet(femTokKep);
                            beolvasztasE = false;
                            kemencereNez = false;

                            Debug.Log("Kulcs megkapva");
                        }
                        else
                        {
                            uzMegjel.megjelenitUzenetet("Tárgy nincs felvéve!");
                            uzIdo = 0;
                            beolvasztasE = false;
                            closeDownBevitelObj();
                            beirtParancs = "";
                            bevitel.text = "";
                        }
                    }
                    else 
                    {
                        if (!karakterTulajdonok.Contains("kulcs"))
                        {
                            Debug.Log("Nincs kulcs");
                            uzMegjel.megjelenitUzenetet("Ismeretlen parancs! (kulcs - fémtok)");
                            uzIdo = 0;
                            if (!beolvasztasE)
                            {
                                closeDownBevitelObj();
                            }
                            beolvasztasE = false;
                        }
                        else
                        {
                            uzMegjel.megjelenitUzenetet("Elkészítetted a kulcsot!");
                            uzIdo = 0;
                        }
                    }
                }

            }
        }
        else
        {
            RaycastHit figyeloSugar = new RaycastHit();

            ray = kameraFpLatas.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out figyeloSugar, 2.8f))
            {
                figyeltTargy = figyeloSugar.collider.name;

                if (figyeltTargy != "Talaj" && !figyeltTargy.Contains("Fal"))
                {
                    //Debug.Log(figyeltTargy);
                }

                if (megjelenitUzenetet)
                {
                }
                else if (!lerakomod && figyeltTargy == "kemenceHit")
                {
                    targyakSzovege.text = "Kemence";
                    kemencereNez = true;
                }
                else if (figyeltTargy == "parasztHit") 
                {
                    targyakSzovege.text = "Paraszt";
                    parasztraNez = true;
                }
                else if (figyeltTargy == "tejesVodor")
                {
                    targyakSzovege.text = "Tejes Vödör";
                    tejesVodorreNez = true;
                }
                else if (figyeltTargy == "uresVodor")
                {
                    targyakSzovege.text = "Vödör";
                    vodorreNez = true;
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
                else if (figyeltTargy == "kesztyuHit")
                {
                    targyakSzovege.text = "Kesztyű";
                    jatekosKesztyureNez = true;
                }
                else
                {
                    //very crucial
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
                    vodorreNez = false;
                    tejesVodorreNez = false;
                    parasztraNez = false;
                    kemencereNez = false;
                }
            }

            if (inputEngedelyezes == false) 
            {
                inputSzamlalo++;
                if (inputSzamlalo >= 150) 
                {
                    inputEngedelyezes = true;
                }
            }

            if (Input.GetKey("e") && inputEngedelyezes)
            {
                if (felvehetoCsontraNez)
                {
                    karakterTulajdonok.Add("Csont");
                    felvehetoCsontraNez = false;
                    csontObj.SetActive(false);
                    hozzaadTargyatInventoryhoz(csontKep);
                }
                else if (tejesVodorreNez && karakterTulajdonok.Contains("Vödör") && !karakterTulajdonok.Contains("Tejes Vödör"))
                {
                    karakterTulajdonok.Add("Tejes Vödör");
                    tejesVodorreNez = false;
                    tejesVodor.SetActive(false);
                    //karakterTulajdonok.Remove("Vödör");
                    athuzKepet(vodorKep);
                    hozzaadTargyatInventoryhoz(tejesVodorKep);
                    uzMegjel.megjelenitUzenetet("Tárgy felvéve");

                }
                else if (parasztraNez)
                {
                    Cursor.visible = true;
                }
                else if (kemencereNez)
                {
                    Cursor.visible = true;
                }
                else if (vodorreNez)
                {
                    karakterTulajdonok.Add("Vödör");
                    vodorreNez = false;
                    vodorObj.SetActive(false);
                    hozzaadTargyatInventoryhoz(vodorKep);

                }
                else if (jatekosKesztyureNez)
                {
                    karakterTulajdonok.Add("kesztyű");
                    jatekosKesztyureNez = false;
                    kesztyuObj.SetActive(false);
                    hozzaadTargyatInventoryhoz(kesztyuKep);

                }
                else if (felvehetoKovekreNez)
                {
                    karakterTulajdonok.Add("Kövek");
                    felvehetoKovekreNez = false;
                    kovekObj.SetActive(false);
                    hozzaadTargyatInventoryhoz(koKep);
                }
                else if (kutyaraNez)
                {
                    if (!kutyaElindultE)
                    {
                        Cursor.visible = true;
                        bevitelObj.SetActive(true);
                        bevitelObjActive = true;
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
                    hangCsengo.Play();
                    ajtoNyitas();
                }
                else if (urraNez)
                {
                    urBeszel();
                }
                else if (satorraNez)
                {
                    pasiKijonE = true;
                }
                else if (helikopterreNez && !karakterTulajdonok.Contains("Vasdarab"))
                {
                    //Cursor.visible = false;
                    uzMegjel.megjelenitUzenetet("Felvetted a vasdarabot.");
                    uzIdo = 0;
                    karakterTulajdonok.Add("Vasdarab");
                    hozzaadTargyatInventoryhoz(vasKep);

                }
                else if (jatekosTogyreNez)
                {
                    if (karakterTulajdonok.Contains("Vödör") && !karakterTulajdonok.Contains("Tejes Vödör"))
                    {
                        inputEngedelyezes = false;
                        kecskeFejes();
                        togyHitObj.SetActive(false);
                    }
                    else
                    {
                        uzMegjel.megjelenitUzenetet("Tárgy nincs felvéve");
                        uzIdo = 0;
                        Cursor.visible = false;
                        bevitel.text = "";
                        bevitelObj.SetActive(false);
                        bevitelObjActive = false;
                        targyakSzovege.text = uzenet;
                    }
                }
                else if (felvehetoBuzaraNez && !karakterTulajdonok.Contains("Búza"))
                {
                    if (kesztyutJatekosFelhuztaE)
                    {
                        felvehetoBuzaraNez = false;
                        buzaObj.SetActive(false);
                        karakterTulajdonok.Add("Búza");
                        hozzaadTargyatInventoryhoz(buzaKep);
                    }
                    else if (karakterTulajdonok.Contains("kesztyű"))
                    {
                        Cursor.visible = true;
                    }
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
            if (HolgySzerelmesE == false)
            {
                holgyTamadJatekost();
            }
            else 
            {
                holgy.SetActive(false);
            }
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

    private void closeDownBevitelObj()
    {

        Cursor.visible = false;
        bevitelObj.SetActive(false);
        bevitelObjActive = false;
        barmikorFelnyithatoE = true;
        idozito = 0;
    }

    private void kesztyuFelveteleSzoveggel()
    {

        beirtParancs = bevitel.text.ToLower();

        if (bevitel.text.Length < 20)
        {
            beirtParancs = bevitel.text.ToLower();
        }
        else
        {
            beirtParancs = bevitel.text.Substring(0, 20).ToLower();
        }

        if ((beirtParancs.Contains("felhúz") || beirtParancs.Contains("felvesz")) && beirtParancs.Contains("kesztyűt"))
        {
            if (karakterTulajdonok.Contains("kesztyű"))
            {
                uzMegjel.megjelenitUzenetet("Rendben!");
                uzIdo = 0;
                athuzKepet(kesztyuKep);
                //karakterTulajdonok.Remove("kesztyű");
                kesztyutJatekosFelhuztaE = true;
            }
            else
            {
                uzMegjel.megjelenitUzenetet("Tárgy nincs felvéve!");
                uzIdo = 0;
            }
        }
        else
        {
            //if (barmikorFelnyithatoE)
            {
                uzMegjel.megjelenitUzenetet("Ismeretlen parancs! (kesztyű-felhúz bármikorfelnyitható)");
                uzIdo = 0;
            }
        }
    }

    private void hozzaadTargyatInventoryhoz(Image hozzaadandoKep)
    {
        //összes tárgy száma ezzel együtt
        rt = hozzaadandoKep.GetComponent<RectTransform>();
        int alap = 200;
        int hozzaadando = 490;

        if (karakterTulajdonok.Count >= 1 && karakterTulajdonok.Count <= 4) 
        {
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, alap + (hozzaadando * (karakterTulajdonok.Count - 1)), rt.rect.width - 10);

            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 15, rt.rect.height - 10);
        }
        if (karakterTulajdonok.Count >= 5 && karakterTulajdonok.Count <= 8)
        {
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, alap + (hozzaadando * (karakterTulajdonok.Count - 5)), rt.rect.width - 10);

            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 80, rt.rect.height - 10);
        }
    }
    #region alvasjelek
    private void ZbetukRoptetese()
    {
        if (AlvasJelA.transform.position.x < -30)
        {
            roptetEloreAlvasjelA();
            mozgatFolLeAlvasJelA();
            roptetEloreAlvasjelB();
            mozgatFolLeAlvasJelB();
            roptetEloreAlvasjelC();
            mozgatFolLeAlvasJelC();
            roptetEloreAlvasjelD();
            mozgatFolLeAlvasJelD();
            roptetEloreAlvasjelE();
            mozgatFolLeAlvasJelE();
        }
        else
        {
            mehetEAlvasJel = true;
            visszaAllitAlvasJelA();
            visszaAllitAlvasJelB();
            visszaAllitAlvasJelC();
            visszaAllitAlvasJelD();
            visszaAllitAlvasJelE();
        }

    }

    private void mozgatFolLeAlvasJelA()
    {
        if (rnd.Next(100) > 98)
        {
            valtozasAlvasJelA = iranyValtAlvasJel(valtozasAlvasJelA);
        }

        if ((AlvasJelA.transform.position.y + valtozasAlvasJelA) > 0.20f && (AlvasJelA.transform.position.y + valtozasAlvasJelA) < 3f)
        {
            helyAlvasJelA.Set(AlvasJelA.transform.position.x, AlvasJelA.transform.position.y + valtozasAlvasJelA, AlvasJelA.transform.position.z);
            AlvasJelA.transform.position = helyAlvasJelA;
        }

    }
    private void mozgatFolLeAlvasJelB()
    {
        if (rnd.Next(100) > 98)
        {
            valtozasAlvasJelB = iranyValtAlvasJel(valtozasAlvasJelB);
        }

        if ((AlvasJelB.transform.position.y + valtozasAlvasJelB) > 0.20f && (AlvasJelB.transform.position.y + valtozasAlvasJelB) < 3f)
        {
            helyAlvasJelB.Set(AlvasJelB.transform.position.x, AlvasJelB.transform.position.y + valtozasAlvasJelB, AlvasJelB.transform.position.z);
            AlvasJelB.transform.position = helyAlvasJelB;
        }

    }
    private void mozgatFolLeAlvasJelC()
    {
        if (rnd.Next(100) > 98)
        {
            valtozasAlvasJelC = iranyValtAlvasJel(valtozasAlvasJelC);
        }

        if ((AlvasJelC.transform.position.y + valtozasAlvasJelC) > 0.20f && (AlvasJelC.transform.position.y + valtozasAlvasJelC) < 3f)
        {
            helyAlvasJelC.Set(AlvasJelC.transform.position.x, AlvasJelC.transform.position.y + valtozasAlvasJelC, AlvasJelC.transform.position.z);
            AlvasJelC.transform.position = helyAlvasJelC;
        }

    }
    private void mozgatFolLeAlvasJelD()
    {
        if (rnd.Next(100) > 98)
        {
            valtozasAlvasJelD = iranyValtAlvasJel(valtozasAlvasJelD);
        }

        if ((AlvasJelD.transform.position.y + valtozasAlvasJelD) > 0.20f && (AlvasJelD.transform.position.y + valtozasAlvasJelD) < 3f)
        {
            helyAlvasJelD.Set(AlvasJelD.transform.position.x, AlvasJelD.transform.position.y + valtozasAlvasJelD, AlvasJelD.transform.position.z);
            AlvasJelD.transform.position = helyAlvasJelD;
        }

    }
    private void mozgatFolLeAlvasJelE()
    {
        if (rnd.Next(100) > 98)
        {
            valtozasAlvasJelE = iranyValtAlvasJel(valtozasAlvasJelE);
        }

        if ((AlvasJelE.transform.position.y + valtozasAlvasJelE) > 0.20f && (AlvasJelE.transform.position.y + valtozasAlvasJelE) < 3f)
        {
            helyAlvasJelE.Set(AlvasJelE.transform.position.x, AlvasJelE.transform.position.y + valtozasAlvasJelE, AlvasJelE.transform.position.z);
            AlvasJelE.transform.position = helyAlvasJelE;
        }

    }

    private float iranyValtAlvasJel(float valtozasAlvasJel)
    {
        valtozasAlvasJel = rnd.Next(10) - 5;
        valtozasAlvasJel /= 200f;
        //Debug.Log(valtozasAlvasJel + "f");

        return valtozasAlvasJel;
    }

    private void visszaAllitAlvasJelA()
    {
        helyAlvasJelA.Set(-41f, 1.6f, AlvasJelA.transform.position.z);
        AlvasJelA.transform.position = helyAlvasJelA;
    }
    private void visszaAllitAlvasJelB()
    {
        helyAlvasJelB.Set(-40f, 2.58f, AlvasJelB.transform.position.z);
        AlvasJelB.transform.position = helyAlvasJelB;
    }
    private void visszaAllitAlvasJelC()
    {
        helyAlvasJelC.Set(-39.85f, 2.2f, AlvasJelC.transform.position.z);
        AlvasJelC.transform.position = helyAlvasJelC;
    }
    private void visszaAllitAlvasJelD()
    {
        helyAlvasJelD.Set(-38f, 1.92f, AlvasJelD.transform.position.z);
        AlvasJelD.transform.position = helyAlvasJelD;
    }
    private void visszaAllitAlvasJelE()
    {
        helyAlvasJelE.Set(-38.8f, 1.76f, AlvasJelE.transform.position.z);
        AlvasJelE.transform.position = helyAlvasJelE;
    }

    private void roptetEloreAlvasjelA()
    {
        helyAlvasJelA.Set(AlvasJelA.transform.position.x + sebessegAlvasJel, AlvasJelA.transform.position.y,
                                                AlvasJelA.transform.position.z);
        AlvasJelA.transform.position = helyAlvasJelA;
    }
    private void roptetEloreAlvasjelB()
    {
        helyAlvasJelB.Set(AlvasJelB.transform.position.x + sebessegAlvasJel, AlvasJelB.transform.position.y,
                                                AlvasJelB.transform.position.z);
        AlvasJelB.transform.position = helyAlvasJelB;
    }
    private void roptetEloreAlvasjelC()
    {
        helyAlvasJelC.Set(AlvasJelC.transform.position.x + sebessegAlvasJel, AlvasJelC.transform.position.y,
                                                AlvasJelC.transform.position.z);
        AlvasJelC.transform.position = helyAlvasJelC;
    }
    private void roptetEloreAlvasjelD()
    {
        helyAlvasJelD.Set(AlvasJelD.transform.position.x + sebessegAlvasJel, AlvasJelD.transform.position.y,
                                                AlvasJelD.transform.position.z);
        AlvasJelD.transform.position = helyAlvasJelD;
    }
    private void roptetEloreAlvasjelE()
    {
        helyAlvasJelE.Set(AlvasJelE.transform.position.x + sebessegAlvasJel, AlvasJelE.transform.position.y,
                                                AlvasJelE.transform.position.z);
        AlvasJelE.transform.position = helyAlvasJelE;
    }

    #endregion

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

    private void kecskeFejes()
    {
        jatekosAnimaltObj.SetActive(false);
        FoKod.jatekosMozoghatE = false;
        fejoJatekos.SetActive(true);
        tejesVodor.SetActive(true);
    }

    private void urBeszel()
    {
        //ha a játékos az ajtót nem látta még, vagy már megvan a öntőforma (és a kutyával kapcsolatban nincs már teendő)
        if (!ajtotLattaMar || karakterTulajdonok.Contains("vas öntőforma"))
        {
            uzMegjel.megjelenitUzenetet("Jó napot!");
            uzIdo = 0;
        }
        else
        {
            uzMegjel.megjelenitUzenetet("Kinyitni az ajtót? Régen nem járt már arra senki. Már csak a " +
                "fémtokja maradt meg,a kulcsot három évszázada senki nem látta.");
            uzIdo = -450;
            Cursor.visible = true;
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
