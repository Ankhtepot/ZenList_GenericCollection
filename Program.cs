using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollection {
    class Program {
        static void Main(string[] args) {
            Zenlist<int> ColInt = new Zenlist<int>("ColInt") { 1, 2, 3, 4, 5, 6, 7 };
            Console.WriteLine(ColInt.List());
            foreach (var oxxo in ColInt) Console.WriteLine(oxxo.ToString());
            ColInt.Insert(5, 20);
            Console.WriteLine(ColInt.List());
            foreach (var oxxo in ColInt) Console.WriteLine(oxxo.ToString());
            ColInt.Add(21); ColInt.Insert(7, 666); ColInt.Add(667);
            Console.WriteLine(ColInt.List());
            foreach (var oxxo in ColInt) Console.WriteLine(oxxo.ToString());

            ColInt.Insert(15, 345);
            ColInt.Remove(666);
            Console.WriteLine(ColInt.List());
            foreach (var oxxo in ColInt) Console.WriteLine(oxxo.ToString());
            //ColInt.TidyUp();
            //Console.WriteLine(ColInt.List());
            //foreach (var oxxo in ColInt) Console.WriteLine(oxxo.ToString());
            Zenlist<int> intCol = new Zenlist<int>(10, "intCol");
            intCol[0] = 5;
            intCol[9] = 10;
            intCol.Add(666);
            intCol.Insert(1, 25);
            Console.WriteLine("After inserts of all kind:\n" + intCol.List());
            intCol.TidyUp();
            Console.WriteLine("After TidyUp(): \n" + intCol.List());
            intCol.Clear();
            Console.WriteLine("After CLear(): \n" + intCol.List());
            intCol.Add(5); intCol.Add(10); intCol.Add(10);
            intCol.Add(15); intCol.Add(15); intCol.Add(20);
            Console.WriteLine("After re-fill:\n" + intCol.List());
            intCol.RemoveAt(3);
            Console.WriteLine("After removing [3]:\n" + intCol.List());
            Console.WriteLine("Contains() test: intCol.Contains(15): {0}",
                intCol.Contains(15).ToString());
            Console.WriteLine("Contains() test2: intCol.Contains(7): {0}",
                   intCol.Contains(7).ToString());
            int testPeek = intCol.PeekAt(1, Zenlist<int>.MovoTo.first);
            Console.WriteLine("After PeekAt(1,first): int testPeek={0}", testPeek.ToString());
            Console.WriteLine(intCol.List());
            testPeek = intCol.PeekAt(3);
            Console.WriteLine("After PeekAt(3)(last): int testPeek={0}", testPeek.ToString());
            Console.WriteLine(intCol.List());
            int[] intTest = intCol.ToArray();
            Console.WriteLine("Arr intTest after = intCol.ToArray():");
            Console.WriteLine(PM.ListIntArray(intTest), "intTest");
            int itemsRemoved = intCol.RemoveAll(x => x == 10);
            Console.WriteLine("Number of items removed after .RemoveAll(x=>x==10): {0}", itemsRemoved);
            Console.WriteLine(intCol.List());
            Zenlist<Trojuhelnik> trCol = new Zenlist<Trojuhelnik>("trCol");
            Console.WriteLine(trCol.List());
            trCol.Add(new Trojuhelnik(5));
            trCol.Insert(3, new Trojuhelnik(3, 4, 5));
            Console.WriteLine(trCol.List());
            foreach (Trojuhelnik oxxo in trCol) Console.WriteLine(oxxo.ToString());


            Console.WriteLine("\n");
            Zenlist<string> stringCol = new Zenlist<string>("stringCol");
            stringCol[0] = "ano";
            stringCol[7] = "ne";
            stringCol.Add("nový");
            Console.WriteLine("Měl by následovat stringCol foreach.");
            foreach (string oxxo in stringCol) Console.WriteLine(oxxo.ToString());
            Console.WriteLine(stringCol.List());

            Zenlist<int> intCol2 = new Zenlist<int>("intCol2");
            intCol2[0] = 1;
            intCol2[7] = 18;
            Console.WriteLine(intCol2.List());
            foreach (int oxxo in intCol2) Console.WriteLine(oxxo.ToString());

            //Console.WriteLine(intCol.List());

            Console.WriteLine(stringCol.List());

            Zenlist<string> stringCol2 = new Zenlist<string>(stringCol, "stringCol2");
            Console.WriteLine("Copy os stringCol into strinCol2: "+ stringCol2.List());
            stringCol2[2] = "možná";
            Console.WriteLine(stringCol2.List());
            foreach (string oxxo in stringCol2) Console.WriteLine(oxxo.ToString());

            Trojuhelnik[] triangles = new Trojuhelnik[] { new Trojuhelnik(5), new Trojuhelnik(4, 5, 6) };
            foreach (Trojuhelnik oxxo in triangles) Console.WriteLine(oxxo.ToString());
            Zenlist<Trojuhelnik> trCol2 = new Zenlist<Trojuhelnik>(triangles, "trCol2");
            Console.WriteLine(trCol2.List());
            foreach (Trojuhelnik oxxo in trCol2) Console.WriteLine(oxxo.ToString());
            Zenlist<Trojuhelnik> trCol3 = new Zenlist<Trojuhelnik>(trCol, "trCol3");
            Console.WriteLine(trCol3.List());
            foreach (Trojuhelnik oxxo in trCol3) Console.WriteLine(oxxo.ToString());
            Console.WriteLine("\n");
            List<string> strList = new List<string> { "Jirka", "Petr", "Pavel" };
            foreach (string oxxo in strList) Console.WriteLine(oxxo);
            Zenlist<string> strZen = new Zenlist<string>(strList, "strList");
            Console.WriteLine(strZen.List());
            strZen.Add("another");
            foreach (String oxxo in strZen) Console.WriteLine(oxxo.ToString());

        }
    }
}
