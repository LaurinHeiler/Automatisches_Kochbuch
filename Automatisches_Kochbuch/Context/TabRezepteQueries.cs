using Automatisches_Kochbuch.Model;
using Castle.DynamicProxy.Generators;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Context
{
    public static class TabRezepteQueries
    {
        public static IEnumerable<TabRezepte> MoeglicheRezepte(this IDataContext context, SortedSet<int> zutatenVomUser)
        {

            //IEnumerable<int> richtige = zutatenVomUser.Intersect(ZutatenSet);
            //Dummy Daten, um die Eingabe eines Users zu Simulieren.
            zutatenVomUser.Add(10);
            zutatenVomUser.Add(31);
            zutatenVomUser.Add(37);
            zutatenVomUser.Add(34);
            zutatenVomUser.Add(31);
            zutatenVomUser.Add(17);
            zutatenVomUser.Add(1);
            zutatenVomUser.Add(10);
            zutatenVomUser.Add(21);


            //-----------------------------------------------------------------

            //var AnzahlRezepte = context.TabRezepte.Select(t => t.Id).Count();

            //Kürzere Variante um RZ zu bestimmen (RZ gibt die notwendige Menge an Zutaten pro Rezept an.)
            var RZOhneSchleife = context.LnkTabRezeptZutaten.GroupBy(t => t.IdRezept)
                .Select(z => new { Rezepte = z.Key, AnzahlRezepte = z.Count() })
                .OrderBy(z => z.Rezepte)
                .Select(z => z);

            List<int> RZNew = new List<int>();
            foreach (var item in RZOhneSchleife)
            {
                RZNew.Add(item.AnzahlRezepte);
            }

            //Kürzere Variante um RU zu bestimmen (RU gibt die vorhandenen Zutaten des Users pro Rezept an.)
            var RUOhneSchleife = context.LnkTabRezeptZutaten.GroupBy(t => t.IdRezept)
                .Select(z => new { Rezepte = z.Key, ZutatenProRezept = z.GroupBy(k => k.IdZutaten)
                .Select(k => new {Zutaten = k.Key }) })
                .OrderBy(z => z.Rezepte)
                .Select(z => z);



            List<int> RUNew = new List<int>();

            foreach (var item2 in RUOhneSchleife)
            {
                IEnumerable<int> Richtige = new SortedSet<int>();
                SortedSet<int> ZutatenProRez = new SortedSet<int>();
                foreach (var item in item2.ZutatenProRezept)
                {
                    ZutatenProRez.Add(item.Zutaten);
                }

                Richtige = ZutatenProRez.Intersect(zutatenVomUser);
                RUNew.Add(Richtige.Count());

            }


            //Trefferquote (TQ) wird berechnet und die Rezepte mit 80% oder mehr werden dann ausgegeben.
            double[] TQ = new double[context.TabRezepte.Count()];
            for (int n = 1; n <= context.TabRezepte.Count(); n++)
            {
                TQ[n - 1] += Convert.ToDouble(RUNew[n-1]) / Convert.ToDouble(RZNew[n-1]);
            }

            //Die Rezepte mit über 80% Trefferquote werden dem User angezeigt. 
            int w = 1;
            List<TabRezepte> MoeglicheRezepte = new List<TabRezepte>();

            foreach (var item in TQ)
            {

                if (item > 0.8)
                {
                    var rezept = context.TabRezepte.Where(r => r.Id == w);

                    TabRezepte Rezept = null;
                    foreach (var x in rezept)
                    {
                        Rezept = x;
                    }

                    MoeglicheRezepte.Add(Rezept);
                }

                w++;

            }

            return MoeglicheRezepte.Select(x => { x.Bild = null; return x; }); //Bild auf null, weil es nur den Code anzeigt und nicht das Bild
        }

        public static string MehrZumGericht(this IDataContext context, int id, int AnzahlPersonen)
        {
            //var ZutatenVonRezeptID = context.LnkTabRezeptZutaten.Where(r => r.IdRezept == id).Select(r => r.IdZutaten);

            List<string> ZutatenVonRezeptList = new List<string>();
            var ZutatenVonRezept = context.LnkTabRezeptZutaten.Where(r => r.IdRezept == id).Select(r => r.IdZutatenNavigation.Zutat);

            foreach (var item in ZutatenVonRezept)
            {
                ZutatenVonRezeptList.Add(item);
            }
            

            var MengeVonRezeptZutaten = context.LnkTabRezeptZutaten.Where(r => r.IdRezept == id).Select(r => r.Menge);
            //var EinheitenVonRezeptZutaten = context.LnkTabRezeptZutaten.Where(r => r.IdRezept == id).Select(r => r.E);


            //List<string> ZutatenVonRezept = new List<string>();

            //foreach (var item in ZutatenVonRezeptID)
            //{
            //    var IDZuZutat = context.TabZutaten.Where(z => z.Id == item).Select(z => z.Zutat);

            //    foreach (var item2 in IDZuZutat)
            //    {
            //        ZutatenVonRezept.Add(item2);
            //    }


            //}

            List<decimal> MengeVonRezept = new List<decimal>();

            foreach (var item in MengeVonRezeptZutaten)
            {
                MengeVonRezept.Add(item);
            }

            string ausgabe = null;
            for (int i = 0; i < ZutatenVonRezept.Count(); i++)
            {
                ausgabe = ausgabe + (MengeVonRezept[i] * AnzahlPersonen) + " " + ZutatenVonRezeptList[i] + " | ";
            }


            return ausgabe;

        }
    }
}
