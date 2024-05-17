using System.Diagnostics.Tracing;

namespace tehenek
{
    
    internal class Program
    {
       
        class Tehen : IEquatable<Tehen>
        {
            public string Id { get; private set; }
            public int[] Mennyisegek { get; private set; }
            public int HetiTej { get; private set; }
            public int HetiAtlag { get; private set; }
            public bool Equals(Tehen masik)
            {
                return masik != null && masik.Id == this.Id;
            }
            int menny_nem_null; int napok = 0;
            public void EredmenytRogzit(string nap, string menyiseg)
            {
                Mennyisegek[int.Parse(nap)] = int.Parse(menyiseg);
                HetiTej += int.Parse(menyiseg);
                napok++;
                if (int.Parse(menyiseg) != 0)
                {
                    menny_nem_null++; 
                }
                if (napok == 7 || menny_nem_null > 3)
                { 
                    HetiAtlag = HetiTej / menny_nem_null; 
                }  
                else HetiAtlag = -1;
            }
            public Tehen(string id)
            {
                Id = id;
                Mennyisegek = new int[7];
            }
        }
        static void Main(string[] args)
        {
            List<Tehen> happyCows = new List<Tehen>();
            StreamReader sr = File.OpenText("hozam.txt");
            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                string id = sor.Split(";")[0];
                string nap = sor.Split(";")[1];
                string mennyiseg = sor.Split(";")[2];
                Tehen aktTehen = new Tehen(id);
                if (!happyCows.Contains(aktTehen)) happyCows.Add(aktTehen);
                int index = happyCows.IndexOf(aktTehen);
                happyCows[index].EredmenytRogzit(nap, mennyiseg);
            }
            int n = happyCows.Count;
            Console.WriteLine($"3. feladat");
            Console.WriteLine($"Az állomány {n} tehén adatait tartalmazza.");
            int hany_sor = 0;
            StreamWriter sw = new StreamWriter("joltejelok.txt");
            foreach (var s in happyCows)
            {
                if (s.HetiAtlag != -1)
                {
                    //Console.WriteLine($"{s.Id} {s.HetiAtlag}");
                    sw.WriteLine($"{s.Id} {s.HetiAtlag}");
                    hany_sor++;
                }
            }
            sw.Close();
            Console.WriteLine($"6. feladat: \n{hany_sor} db sort írtam az állományba.");
            Console.WriteLine("7. feladat:");
            Console.WriteLine("Kérem, adaja meg a tehén azonosítóját:");
            string azon = Console.ReadLine();
            int hossz = azon.Length;
            int utodok = 0;
            foreach (var i in happyCows)
            {
                if(i.Id.Substring(0,hossz) == azon && i.Id.Length>=4) utodok++;
            }
            Console.WriteLine($"A leszármazottak száma: {utodok}");
            if (utodok==0) Console.WriteLine("A megadott tehénnek nincs leszármazottja");
        }
    }
}
