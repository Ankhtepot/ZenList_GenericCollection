using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollection {
    class Trojuhelnik {
        private float aside;
        private float bside;
        private float cside;
        public float Aside { get => aside; set => aside = (float)value; }
        public float Bside { get => bside; set => bside = (float)value; }
        public float Cside { get => cside; set => cside = (float)value; }

        public Trojuhelnik(float side) : this(side, side) { }
        public Trojuhelnik(float aside, float bside, trKind kind = trKind.normal) : this(aside, aside, bside) {
            if (kind == trKind.right) {
                this.Bside = Cside;
                this.Cside = (float)Math.Sqrt(Math.Pow(this.Aside, 2) + Math.Pow(this.Bside, 2));
            }
        }
        public Trojuhelnik(double side) : this((float)side) { }
        public Trojuhelnik(double aside, double bside, trKind kind = trKind.normal) : this((float)aside, (float)bside) {
            if (kind == trKind.right) {
                this.Bside = Cside;
                this.Cside = (float)Math.Sqrt(Math.Pow(this.Aside, 2) + Math.Pow(this.Bside, 2));
            }
        }

        //public Trojuhelnik(float side) { this.Aside = side; this.Bside = side; this.Cside = side; }
        //public Trojuhelnik(float side1, float side2) {
        //    this.Aside = side1; this.Bside = side1; this.Cside = side2;
        //}

        public Trojuhelnik(float aside, float bside, float cside, trKind kind = trKind.normal) {
            this.Aside = aside; this.Bside = bside; this.Cside = cside;
        }
        public Trojuhelnik(double aside, double bside, double cside, trKind kind = trKind.normal) {
            this.Aside = (float)aside; this.Bside = (float)bside; this.Cside = (float)cside;
        }

        public override string ToString() {
            return String.Format("A-side: {0}, B-side: {1}, C-side: {2}.", this.Aside.ToString(), this.Bside.ToString(), this.Cside.ToString());
        }

        private bool validateTriangle() {
            Boolean result = false;
            if (aside + bside > cside && aside + cside > bside && bside + cside > aside) result = true;
            return result;
        }

        public float Obvod() {
            if (validateTriangle()) return Aside + Bside + Cside;
            Console.WriteLine("Not real triangle."); return -1;
        }

        public enum trKind { normal, right }
    }

    class TrojuhelnikPr : Trojuhelnik {
        public TrojuhelnikPr(double sideA, double sideB) : base(sideA, sideB) {
            this.Bside = (float)sideB;
            this.Cside = (float)Math.Sqrt(Math.Pow(this.Aside, 2) + Math.Pow(this.Bside, 2));
        }
    }
}
