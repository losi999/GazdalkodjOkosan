using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan
{
    public class Szerencsekartya
    {
        public int Sorszam { get; set; }
        public String Szoveg { get; set; }
        public int Hatas { get; set; }
        public int Ertek { get; set; }

        public Szerencsekartya(int s, String sz, int h, int e)
        {
            this.Sorszam = s;
            this.Szoveg = sz;
            this.Hatas = h;
            this.Ertek=e;
        }

        public override string ToString()
        {
            return this.Szoveg;
        }

    }
}
