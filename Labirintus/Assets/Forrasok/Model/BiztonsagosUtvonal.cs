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
