using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan
{
    public class Tabla
    {
        public Dictionary<int, Mezo> Mezok { get; set; }
        public Dictionary<int, Szerencsekartya> Kartyak { get; set; }
        public Dictionary<int, bool> Huzottak { get; set; }
        public int Kihuzott { get; set; }
        public int LakasAr { get; set; }
        /// <summary>
        /// Jutalék az ingatlanirodás vásárlásnál (százalékban)
        /// </summary>
        public int IngatlanirodaJutalek { get; set; }
        /// <summary>
        /// Mennyi a részletfizetési kamat (százalékban)
        /// </summary>
        public int ReszletKamat { get; set; }
        /// <summary>
        /// Hány részletben kell törleszteni
        /// </summary>
        public int ReszletDB { get; set; }
        /// <summary>
        /// Hány körönként kell törleszteni
        /// </summary>
        public int ReszletKor { get; set; }
        /// <summary>
        /// Mennyi a kezdőrészlet (százalékban)
        /// </summary>
        public int Kezdoreszlet { get; set; }
        public int GarazsAr { get; set; }
        public int AutoAr { get; set; }
        public int LakasBiztositasAr { get; set; }
        public int AutoBiztositasAr { get; set; }
        public int MosogepAr { get; set; }
        public int TVAr { get; set; }
        public int PCAr { get; set; }
        public int HutoAr { get; set; }
        public int KonyhabutorAr { get; set; }
        public int AgyAr { get; set; }
        public int SzekrenyAr { get; set; }
        public int Kezdo { get; set; }
        public int Fizetes { get; set; }
        public int BetetKamat { get; set; }
        public int BetetKamatKorDB { get; set; }


        public Tabla()
        {
            this.Mezok = new Dictionary<int, Mezo>();
            this.Kartyak = new Dictionary<int, Szerencsekartya>();
            this.Huzottak = new Dictionary<int, bool>();
            this.Kihuzott = 0;
            this.Fizetes = Konstansok.Fizetes;
            this.Kezdo = Konstansok.Kezdo;
            this.IngatlanirodaJutalek = Konstansok.IngatlanirodaJutalek;
            this.ReszletKamat = Konstansok.ReszletKamat;
            this.ReszletDB = Konstansok.ReszletDB;
            this.ReszletKor = Konstansok.ReszletKor;
            this.Kezdoreszlet = Konstansok.Kezdoreszlet;
            this.LakasAr = Konstansok.LakasAr;
            this.GarazsAr = Konstansok.GarazsAr;
            this.AutoAr = Konstansok.AutoAr;
            this.LakasBiztositasAr = Konstansok.LakasBiztositasAr;
            this.AutoBiztositasAr = Konstansok.AutoBiztositasAr;
            this.MosogepAr = Konstansok.MosogepAr;
            this.TVAr = Konstansok.TVAr;
            this.PCAr = Konstansok.PCAr;
            this.HutoAr = Konstansok.HutoAr;
            this.KonyhabutorAr = Konstansok.KonyhabutorAr;
            this.AgyAr = Konstansok.AgyAr;
            this.SzekrenyAr = Konstansok.SzekrenyAr;
            this.BetetKamat = Konstansok.BetetKamat;
            this.BetetKamatKorDB = Konstansok.BetetKamatKorDB;

            #region mezők hozzáadása

            Mezok.Add(1, new Mezo(1, "START", 7, 0, 15, 222));
            Mezok.Add(2, new Mezo(2, "START", 7, 0, 15, 257));
            Mezok.Add(3, new Mezo(3, "START", 7, 0, 15, 292));
            Mezok.Add(4, new Mezo(4, "START", 7, 0, 50, 292));
            Mezok.Add(5, new Mezo(5, "START", 7, 0, 50, 257));
            Mezok.Add(6, new Mezo(6, "START", 7, 0, 50, 222));
            Mezok.Add(7, new Mezo(7, "Defektet kaptál", 1, 1, 85, 222));
            Mezok.Add(8, new Mezo(8, "Telekocsival utazol", 3, 1, 120, 222));
            Mezok.Add(9, new Mezo(9, "Szerencsekártya", 6, 0, 155, 222));
            Mezok.Add(10, new Mezo(10, "Pláza", 9, 1, 190, 432));
            Mezok.Add(11, new Mezo(11, "Pláza", 9, 1, 190, 397));
            Mezok.Add(12, new Mezo(12, "Lakás", 9, 8, 190, 362));
            Mezok.Add(13, new Mezo(13, "Szerencsekártya", 6, 0, 190, 327));
            Mezok.Add(14, new Mezo(14, "Rendszeresen Sportolsz", 3, 1, 190, 292));
            Mezok.Add(15, new Mezo(15, "Autóversenyre mentél", 2, 5000, 190, 257));
            Mezok.Add(16, new Mezo(16, "Forgalmi dugóba kerültél", 1, 1, 190, 222));
            Mezok.Add(17, new Mezo(17, "Bankfiók", 9, 7, 190, 187));
            Mezok.Add(18, new Mezo(18, "Ingatlaniroda", 9, 2, 190, 152));
            Mezok.Add(19, new Mezo(19, "Ingatlaniroda", 9, 2, 190, 117));
            Mezok.Add(20, new Mezo(20, "Kiraboltak és a rendőrségen vagy", 1, 2, 190, 82));
            Mezok.Add(21, new Mezo(21, "Szerencsekártya", 6, 0, 190, 47));
            Mezok.Add(22, new Mezo(22, "Bankrablásba keveredtél", 1, 3, 190, 12));
            Mezok.Add(23, new Mezo(23, "Pláza", 9, 1, 225, 432));
            Mezok.Add(24, new Mezo(24, "Elterelik a forgalmat", 1, 1, 225, 292));
            Mezok.Add(25, new Mezo(25, "Ingatlaniroda", 9, 2, 225, 152));
            Mezok.Add(26, new Mezo(26, "Bankfiók", 9, 7, 225, 12));
            Mezok.Add(27, new Mezo(27, "Szerencsekártya", 6, 0, 260, 432));
            Mezok.Add(28, new Mezo(28, "Bevásároltál a hétvégére", 2, 3000, 260, 292));
            Mezok.Add(29, new Mezo(29, "Kerékpárral közlekedsz", 3, 1, 260, 152));
            Mezok.Add(30, new Mezo(30, "Szerencsekártya", 6, 0, 260, 12));
            Mezok.Add(31, new Mezo(31, "Koncertre mentél", 2, 2000, 295, 432));
            Mezok.Add(32, new Mezo(32, "Autószalon", 9, 3, 295, 292));
            Mezok.Add(33, new Mezo(33, "Szerencsekártya", 6, 0, 295, 152));
            Mezok.Add(34, new Mezo(34, "Elektronikai bolt", 9, 5, 295, 12));
            Mezok.Add(35, new Mezo(35, "Tiltott helyen dohányoztál", 2, 5000, 330, 432));
            Mezok.Add(36, new Mezo(36, "Autószalon", 9, 3, 330, 292));
            Mezok.Add(37, new Mezo(37, "Kaszinóban sokat nyertél", 4, 10000, 330, 152));
            Mezok.Add(38, new Mezo(38, "Elektronikai bolt", 9, 5, 330, 12));
            Mezok.Add(39, new Mezo(39, "Elrántottál egy embert az autók elől", 3, 3, 365, 432));
            Mezok.Add(40, new Mezo(40, "Bankfiók", 9, 7, 365, 397));
            Mezok.Add(41, new Mezo(41, "Hétvégén sétáltál a parkban", 3, 1, 365, 362));
            Mezok.Add(42, new Mezo(42, "Másnaposság gyötör", 1, 2, 365, 327));
            Mezok.Add(43, new Mezo(43, "Balesetet szenvedtél", 1, 2, 365, 292));
            Mezok.Add(44, new Mezo(44, "Környezettudatosan élsz", 3, 2, 365, 257));
            Mezok.Add(45, new Mezo(45, "Lakás", 9, 8, 365, 222));
            Mezok.Add(46, new Mezo(46, "Bankfiók", 9, 7, 365, 187));
            Mezok.Add(47, new Mezo(47, "Kollégád elvisz a munkahelyre", 5, 88, 365, 152));
            Mezok.Add(48, new Mezo(48, "Kaszinóban sokat vesztettél", 2, 12000, 365, 117));
            Mezok.Add(49, new Mezo(49, "Szerencsekártya", 6, 0, 365, 82));
            Mezok.Add(50, new Mezo(50, "Megbírságolt az adóhivatal", 2, 10000, 365, 47));
            Mezok.Add(51, new Mezo(51, "Átmentél a piroson", 2, 10000, 365, 12));
            Mezok.Add(52, new Mezo(52, "Lakás", 9, 8, 400, 432));
            Mezok.Add(53, new Mezo(53, "Szerencsekártya", 6, 0, 400, 292));
            Mezok.Add(54, new Mezo(54, "Szemeteltél", 2, 5000, 400, 152));
            Mezok.Add(55, new Mezo(55, "Adóvisszatérítést kapsz", 4, 7000, 400, 12));
            Mezok.Add(56, new Mezo(56, "Moziba mentél", 2, 1500, 435, 432));
            Mezok.Add(57, new Mezo(57, "Éjszaka szórakozni mentél", 2, 5000, 435, 292));
            Mezok.Add(58, new Mezo(58, "Lóversenyen nyertél", 4, 4000, 435, 152));
            Mezok.Add(59, new Mezo(59, "Lakás", 9, 8, 435, 12));
            Mezok.Add(60, new Mezo(60, "Szerencsekártya", 6, 0, 470, 432));
            Mezok.Add(61, new Mezo(61, "Bútorbolt", 9, 6, 470, 292));
            Mezok.Add(62, new Mezo(62, "Bankfiók", 9, 7, 470, 152));
            Mezok.Add(63, new Mezo(63, "Strandra mentél", 2, 1000, 470, 12));
            Mezok.Add(64, new Mezo(64, "Bankfiók", 9, 7, 505, 432));
            Mezok.Add(65, new Mezo(65, "Bútorbolt", 9, 6, 505, 292));
            Mezok.Add(66, new Mezo(66, "Szerencsekártya", 6, 0, 505, 152));
            Mezok.Add(67, new Mezo(67, "Szerencsekártya", 6, 0, 505, 12));
            Mezok.Add(68, new Mezo(68, "Tömegközlekedsz", 3, 2, 540, 432));
            Mezok.Add(69, new Mezo(69, "Lakás", 9, 8, 540, 397));
            Mezok.Add(70, new Mezo(70, "Focimeccsre mentél", 2, 500, 540, 362));
            Mezok.Add(71, new Mezo(71, "Szerencsekártya", 6, 0, 540, 327));
            Mezok.Add(72, new Mezo(72, "Szomszédod hazavisz", 5, 6, 540, 292));
            Mezok.Add(73, new Mezo(73, "Színházba mentél", 2, 1000, 540, 257));
            Mezok.Add(74, new Mezo(74, "Lóversenyen vesztettél", 2, 5000, 540, 222));
            Mezok.Add(75, new Mezo(75, "Kompra szálltál, ami átvisz a munkahelyre", 5, 88, 540, 187));
            Mezok.Add(76, new Mezo(76, "Egy barátod elvisz a plázába", 5, 10, 540, 152));
            Mezok.Add(77, new Mezo(77, "Biztosító", 9, 4, 540, 117));
            Mezok.Add(78, new Mezo(78, "Szerencsekártya", 6, 0, 540, 82));
            Mezok.Add(79, new Mezo(79, "Vidámparkba mentél", 2, 3000, 540, 47));
            Mezok.Add(80, new Mezo(80, "Bekajáltál", 1, 1, 540, 12));
            Mezok.Add(81, new Mezo(81, "Fizett szabadságot kaptál", 4, 20000, 575, 47));
            Mezok.Add(82, new Mezo(82, "Kimentettél egy fuldoklót a folyóból", 3, 2, 610, 47));
            Mezok.Add(83, new Mezo(83, "Pazaroltál", 1, 1, 645, 47));
            Mezok.Add(84, new Mezo(84, "Bankfiók", 9, 7, 680, 47));
            Mezok.Add(85, new Mezo(85, "Egy barátod visszafizeti a kölcsönt", 4, 4000, 680, 82));
            Mezok.Add(86, new Mezo(86, "Szerencsekártya", 6, 0, 680, 117));
            Mezok.Add(87, new Mezo(87, "Lebetegedtél", 1, 1, 680, 152));
            Mezok.Add(88, new Mezo(88, "Munkahely", 8, this.Fizetes, 680, 187));
            Mezok.Add(89, new Mezo(89, "Munkahely", 8, this.Fizetes, 680, 222));
            Mezok.Add(90, new Mezo(90, "Munkahely", 8, this.Fizetes, 715, 222));
            Mezok.Add(91, new Mezo(91, "Munkahely", 8, this.Fizetes, 750, 222));
            Mezok.Add(92, new Mezo(92, "Munkahely", 8, this.Fizetes, 750, 187));
            Mezok.Add(93, new Mezo(93, "Munkahely", 8, this.Fizetes, 715, 187));

            #endregion

            #region lépések hozzáadása

            this.Mezokke(1, new List<int> { 2 }, new List<int> { 3 }, new List<int> { 4 }, new List<int> { 5 }, new List<int> { 6 }, new List<int> { 7 });
            this.Mezokke(2, new List<int> { 3 }, new List<int> { 4 }, new List<int> { 5 }, new List<int> { 6 }, new List<int> { 7 }, new List<int> { 8 });
            this.Mezokke(3, new List<int> { 4 }, new List<int> { 5 }, new List<int> { 6 }, new List<int> { 7 }, new List<int> { 8 }, new List<int> { 9 });
            this.Mezokke(4, new List<int> { 5 }, new List<int> { 6 }, new List<int> { 7 }, new List<int> { 8 }, new List<int> { 9 }, new List<int> { 16 });
            this.Mezokke(5, new List<int> { 6 }, new List<int> { 7 }, new List<int> { 8 }, new List<int> { 9 }, new List<int> { 16 }, new List<int> { 15, 17 });
            this.Mezokke(6, new List<int> { 7 }, new List<int> { 8 }, new List<int> { 9 }, new List<int> { 16 }, new List<int> { 15, 17 }, new List<int> { 14, 18 });
            this.Mezokke(7, new List<int> { 6, 8 }, new List<int> { 1, 9 }, new List<int> { 2, 16 }, new List<int> { 3, 15, 17 }, new List<int> { 4, 14, 18 }, new List<int> { 5, 13, 24, 19, 25 });
            this.Mezokke(8, new List<int> { 7, 9 }, new List<int> { 6, 16 }, new List<int> { 1, 15, 17 }, new List<int> { 2, 14, 18 }, new List<int> { 3, 13, 24, 19, 25 }, new List<int> { 4, 12, 28, 20, 29 });
            this.Mezokke(9, new List<int> { 8, 16 }, new List<int> { 7, 15, 17 }, new List<int> { 6, 14, 18 }, new List<int> { 1, 13, 24, 25, 19 }, new List<int> { 2, 12, 28, 20, 29 }, new List<int> { 3, 11, 32, 33, 21 });
            this.Mezokke(10, new List<int> { 11, 23 }, new List<int> { 12, 27 }, new List<int> { 31, 13 }, new List<int> { 35, 14 }, new List<int> { 15, 39, 24 }, new List<int> { 16, 28, 52, 40 });
            this.Mezokke(11, new List<int> { 10, 12 }, new List<int> { 13, 23 }, new List<int> { 14, 27 }, new List<int> { 15, 24, 31 }, new List<int> { 16, 28, 35 }, new List<int> { 9, 17, 32, 39 });
            this.Mezokke(12, new List<int> { 11, 13 }, new List<int> { 10, 14 }, new List<int> { 15, 23, 24 }, new List<int> { 16, 27, 28 }, new List<int> { 9, 17, 31, 32 }, new List<int> { 8, 18, 35, 36 });
            this.Mezokke(13, new List<int> { 12, 14 }, new List<int> { 11, 15, 24 }, new List<int> { 10, 16, 28 }, new List<int> { 9, 17, 23, 32 }, new List<int> { 8, 18, 27, 36 }, new List<int> { 7, 19, 31, 43, 25 });
            this.Mezokke(14, new List<int> { 13, 15, 24 }, new List<int> { 12, 16, 28 }, new List<int> { 9, 11, 17, 32 }, new List<int> { 10, 36, 8, 18 }, new List<int> { 23, 43, 7, 19, 25 }, new List<int> { 53, 27, 42, 44, 6, 20, 29 });
            this.Mezokke(15, new List<int> { 14, 16 }, new List<int> { 9, 13, 24, 17 }, new List<int> { 12, 28, 18, 8 }, new List<int> { 11, 32, 7, 19, 25 }, new List<int> { 10, 36, 6, 20, 29 }, new List<int> { 23, 43, 1, 21, 33 });
            this.Mezokke(16, new List<int> { 9, 15, 17 }, new List<int> { 8, 14, 18 }, new List<int> { 7, 13, 24, 19, 25 }, new List<int> { 6, 12, 28, 29, 20 }, new List<int> { 11, 32, 33, 21, 1 }, new List<int> { 10, 36, 37, 22, 2 });
            this.Mezokke(17, new List<int> { 16, 18 }, new List<int> { 9, 15, 19, 25 }, new List<int> { 8, 14, 20, 29 }, new List<int> { 7, 13, 24, 21, 33 }, new List<int> { 6, 12, 28, 22, 37 }, new List<int> { 1, 11, 32, 26, 47 });
            this.Mezokke(18, new List<int> { 17, 19, 25 }, new List<int> { 16, 20, 29 }, new List<int> { 9, 15, 33, 21 }, new List<int> { 8, 14, 37, 22 }, new List<int> { 7, 13, 24, 47, 26 }, new List<int> { 6, 12, 28, 46, 54, 48, 30 });
            this.Mezokke(19, new List<int> { 18, 20 }, new List<int> { 17, 21, 25 }, new List<int> { 16, 22, 29 }, new List<int> { 9, 15, 33, 26 }, new List<int> { 8, 14, 37, 30 }, new List<int> { 7, 24, 13, 47, 34 });
            this.Mezokke(20, new List<int> { 19, 21 }, new List<int> { 18, 22 }, new List<int> { 17, 25, 26 }, new List<int> { 16, 29, 30 }, new List<int> { 9, 15, 33, 34 }, new List<int> { 8, 14, 37, 38 });
            this.Mezokke(21, new List<int> { 20, 22 }, new List<int> { 19, 26 }, new List<int> { 18, 30 }, new List<int> { 17, 25, 34 }, new List<int> { 16, 29, 38 }, new List<int> { 9, 15, 33, 51 });
            this.Mezokke(22, new List<int> { 21, 26 }, new List<int> { 20, 30 }, new List<int> { 19, 34 }, new List<int> { 18, 38 }, new List<int> { 17, 25, 51 }, new List<int> { 16, 29, 50, 55 });
            this.Mezokke(23, new List<int> { 10, 27 }, new List<int> { 11, 31 }, new List<int> { 12, 35 }, new List<int> { 13, 39 }, new List<int> { 14, 40, 52 }, new List<int> { 15, 24, 41, 56 });
            this.Mezokke(24, new List<int> { 14, 28 }, new List<int> { 13, 15, 32 }, new List<int> { 12, 16, 36 }, new List<int> { 9, 11, 17, 43 }, new List<int> { 8, 10, 18, 42, 44, 53 }, new List<int> { 23, 41, 45, 57, 19, 25, 7 });
            this.Mezokke(25, new List<int> { 18, 29 }, new List<int> { 17, 19, 33 }, new List<int> { 16, 20, 37 }, new List<int> { 9, 15, 21, 47 }, new List<int> { 8, 14, 22, 46, 48, 54 }, new List<int> { 7, 13, 24, 26, 45, 49, 58 });
            this.Mezokke(26, new List<int> { 22, 30 }, new List<int> { 21, 34 }, new List<int> { 20, 38 }, new List<int> { 19, 51 }, new List<int> { 18, 50, 55 }, new List<int> { 17, 25, 49, 59 });
            this.Mezokke(27, new List<int> { 23, 31 }, new List<int> { 10, 35 }, new List<int> { 11, 39 }, new List<int> { 12, 40, 52 }, new List<int> { 13, 41, 56 }, new List<int> { 14, 42, 60 });
            this.Mezokke(28, new List<int> { 24, 32 }, new List<int> { 14, 36 }, new List<int> { 13, 15, 43 }, new List<int> { 12, 16, 42, 44, 53 }, new List<int> { 11, 17, 9, 41, 57, 45 }, new List<int> { 10, 18, 8, 40, 61, 46 });
            this.Mezokke(29, new List<int> { 25, 33 }, new List<int> { 18, 37 }, new List<int> { 17, 19, 47 }, new List<int> { 16, 20, 46, 48, 54 }, new List<int> { 9, 15, 21, 45, 58, 49 }, new List<int> { 8, 14, 22, 44, 62, 50 });
            this.Mezokke(30, new List<int> { 26, 34 }, new List<int> { 22, 38 }, new List<int> { 21, 51 }, new List<int> { 20, 50, 55 }, new List<int> { 19, 49, 59 }, new List<int> { 18, 48, 63 });
            this.Mezokke(31, new List<int> { 27, 35 }, new List<int> { 23, 39 }, new List<int> { 10, 40, 52 }, new List<int> { 11, 41, 56 }, new List<int> { 12, 42, 60 }, new List<int> { 13, 43, 64 });
            this.Mezokke(32, new List<int> { 28, 36 }, new List<int> { 24, 43 }, new List<int> { 14, 42, 44, 53 }, new List<int> { 13, 15, 41, 45, 57 }, new List<int> { 12, 16, 40, 61, 46 }, new List<int> { 11, 17, 9, 39, 65, 47 });
            this.Mezokke(33, new List<int> { 29, 37 }, new List<int> { 25, 47 }, new List<int> { 54, 18, 46, 48 }, new List<int> { 17, 19, 45, 49, 58 }, new List<int> { 16, 20, 44, 50, 62 }, new List<int> { 9, 15, 21, 51, 66, 43 });
            this.Mezokke(34, new List<int> { 30, 38 }, new List<int> { 26, 51 }, new List<int> { 22, 50, 55 }, new List<int> { 21, 49, 59 }, new List<int> { 20, 48, 63 }, new List<int> { 19, 47, 67 });
            this.Mezokke(35, new List<int> { 31, 39 }, new List<int> { 27, 40, 52 }, new List<int> { 23, 41, 56 }, new List<int> { 10, 42, 60 }, new List<int> { 11, 43, 64 }, new List<int> { 12, 36, 44, 53, 68 });
            this.Mezokke(36, new List<int> { 32, 43 }, new List<int> { 28, 42, 44, 53 }, new List<int> { 24, 41, 45, 57 }, new List<int> { 14, 40, 61, 46 }, new List<int> { 13, 15, 39, 65, 47 }, new List<int> { 12, 16, 35, 52, 72, 37, 48, 54 });
            this.Mezokke(37, new List<int> { 33, 47 }, new List<int> { 29, 46, 48, 54 }, new List<int> { 25, 45, 49, 58 }, new List<int> { 18, 44, 50, 62 }, new List<int> { 17, 19, 43, 51, 66 }, new List<int> { 16, 20, 42, 53, 36, 76, 55, 38 });
            this.Mezokke(38, new List<int> { 34, 51 }, new List<int> { 30, 50, 55 }, new List<int> { 26, 49, 59 }, new List<int> { 22, 48, 63 }, new List<int> { 21, 47, 67 }, new List<int> { 20, 54, 46, 37, 80 });
            this.Mezokke(39, new List<int> { 35, 40, 52 }, new List<int> { 31, 41, 56 }, new List<int> { 27, 42, 60 }, new List<int> { 23, 43, 64 }, new List<int> { 10, 36, 44, 53, 68 }, new List<int> { 11, 32, 45, 57, 69 });
            this.Mezokke(40, new List<int> { 39, 41 }, new List<int> { 35, 42, 52 }, new List<int> { 31, 43, 56 }, new List<int> { 27, 36, 44, 53, 60 }, new List<int> { 23, 32, 45, 57, 64 }, new List<int> { 10, 68, 28, 46, 61 });
            this.Mezokke(41, new List<int> { 40, 42 }, new List<int> { 39, 43 }, new List<int> { 35, 52, 36, 44, 53 }, new List<int> { 31, 56, 32, 45, 57 }, new List<int> { 27, 60, 28, 46, 61 }, new List<int> { 23, 64, 24, 47, 65 });
            this.Mezokke(42, new List<int> { 41, 43 }, new List<int> { 36, 40, 53, 44 }, new List<int> { 32, 39, 45, 57 }, new List<int> { 35, 52, 28, 46, 61 }, new List<int> { 31, 24, 47, 65, 56 }, new List<int> { 14, 37, 48, 54, 72, 60, 27 });
            this.Mezokke(43, new List<int> { 36, 42, 44, 53 }, new List<int> { 32, 45, 57, 41 }, new List<int> { 28, 46, 61, 40 }, new List<int> { 24, 39, 47, 65 }, new List<int> { 35, 52, 14, 37, 48, 54, 72 }, new List<int> { 13, 15, 33, 49, 58, 71, 73, 31, 56 });
            this.Mezokke(44, new List<int> { 43, 45 }, new List<int> { 36, 42, 53, 46 }, new List<int> { 32, 41, 57, 47 }, new List<int> { 28, 40, 61, 37, 48, 54 }, new List<int> { 24, 39, 65, 33, 49, 58 }, new List<int> { 14, 35, 52, 72, 29, 50, 62 });
            this.Mezokke(45, new List<int> { 44, 46 }, new List<int> { 43, 47 }, new List<int> { 36, 42, 53, 37, 48, 54 }, new List<int> { 32, 41, 57, 33, 49, 58 }, new List<int> { 28, 40, 61, 29, 50, 62 }, new List<int> { 24, 39, 65, 25, 51, 66 });
            this.Mezokke(46, new List<int> { 45, 47 }, new List<int> { 37, 44, 48, 54 }, new List<int> { 33, 43, 49, 58 }, new List<int> { 29, 50, 62, 36, 42, 53 }, new List<int> { 25, 51, 66, 32, 41, 57 }, new List<int> { 28, 61, 40, 18, 38, 55, 76 });
            this.Mezokke(47, new List<int> { 37, 46, 48, 54 }, new List<int> { 33, 49, 58, 45 }, new List<int> { 29, 50, 62, 44 }, new List<int> { 25, 51, 66, 43 }, new List<int> { 18, 38, 55, 76, 36, 42, 53 }, new List<int> { 17, 19, 34, 59, 77, 75, 41, 32, 57 });
            this.Mezokke(48, new List<int> { 47, 49 }, new List<int> { 37, 46, 54, 50 }, new List<int> { 33, 45, 58, 51 }, new List<int> { 29, 44, 62, 38, 55 }, new List<int> { 43, 25, 66, 34, 59 }, new List<int> { 42, 36, 53, 76, 63, 30, 18 });
            this.Mezokke(49, new List<int> { 48, 50 }, new List<int> { 47, 51 }, new List<int> { 37, 46, 54, 38, 55 }, new List<int> { 33, 45, 58, 34, 59 }, new List<int> { 29, 44, 62, 30, 63 }, new List<int> { 25, 43, 66, 26, 67 });
            this.Mezokke(50, new List<int> { 49, 51 }, new List<int> { 48, 38, 55 }, new List<int> { 47, 34, 59 }, new List<int> { 37, 46, 54, 30, 63 }, new List<int> { 33, 45, 58, 26, 67 }, new List<int> { 29, 44, 62, 22, 80 });
            this.Mezokke(51, new List<int> { 38, 50, 55 }, new List<int> { 34, 49, 59 }, new List<int> { 30, 48, 63 }, new List<int> { 26, 47, 67 }, new List<int> { 22, 37, 46, 54, 80 }, new List<int> { 21, 33, 45, 58, 79 });
            this.Mezokke(52, new List<int> { 39, 56 }, new List<int> { 35, 40, 60 }, new List<int> { 31, 41, 64 }, new List<int> { 27, 68, 42 }, new List<int> { 23, 43, 69 }, new List<int> { 53, 10, 36, 44, 70 });
            this.Mezokke(53, new List<int> { 43, 57 }, new List<int> { 42, 36, 44, 61 }, new List<int> { 41, 32, 45, 65 }, new List<int> { 40, 28, 46, 72 }, new List<int> { 39, 24, 47, 71, 73 }, new List<int> { 14, 37, 48, 54, 70, 74, 35, 52 });
            this.Mezokke(54, new List<int> { 47, 58 }, new List<int> { 37, 46, 48, 62 }, new List<int> { 33, 45, 49, 66 }, new List<int> { 29, 44, 50, 76 }, new List<int> { 25, 43, 75, 77, 51 }, new List<int> { 18, 38, 55, 74, 78, 36, 42, 53 });
            this.Mezokke(55, new List<int> { 51, 59 }, new List<int> { 50, 38, 63 }, new List<int> { 34, 49, 67 }, new List<int> { 30, 48, 80 }, new List<int> { 26, 47, 79 }, new List<int> { 22, 78, 81, 37, 46, 54 });
            this.Mezokke(56, new List<int> { 52, 60 }, new List<int> { 39, 64 }, new List<int> { 35, 40, 68 }, new List<int> { 31, 41, 69 }, new List<int> { 27, 42, 70 }, new List<int> { 23, 43, 71 });
            this.Mezokke(57, new List<int> { 53, 61 }, new List<int> { 43, 65 }, new List<int> { 42, 44, 36, 72 }, new List<int> { 32, 41, 45, 71, 73 }, new List<int> { 40, 28, 46, 70, 74 }, new List<int> { 39, 24, 47, 75, 69 });
            this.Mezokke(58, new List<int> { 54, 62 }, new List<int> { 47, 66 }, new List<int> { 46, 37, 48, 76 }, new List<int> { 33, 45, 49, 75, 77 }, new List<int> { 44, 29, 50, 74, 78 }, new List<int> { 25, 51, 79, 73, 43 });
            this.Mezokke(59, new List<int> { 55, 63 }, new List<int> { 51, 67 }, new List<int> { 38, 50, 80 }, new List<int> { 34, 49, 79 }, new List<int> { 30, 48, 78, 81 }, new List<int> { 26, 47, 77, 82 });
            this.Mezokke(60, new List<int> { 56, 64 }, new List<int> { 52, 68 }, new List<int> { 39, 69 }, new List<int> { 35, 40, 70 }, new List<int> { 31, 41, 71 }, new List<int> { 27, 42, 72 });
            this.Mezokke(61, new List<int> { 57, 65 }, new List<int> { 53, 72 }, new List<int> { 43, 71, 73 }, new List<int> { 42, 36, 44, 70, 74 }, new List<int> { 32, 41, 45, 69, 75 }, new List<int> { 28, 40, 46, 68, 76 });
            this.Mezokke(62, new List<int> { 58, 66 }, new List<int> { 54, 76 }, new List<int> { 47, 75, 77 }, new List<int> { 37, 46, 48, 74, 78 }, new List<int> { 33, 45, 49, 79, 73 }, new List<int> { 29, 50, 44, 72, 80, 81 });
            this.Mezokke(63, new List<int> { 59, 67 }, new List<int> { 55, 80 }, new List<int> { 51, 79 }, new List<int> { 38, 50, 78, 81 }, new List<int> { 34, 49, 77, 82 }, new List<int> { 30, 48, 76, 83 });
            this.Mezokke(64, new List<int> { 60, 68 }, new List<int> { 56, 69 }, new List<int> { 52, 70 }, new List<int> { 39, 71 }, new List<int> { 35, 40, 72 }, new List<int> { 31, 41, 65, 73 });
            this.Mezokke(65, new List<int> { 61, 72 }, new List<int> { 57, 71, 73 }, new List<int> { 53, 70, 74 }, new List<int> { 43, 69, 75 }, new List<int> { 36, 42, 44, 68, 76 }, new List<int> { 32, 41, 45, 66, 77, 64 });
            this.Mezokke(66, new List<int> { 62, 76 }, new List<int> { 58, 75, 77 }, new List<int> { 54, 74, 78 }, new List<int> { 47, 73, 79 }, new List<int> { 37, 46, 48, 72, 80, 81 }, new List<int> { 33, 45, 49, 71, 65, 82, 67 });
            this.Mezokke(67, new List<int> { 63, 80 }, new List<int> { 59, 79 }, new List<int> { 55, 78, 81 }, new List<int> { 51, 77, 82 }, new List<int> { 38, 50, 76, 83 }, new List<int> { 34, 49, 75, 84, 66 });
            this.Mezokke(68, new List<int> { 64, 69 }, new List<int> { 60, 70 }, new List<int> { 56, 71 }, new List<int> { 52, 72 }, new List<int> { 39, 65, 73 }, new List<int> { 35, 40, 61, 74 });
            this.Mezokke(69, new List<int> { 68, 70 }, new List<int> { 64, 71 }, new List<int> { 60, 72 }, new List<int> { 56, 65, 73 }, new List<int> { 52, 61, 74 }, new List<int> { 39, 57, 75 });
            this.Mezokke(70, new List<int> { 69, 71 }, new List<int> { 68, 72 }, new List<int> { 64, 65, 73 }, new List<int> { 60, 61, 74 }, new List<int> { 56, 57, 75 }, new List<int> { 52, 53, 76 });
            this.Mezokke(71, new List<int> { 70, 72 }, new List<int> { 69, 65, 73 }, new List<int> { 61, 68, 74 }, new List<int> { 57, 64, 75 }, new List<int> { 53, 60, 76 }, new List<int> { 56, 43, 66, 77 });
            this.Mezokke(72, new List<int> { 65, 71, 73 }, new List<int> { 61, 70, 74 }, new List<int> { 57, 69, 75 }, new List<int> { 53, 68, 76 }, new List<int> { 43, 64, 66, 77 }, new List<int> { 42, 36, 44, 60, 62, 78 });
            this.Mezokke(73, new List<int> { 72, 74 }, new List<int> { 65, 71, 75 }, new List<int> { 61, 70, 76 }, new List<int> { 57, 69, 77, 66 }, new List<int> { 53, 68, 62, 78 }, new List<int> { 43, 64, 58, 79 });
            this.Mezokke(74, new List<int> { 73, 75 }, new List<int> { 72, 76 }, new List<int> { 65, 71, 66, 77 }, new List<int> { 61, 70, 62, 78 }, new List<int> { 57, 69, 58, 79 }, new List<int> { 53, 68, 54, 80, 81 });
            this.Mezokke(75, new List<int> { 74, 76 }, new List<int> { 66, 73, 77 }, new List<int> { 62, 72, 78 }, new List<int> { 65, 71, 58, 79 }, new List<int> { 61, 70, 54, 80, 81 }, new List<int> { 69, 57, 47, 67, 82 });
            this.Mezokke(76, new List<int> { 66, 75, 77 }, new List<int> { 62, 74, 78 }, new List<int> { 58, 73, 79 }, new List<int> { 54, 72, 80, 81 }, new List<int> { 47, 71, 65, 67, 82 }, new List<int> { 37, 46, 48, 61, 70, 63, 83 });
            this.Mezokke(77, new List<int> { 76, 78 }, new List<int> { 66, 75, 79 }, new List<int> { 62, 74, 80, 81 }, new List<int> { 58, 73, 67, 82 }, new List<int> { 54, 72, 63, 83 }, new List<int> { 47, 65, 71, 84, 59 });
            this.Mezokke(78, new List<int> { 77, 79 }, new List<int> { 76, 80, 81 }, new List<int> { 66, 75, 67, 82 }, new List<int> { 62, 63, 74, 83 }, new List<int> { 58, 59, 73, 84 }, new List<int> { 54, 55, 72, 85 });
            this.Mezokke(79, new List<int> { 78, 80, 81 }, new List<int> { 77, 67, 82 }, new List<int> { 63, 76, 83 }, new List<int> { 66, 75, 59, 84 }, new List<int> { 62, 55, 74, 85 }, new List<int> { 58, 73, 51, 86 });
            this.Mezokke(80, new List<int> { 67, 79 }, new List<int> { 63, 78, 81 }, new List<int> { 59, 77, 82 }, new List<int> { 55, 76, 83 }, new List<int> { 51, 66, 75, 84 }, new List<int> { 38, 50, 62, 74, 85 });
            this.Mezokke(81, new List<int> { 79, 82 }, new List<int> { 78, 80, 83 }, new List<int> { 67, 77, 84 }, new List<int> { 76, 63, 85 }, new List<int> { 66, 75, 59, 86 }, new List<int> { 62, 74, 55, 87 });
            this.Mezokke(82, new List<int> { 81, 83 }, new List<int> { 79, 84 }, new List<int> { 78, 80, 85 }, new List<int> { 77, 67, 86 }, new List<int> { 76, 63, 87 }, new List<int> { 66, 75, 59, 88 });
            this.Mezokke(83, new List<int> { 82, 84 }, new List<int> { 81, 85 }, new List<int> { 79, 86 }, new List<int> { 78, 80, 87 }, new List<int> { 77, 67, 88 }, new List<int> { 76, 63, 89 });
            this.Mezokke(84, new List<int> { 83, 85 }, new List<int> { 82, 86 }, new List<int> { 81, 87 }, new List<int> { 79, 88 }, new List<int> { 78, 80, 89 }, new List<int> { 67, 77, 90 });
            this.Mezokke(85, new List<int> { 84, 86 }, new List<int> { 83, 87 }, new List<int> { 82, 88 }, new List<int> { 81, 89 }, new List<int> { 79, 90 }, new List<int> { 78, 80, 91 });
            this.Mezokke(86, new List<int> { 85, 87 }, new List<int> { 84, 88 }, new List<int> { 83, 89 }, new List<int> { 82, 90 }, new List<int> { 81, 91 }, new List<int> { 79, 92 });
            this.Mezokke(87, new List<int> { 88, 86 }, new List<int> { 85, 89 }, new List<int> { 84, 90 }, new List<int> { 91, 83 }, new List<int> { 82, 92 }, new List<int> { 81, 93 });
            this.Mezokke(88, new List<int> { 87 }, new List<int> { 86 }, new List<int> { 85 }, new List<int> { 84 }, new List<int> { 83 }, new List<int> { 82 });
            this.Mezokke(89, new List<int> { 90 }, new List<int> { 91 }, new List<int> { 92 }, new List<int> { 93 }, new List<int> { 88 }, new List<int> { 87 });
            this.Mezokke(90, new List<int> { 91 }, new List<int> { 92 }, new List<int> { 93 }, new List<int> { 88 }, new List<int> { 87 }, new List<int> { 86 });
            this.Mezokke(91, new List<int> { 92 }, new List<int> { 93 }, new List<int> { 88 }, new List<int> { 87 }, new List<int> { 86 }, new List<int> { 85 });
            this.Mezokke(92, new List<int> { 93 }, new List<int> { 88 }, new List<int> { 87 }, new List<int> { 86 }, new List<int> { 85 }, new List<int> { 84 });
            this.Mezokke(93, new List<int> { 88 }, new List<int> { 87 }, new List<int> { 86 }, new List<int> { 85 }, new List<int> { 84 }, new List<int> { 83 });

            #endregion

            #region kártyák hozzáadása
            int i = 1;
            Kartyak.Add(i, new Szerencsekartya(i++, "Defeketet kaptál", 1, 1));
            Kartyak.Add(i, new Szerencsekartya(i++, "Telekocsi", 3, 1));
            Kartyak.Add(i, new Szerencsekartya(i++, "Rendszeresen sportolsz", 3, 1));
            Kartyak.Add(i, new Szerencsekartya(i++, "Autóversenyre mentél", 2, 5000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Forgalmi dugóba kerültél", 1, 1));
            Kartyak.Add(i, new Szerencsekartya(i++, "Kirabolnak és a rendőrségen vagy", 1, 2));
            Kartyak.Add(i, new Szerencsekartya(i++, "Bankrablásba keveredsz", 1, 3));
            Kartyak.Add(i, new Szerencsekartya(i++, "Elterelik a forgalmat", 1, 1));
            Kartyak.Add(i, new Szerencsekartya(i++, "Bevásároltál a hétvégére", 2, 3000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Kerékpárral közlekedsz", 3, 2));
            Kartyak.Add(i, new Szerencsekartya(i++, "Koncerten voltál", 2, 2000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Tiltott helyen dohányoztál", 2, 5000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Kaszinóban sokat nyertél", 4, 10000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Elrántottál egy embert egy autó elől", 3, 3));
            Kartyak.Add(i, new Szerencsekartya(i++, "Hétvégén sétáltál a parkban", 3, 1));
            Kartyak.Add(i, new Szerencsekartya(i++, "Másnaposság gyötör", 1, 2));
            Kartyak.Add(i, new Szerencsekartya(i++, "Balesetet szenvedsz", 1, 3));
            Kartyak.Add(i, new Szerencsekartya(i++, "Környezettudatosan élsz", 3, 3));
            Kartyak.Add(i, new Szerencsekartya(i++, "Kaszinóban sokat vesztettél", 2, 12000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Megbírságol az adóhivatal", 2, 10000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Átmentél a piroson", 2, 10000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Szemeteltél", 2, 5000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Adóvisszatérítést kapsz", 4, 7000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Moziba mentél", 2, 1500));
            Kartyak.Add(i, new Szerencsekartya(i++, "Éjszaka szórakozni mentél", 2, 5000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Lóversenyeztél", 4, 4000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Strandon voltál", 2, 1000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Tömegközlekedsz", 3, 2));
            Kartyak.Add(i, new Szerencsekartya(i++, "Focimeccsen voltál", 2, 500));
            Kartyak.Add(i, new Szerencsekartya(i++, "Színházba mentél", 2, 1000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Lóversenyeztél", 2, 5000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Vidámparkban voltál", 2, 3000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Bekajáltál", 1, 1));
            Kartyak.Add(i, new Szerencsekartya(i++, "Fizett szabadságot kapsz", 4, 20000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Kimentettél egy fuldoklót a folyóból", 3, 2));
            Kartyak.Add(i, new Szerencsekartya(i++, "Pazaroltál", 1, 1));
            Kartyak.Add(i, new Szerencsekartya(i++, "Egy barátod visszafizeti a kölcsönt", 4, 4000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Lebetegedsz", 1, 1));
            Kartyak.Add(i, new Szerencsekartya(i++, "Sorsoláson mosógépet nyertél",5 ,1 ));
            Kartyak.Add(i, new Szerencsekartya(i++, "Sorsoláson TV-t nyertél", 5, 2));
            Kartyak.Add(i, new Szerencsekartya(i++, "Sorsoláson számítógépet nyertél", 5, 3));
            Kartyak.Add(i, new Szerencsekartya(i++, "Sorsoláson hűtőt nyertél", 5, 4));
            Kartyak.Add(i, new Szerencsekartya(i++, "Sorsoláson konyhabútort nyertél", 5, 5));
            Kartyak.Add(i, new Szerencsekartya(i++, "Sorsoláson ágyat nyertél", 5, 6));
            Kartyak.Add(i, new Szerencsekartya(i++, "Sorsoláson szekrényt nyertél", 5, 7));
            Kartyak.Add(i, new Szerencsekartya(i++, "Megkötheted lakásodra a biztosítást", 6, 8));
            Kartyak.Add(i, new Szerencsekartya(i++, "Megkötheted autódra a biztosítást", 6, 9));
            Kartyak.Add(i, new Szerencsekartya(i++, "Innen csak 6-os dobással mehetsz tovább", 8, 0));
            Kartyak.Add(i, new Szerencsekartya(i++, "Lottón 3 találatod volt", 4, 30000));
            Kartyak.Add(i, new Szerencsekartya(i++, "Elhelyezted készpénzedet kamatozó bankbetétbe", 6, 0));
            Kartyak.Add(i, new Szerencsekartya(i++, "Bankbetéteidre 20% kamatot kapsz", 4, 20));
            Kartyak.Add(i, new Szerencsekartya(i++, "Bankbetéteidre 10% kamatot kapsz", 4, 10));
            Kartyak.Add(i, new Szerencsekartya(i++, "Lakásodban tűz ütött ki.", 7, 0));
            Kartyak.Add(i, new Szerencsekartya(i++, "Mosógépet vehetsz", 6, 1));
            Kartyak.Add(i, new Szerencsekartya(i++, "TV-t vehetsz", 6, 2));
            Kartyak.Add(i, new Szerencsekartya(i++, "Számítógépet vehetsz", 6, 3));
            Kartyak.Add(i, new Szerencsekartya(i++, "Hűtőt vehetsz", 6, 4));
            Kartyak.Add(i, new Szerencsekartya(i++, "Konyhabútort vehetsz", 6, 5));
            Kartyak.Add(i, new Szerencsekartya(i++, "Ágyat vehetsz", 6, 6));
            Kartyak.Add(i, new Szerencsekartya(i++, "Szekrényt vehetsz", 6, 7));
            Kartyak.Add(i, new Szerencsekartya(i++, "Kamat- és jutalékmentes részletre vásárolhatsz lakást", 6, 10));

            for (int j = 1; j <= this.Kartyak.Count; j++)
            {
                this.Huzottak.Add(j, false);
            }

            #endregion
        }

        private void Mezokke(int m, List<int> d1, List<int> d2, List<int> d3, List<int> d4, List<int> d5, List<int> d6)
        {
            List<Mezo> egy = new List<Mezo>();
            List<Mezo> ketto = new List<Mezo>();
            List<Mezo> harom = new List<Mezo>();
            List<Mezo> negy = new List<Mezo>();
            List<Mezo> ot = new List<Mezo>();
            List<Mezo> hat = new List<Mezo>();

            foreach (int i in d1)
            {
                egy.Add(this.Mezok[i]);
            }
            foreach (int i in d2)
            {
                ketto.Add(this.Mezok[i]);
            }
            foreach (int i in d3)
            {
                harom.Add(this.Mezok[i]);
            }
            foreach (int i in d4)
            {
                negy.Add(this.Mezok[i]);
            }
            foreach (int i in d5
                )
            {
                ot.Add(this.Mezok[i]);
            }
            foreach (int i in d6)
            {
                hat.Add(this.Mezok[i]);
            }
            this.Mezok[m].Elrendez(egy, ketto, harom, negy, ot, hat);
        }
    }
}
