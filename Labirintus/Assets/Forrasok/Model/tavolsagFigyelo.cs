using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model
{
    class TavolsagFigyelo
    {
        Ray sugar;

        Vector3 sugarHossz;

        public TavolsagFigyelo(Ray sugar)
        {
            this.sugar = sugar;
            this.sugarHossz = sugar.origin;
        }

        public TavolsagFigyelo(Vector3 kezdoPont, Vector3 eltolas)
        {
            this.sugar = new Ray(kezdoPont, eltolas);
            this.sugarHossz = eltolas;
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

        public bool utkozikE()
        {
            return Physics.Raycast(sugar, sugarHossz.x);
        }

        public void setSugarHossz(float x)
        {
            sugarHossz.Set(x, 0, 0);
        }

        public void rajzolFigyelo()
        {
            Debug.DrawLine(sugar.origin, sugar.origin + sugarHossz);
        }

    }
}
