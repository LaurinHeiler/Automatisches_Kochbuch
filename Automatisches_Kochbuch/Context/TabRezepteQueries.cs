﻿using Automatisches_Kochbuch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Context
{
    public static class TabRezepteQueries
    {
        public static IEnumerable<TabRezepte> MoeglicheRezepte(this IDataContext context, SortedSet<int> zutatenVomUser)
        {

            //IEnumerable<int> richtige = zutatenVomUser.Intersect(ZutatenSet);

            //zutatenVomUser.Add(10);
            //zutatenVomUser.Add(31);
            //zutatenVomUser.Add(37);
            //zutatenVomUser.Add(34);
            //zutatenVomUser.Add(31);
            //zutatenVomUser.Add(17);
            //zutatenVomUser.Add(1);
            //zutatenVomUser.Add(10);
            //zutatenVomUser.Add(21);


            //-----------------------------------------------------------------

            //var 

            //RZ gibt die notwendige Menge an Zutaten pro Rezept an.
            int[] RZ = new int[context.TabRezepte.Count()];
            //RU gibt die vorhandenen Zutaten des Users pro Rezept an.
            int[] RU = new int[context.TabRezepte.Count()];

            for (int n = 1; n <= context.TabRezepte.Count(); n++)
            {

                for (int i = 1; i <= context.LnkTabRezeptZutaten.Count(); i++)
                {

                    var RezeptID = context.LnkTabRezeptZutaten.Where(r => r.Id == i).Select(r => r.IdRezept);
                    int ID_Rezept = 0;
                    foreach (var item in RezeptID)
                    {
                        ID_Rezept = item;
                    }

                    var ZutatID = context.LnkTabRezeptZutaten.Where(r => r.Id == i).Select(r => r.IdZutaten);
                    int ID_Zutat = 0;
                    foreach (var item in ZutatID)
                    {
                        ID_Zutat = item;
                    }

                    if (ID_Rezept == n)
                    {
                        RZ[n - 1] += 1;

                        if (zutatenVomUser.Contains(ID_Zutat))
                        {
                            RU[n - 1] += 1;
                        }
                    }
                }
            }


            //Trefferquote (TQ) wird berechnet und die Rezepte mit 80% oder mehr werden dann ausgegeben.
            double[] TQ = new double[context.TabRezepte.Count()];
            for (int n = 1; n <= context.TabRezepte.Count(); n++)
            {
                TQ[n - 1] += Convert.ToDouble(RU[n-1]) / Convert.ToDouble(RZ[n-1]);
            }

            int w = 1;
                

            foreach (var item in TQ)
            {



                //return context.TabRezepte.Where(r => r.Id == w);
                if (item > 0.8)
                {
                    return context.TabRezepte.Where(r => r.Id == w);
                }

                w++;

            }



            return context.TabRezepte.ToList();
        }
    }
}
