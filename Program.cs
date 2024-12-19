using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kalapacsvetes
{
    class Sportolo
    {
        public int Helyezes;
        public double Eredmeny;
        public string Nev;
        public string Kod;
        public string Helyszin;
        public string Datum;

        public Sportolo(int helyezes, double eredmeny, string nev, string kod, string helyszin, string datum)
        {
            Helyezes = helyezes;
            Eredmeny = eredmeny;
            Nev = nev;
            Kod = kod;
            Helyszin = helyszin;
            Datum = datum;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Létrehozok egy streamreadert meg egy listát amiben a sporolók lesznek tárolva
            StreamReader sr = new StreamReader("kalapacsvetes.txt");
            List<Sportolo> sportolok = new List<Sportolo>();

            //Beolvasom a txt fájl tartalmát

            byte round = 0;
            while (!sr.EndOfStream)
            {
                if (round != 0)
                {
                    //Beolvasom az adatokat soronként
                    string[] adat = sr.ReadLine().Split(';');
                    //Példányosítom a sportolókat és eltárolom őket a listában
                    sportolok.Add(new Sportolo(int.Parse(adat[0]), Convert.ToDouble(adat[1]), adat[2], adat[3], adat[4], adat[5]));
                }
                else
                {
                    //Itt beolvasom az első sort, hogy ne csináljunk belőle sportolót, mert az a fejléc
                    round++;
                    sr.ReadLine();
                }
            }

            //Kiszámolom az átlagot
            double osszeg = 0;
            for (int i = 0; i < sportolok.Count; i++)
            {
                osszeg += sportolok[i].Eredmeny;
            }
            double atlag = Math.Round(osszeg / sportolok.Count,2);
            Console.WriteLine("Az eredmények átlaga: "+ atlag);

            //Kiszámolom a minimumot és maximumot
            double min = sportolok[0].Eredmeny;
            double max = sportolok[0].Eredmeny;
            for (int i = 0;i < sportolok.Count;i++)
            {
                if (sportolok[i].Eredmeny > max) max = sportolok[i].Eredmeny;
                else if (sportolok[i].Eredmeny < min) min = sportolok[i].Eredmeny;
            }
            Console.WriteLine("A legkisebb eredmény: "+min);
            Console.WriteLine("A legnagyobb eredmény: "+ max);

            double terjedelem = max-min;
            Console.WriteLine("A terjedelem: "+terjedelem);

            Console.WriteLine();
            Console.Write("Sporolók kereséséhez adjon meg egy országkódot: ");
            string kod = Console.ReadLine();
            Console.Write("és egy évszámot: ");
            int ev = int.Parse(Console.ReadLine());

            for (int i = 0; i < sportolok.Count;i++)
            {
                //Megnézem, hogy egy sportolónak egyezik-e a kódja, majd a sportolóhoz társított dátumot '.'-ként feladarabolom, majd annak az első elemét, ami az évszám, átkonvertálom intté, mivel a program elején stringként mentettem el
                if (sportolok[i].Kod == kod && int.Parse(sportolok[i].Datum.Split('.')[0]) == ev) Console.WriteLine("Név: " + sportolok[i].Nev+", eredménye: " + sportolok[i].Eredmeny+", ország: "+ sportolok[i].Kod+", dátum: " + sportolok[i].Datum);
            }
        }
    }
}
