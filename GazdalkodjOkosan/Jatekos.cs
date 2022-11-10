using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GazdalkodjOkosan
{
    public class Jatekos
    {
        internal Tabla Tabla;

        public String Nev { get; set; }
        public int Szin { get; set; }
        public Mezo Pozicio { get; set; }
        public int Penz { get; set; }
        public int Bankbetet { get; set; }
        public int Allapot { get; set; }
        public int Hanyszor { get; set; }
        public bool Aktiv { get; set; }
        /// <summary>
        /// Hány részletet kell még törleszteni
        /// </summary>
        public int ReszletDB { get; set; }
        /// <summary>
        /// Törlesztőrészlet
        /// </summary>
        public int Reszlet { get; set; }
        /// <summary>
        /// Hány kör van még a törlesztésig
        /// </summary>
        public int Torlesztesig { get; set; }
        public bool CsakHatos { get; set; }
        public bool FizetestKaphat { get; set; }

        public bool Lakas { get; set; }
        public bool Auto { get; set; }
        public bool LakasBiztositas { get; set; }
        public bool AutoBiztositas { get; set; }
        public bool Mosogep { get; set; }
        public bool Konyhabutor { get; set; }
        public bool TV { get; set; }
        public bool PC { get; set; }
        public bool Huto { get; set; }
        public bool Agy { get; set; }
        public bool Szekreny { get; set; }
        public bool Garazs { get; set; }
        public bool MosogepKupon { get; set; }
        public bool KonyhabutorKupon { get; set; }
        public bool TVKupon { get; set; }
        public bool PCKupon { get; set; }
        public bool HutoKupon { get; set; }
        public bool AgyKupon { get; set; }
        public bool SzekrenyKupon { get; set; }

        public Jatekos(String n, int s, Tabla t)
        {
            this.Nev = n;
            this.Szin = s;
            this.Pozicio = t.Mezok[6];
            this.Pozicio.Jatekosok.Add(this);
            this.Allapot = Konstansok.DOBAS;
            this.Aktiv = true;
            this.Penz = t.Kezdo;
            this.Bankbetet = 0;
            this.ReszletDB = 0;
            this.Reszlet = 0;
            this.Torlesztesig = 0;
            this.Hanyszor = 1;
            this.FizetestKaphat = true;
            this.CsakHatos = false;
            this.AgyKupon = this.HutoKupon = this.KonyhabutorKupon = this.PCKupon = this.SzekrenyKupon = this.TVKupon = this.MosogepKupon = false;
            this.Auto = this.LakasBiztositas = this.AutoBiztositas = this.Mosogep = this.Konyhabutor = this.TV = this.PC = this.Huto = this.Agy = this.Szekreny = false;
            this.Lakas = this.Garazs = false;
            this.Tabla = t;
        }

        public int Dobas()
        {
            Random random = new Random();
            int dobas = random.Next(6) + 1;
            return dobas;
        }

        public List<Mezo> Lepesek(int d)
        {
            List<Mezo> ret = new List<Mezo>();
                for (int i = 0; i < this.Pozicio.Dobas[d].Count; i++)
                {
                    ret.Add(this.Pozicio.Dobas[d][i]);
                }
            return ret;
        }

        public String Megnez(Mezo m)
        {
            String ret="";
            switch (m.Hatas)
            {
                case 1: ret += "Kimaradsz " + m.Ertek + " körből!"; break;
                case 2: ret += "Fizess " + m.Ertek + " Ft-t!"; break;
                case 3: switch (m.Ertek)
                    {
                        case 1: ret += "Egyszer dobhatsz!"; break;
                        case 2: ret += "Kétszer dobhatsz!"; break;
                        case 3: ret += "Háromszor dobhatsz!"; break;
                    } break;
                case 4: ret += "Kapsz " + m.Ertek + " Ft-t!";  break;
                case 6: ret += "Húzz egy kártyát!"; break;
                case 9: if (m.Ertek == 4) { ret += "Biztosítást köthetsz!"; } else { if (m.Ertek == 7) { ret += "Elhelyezheted pénzed kamatozó betétkönyvekben"; } else { ret += "Vásárolhatsz!"; } } break;
            }

            this.Allapot = 3;
            return ret;
        }

        public String Lepessor(Mezo m, int d)
        {
            String ret = "";
            bool lepett;
            Mezo akt = this.Pozicio;
            List<Mezo> csomopontok = new List<Mezo> {   Tabla.Mezok[6],
                                                        Tabla.Mezok[10],
                                                        Tabla.Mezok[14], 
                                                        Tabla.Mezok[16],
                                                        Tabla.Mezok[18],
                                                        Tabla.Mezok[22],
                                                        Tabla.Mezok[39],
                                                        Tabla.Mezok[43],
                                                        Tabla.Mezok[47],
                                                        Tabla.Mezok[51],
                                                        Tabla.Mezok[68],
                                                        Tabla.Mezok[72],
                                                        Tabla.Mezok[76],
                                                        Tabla.Mezok[79],
                                                        Tabla.Mezok[80],
                                                        Tabla.Mezok[84]};

            if (this.Pozicio.Sorszam < 6 || this.Pozicio.Sorszam > 88)
            {
                while (akt.Sorszam != 6 && akt.Sorszam != 88 && d > 0)
                {
                    switch (akt.Sorszam)
                    {
                        case 1: ret += "B"; d--; akt = this.Tabla.Mezok[2]; break;
                        case 2: ret += "B"; d--; akt = this.Tabla.Mezok[3]; break;
                        case 3: ret += "L"; d--; akt = this.Tabla.Mezok[4]; break;
                        case 4: ret += "J"; d--; akt = this.Tabla.Mezok[5]; break;
                        case 5: ret += "J"; d--; akt = this.Tabla.Mezok[6]; break;
                        case 89: ret += "L"; d--; akt = this.Tabla.Mezok[90]; break;
                        case 90: ret += "L"; d--; akt = this.Tabla.Mezok[91]; break;
                        case 91: ret += "J"; d--; akt = this.Tabla.Mezok[92]; break;
                        case 92: ret += "F"; d--; akt = this.Tabla.Mezok[93]; break;
                        case 93: ret += "F"; d--; akt = this.Tabla.Mezok[88]; break;
                    }
                }
            }

            int vizszintes = (m.YPos - akt.YPos) / 35;
            int fuggoleges = (m.XPos - akt.XPos) / 35;

            while (!akt.Equals(m))
            {
                if (((Math.Abs(vizszintes) + Math.Abs(fuggoleges)) == d) || m.Sorszam < 6 || m.Sorszam > 88)
                {
                    lepett = false;
                    if (akt.Sorszam < 6 || akt.Sorszam > 88)
                    {
                        switch (akt.Sorszam)
                        {
                            case 1: ret += "B"; d--; akt = this.Tabla.Mezok[2]; break;
                            case 2: ret += "B"; d--; akt = this.Tabla.Mezok[3]; break;
                            case 3: ret += "L"; d--; akt = this.Tabla.Mezok[4]; break;
                            case 4: ret += "J"; d--; akt = this.Tabla.Mezok[5]; break;
                            case 5: ret += "J"; d--; akt = this.Tabla.Mezok[6]; break;
                            case 89: ret += "L"; d--; akt = this.Tabla.Mezok[90]; break;
                            case 90: ret += "L"; d--; akt = this.Tabla.Mezok[91]; break;
                            case 91: ret += "J"; d--; akt = this.Tabla.Mezok[92]; break;
                            case 92: ret += "F"; d--; akt = this.Tabla.Mezok[93]; break;
                            case 93: ret += "F"; d--; akt = this.Tabla.Mezok[88]; break;
                        }
                    }
                    else
                    {
                        if ((akt.Sorszam == 6 && this.Pozicio.Sorszam > 6 || (akt.Sorszam == 88 && this.Pozicio.Sorszam < 88)))
                        {
                            if (akt.Sorszam == 6)
                            {
                                ret += "F";
                                fuggoleges++;
                                d--;
                                akt = this.Tabla.Mezok[1];
                            }
                            else
                            {
                                ret += "B";
                                fuggoleges++;
                                d--;
                                akt = this.Tabla.Mezok[89];
                            }
                        }
                        else
                        {
                            if (Math.Abs(fuggoleges) > Math.Abs(vizszintes))
                            {
                                if (fuggoleges > 0 && !lepett)
                                {
                                    foreach (Mezo mezo in this.Tabla.Mezok.Values)
                                    {
                                        if (akt.XPos + 35 == mezo.XPos && akt.YPos == mezo.YPos)
                                        {
                                            ret += "L";
                                            lepett = true;
                                            fuggoleges--;
                                            d--;
                                            akt = mezo;
                                            break;
                                        }
                                    }
                                }
                                if (fuggoleges < 0 && !lepett)
                                {
                                    foreach (Mezo mezo in this.Tabla.Mezok.Values)
                                    {
                                        if (akt.XPos - 35 == mezo.XPos && akt.YPos == mezo.YPos)
                                        {
                                            ret += "F";
                                            lepett = true;
                                            fuggoleges++;
                                            d--;
                                            akt = mezo;
                                            break;
                                        }
                                    }
                                }
                                if (vizszintes > 0 && !lepett)
                                {
                                    foreach (Mezo mezo in this.Tabla.Mezok.Values)
                                    {
                                        if (akt.YPos + 35 == mezo.YPos && akt.XPos == mezo.XPos)
                                        {
                                            ret += "B";
                                            lepett = true;
                                            vizszintes--;
                                            d--;
                                            akt = mezo;
                                            break;
                                        }
                                    }
                                }
                                if (vizszintes < 0 && !lepett)
                                {
                                    foreach (Mezo mezo in this.Tabla.Mezok.Values)
                                    {
                                        if (akt.YPos - 35 == mezo.YPos && akt.XPos == mezo.XPos)
                                        {
                                            ret += "J";
                                            lepett = true;
                                            vizszintes++;
                                            d--;
                                            akt = mezo;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (vizszintes > 0 && !lepett)
                                {
                                    foreach (Mezo mezo in this.Tabla.Mezok.Values)
                                    {
                                        if (akt.YPos + 35 == mezo.YPos && akt.XPos == mezo.XPos)
                                        {
                                            ret += "B";
                                            lepett = true;
                                            vizszintes--;
                                            d--;
                                            akt = mezo;
                                            break;
                                        }
                                    }
                                }
                                if (vizszintes < 0 && !lepett)
                                {
                                    foreach (Mezo mezo in this.Tabla.Mezok.Values)
                                    {
                                        if (akt.YPos - 35 == mezo.YPos && akt.XPos == mezo.XPos)
                                        {
                                            ret += "J";
                                            lepett = true;
                                            vizszintes++;
                                            d--;
                                            akt = mezo;
                                            break;
                                        }
                                    }
                                }
                                if (fuggoleges > 0 && !lepett)
                                {
                                    foreach (Mezo mezo in this.Tabla.Mezok.Values)
                                    {
                                        if (akt.XPos + 35 == mezo.XPos && akt.YPos == mezo.YPos)
                                        {
                                            ret += "L";
                                            lepett = true;
                                            fuggoleges--;
                                            d--;
                                            akt = mezo;
                                            break;
                                        }
                                    }
                                }
                                if (fuggoleges < 0 && !lepett)
                                {
                                    foreach (Mezo mezo in this.Tabla.Mezok.Values)
                                    {
                                        if (akt.XPos - 35 == mezo.XPos && akt.YPos == mezo.YPos)
                                        {
                                            ret += "F";
                                            lepett = true;
                                            fuggoleges++;
                                            d--;
                                            akt = mezo;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                else
                {
                    foreach (Mezo mezo in csomopontok)
                    {
                        if (akt.XPos + 35 == mezo.XPos && akt.YPos == mezo.YPos)
                        {
                            ret += "L";
                            akt = mezo;
                            fuggoleges--;
                            d--;
                            break;
                        }
                        if (akt.XPos - 35 == mezo.XPos && akt.YPos == mezo.YPos)
                        {
                            ret += "F";
                            akt = mezo;
                            fuggoleges++;
                            d--;
                            break;
                        }
                        if (akt.YPos + 35 == mezo.YPos && akt.XPos == mezo.XPos)
                        {
                            ret += "B";
                            akt = mezo;
                            vizszintes--;
                            d--;
                            break;
                        }
                        if (akt.YPos - 35 == mezo.YPos && akt.XPos == mezo.XPos)
                        {
                            ret += "J";
                            akt = mezo;
                            vizszintes++;
                            d--;
                            break;
                        }
                    }


                }
            }

            return ret;
        }

        public int Lep(Mezo m)
        {
            Tabla.Mezok[this.Pozicio.Sorszam].Jatekosok.Remove(this);
            this.Pozicio = m;
            Tabla.Mezok[m.Sorszam].Jatekosok.Add(this);
            this.Allapot = 0;
            this.CsakHatos = false;
            this.Hanyszor--;
            switch (m.Hatas)
            {
                case 1: this.Hanyszor -= m.Ertek; break;
                case 2: if ((this.Penz + this.Bankbetet) >= m.Ertek)
                    {
                        this.Penz -= m.Ertek;
                        if (this.Penz < 0)
                        {
                            this.Bankbetet += this.Penz;
                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                            this.Bankbetet -= this.Penz;
                        }
                    }
                    else
                    {
                        this.Penz = 0;
                        this.Bankbetet = 0;
                        this.Aktiv = false;
                        this.Hanyszor = 0;

                    } break;
                case 3: this.Hanyszor += m.Ertek; break;
                case 4: this.Penz += m.Ertek; break;
                case 5: this.Hanyszor++; this.Allapot = Konstansok.MEZORELEPES; break;
                case 6: this.Allapot = Konstansok.KARTYAHUZAS; int k = this.Huz(); return k;
                case 7: this.FizetestKaphat = true; break;
                case 8: if (this.FizetestKaphat)
                    {
                        this.Penz += m.Ertek;
                        this.FizetestKaphat = false;
                    } break;
                case 9: this.Allapot = Konstansok.VASARLASABLAK; return m.Ertek;
            }
            return 0;
        }

        public int Huz()
        {
            Random random = new Random();
            int x;
            while (true)
            {
                x = random.Next(Tabla.Kartyak.Count) + 1;
                if (!Tabla.Huzottak[x])
                {
                    Tabla.Huzottak[x] = true;
                    Tabla.Kihuzott++;
                    break;
                }
            }

            if (Tabla.Kihuzott == Tabla.Kartyak.Count)
            {
                for (int i = 1; i <= Tabla.Kartyak.Count; i++)
                {
                    Tabla.Huzottak[i] = false;
                }
                Tabla.Kihuzott = 0;
            }

            return x;
        }
        
        public bool Felolvas(Szerencsekartya s)
        {
            if (s.Hatas == 6 && s.Ertek!=0)
            {
                return true;
            }
            return false;
        }

        public int KartyaVegrehajt(Szerencsekartya s)
        {
            int hatas = s.Hatas;
            int ertek = s.Ertek;

            switch (hatas)
            {
                case 1: this.Hanyszor -= ertek; break;
                case 2: if ((this.Penz + this.Bankbetet) >= ertek)
                    {
                        this.Penz -= ertek;
                        if (this.Penz < 0)
                        {
                            this.Bankbetet += this.Penz;
                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                            this.Bankbetet -= this.Penz;
                        }
                    }
                    else
                    {
                        this.Penz = 0;
                        this.Bankbetet = 0;
                        this.Aktiv = false;
                        this.Hanyszor = 0;
                    } break;
                case 3: this.Hanyszor += ertek; break;
                case 4: if (ertek < 100)
                    {
                        this.Penz += this.Bankbetet * ertek / 100;
                    }
                    else
                    {
                        this.Penz += ertek;
                    } break;
                case 5: switch (ertek)
                    {
                        case 1: if (this.Lakas)
                            {
                                if (this.Mosogep)
                                {
                                    this.Penz += Tabla.MosogepAr;
                                }
                                else
                                {
                                    this.Mosogep = true;
                                }
                            }
                            else
                            {
                                this.MosogepKupon = true;
                            } break;
                        case 2: if (this.Lakas)
                            {
                                if (this.TV)
                                {
                                    this.Penz += Tabla.TVAr;
                                }
                                else
                                {
                                    this.TV = true;
                                }
                            }
                            else
                            {
                                this.TVKupon = true;
                            } break;
                        case 3: if (this.Lakas)
                            {
                                if (this.PC)
                                {
                                    this.Penz += Tabla.PCAr;
                                }
                                else
                                {
                                    this.PC = true;
                                }
                            }
                            else
                            {
                                this.PCKupon = true;
                            } break;
                        case 4: if (this.Lakas)
                            {
                                if (this.Huto)
                                {
                                    this.Penz += Tabla.HutoAr;
                                }
                                else
                                {
                                    this.Huto = true;
                                }
                            }
                            else
                            {
                                this.HutoKupon = true;
                            } break;
                        case 5: if (this.Lakas)
                            {
                                if (this.Konyhabutor)
                                {
                                    this.Penz += Tabla.KonyhabutorAr;
                                }
                                else
                                {
                                    this.Konyhabutor = true;
                                }
                            }
                            else
                            {
                                this.KonyhabutorKupon = true;
                            } break;
                        case 6: if (this.Lakas)
                            {
                                if (this.Agy)
                                {
                                    this.Penz += Tabla.AgyAr;
                                }
                                else
                                {
                                    this.Agy = true;
                                }
                            }
                            else
                            {
                                this.AgyKupon = true;
                            } break;
                        case 7: if (this.Lakas)
                            {
                                if (this.Szekreny)
                                {
                                    this.Penz += Tabla.SzekrenyAr;
                                }
                                else
                                {
                                    this.Szekreny = true;
                                }
                            }
                            else
                            {
                                this.SzekrenyKupon = true;
                            } break;
                    } break;
                case 6: switch (ertek)
                    {
                        case 0: this.Bankbetet += (this.Penz / 5000) * 5000; this.Penz -= (this.Penz / 5000) * 5000; break;
                        case 1: if (!this.Mosogep && this.Lakas)
                            {
                                if (this.MosogepKupon)
                                {
                                    this.MosogepKupon = false;
                                    this.Mosogep = true;
                                    return 3;
                                }
                                else
                                {
                                    if ((this.Penz + this.Bankbetet) >= Tabla.MosogepAr)
                                    {
                                        this.Penz -= Tabla.MosogepAr;
                                        this.Mosogep = true;
                                        if (this.Penz < 0)
                                        {
                                            this.Bankbetet += this.Penz;
                                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                            this.Bankbetet -= this.Penz;
                                        }
                                        return 3;
                                    }
                                    else
                                    {
                                        return 4;
                                    }
                                }
                            }
                            if (this.Mosogep)
                            {
                                return 2;
                            }
                            if (!this.Lakas)
                            {
                                return 1;
                            } break;
                        case 2: if (!this.TV && this.Lakas)
                            {

                                if (this.TVKupon)
                                {
                                    this.TVKupon = false;
                                    this.TV = true;
                                    return 3;
                                }
                                else
                                {
                                    if ((this.Penz + this.Bankbetet) >= Tabla.TVAr)
                                    {
                                        this.Penz -= Tabla.TVAr;
                                        this.TV = true;
                                        if (this.Penz < 0)
                                        {
                                            this.Bankbetet += this.Penz;
                                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                            this.Bankbetet -= this.Penz;
                                        }
                                        return 3;
                                    }
                                    else
                                    {
                                        return 4;
                                    }
                                }
                            }
                            if (this.TV)
                            {
                                return 2;
                            }
                            if (!this.Lakas)
                            {
                                return 1;
                            } break;
                        case 3: if (!this.PC && this.Lakas)
                            {

                                if (this.PCKupon)
                                {
                                    this.PCKupon = false;
                                    this.PC = true;
                                    return 3;
                                }
                                else
                                {
                                    if ((this.Penz + this.Bankbetet) >= Tabla.PCAr)
                                    {
                                        this.Penz -= Tabla.PCAr;
                                        this.PC = true;
                                        if (this.Penz < 0)
                                        {
                                            this.Bankbetet += this.Penz;
                                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                            this.Bankbetet -= this.Penz;
                                        }
                                        return 3;
                                    }
                                    else
                                    {
                                        return 4;
                                    }
                                }
                            }
                            if (this.PC)
                            {
                                return 2;
                            }
                            if (!this.Lakas)
                            {
                                return 1;
                            } break;
                        case 4: if (!this.Huto && this.Lakas)
                            {

                                if (this.HutoKupon)
                                {
                                    this.HutoKupon = false;
                                    this.Huto = true;
                                    return 3;
                                }
                                else
                                {
                                    if ((this.Penz + this.Bankbetet) >= Tabla.HutoAr)
                                    {
                                        this.Penz -= Tabla.HutoAr;
                                        this.Huto = true;
                                        if (this.Penz < 0)
                                        {
                                            this.Bankbetet += this.Penz;
                                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                            this.Bankbetet -= this.Penz;
                                        }
                                        return 3;
                                    }
                                    else
                                    {
                                        return 4;
                                    }
                                }
                            }
                            if (this.Huto)
                            {
                                return 2;
                            }
                            if (!this.Lakas)
                            {
                                return 1;
                            } break;
                        case 5: if (!this.Konyhabutor && this.Lakas)
                            {

                                if (this.KonyhabutorKupon)
                                {
                                    this.KonyhabutorKupon = false;
                                    this.Konyhabutor = true;
                                    return 3;
                                }
                                else
                                {
                                    if ((this.Penz + this.Bankbetet) >= Tabla.KonyhabutorAr)
                                    {
                                        this.Penz -= Tabla.KonyhabutorAr;
                                        this.Konyhabutor = true;
                                        if (this.Penz < 0)
                                        {
                                            this.Bankbetet += this.Penz;
                                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                            this.Bankbetet -= this.Penz;
                                        }
                                        return 3;
                                    }
                                    else
                                    {
                                        return 4;
                                    }
                                }
                            }
                            if (this.Konyhabutor)
                            {
                                return 2;
                            }
                            if (!this.Lakas)
                            {
                                return 1;
                            } break;
                        case 6: if (!this.Agy && this.Lakas)
                            {

                                if (this.AgyKupon)
                                {
                                    this.AgyKupon = false;
                                    this.Agy = true;
                                    return 3;
                                }
                                else
                                {
                                    if ((this.Penz + this.Bankbetet) >= Tabla.AgyAr)
                                    {
                                        this.Penz -= Tabla.AgyAr;
                                        this.AgyKupon = true;
                                        if (this.Penz < 0)
                                        {
                                            this.Bankbetet += this.Penz;
                                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                            this.Bankbetet -= this.Penz;
                                        }
                                        return 3;
                                    }
                                    else
                                    {
                                        return 4;
                                    }
                                }
                            }
                            if (this.Agy)
                            {
                                return 2;
                            }
                            if (!this.Lakas)
                            {
                                return 1;
                            } break;
                        case 7: if (!this.Szekreny && this.Lakas)
                            {

                                if (this.SzekrenyKupon)
                                {
                                    this.SzekrenyKupon = false;
                                    this.Szekreny = true;
                                    return 3;
                                }
                                else
                                {
                                    if ((this.Penz + this.Bankbetet) >= Tabla.SzekrenyAr)
                                    {
                                        this.Penz -= Tabla.SzekrenyAr;
                                        this.Szekreny = true;
                                        if (this.Penz < 0)
                                        {
                                            this.Bankbetet += this.Penz;
                                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                            this.Bankbetet -= this.Penz;
                                        }
                                        return 3;
                                    }
                                    else
                                    {
                                        return 4;
                                    }
                                }
                            }
                            if (this.Szekreny)
                            {
                                return 2;
                            }
                            if (!this.Lakas)
                            {
                                return 1;
                            } break;
                        case 8: if (!this.LakasBiztositas && this.Lakas)
                            {
                                if ((this.Penz + this.Bankbetet) >= Tabla.LakasBiztositasAr)
                                {
                                    this.LakasBiztositas = true;
                                    this.Penz -= Tabla.LakasBiztositasAr;
                                    if (this.Penz < 0)
                                    {
                                        this.Bankbetet += this.Penz;
                                        this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                        this.Bankbetet -= this.Penz;
                                    }
                                    return 3;
                                }
                                else
                                {
                                    return 4;
                                }
                            }
                            if (this.LakasBiztositas)
                            {
                                return 2;
                            }
                            if (!this.Lakas)
                            {
                                return 1;
                            } break;
                        case 9: if (!this.AutoBiztositas && this.Auto)
                            {
                                if ((this.Penz + this.Bankbetet) >= Tabla.AutoBiztositasAr)
                                {
                                    this.AutoBiztositas = true;
                                    this.Penz -= Tabla.AutoBiztositasAr;
                                    if (this.Penz < 0)
                                    {
                                        this.Bankbetet += this.Penz;
                                        this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                        this.Bankbetet -= this.Penz;
                                    }
                                    return 3;
                                }
                                else
                                {
                                    return 4;
                                }
                            }
                            if (this.AutoBiztositas)
                            {
                                return 2;
                            }
                            if (!this.Auto)
                            {
                                return 6;
                            } break;
                        case 10: if (!this.Lakas)
                            {
                                if ((this.Penz + this.Bankbetet) >= (Convert.ToInt32(Tabla.LakasAr * Tabla.Kezdoreszlet / 100)))
                                {
                                    this.Lakas = true;
                                    this.ReszletDB = Tabla.ReszletDB;
                                    this.Torlesztesig = Tabla.ReszletKor;
                                    this.Reszlet = Convert.ToInt32(Tabla.LakasAr * (100 - Tabla.Kezdoreszlet) / 100 / Tabla.ReszletDB);
                                    this.Penz -= Convert.ToInt32(Tabla.LakasAr * Tabla.Kezdoreszlet / 100);
                                    if (this.Penz < 0)
                                    {
                                        this.Bankbetet += this.Penz;
                                        this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                        this.Bankbetet -= this.Penz;
                                    }
                                    return 3;
                                }
                                else
                                {
                                    return 4;
                                }
                            }
                            else
                            {
                                return 2;
                            }
                    } break;
                case 7: 
                    if (this.LakasBiztositas)
                    {
                        if (this.Mosogep)
                        {
                            this.Penz += Tabla.MosogepAr;
                        }
                        if (this.TV)
                        {
                            this.Penz += Tabla.TVAr;
                        }
                        if (this.PC)
                        {
                            this.Penz += Tabla.PCAr;
                        }
                        if (this.Huto)
                        {
                            this.Penz += Tabla.HutoAr;
                        }
                        if (this.Konyhabutor)
                        {
                            this.Penz += Tabla.KonyhabutorAr;
                        }
                        if (this.Agy)
                        {
                            this.Penz += Tabla.AgyAr;
                        }
                        if (this.Szekreny)
                        {
                            this.Penz += Tabla.SzekrenyAr; ;
                        }
                    } 
                    this.Mosogep = this.TV = this.PC = this.Huto = this.Konyhabutor = this.Agy = this.Szekreny = false; break;
                case 8: this.CsakHatos = true; break;
            }
            return 0;
        }

        public List<String> KartyaHatasSzoveg(Szerencsekartya s)
        {
            List<String> ret = new List<String>();
            switch (s.Hatas)
            {
                case 1: ret.Add("Kimaradsz " + s.Ertek + " körből!"); break;
                case 2: ret.Add("Fizess " + s.Ertek + " Ft-t!"); break;
                case 3: switch (s.Ertek)
                    {
                        case 1: ret.Add("Egyszer dobhatsz!"); break;
                        case 2: ret.Add("Kétszer dobhatsz!"); break;
                        case 3: ret.Add("Háromszor dobhatsz!"); break;
                    } break;
                case 4: if (s.Ertek < 100)
                    {
                        ret.Add("Kapsz " + this.Bankbetet * s.Ertek / 100 + " Ft-t!");
                    }
                    else
                    {
                        ret.Add("Kapsz " + s.Ertek + " Ft-t!");
                    } break;
                case 5: ret.Add("Ha nincs lakásod, egy kupont kapsz, amit később felhasználhatsz!");
                    ret.Add("Ha van már ilyen tárgyad, akkor megkapod az árát!");
                     break;
                case 6: String szoveg="";
                     if (s.Ertek != 0)
                     {
                         szoveg = "Ára ";
                     }
                     switch (s.Ertek)
                     {
                         case 1: szoveg += Tabla.MosogepAr.ToString() + " Ft"; break;
                         case 2: szoveg += Tabla.TVAr.ToString() + " Ft"; break;
                         case 3: szoveg += Tabla.PCAr.ToString() + " Ft"; break;
                         case 4: szoveg += Tabla.HutoAr.ToString() + " Ft"; break;
                         case 5: szoveg += Tabla.KonyhabutorAr.ToString() + " Ft"; break;
                         case 6: szoveg += Tabla.AgyAr.ToString() + " Ft"; break;
                         case 7: szoveg += Tabla.SzekrenyAr.ToString() + " Ft"; break;
                         case 8: szoveg += Tabla.LakasBiztositasAr.ToString() + " Ft"; break;
                         case 9: szoveg += Tabla.AutoBiztositasAr.ToString() + " Ft"; break;
                         case 10: szoveg += Tabla.LakasAr.ToString() + " Ft"; break;
                     }
                    ret.Add(szoveg); break;
                case 7: ret.Add("Ha volt biztosításod, akkor megtérítik a károdat."); 
                    ret.Add("Ha nem volt, mindent újra meg kell venned!"); break;
            }
            return ret;
        }

        public int Vasarol(int t, int s)
        {
            if ((t == 1 && s == 4) || (t == 5 && s == 2))
            {
                //Mosógép
                if (!this.Mosogep && this.Lakas)
                {
                    if (this.MosogepKupon)
                    {
                        this.MosogepKupon = false;
                        this.Mosogep = true;
                        return 3;
                    }
                    else
                    {
                        if ((this.Penz + this.Bankbetet) >= Tabla.MosogepAr)
                        {
                            this.Penz -= Tabla.MosogepAr;
                            this.Mosogep = true;
                            if (this.Penz < 0)
                            {
                                this.Bankbetet += this.Penz;
                                this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                this.Bankbetet -= this.Penz;
                            }
                            return 3;
                        }
                        else
                        {
                            return 4;
                        }
                    }
                }
                if (!this.Lakas)
                {
                    return 1;
                }
                if (this.Mosogep)
                {
                    return 2;
                }
            }
            if ((t == 1 && s == 7) || (t == 5 && s == 4))
            {
                //TV
                if (!this.TV && this.Lakas)
                {
                    if (this.TVKupon)
                    {
                        this.TVKupon = false;
                        this.TV = true;
                        return 3;
                    }
                    else
                    {
                        if ((this.Penz + this.Bankbetet) >= Tabla.TVAr)
                        {
                            this.Penz -= Tabla.TVAr;
                            this.TV = true;
                            if (this.Penz < 0)
                            {
                                this.Bankbetet += this.Penz;
                                this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                this.Bankbetet -= this.Penz;
                            }
                            return 3;
                        }
                        else
                        {
                            return 4;
                        }

                    }
                }
                if (!this.Lakas)
                {
                    return 1;
                }
                if (this.TV)
                {
                    return 2;
                }
            }
            if ((t == 1 && s == 5) || (t == 5 && s == 3))
            {
                //Számítógép
                if (!this.PC && this.Lakas)
                {
                    if (this.PCKupon)
                    {
                        this.PCKupon = false;
                        this.PC = true;
                        return 3;
                    }
                    else
                    {
                        if ((this.Penz + this.Bankbetet) >= Tabla.PCAr)
                        {
                            this.Penz -= Tabla.PCAr;
                            this.PC = true;
                            if (this.Penz < 0)
                            {
                                this.Bankbetet += this.Penz;
                                this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                this.Bankbetet -= this.Penz;

                            }
                            return 3;
                        }
                        else
                        {
                            return 4;
                        }

                    }
                }
                if (!this.Lakas)
                {
                    return 1;
                }
                if (this.PC)
                {
                    return 2;
                }
            }
            if ((t == 1 && s == 2) || (t == 5 && s == 1))
            {
                //Hűtő
                if (!this.Huto && this.Lakas)
                {
                    if (this.HutoKupon)
                    {
                        this.HutoKupon = false;
                        this.Huto = true;
                        return 3;
                    }
                    else
                    {
                        if ((this.Penz + this.Bankbetet) >= Tabla.HutoAr)
                        {
                            this.Penz -= Tabla.HutoAr;
                            this.Huto = true;
                            if (this.Penz < 0)
                            {
                                this.Bankbetet += this.Penz;
                                this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                this.Bankbetet -= this.Penz;
                            }
                            return 3;
                        }
                        else
                        {
                            return 4;
                        }

                    }
                }
                if (!this.Lakas)
                {
                    return 1;
                }
                if (this.Huto)
                {
                    return 2;
                }
            }
            if ((t == 1 && s == 3) || (t == 6 && s == 2))
            {
                //Konyhabútor
                if (!this.Konyhabutor && this.Lakas)
                {
                    if (this.KonyhabutorKupon)
                    {
                        this.KonyhabutorKupon = false;
                        this.Konyhabutor = true;
                        return 3;
                    }
                    else
                    {
                        if ((this.Penz + this.Bankbetet) >= Tabla.KonyhabutorAr)
                        {
                            this.Penz -= Tabla.KonyhabutorAr;
                            this.Konyhabutor = true;
                            if (this.Penz < 0)
                            {
                                this.Bankbetet += this.Penz;
                                this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                this.Bankbetet -= this.Penz;
                            }
                            return 3;
                        }
                        else
                        {
                            return 4;
                        }

                    }
                }
                if (!this.Lakas)
                {
                    return 1;
                }
                if (this.Konyhabutor)
                {
                    return 2;
                }
            }
            if ((t == 1 && s == 1) || (t == 6 && s == 1))
            {
                //Ágy
                if (!this.Agy && this.Lakas)
                {
                    if (this.AgyKupon)
                    {
                        this.AgyKupon = false;
                        this.Agy = true;
                        return 3;
                    }
                    else
                    {
                        if ((this.Penz + this.Bankbetet) >= Tabla.AgyAr)
                        {
                            this.Penz -= Tabla.AgyAr;
                            this.Agy = true;
                            if (this.Penz < 0)
                            {
                                this.Bankbetet += this.Penz;
                                this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                this.Bankbetet -= this.Penz;
                            }
                            return 3;
                        }
                        else
                        {
                            return 4;
                        }

                    }
                }
                if (!this.Lakas)
                {
                    return 1;
                }
                if (this.Agy)
                {
                    return 2;
                }
            }
            if ((t == 1 && s == 6) || (t == 6 && s == 3))
            {
                //Szekrény
                if (!this.Szekreny && this.Lakas)
                {

                    if (this.SzekrenyKupon)
                    {
                        this.SzekrenyKupon = false;
                        this.Szekreny = true;
                        return 3;
                    }
                    else
                    {
                        if ((this.Penz + this.Bankbetet) >= Tabla.SzekrenyAr)
                        {
                            this.Penz -= Tabla.SzekrenyAr;
                            this.Szekreny = true;
                            if (this.Penz < 0)
                            {
                                this.Bankbetet += this.Penz;
                                this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                                this.Bankbetet -= this.Penz;
                            }
                            return 3;
                        }
                        else
                        {
                            return 4;
                        }
                    }
                }
                if (!this.Lakas)
                {
                    return 1;
                }
                if (this.Szekreny)
                {
                    return 2;
                }
            }
            if (t == 2 && s == 1)
            {
                //Garázs
                if (!this.Garazs)
                {
                    if ((this.Penz + this.Bankbetet) >= Tabla.GarazsAr)
                    {
                        this.Garazs = true;
                        this.Penz -= Tabla.GarazsAr;
                        if (this.Penz < 0)
                        {
                            this.Bankbetet += this.Penz;
                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                            this.Bankbetet -= this.Penz;
                        }
                        return 3;
                    }
                    else
                    {
                        return 4;
                    }
                }
                else
                {
                    return 2;
                }
            }
            if (t == 2 && s == 2)
            {
                //Lakás egyösszegben
                if (!this.Lakas)
                {
                    if ((this.Penz + this.Bankbetet) >= Convert.ToInt32(Tabla.LakasAr * (100 + Tabla.IngatlanirodaJutalek) / 100))
                    {
                        this.Lakas = true;
                        this.Penz -= Convert.ToInt32(Tabla.LakasAr * (100 + Tabla.IngatlanirodaJutalek) / 100);
                        if (this.Penz < 0)
                        {
                            this.Bankbetet += this.Penz;
                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                            this.Bankbetet -= this.Penz;
                        }
                        return 3;
                    }
                    else
                    {
                        return 4;
                    }
                }
                else
                {
                    return 2;
                }
            }
            if (t == 2 && s == 3)
            {
                //Lakás részletre
                if (!this.Lakas)
                {
                    if ((this.Penz + this.Bankbetet) >= Convert.ToInt32(Tabla.LakasAr * (100 + Tabla.IngatlanirodaJutalek) / 100 * (100 + Tabla.ReszletKamat) / 100 * Tabla.Kezdoreszlet / 100))
                    {
                        this.Lakas = true;
                        this.ReszletDB = Tabla.ReszletDB;
                        this.Torlesztesig = Tabla.ReszletKor;
                        this.Reszlet = Convert.ToInt32(Tabla.LakasAr * (100 + Tabla.IngatlanirodaJutalek) / 100 * (100 + Tabla.ReszletKamat) / 100 * (100 - Tabla.Kezdoreszlet) / 100 / Tabla.ReszletDB);
                        this.Penz -= Convert.ToInt32(Tabla.LakasAr * (100 + Tabla.IngatlanirodaJutalek) / 100 * (100 + Tabla.ReszletKamat) / 100 * Tabla.Kezdoreszlet / 100);
                        if (this.Penz < 0)
                        {
                            this.Bankbetet += this.Penz;
                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                            this.Bankbetet -= this.Penz;
                        }
                        return 3;
                    }
                    else
                    {
                        return 4;
                    }
                }
                else
                {
                    return 2;
                }
            }
            if (t == 3 && s == 1)
            {
                //Autó
                if (!this.Auto && this.Garazs)
                {
                    if ((this.Penz + this.Bankbetet) >= Tabla.AutoAr)
                    {
                        this.Auto = true;
                        this.Penz -= Tabla.AutoAr;
                        if (this.Penz < 0)
                        {
                            this.Bankbetet += this.Penz;
                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                            this.Bankbetet -= this.Penz;
                        }
                        return 3;
                    }
                    else
                    {
                        return 4;
                    }
                }
                if (this.Auto)
                {
                    return 2;
                }
                if (!this.Garazs)
                {
                    return 5;
                }
            }
            if (t == 4 && s == 1)
            {
                //Lakásbiztosítás
                if (!this.LakasBiztositas && this.Lakas)
                {
                    if ((this.Penz + this.Bankbetet) >= Tabla.LakasBiztositasAr)
                    {
                        this.LakasBiztositas = true;
                        this.Penz -= Tabla.LakasBiztositasAr;
                        if (this.Penz < 0)
                        {
                            this.Bankbetet += this.Penz;
                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                            this.Bankbetet -= this.Penz;
                        }
                        return 3;
                    }
                    else
                    {
                        return 4;
                    }
                }
                if (!this.Lakas)
                {
                    return 1;
                }
                if (this.LakasBiztositas)
                {
                    return 2;
                }
            }
            if (t == 4 && s == 2)
            {
                //Autó biztosítás
                if (!this.AutoBiztositas && this.Auto)
                {
                    if ((this.Penz + this.Bankbetet) >= Tabla.AutoBiztositasAr)
                    {
                        this.AutoBiztositas = true;
                        this.Penz -= Tabla.AutoBiztositasAr;
                        if (this.Penz < 0)
                        {
                            this.Bankbetet += this.Penz;
                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                            this.Bankbetet -= this.Penz;
                        }
                        return 3;
                    }
                    else
                    {
                        return 4;
                    }
                }
                if (!this.Auto)
                {
                    return 6;
                }
                if (this.AutoBiztositas)
                {
                    return 2;
                }
            }
            if (t == 8 && s == 1)
            {
                //Lakás készpénzben
                if (!this.Lakas)
                {
                    if ((this.Penz + this.Bankbetet) >= Tabla.LakasAr)
                    {
                        this.Lakas = true;
                        this.Penz -= Tabla.LakasAr;
                        if (this.Penz < 0)
                        {
                            this.Bankbetet += this.Penz;
                            this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                            this.Bankbetet -= this.Penz;
                        }
                        return 3;
                    }
                    else
                    {
                        return 4;
                    }
                }
                else
                {
                    return 2;
                }
            }
            if (t == 7)
            {
                //Bankfiók
                this.Bankbetet += (this.Penz / 5000) * 5000;
                this.Penz -= (this.Penz / 5000) * 5000;
                return 0;
            }

            return 0;

        }

        public String VasarolVisszakerdez(int t, int s)
        {
            String ret = "";

            if ((t == 1 && s == 4) || (t == 5 && s == 2))
            {
                //Mosógép
                ret = "Mosógép";
            }
            if ((t == 1 && s == 7) || (t == 5 && s == 4))
            {
                //TV
                ret = "TV";
            }
            if ((t == 1 && s == 5) || (t == 5 && s == 3))
            {
                //Számítógép
                ret = "Számítógép";
                
            }
            if ((t == 1 && s == 2) || (t == 5 && s == 1))
            {
                //Hűtő
                ret = "Hűtő";
                
            }
            if ((t == 1 && s == 3) || (t == 6 && s == 2))
            {
                //Konyhabútor
                ret = "Konyhabútor";
                
            }
            if ((t == 1 && s == 1) || (t == 6 && s == 1))
            {
                //Ágy
                ret = "Ágy";
                
            }
            if ((t == 1 && s == 6) || (t == 6 && s == 3))
            {
                //Szekrény
                ret = "Szekrény";
                
            }
            if (t == 2 && s == 1)
            {
                //Garázs
                ret = "Garázs";
                   
            }
            if (t == 2 && s == 2)
            {
                //Lakás egyösszegben
                ret = "Lakás (egyösszegben)";
                
            }
            if (t == 2 && s == 3)
            {
                //Lakás részletre
                ret = "Lakás (részletre)";
                
            }
            if (t == 3 && s == 1)
            {
                //Autó
                ret = "Autó";
                
            }
            if (t == 4 && s == 1)
            {
                //Lakásbiztosítás
                ret = "Lakásbiztosítás";
                
            }
            if (t == 4 && s == 2)
            {
                //Autó biztosítás
                ret = "Autó biztosítás";
                
            }
            if (t == 8 && s == 1)
            {
                //Lakás készpénzben
                ret = "Lakás";                
            }

            return ret;
        }

        public bool NyertE()
        {
            if (this.Lakas && this.Garazs && this.LakasBiztositas && this.AutoBiztositas && this.Agy && this.Konyhabutor && this.Szekreny && this.PC && this.TV && this.Mosogep && this.Huto && this.ReszletDB == 0)
            {
                return true;
            }
            return false;
        }

        public bool Torleszt()
        {
            if (this.Torlesztesig == 0)
            {
                if ((this.Penz + this.Bankbetet) >= this.Reszlet)
                {
                    this.Penz -= this.Reszlet;
                    this.Torlesztesig = Tabla.ReszletKor;
                    this.ReszletDB--;
                    if (this.Penz < 0)
                    {
                        this.Bankbetet += this.Penz;
                        this.Penz = this.Bankbetet - (this.Bankbetet / 5000) * 5000;
                        this.Bankbetet -= this.Penz;
                    }
                }
                else
                {
                    this.Aktiv = false;
                    return false;
                }
            }
            else
            {
                this.Torlesztesig--;
            }
            return true;
        }

        public override string ToString()
        {
            return this.Nev + " P " + this.Penz + " B " + this.Bankbetet + " R " + this.ReszletDB + " T "+this.Torlesztesig; 
        }
    }
}
