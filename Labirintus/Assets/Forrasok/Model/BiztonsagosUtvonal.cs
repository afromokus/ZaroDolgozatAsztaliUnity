using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model
{
    class BiztonsagosUtvonal
    {
        int hossz = 15;
        int szelesseg = 5;
        int i = 0;
        System.Random rnd;
        int[] utvonalTomb;
        List<float> jatekosMellettiHalalFejek;
        List<float> osszesKoord;
        bool jatekosSerulE;
        List<float> halalFejZLista;

        public BiztonsagosUtvonal()
        {
            rnd = new System.Random();
            utvonalTomb = new int[hossz];

            utvonalTomb[0] = rnd.Next(0, szelesseg);

            i = 1;

            while (i < utvonalTomb.Length)
            {
                utvonalTomb[i] = generalKovUt(utvonalTomb[i - 1]);
                i++;
            }
        }

        public List<float> getSzomszedosKoordinatak()
        {
            /*List<float> szomszedosKoordinatak = szamolSzomszedosKoord(utvonalTomb);

            for (i = 0; i < szomszedosKoordinatak.Count - 1; i += 2)
            {
                Debug.Log("(" + szomszedosKoordinatak[i] + "; " + szomszedosKoordinatak[i + 1] + ")");
            }*/
            return szamolSzomszedosKoord(utvonalTomb);
        }

        private bool jatekosTeruletenVanE(float jatekosX, float jatekosZ)
        {
            // -42--- 27 x     -25---- -11 z
            if ((jatekosX >= -42 && jatekosX <= 24.7) && jatekosZ >= -25 && jatekosZ <= -11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private List<float> keresHalalFejZKoord(float jatekosX, List<float> osszesKoord)
        {
            float keresettX = kerekites(jatekosX, 4.6f);
            halalFejZLista = new List<float>();

            //Debug.Log("Koponyák keresése ezen az X-en:\t" + keresettX);

            for (i = 0; i < osszesKoord.Count; i += 2)
            {
                if (osszesKoord[i] == keresettX)
                {
                    halalFejZLista.Add(osszesKoord[i + 1]);
                }
            }

            return halalFejZLista;
        }

        private float kerekites(float szam, float rendszer)
        {
            float eredmeny = 0f;

            float tizedesSzam = (float)Math.Round(szam, 1);
            eredmeny = tizedesSzam * 10;
            rendszer = rendszer * 10;

            i = 1;
            if (szam > -0.6)
            {
                while (eredmeny % rendszer != 17)
                {
                    eredmeny = tizedesSzam * 10 + i;
                    if (eredmeny % rendszer != 17)
                    {
                        eredmeny = tizedesSzam * 10 - i;
                    }
                    i++;
                }
            }
            else if (szam <= -0.6)
            {
                while (eredmeny % rendszer != -29)
                {
                    eredmeny = tizedesSzam * 10 + i;
                    if (eredmeny % rendszer != -29)
                    {
                        eredmeny = tizedesSzam * 10 - i;
                    }
                    i++;
                }
            }
            return eredmeny / 10;
        }

        public bool serulEJatekos(float jatekosX, float jatekosZ, bool kutyaMegkaptaECsontot)
        {
            //Debug.Log("Játékos:\t" + jatekosZ);
            if (osszesKoord == null)
            {
                osszesKoord = szamolSzomszedosKoord(utvonalTomb);
            }

            jatekosSerulE = false;

            if (jatekosTeruletenVanE(jatekosX, jatekosZ))
            {
                if (kutyaMegkaptaECsontot)
                {
                    jatekosMellettiHalalFejek = keresHalalFejZKoord(jatekosX, osszesKoord);

                    if (jatekosMellettiHalalFejek.Count == 2)
                    {
                        //Debug.Log(jatekosMellettiHalalFejek[0]);
                        //Debug.Log(jatekosMellettiHalalFejek[1]);
                        if (jatekosZ < jatekosMellettiHalalFejek[1] + 0.7f ||
                                              jatekosZ > jatekosMellettiHalalFejek[0] - 0.7f)
                        {
                            jatekosSerulE = true;
                        }
                    }
                    else
                    {
                        if (jatekosMellettiHalalFejek[0] == (float)-20.8)
                        {
                            if (jatekosZ > -20.8f - 0.7f)
                            {
                                jatekosSerulE = true;
                            }
                        }
                        else
                        {
                            if (jatekosZ < -15.2f + 0.7f)
                            {
                                jatekosSerulE = true;
                            }
                        }
                    }

                }
                else
                {
                    jatekosSerulE = true;
                }
            }
            else
            {
                jatekosSerulE = false;
            }

            return jatekosSerulE;
        }
        private List<float> szamolSzomszedosKoord(int[] tomb)
        {
            List<float> szomszedosKoordinatak = new List<float>();
            List<int> szomszedok;

            i = 0;

            for (i = 0; i < tomb.Length; i++)
            {
                if (tomb[i] == 0)
                {
                    szomszedok = new List<int>();
                    szomszedok.Add(1);
                }
                else
                {
                    if (tomb[i] < szelesseg - 1)
                    {
                        szomszedok = new List<int>();
                        szomszedok.Add(tomb[i] - 1);
                        szomszedok.Add(tomb[i] + 1);
                    }
                    else
                    {
                        szomszedok = new List<int>();
                        szomszedok.Add(3);
                    }
                }
                for (int j = 0; j < szomszedok.Count; j++)
                {
                    double[] koordTomb = szamolKoordinata(i, szomszedok[j], tomb);

                    szomszedosKoordinatak.Add((float)(koordTomb[0] + koordTomb[1]) / 2);
                    szomszedosKoordinatak.Add((float)(koordTomb[2] + koordTomb[3]) / 2);
                }
            }

            return szomszedosKoordinatak;
        }

        public List<double[]> getKoordinatak()
        {
            return koordinatakSzamitasa(utvonalTomb);
        }

        public List<float> lekerBiztonsagosKoord()
        {
            List<float> biztKoord = new List<float>();
            List<double[]> koordHatarok = koordinatakSzamitasa(utvonalTomb);

            for (i = koordHatarok.Count - 1; i >= 0; i--)
            {
                biztKoord.Add((float)((koordHatarok[i][0] + koordHatarok[i][1]) / 2));
                biztKoord.Add((float)((koordHatarok[i][2] + koordHatarok[i][3]) / 2));
            }

            /*foreach (float koordinata in biztKoord)
            {
                Debug.Log(koordinata);
            }*/

            return biztKoord;
        }

        private List<double[]> koordinatakSzamitasa(int[] utvonalTomb)
        {
            i = 0;
            List<double[]> osszesKoordinata = new List<double[]>();
            while (i < utvonalTomb.Length)
            {
                osszesKoordinata.Add(szamolKoordinata(i, utvonalTomb[i], utvonalTomb));
                i++;
            }

            return osszesKoordinata;
        }

        private double[] szamolKoordinata(int x, int z, int[] tomb)
        {
            int kezdX = -42;
            int vegX = 27;
            int kezdZ = -11;
            int vegZ = -25;

            double hosszTor = (vegX - kezdX) / (double)tomb.Length;
            double szelTor = (kezdZ - vegZ) / (double)szelesseg;

            double biztZonX0 = Math.Round(kezdX + hosszTor * x, 2);
            double biztZonX1 = Math.Round(kezdX + hosszTor * (x + 1), 2);
            double biztZonZ0 = Math.Round(-11 - szelTor * z, 2);
            double biztZonZ1 = Math.Round(-11 - szelTor * (z + 1), 2);

            double[] koordinatak = new double[4];
            koordinatak[0] = biztZonX0;
            koordinatak[1] = biztZonX1;
            koordinatak[2] = biztZonZ0;
            koordinatak[3] = biztZonZ1;

            //return koordinatak[0] + ", " + koordinatak[1] + ";\t\t\t" + koordinatak[2] + ", " + koordinatak[3];
            return koordinatak;
        }

        private int generalKovUt(int elozohely)
        {
            int veletlen;

            if (elozohely < szelesseg - 1 && elozohely > 0)
            {
                veletlen = rnd.Next(0, 3) - 1;
                return elozohely + veletlen;
            }
            else
            {
                veletlen = rnd.Next(0, 2);
                if (elozohely == 0)
                {
                    return elozohely + veletlen;
                }
                else
                {
                    return elozohely - veletlen;
                }
            }
        }

    }
}
