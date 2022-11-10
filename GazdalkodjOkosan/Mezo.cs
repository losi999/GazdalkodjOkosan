using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan
{
    public class Mezo
    {
        public int Sorszam { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public String Szoveg { get; set; }
        public int Hatas { get; set; }
        public int Ertek { get; set; }
        public Dictionary<int, List<Mezo>> Dobas { get; set; }
        public List<Jatekos> Jatekosok { get; set; }

        public override string ToString()
        {
            return Sorszam.ToString();
        }

        public Mezo(int s, String sz, int h, int e, int x, int y)
        {
            this.Sorszam = s;
            this.XPos = x;
            this.YPos = y;
            this.Szoveg = sz;
            this.Hatas = h;
            this.Ertek = e;
            this.Jatekosok = new List<Jatekos>();
            this.Dobas = new Dictionary<int, List<Mezo>>();

            this.Dobas.Add(1, new List<Mezo>());
            this.Dobas.Add(2, new List<Mezo>());
            this.Dobas.Add(3, new List<Mezo>());
            this.Dobas.Add(4, new List<Mezo>());
            this.Dobas.Add(5, new List<Mezo>());
            this.Dobas.Add(6, new List<Mezo>());
        }

        public void Elrendez(List<Mezo> egy, List<Mezo> ketto, List<Mezo> harom, List<Mezo> negy, List<Mezo> ot, List<Mezo> hat)
        {
            foreach (Mezo m in egy)
            {
                this.Dobas[1].Add(m);
            }
            foreach (Mezo m in ketto)
            {
                this.Dobas[2].Add(m);
            }
            foreach (Mezo m in harom)
            {
                this.Dobas[3].Add(m);
            }
            foreach (Mezo m in negy)
            {
                this.Dobas[4].Add(m);
            }
            foreach (Mezo m in ot)
            {
                this.Dobas[5].Add(m);
            }
            foreach (Mezo m in hat)
            {
                this.Dobas[6].Add(m);
            }
        }
    }
}
