using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model
{
    class VektorSugar
    {
        Ray sugar;

        Vector3 sugarHossz;

        public VektorSugar(Ray sugar)
        {
            this.sugar = sugar;
            this.sugarHossz = sugar.origin;
        }

        public VektorSugar(Vector3 kezdoPont, Vector3 eltolas)
        {
            this.sugar = new Ray(kezdoPont, eltolas);
            this.sugarHossz = eltolas;
        }

        public VektorSugar(Vector3 kezdoPont, float eltolas)
        {
            this.sugarHossz = new Vector3(eltolas, 0, 0);
            this.sugar = new Ray(kezdoPont, sugarHossz);
        }

        public Ray getSugar()
        {
            return sugar;
        }

        public void setSugar(Ray sugar)
        {
            this.sugar = sugar;
        }

        public void setSugarOrigin(Vector3 origin)
        {
            sugar.origin = origin;
        }

        public void setSugarOrigin(Vector3 origin, float eltolas)
        {
            sugar.origin = origin + new Vector3(eltolas, 0, 0);
        }

        public bool utkozikEX()
        {
            return Physics.Raycast(sugar,Mathf.Abs(sugarHossz.x));
        }

        public bool utkozikE(float maxDistance)
        {
            return Physics.Raycast(sugar, maxDistance);
        }

        public void setSugarHosszX(float x)
        {
            sugarHossz.Set(x, 0, 0);
        }

        public void rajzolFigyelo()
        {
            Debug.DrawLine(sugar.origin, sugar.origin + sugarHossz, Color.blue);
        }

        public void rajzolFigyelo(Color szin)
        {
            Debug.DrawLine(sugar.origin, sugar.origin + sugarHossz, szin);
        }

    }
}
