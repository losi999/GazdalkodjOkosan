using System;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GazdalkodjOkosan
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TouchCollection touch;

        static readonly TimeSpan varakozas = TimeSpan.FromMilliseconds(1000);
        static readonly TimeSpan villogas = TimeSpan.FromMilliseconds(500);
        TimeSpan varakozasstart, villogasstart;

        //Texture2D cursor;
        Texture2D igenikon, nemikon, kuponikon;
        Texture2D dobas, ok, vissza, tovabb, megveszem, igen, nem, vasarolok, felszereles, felszerelesnagy, mentes, kilepes;
        Texture2D lepesablak, hatter, fopanel, vasarlasablak, visszakerdezoablak, valaszablak, szerencsekartyaablak, felszerelesablak, kovetkezoablak, dobasablak, kimaradablak, kiesettablak;
        Texture2D menuhatter, szovegmezo, jatekospanel, szinvalasztoablak, szovegmezohiba, nyertesablak;
        Texture2D ujjatek, beallitasok, start, plusz, minusz, betoltes;
        Texture2D beallitaspanel, scroll, beallitasokszovegmezo, alapertek, megse;
        Texture2D backspace, enter, karakter, shift, space, billhatter, szovegmezohatter, billszovegmezo;
        Texture2D szam, backspacenagy, enternagy, szamlaphatter, szammezohatter;
        Dictionary<int, Texture2D> MezoNormal;
        Dictionary<int, Texture2D> MezoOpcio;
        Dictionary<int, Texture2D> MezoNagy;
        Dictionary<int, Texture2D> BabuNormal;
        Dictionary<int, Texture2D> BabuNagy;
        Dictionary<int, Texture2D> FopanelFulek;
        Dictionary<int, Texture2D> FopanelAktivFulek;

        Mezo mezo;
        public Tabla tabla { get; set; }
        Dictionary<int, Jatekos> jatekosok;

        int dob;
        int jatekkor;
        List<Mezo> lepesek;
        float touchX, touchY;
        float lepesablakscale, vasarlasablakscale, visszakerdezoablakscale, valaszablakscale, szerencsekartyaablakscale, felszerelesablakscale, dobasablakscale, kimaradablakscale, kiesettablakscale, kovetkezoablakscale, szinablakscale;
        float ablakscalemertek;
        float tablascale;
        float tablazoom;
        float tablascalemertek;
        float billmertek = 30;
        float tablaXMozgat, tablaYMozgat;
        float lepesXMozgas, lepesYMozgas;
        float panelfontmagassag;
        int lepesszam, mozdulas;
        int aktivjatekos;

        int sorszam;
        int jatekossorszam;
        int mezosorszam;
        String jatekosnev;
        String mezoertek, mezocimke;
        int maxszamjegy;
        int aktualis, aktualispanel;
        int termeksorszam, termekdb;
        int vasarlasvalasz;
        int szerencsekartyavalasz;
        String vasarlasvalaszszoveg;
        String szerencsekartyavalaszszoveg;
        String lepessor;
        String mezohatasszoveg;
        String nyertes;
        Dictionary<int, List<float>> eltolasX, eltolasY;
        Dictionary<int, String> nevek;
        Dictionary<int, int> szinek;
        Dictionary<int, List<String>> karakterek;
        Dictionary<int, List<String>> szamok;
        Dictionary<int, bool> valasztottszinek;
        Dictionary<int, int> ertekek;
        Dictionary<int, String> cimkek;
        
        float keret;
        float tovabbX, tovabbY;
        float vasarlasablakX, vasarlasablakY;
        float fopanelX, fopanelY;
        float fopanelfulekX, fopanelfulekY;
        float minuszX, minuszY;
        float jatekospanelX, jatekospanelY;
        float szovegmezoX, szovegmezoY;
        float billhatterX, billhatterY;
        float szovegmezohatterX, szovegmezohatterY;
        float szamlaphatterX, szamlaphatterY;
        float szammezohatterX, szammezohatterY;
        float startX, startY;
        float visszaX, visszaY;
        float pluszX, pluszY;
        float alapertekX, alapertekY;
        float mentesX, mentesY;
        float megseX, megseY;
        float beallitaspanelX, beallitaspanelY;
        float scrollX, scrollY;
        float dobasX, dobasY;
        float jatekmentesX, jatekmentesY;
        float kilepesX, kilepesY;
        float felszerelesX, felszerelesY;
        float X;
        float alpha = 1;
        float alphamertek = 0.1f;
        float csuszas = 0;

        float scrollmozgas = 0;
        float scrolltavolsag;
        int kov;
        bool nagybetu = false;
        bool villog = false;
        int halvanyul;
        bool hozzaad = false;

        SpriteFont gamefont;
        SpriteFont panelfont;
        SpriteFont ujjatekfont;
        SpriteFont billfont;
        SpriteFont szamfont;

        int Allapot ;


        public Game()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 800;

            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;

            Content.RootDirectory = "Content";

            TargetElapsedTime = TimeSpan.FromTicks(333333);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            jatekkor = 1;
            ablakscalemertek = 0.05f;

            eltolasX = new Dictionary<int, List<float>>();
            eltolasX.Add(1, new List<float> { 0f });
            eltolasX.Add(2, new List<float> { 0f, 0f });
            eltolasX.Add(3, new List<float> { 5.0f, 5.0f, -10.0f });
            eltolasX.Add(4, new List<float> { -7.07f, -7.07f, 7.07f, 7.07f });
            eltolasX.Add(5, new List<float> { -10.0f, 7.51f, 7.51f, -3.09f, -3.09f });

            eltolasY = new Dictionary<int, List<float>>();
            eltolasY.Add(1, new List<float> { -10.0f });
            eltolasY.Add(2, new List<float> { -10.0f, 10.0f });
            eltolasY.Add(3, new List<float> { -8.66f, 8.66f, 0f });
            eltolasY.Add(4, new List<float> { 7.07f, -7.07f, 7.07f, -7.07f });
            eltolasY.Add(5, new List<float> { 0f, +6.66f, -6.66f, -9.51f, 9.51f });

            this.tabla = new Tabla();
            this.aktualis = 1;
            this.aktualispanel = 1;

            this.Allapot = Konstansok.START;
            dob = 0;
            
            lepesek = new List<Mezo>();
            
            MezoNormal = new Dictionary<int, Texture2D>();
            MezoOpcio = new Dictionary<int, Texture2D>();
            MezoNagy = new Dictionary<int, Texture2D>();
            BabuNormal = new Dictionary<int, Texture2D>();
            BabuNagy = new Dictionary<int, Texture2D>();
            FopanelFulek = new Dictionary<int, Texture2D>();
            FopanelAktivFulek = new Dictionary<int, Texture2D>();

            jatekosok = new Dictionary<int, Jatekos>();
            nevek = new Dictionary<int, string>();
            szinek = new Dictionary<int, int>();
            karakterek = new Dictionary<int, List<String>>();
            szamok = new Dictionary<int, List<String>>();
            valasztottszinek = new Dictionary<int, bool>();
            ertekek = new Dictionary<int, int>();
            cimkek = new Dictionary<int, String>();

            karakterek.Add(1, new List<String> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "ö", "ü", "ó" });
            karakterek.Add(2, new List<String> { "q", "w", "e", "r", "t", "z", "u", "i", "o", "p", "õ", "ú", "" });
            karakterek.Add(3, new List<String> { "a", "s", "d", "f", "g", "h", "j", "k", "l", "é", "á", "û", "" });
            karakterek.Add(4, new List<String> { "", "", "í", "y", "x", "c", "v", "b", "n", "m", "", "", "", "" });
            karakterek.Add(5, new List<String> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "Ö", "Ü", "Ó" });
            karakterek.Add(6, new List<String> { "Q", "W", "E", "R", "T", "Z", "U", "I", "O", "P", "Õ", "Ú", "" });
            karakterek.Add(7, new List<String> { "A", "S", "D", "F", "G", "H", "J", "K", "L", "É", "Á", "Û", "" });
            karakterek.Add(8, new List<String> { "", "", "Í", "Y", "X", "C", "V", "B", "N", "M", "", "", "", "" });

            szamok.Add(1, new List<String> { "3", "6", "9", "" });
            szamok.Add(2, new List<String> { "2", "5", "8", "0" });
            szamok.Add(3, new List<String> { "1", "4", "7", "" });

            try
            {
                IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
                FileStream stream = store.OpenFile("ertekek.txt", FileMode.Open, FileAccess.Read);
                TextReader tr = new StreamReader(stream);

                ertekek.Add(1, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(2, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(3, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(4, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(5, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(6, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(7, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(8, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(9, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(10, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(11, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(12, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(13, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(14, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(15, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(16, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(17, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(18, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(19, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(20, Convert.ToInt32(tr.ReadLine()));
                ertekek.Add(21, Convert.ToInt32(tr.ReadLine()));

                tr.Close();
            }
            catch
            {
                ertekek.Add(1, Konstansok.Kezdo);
                ertekek.Add(2, Konstansok.Fizetes);
                ertekek.Add(3, Konstansok.LakasAr);
                ertekek.Add(4, Konstansok.GarazsAr);
                ertekek.Add(5, Konstansok.AutoAr);
                ertekek.Add(6, Konstansok.AgyAr);
                ertekek.Add(7, Konstansok.HutoAr);
                ertekek.Add(8, Konstansok.MosogepAr);
                ertekek.Add(9, Konstansok.KonyhabutorAr);
                ertekek.Add(10, Konstansok.PCAr);
                ertekek.Add(11, Konstansok.SzekrenyAr);
                ertekek.Add(12, Konstansok.TVAr);
                ertekek.Add(13, Konstansok.LakasBiztositasAr);
                ertekek.Add(14, Konstansok.AutoBiztositasAr);
                ertekek.Add(15, Konstansok.IngatlanirodaJutalek);
                ertekek.Add(16, Konstansok.ReszletKamat);
                ertekek.Add(17, Konstansok.ReszletDB);
                ertekek.Add(18, Konstansok.ReszletKor);
                ertekek.Add(19, Konstansok.Kezdoreszlet);
                ertekek.Add(20, Konstansok.BetetKamat);
                ertekek.Add(21, Konstansok.BetetKamatKorDB);
            }

            tabla.Kezdo = ertekek[1];
            tabla.Fizetes = ertekek[2];
            tabla.LakasAr = ertekek[3];
            tabla.GarazsAr = ertekek[4];
            tabla.AutoAr = ertekek[5];
            tabla.AgyAr = ertekek[6];
            tabla.HutoAr = ertekek[7];
            tabla.MosogepAr = ertekek[8];
            tabla.KonyhabutorAr = ertekek[9];
            tabla.PCAr = ertekek[10];
            tabla.SzekrenyAr = ertekek[11];
            tabla.TVAr = ertekek[12];
            tabla.LakasBiztositasAr = ertekek[13];
            tabla.AutoBiztositasAr = ertekek[14];
            tabla.IngatlanirodaJutalek = ertekek[15];
            tabla.ReszletKamat = ertekek[16];
            tabla.ReszletDB = ertekek[17];
            tabla.ReszletKor = ertekek[18];
            tabla.Kezdoreszlet = ertekek[19];
            tabla.BetetKamat = ertekek[20];
            tabla.BetetKamatKorDB = ertekek[21];

            cimkek.Add(1, "Kezdõ összeg");
            cimkek.Add(2, "Fizetés");
            cimkek.Add(3, "Lakás ár");
            cimkek.Add(4, "Garázs ár");
            cimkek.Add(5, "Autó ár");
            cimkek.Add(6, "Ágy ár");
            cimkek.Add(7, "Hûtõ ár");
            cimkek.Add(8, "Mosógép ár");
            cimkek.Add(9, "Konyhabútor ár");
            cimkek.Add(10, "Számítógép ár");
            cimkek.Add(11, "Szekrény ár");
            cimkek.Add(12, "TV ár");
            cimkek.Add(13, "Lakásbiztosítás ár");
            cimkek.Add(14, "Autó biztosítás ár");
            cimkek.Add(15, "Ingatlaniroda jutalék");
            cimkek.Add(16, "Részletfizetés kamata");
            cimkek.Add(17, "Részletek száma");
            cimkek.Add(18, "Törlesztés");
            cimkek.Add(19, "Kezdõrészlet");
            cimkek.Add(20, "Bankbetét kamat");
            cimkek.Add(21, "Bankbetét kamatozás");

            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.HorizontalDrag |GestureType.Hold;

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region játék elemek

            gamefont = Content.Load<SpriteFont>("Fontok/gameFont");
            panelfont = Content.Load<SpriteFont>("Fontok/panelFont");

            panelfontmagassag = panelfont.MeasureString("A").Y;

            igenikon = Content.Load<Texture2D>("igen");
            nemikon = Content.Load<Texture2D>("nem");
            kuponikon = Content.Load<Texture2D>("kupon");

            dobas = Content.Load<Texture2D>("Gombok/dobas");
            ok = Content.Load<Texture2D>("Gombok/ok");
            vissza = Content.Load<Texture2D>("Gombok/vissza");
            tovabb = Content.Load<Texture2D>("Gombok/tovabb");
            megveszem = Content.Load<Texture2D>("Gombok/megveszem");
            igen = Content.Load<Texture2D>("Gombok/igen");
            nem = Content.Load<Texture2D>("Gombok/nem");
            vasarolok = Content.Load<Texture2D>("Gombok/vasarolok");
            felszereles = Content.Load<Texture2D>("Gombok/felszereles");
            felszerelesnagy = Content.Load<Texture2D>("Gombok/felszerelesnagy");
            mentes = Content.Load<Texture2D>("Gombok/mentes");
            kilepes = Content.Load<Texture2D>("Gombok/kilepes");

            lepesablak = Content.Load<Texture2D>("Panelek/lepes_ablak");
            vasarlasablak = Content.Load<Texture2D>("Panelek/vasarlas_ablak");
            szerencsekartyaablak = Content.Load<Texture2D>("Panelek/szerencsekartya_ablak");
            visszakerdezoablak = Content.Load<Texture2D>("Panelek/visszakerdezo_ablak");
            valaszablak = Content.Load<Texture2D>("Panelek/valasz_ablak");
            felszerelesablak = Content.Load<Texture2D>("Panelek/felszereles_ablak");
            kovetkezoablak = Content.Load<Texture2D>("Panelek/kovetkezo_ablak");
            dobasablak = Content.Load<Texture2D>("Panelek/dobas_ablak");
            kimaradablak = Content.Load<Texture2D>("Panelek/kimarad_ablak");
            kiesettablak = Content.Load<Texture2D>("Panelek/kiesett_ablak");
            fopanel = Content.Load<Texture2D>("Panelek/fopanel");

            hatter = Content.Load<Texture2D>("hatter");

            for (int i = 1; i <= 7; i++)
            {
                BabuNormal.Add(i, Content.Load<Texture2D>("Babuk/" + i.ToString() + "_normal"));
                BabuNagy.Add(i, Content.Load<Texture2D>("Babuk/" + i.ToString() + "_nagy"));
                FopanelFulek.Add(i, Content.Load<Texture2D>("Panelek/fopanel_" + i.ToString()));
                FopanelAktivFulek.Add(i, Content.Load<Texture2D>("Panelek/fopanelaktiv_" + i.ToString()));
            }

            for (int i = 1; i <= tabla.Mezok.Count; i++)
            {
                MezoNormal.Add(i, Content.Load<Texture2D>("Mezok/Normal/" + (i < 10 ? "0" + i.ToString() : i.ToString()) + "_normal"));
                MezoOpcio.Add(i, Content.Load<Texture2D>("Mezok/Opcio/" + (i < 10 ? "0" + i.ToString() : i.ToString()) + "_opcio"));
                MezoNagy.Add(i, Content.Load<Texture2D>("Mezok/Nagy/" + (i < 10 ? "0" + i.ToString() : i.ToString()) + "_nagy"));
            }

            #endregion

            #region fõmenü elemek

            ujjatekfont = Content.Load<SpriteFont>("Fontok/ujjatekFont");
            billfont = Content.Load<SpriteFont>("Fontok/billFont");
            szamfont = Content.Load<SpriteFont>("Fontok/szamFont");

            menuhatter = Content.Load<Texture2D>("menuhatter");
            

            szovegmezo = Content.Load<Texture2D>("Panelek/szovegmezo");
            szovegmezohiba = Content.Load<Texture2D>("Panelek/szovegmezohiba");
            jatekospanel = Content.Load<Texture2D>("Panelek/jatekospanel");
            szinvalasztoablak = Content.Load<Texture2D>("Panelek/szinvalaszto_ablak");
            nyertesablak = Content.Load<Texture2D>("Panelek/nyertesablak");
            beallitaspanel = Content.Load<Texture2D>("Panelek/beallitaspanel");
            scroll = Content.Load<Texture2D>("Panelek/scroll");
            beallitasokszovegmezo = Content.Load<Texture2D>("Panelek/beallitasokszovegmezo");
            alapertek = Content.Load<Texture2D>("Gombok/alapertek");
            megse = Content.Load<Texture2D>("Gombok/megse");

            ujjatek = Content.Load<Texture2D>("Gombok/ujjatek");
            beallitasok = Content.Load<Texture2D>("Gombok/beallitasok");
            start = Content.Load<Texture2D>("Gombok/start");
            plusz = Content.Load<Texture2D>("Gombok/plusz");
            minusz = Content.Load<Texture2D>("Gombok/minusz");
            betoltes = Content.Load<Texture2D>("Gombok/betoltes");

            backspace = Content.Load<Texture2D>("Billentyuzet/backspace");
            enter = Content.Load<Texture2D>("Billentyuzet/enter");
            karakter = Content.Load<Texture2D>("Billentyuzet/karakter");
            shift = Content.Load<Texture2D>("Billentyuzet/shift");
            space = Content.Load<Texture2D>("Billentyuzet/space");
            billhatter = Content.Load<Texture2D>("Billentyuzet/billhatter");
            szovegmezohatter = Content.Load<Texture2D>("Billentyuzet/szovegmezohatter");
            billszovegmezo = Content.Load<Texture2D>("Billentyuzet/szovegmezo");
            szam = Content.Load<Texture2D>("Billentyuzet/szam");
            backspacenagy = Content.Load<Texture2D>("Billentyuzet/backspacenagy");
            enternagy = Content.Load<Texture2D>("Billentyuzet/enternagy");
            szamlaphatter = Content.Load<Texture2D>("Billentyuzet/szamlaphatter");
            szammezohatter = Content.Load<Texture2D>("Billentyuzet/szammezohatter");

            #endregion

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            touch = TouchPanel.GetState();

            if (touch.Count > 0)
            {
                touchX = touch[0].Position.X;
                touchY = touch[0].Position.Y;                
            }
            
            if (villogasstart + villogas <= gameTime.TotalGameTime)
            {
                villog = !villog;
                villogasstart = gameTime.TotalGameTime;
            }

            base.Update(gameTime);
        }

        private void TablaNormalRajzolas(float scale, float mozgatX, float mozgatY, float a)
        {

            spriteBatch.Draw(hatter, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X - (hatter.Width - GraphicsDevice.Viewport.TitleSafeArea.Width) / 2 - (hatter.Width * (scale - 1) / 2) + mozgatX, GraphicsDevice.Viewport.TitleSafeArea.Y - (hatter.Height * (scale - 1) / 2) + mozgatY), null, Color.White * a, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            
            for (int i = 1; i <= 93; i++)
            {
                float x = (tabla.Mezok[i].XPos * scale - (GraphicsDevice.Viewport.TitleSafeArea.Width * (scale - 1) / 2)) + mozgatX;
                float y = (tabla.Mezok[i].YPos * scale - (GraphicsDevice.Viewport.TitleSafeArea.Height * (scale - 1) / 2)) + mozgatY;
                if (x + MezoNormal[i].Width * scale >= 0 && x <= GraphicsDevice.Viewport.TitleSafeArea.Width &&
                    y + MezoNormal[i].Height * scale >= 0 && y <= GraphicsDevice.Viewport.TitleSafeArea.Height)
                {
                    spriteBatch.Draw(MezoNormal[i], new Vector2(x, y), null, Color.White * a, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                }
            }
            if (tablascale <= 1)
            {
                    dobasX = GraphicsDevice.Viewport.TitleSafeArea.X + mentes.Width + 15;
                    dobasY = GraphicsDevice.Viewport.TitleSafeArea.Height - dobas.Height - 10;
                    jatekmentesX = GraphicsDevice.Viewport.TitleSafeArea.X + 10;
                    jatekmentesY = GraphicsDevice.Viewport.TitleSafeArea.Height - mentes.Height - 10;
                    kilepesX = GraphicsDevice.Viewport.TitleSafeArea.Width - kilepes.Width - 10;
                    kilepesY = GraphicsDevice.Viewport.TitleSafeArea.Height - kilepes.Height - 10;
                    spriteBatch.Draw(mentes, new Vector2(jatekmentesX, jatekmentesY), Color.White * a);
                    spriteBatch.Draw(dobas, new Vector2(dobasX, dobasY), Color.White * a);
                    spriteBatch.Draw(kilepes, new Vector2(kilepesX, kilepesY), Color.White * a);
                    this.FopanelRajzolas(aktualispanel, alpha);
            }
            
        }

        private void TablaNagyRajzolas(float scale, float mozgatX, float mozgatY, float a)
        {

            spriteBatch.Draw(hatter, new Vector2((GraphicsDevice.Viewport.TitleSafeArea.X- (hatter.Width - GraphicsDevice.Viewport.TitleSafeArea.Width) / 2 - jatekosok[aktualis].Pozicio.XPos) * scale + 225 + mozgatX, (GraphicsDevice.Viewport.TitleSafeArea.Y - jatekosok[aktualis].Pozicio.YPos) * scale + 65 + mozgatY), null, Color.White * a, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            for (int i = 1; i <= 93; i++)
            {

                float x = (tabla.Mezok[i].XPos - jatekosok[aktualis].Pozicio.XPos) * scale + (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - MezoNagy[i].Width / 2) + mozgatX;
                float y = (tabla.Mezok[i].YPos - jatekosok[aktualis].Pozicio.YPos) * scale + (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - MezoNagy[i].Height / 2) + mozgatY;
                if (x + MezoNagy[i].Width * scale >= 0 && x <= GraphicsDevice.Viewport.TitleSafeArea.Width &&
                    y + MezoNagy[i].Height * scale >= 0 && y <= GraphicsDevice.Viewport.TitleSafeArea.Height)
                {
                    spriteBatch.Draw(MezoNagy[i], new Vector2(x, y), Color.White * a);
                }
            }
        }

        private void BabukNormalRajzolas(float scale, float mozgatX, float mozgatY, float a)
        {
            for (int i = 1; i <= 93; i++)
            {
                if (tabla.Mezok[i].Jatekosok.Count > 1)
                {
                    int j = 0;
                    int k = 0;
                    bool van = false;
                    foreach (Jatekos jat in tabla.Mezok[i].Jatekosok)
                    {
                        if (jatekosok[aktualis].Equals(jat))
                        {
                            van = true;
                            break;
                        }
                    }
                    if (van)
                    {
                        while (j < tabla.Mezok[i].Jatekosok.Count)
                        {
                            if (tabla.Mezok[i].Jatekosok[j].Equals(jatekosok[aktualis]))
                            {
                                j++;
                            }
                            else
                            {
                                float x = tabla.Mezok[i].Jatekosok[j].Pozicio.XPos * scale - (GraphicsDevice.Viewport.TitleSafeArea.Width * (scale - 1) / 2) + mozgatX + eltolasX[tabla.Mezok[i].Jatekosok.Count - 1][k] * scale;
                                float y = tabla.Mezok[i].Jatekosok[j].Pozicio.YPos * scale - (GraphicsDevice.Viewport.TitleSafeArea.Height * (scale - 1) / 2) + mozgatY + eltolasY[tabla.Mezok[i].Jatekosok.Count - 1][k] * scale;
                                if (x + BabuNormal[tabla.Mezok[i].Jatekosok[j].Szin].Width * scale >= GraphicsDevice.Viewport.TitleSafeArea.X && x <= GraphicsDevice.Viewport.TitleSafeArea.Width &&
                                    y + BabuNormal[tabla.Mezok[i].Jatekosok[j].Szin].Height * scale >= GraphicsDevice.Viewport.TitleSafeArea.Y && y <= GraphicsDevice.Viewport.TitleSafeArea.Height)
                                {
                                    spriteBatch.Draw(BabuNormal[tabla.Mezok[i].Jatekosok[j].Szin], new Vector2(x, y), null, Color.White * a, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                                }
                                k++;
                                j++;
                            }
                        }
                    }
                    else
                    {
                        while (j < tabla.Mezok[i].Jatekosok.Count)
                        {
                            float x = tabla.Mezok[i].Jatekosok[j].Pozicio.XPos * scale - (GraphicsDevice.Viewport.TitleSafeArea.Width * (scale - 1) / 2) + mozgatX + eltolasX[tabla.Mezok[i].Jatekosok.Count][k] * scale;
                            float y = tabla.Mezok[i].Jatekosok[j].Pozicio.YPos * scale - (GraphicsDevice.Viewport.TitleSafeArea.Height * (scale - 1) / 2) + mozgatY + eltolasY[tabla.Mezok[i].Jatekosok.Count][k] * scale;
                            if (x + BabuNormal[tabla.Mezok[i].Jatekosok[j].Szin].Width * scale >= GraphicsDevice.Viewport.TitleSafeArea.X && x <= GraphicsDevice.Viewport.TitleSafeArea.Width &&
                                y + BabuNormal[tabla.Mezok[i].Jatekosok[j].Szin].Height * scale >= GraphicsDevice.Viewport.TitleSafeArea.Y && y <= GraphicsDevice.Viewport.TitleSafeArea.Height)
                            {
                                spriteBatch.Draw(BabuNormal[tabla.Mezok[i].Jatekosok[j].Szin], new Vector2(x, y), null, Color.White * a, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                            }
                            k++;
                            j++;
                        }
                    }
                }
                if (tabla.Mezok[i].Jatekosok.Count == 1)
                {
                    float x = tabla.Mezok[i].Jatekosok[0].Pozicio.XPos * scale - (GraphicsDevice.Viewport.TitleSafeArea.Width * (scale - 1) / 2) + mozgatX;
                    float y = tabla.Mezok[i].Jatekosok[0].Pozicio.YPos * scale - (GraphicsDevice.Viewport.TitleSafeArea.Height * (scale - 1) / 2) + mozgatY;
                    if (x + BabuNormal[tabla.Mezok[i].Jatekosok[0].Szin].Width * scale >= GraphicsDevice.Viewport.TitleSafeArea.X && x <= GraphicsDevice.Viewport.TitleSafeArea.Width &&
                        y + BabuNormal[tabla.Mezok[i].Jatekosok[0].Szin].Height * scale >= GraphicsDevice.Viewport.TitleSafeArea.Y && y <= GraphicsDevice.Viewport.TitleSafeArea.Height)
                    {
                        spriteBatch.Draw(BabuNormal[tabla.Mezok[i].Jatekosok[0].Szin], new Vector2(x, y), null, Color.White * a, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    }
                }
            }


            spriteBatch.Draw(BabuNormal[jatekosok[aktualis].Szin], new Vector2(jatekosok[aktualis].Pozicio.XPos * scale - (GraphicsDevice.Viewport.TitleSafeArea.Width * (scale - 1) / 2) + mozgatX,
                                                                               jatekosok[aktualis].Pozicio.YPos * scale - (GraphicsDevice.Viewport.TitleSafeArea.Height * (scale - 1) / 2) + mozgatY), null, Color.White * a, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }

        private void BabukNagyRajzolas(float scale, float mozgatX, float mozgatY, float a)
        {
            for (int i = 1; i <= 93; i++)
            {
                if (tabla.Mezok[i].Jatekosok.Count > 1)
                {
                    int j = 0;
                    int k = 0;
                    bool van = false;
                    foreach (Jatekos jat in tabla.Mezok[i].Jatekosok)
                    {
                        if (jatekosok[aktualis].Equals(jat))
                        {
                            van = true;
                            break;
                        }
                    }
                    if (van)
                    {
                        while (j < tabla.Mezok[i].Jatekosok.Count)
                        {
                            if (tabla.Mezok[i].Jatekosok[j].Equals(jatekosok[aktualis]))
                            {
                                j++;
                            }
                            else
                            {
                                float x = (tabla.Mezok[i].Jatekosok[j].Pozicio.XPos - jatekosok[aktualis].Pozicio.XPos) * scale + (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - MezoNagy[tabla.Mezok[i].Jatekosok[j].Szin].Width / 2) + mozgatX + eltolasX[tabla.Mezok[i].Jatekosok.Count - 1][k] * scale;
                                float y = (tabla.Mezok[i].Jatekosok[j].Pozicio.YPos - jatekosok[aktualis].Pozicio.YPos) * scale + (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - MezoNagy[tabla.Mezok[i].Jatekosok[j].Szin].Height / 2) + mozgatY + eltolasY[tabla.Mezok[i].Jatekosok.Count - 1][k] * scale;
                                if (x + BabuNagy[tabla.Mezok[i].Jatekosok[j].Szin].Width >= GraphicsDevice.Viewport.TitleSafeArea.X && x <= GraphicsDevice.Viewport.TitleSafeArea.Width &&
                                    y + BabuNagy[tabla.Mezok[i].Jatekosok[j].Szin].Height >= GraphicsDevice.Viewport.TitleSafeArea.Y && y <= GraphicsDevice.Viewport.TitleSafeArea.Height)
                                {
                                    spriteBatch.Draw(BabuNagy[tabla.Mezok[i].Jatekosok[j].Szin], new Vector2(x, y), Color.White * a);
                                }
                                k++;
                                j++;
                            }
                        }
                    }
                    else
                    {
                        while (j < tabla.Mezok[i].Jatekosok.Count)
                        {
                            float x = (tabla.Mezok[i].Jatekosok[j].Pozicio.XPos - jatekosok[aktualis].Pozicio.XPos) * scale + (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - MezoNagy[tabla.Mezok[i].Jatekosok[j].Szin].Width / 2) + mozgatX + eltolasX[tabla.Mezok[i].Jatekosok.Count][k] * scale;
                            float y = (tabla.Mezok[i].Jatekosok[j].Pozicio.YPos - jatekosok[aktualis].Pozicio.YPos) * scale + (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - MezoNagy[tabla.Mezok[i].Jatekosok[j].Szin].Height / 2) + mozgatY + eltolasY[tabla.Mezok[i].Jatekosok.Count][k] * scale;
                            if (x + BabuNagy[tabla.Mezok[i].Jatekosok[j].Szin].Width >= GraphicsDevice.Viewport.TitleSafeArea.X && x <= GraphicsDevice.Viewport.TitleSafeArea.Width &&
                                y + BabuNagy[tabla.Mezok[i].Jatekosok[j].Szin].Height >= GraphicsDevice.Viewport.TitleSafeArea.Y && y <= GraphicsDevice.Viewport.TitleSafeArea.Height)
                            {
                                spriteBatch.Draw(BabuNagy[tabla.Mezok[i].Jatekosok[j].Szin], new Vector2(x, y), Color.White * a);
                            }
                            k++;
                            j++;
                        }
                    }
                }
                if (tabla.Mezok[i].Jatekosok.Count == 1 && !tabla.Mezok[i].Jatekosok[0].Equals(jatekosok[aktualis]))
                {
                    float x = (tabla.Mezok[i].Jatekosok[0].Pozicio.XPos - jatekosok[aktualis].Pozicio.XPos) * scale + (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - MezoNagy[tabla.Mezok[i].Jatekosok[0].Szin].Width / 2) + mozgatX;
                    float y = (tabla.Mezok[i].Jatekosok[0].Pozicio.YPos - jatekosok[aktualis].Pozicio.YPos) * scale + (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - MezoNagy[tabla.Mezok[i].Jatekosok[0].Szin].Height / 2) + mozgatY;
                    if (x + BabuNagy[tabla.Mezok[i].Jatekosok[0].Szin].Width >= GraphicsDevice.Viewport.TitleSafeArea.X && x <= GraphicsDevice.Viewport.TitleSafeArea.Width &&
                        y + BabuNagy[tabla.Mezok[i].Jatekosok[0].Szin].Height >= GraphicsDevice.Viewport.TitleSafeArea.Y && y <= GraphicsDevice.Viewport.TitleSafeArea.Height)
                    {
                        spriteBatch.Draw(BabuNagy[tabla.Mezok[i].Jatekosok[0].Szin], new Vector2(x, y), Color.White * a);
                    }
                }
            }

            spriteBatch.Draw(BabuNagy[jatekosok[aktualis].Szin], new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - BabuNagy[jatekosok[aktualis].Szin].Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - BabuNagy[jatekosok[aktualis].Szin].Height / 2), Color.White * a);
        }

        private void VasarlasablakRajzolas(int sorszam)
        {
            String szoveg, ar;
            int j = 0;
            keret = 0;
            termekdb = 0;

            spriteBatch.Draw(vasarlasablak, new Vector2(vasarlasablakX, vasarlasablakY), null, Color.White, 0f, new Vector2(0, 0), vasarlasablakscale, SpriteEffects.None, 0);

            switch (sorszam)
            {
                case 1:
                    termekdb = 7;
                    keret = (vasarlasablak.Height - 25 - 25 - tovabb.Height - termekdb * panelfontmagassag) / 2;
                    szoveg = "Ágy";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.AgyAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Hûtõ";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.HutoAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Konyhabútor";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.KonyhabutorAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Mosógép";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar = this.tabla.MosogepAr.ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Számítógép";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.PCAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Szekrény";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.SzekrenyAr.ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "TV";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.TVAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    break;
                case 2:
                    termekdb = 3;
                    keret = (vasarlasablak.Height - 25 - 25 - tovabb.Height - (termekdb + 3) * panelfontmagassag - (termekdb + 2)) / 2;
                    int jutalekosar = this.tabla.LakasAr * (100 + this.tabla.IngatlanirodaJutalek) / 100;
                    int reszletfizetesesar = jutalekosar * (100 + this.tabla.ReszletKamat) / 100;
                    int kezdoreszlet = reszletfizetesesar * this.tabla.Kezdoreszlet / 100;
                    szoveg = "Garázs";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar = this.tabla.GarazsAr.ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Lakás (egyösszegben)";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar = jutalekosar.ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Lakás (részletre)";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "\tKezdõrészlet";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar = kezdoreszlet.ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "\tTörlesztés";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar = ((reszletfizetesesar - kezdoreszlet) / this.tabla.ReszletDB).ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "\tFizetés";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar = this.tabla.ReszletKor.ToString() + " körönként";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    break;
                case 3:
                    termekdb = 1;
                    keret = (vasarlasablak.Height - 25 - 25 - tovabb.Height - termekdb * panelfontmagassag) / 2;
                    szoveg = "Autó";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar = this.tabla.AutoAr.ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    break;
                case 4:
                    termekdb = 2;
                    keret = (vasarlasablak.Height - 25 - 25 - tovabb.Height - termekdb * panelfontmagassag) / 2;
                    szoveg = "Lakásbiztosítás";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.LakasBiztositasAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Autó biztosítás";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar = this.tabla.AutoBiztositasAr.ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    break;
                case 5:
                    termekdb = 4;
                    keret = (vasarlasablak.Height - 25 - 25 - tovabb.Height - termekdb * panelfontmagassag) / 2;
                    szoveg = "Hûtõ";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.HutoAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Mosógép";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar = this.tabla.MosogepAr.ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Számítógép";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.PCAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "TV";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.TVAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    break;
                case 6:
                    termekdb = 3;
                    keret = (vasarlasablak.Height - 25 - 25 - tovabb.Height - termekdb * panelfontmagassag) / 2;
                    szoveg = "Ágy";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.AgyAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Konyhabútor";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.KonyhabutorAr.ToString() + " Ft\n";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    szoveg = "Szekrény";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar= this.tabla.SzekrenyAr.ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    break;
                case 8:
                    termekdb = 1;
                    keret = (vasarlasablak.Height - 25 - 25 - tovabb.Height - termekdb * panelfontmagassag) / 2;
                    szoveg = "Lakás";
                    spriteBatch.DrawString(panelfont, szoveg, new Vector2(vasarlasablakX + 25, vasarlasablakY + 25 + keret + j * (panelfontmagassag)), Color.White);
                    ar = this.tabla.LakasAr.ToString() + " Ft";
                    spriteBatch.DrawString(panelfont, ar, new Vector2(vasarlasablakX + vasarlasablak.Width - 25 - megveszem.Width - 25 - panelfont.MeasureString(ar).X, vasarlasablakY + 25 + keret + j++ * (panelfontmagassag)), Color.White);
                    break;
            }
            spriteBatch.DrawString(panelfont, "Készpénz", new Vector2(vasarlasablakX + 25, vasarlasablakY + vasarlasablak.Height - 25 - 2 * panelfontmagassag), Color.White);
            spriteBatch.DrawString(panelfont, "Bankbetét", new Vector2(vasarlasablakX + 25, vasarlasablakY + vasarlasablak.Height - 25 - panelfontmagassag), Color.White);
            spriteBatch.DrawString(panelfont, jatekosok[aktualis].Penz.ToString() + " Ft", new Vector2(tovabbX - 25 - panelfont.MeasureString(jatekosok[aktualis].Penz.ToString() + " Ft").X, vasarlasablakY + vasarlasablak.Height - 25 - 2 * panelfontmagassag), Color.White);
            spriteBatch.DrawString(panelfont, jatekosok[aktualis].Bankbetet.ToString() + " Ft", new Vector2(tovabbX - 25 - panelfont.MeasureString(jatekosok[aktualis].Bankbetet.ToString() + " Ft").X, vasarlasablakY + vasarlasablak.Height - 25 - panelfontmagassag), Color.White);

            for (int i = 0; i < termekdb; i++)
            {
                spriteBatch.Draw(megveszem, new Vector2(vasarlasablakX + vasarlasablak.Width - megveszem.Width - 25, vasarlasablakY + 25 + keret + i * (panelfontmagassag)), Color.White);
            }
            spriteBatch.Draw(tovabb, new Vector2(tovabbX, tovabbY), Color.White);
            spriteBatch.Draw(felszerelesnagy, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + vasarlasablak.Width / 2 - 25 - felszerelesnagy.Width, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + vasarlasablak.Height / 2 - felszerelesnagy.Height - 25), Color.White);
            spriteBatch.Draw(visszakerdezoablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - visszakerdezoablak.Width * visszakerdezoablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - visszakerdezoablak.Height * visszakerdezoablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), visszakerdezoablakscale, SpriteEffects.None, 0);
            spriteBatch.Draw(valaszablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - valaszablak.Width * valaszablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - valaszablak.Height * valaszablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), valaszablakscale, SpriteEffects.None, 0);
            spriteBatch.Draw(felszerelesablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - felszerelesablak.Width * felszerelesablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - felszerelesablak.Height * felszerelesablakscale / 2), null, Color.White, 0, new Vector2(0, 0), felszerelesablakscale, SpriteEffects.None, 0);
        }

        private void SzerencsekartyaHatasKiiras(int sorszam)
        {
            spriteBatch.DrawString(panelfont, tabla.Kartyak[sorszam].Szoveg, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - panelfont.MeasureString(tabla.Kartyak[sorszam].Szoveg).X / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - szerencsekartyaablak.Height / 2 + 25), Color.White);
            List<String> kartyahatassoveg = jatekosok[aktualis].KartyaHatasSzoveg(tabla.Kartyak[sorszam]);
            float keret = (szerencsekartyaablak.Height - 25 - 25 - tovabb.Height - panelfontmagassag - kartyahatassoveg.Count * panelfontmagassag) / 2;
            for (int i = 0; i < kartyahatassoveg.Count; i++)
            {
                spriteBatch.DrawString(panelfont, kartyahatassoveg[i], new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - panelfont.MeasureString(kartyahatassoveg[i]).X / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - szerencsekartyaablak.Height / 2 + 25 + panelfontmagassag + keret + i * panelfontmagassag), Color.White);
            }
        }

        private void FopanelRajzolas(int aktualis, float a)
        {
            fopanelX = GraphicsDevice.Viewport.TitleSafeArea.X + 50;
            fopanelY = GraphicsDevice.Viewport.TitleSafeArea.Y + 5;
            fopanelfulekX = fopanelX - FopanelFulek[1].Width;
            fopanelfulekY = fopanelY + fopanel.Height;

            spriteBatch.Draw(fopanel, new Vector2(fopanelX, fopanelY), Color.White * a);
            for (int i = 1; i <= jatekosok.Count; i++)
            {
                if (i == aktualis)
                {
                    spriteBatch.Draw(FopanelAktivFulek[jatekosok[i].Szin], new Vector2(fopanelfulekX, fopanelfulekY - i * FopanelAktivFulek[jatekosok[i].Szin].Height), Color.White * a);
                }
                else
                {

                    spriteBatch.Draw(FopanelFulek[jatekosok[i].Szin], new Vector2(fopanelfulekX, fopanelfulekY - i * FopanelFulek[jatekosok[i].Szin].Height), Color.White * a);
                }
            }

            if (jatekosok[aktualis].Aktiv)
            {
                felszerelesX = fopanelX + 90;
                felszerelesY = 15;

                spriteBatch.DrawString(panelfont, jatekosok[aktualis].Nev, new Vector2(fopanelX, fopanelY + fopanel.Height / 2 + panelfont.MeasureString(jatekosok[aktualis].Nev).X / 2), Color.White * a, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(panelfont, jatekosok[aktualis].Penz.ToString() + " Ft", new Vector2(fopanelX + 30, fopanelY + panelfont.MeasureString(jatekosok[aktualis].Penz.ToString() + " Ft").X + 15), Color.White * a, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(panelfont, jatekosok[aktualis].Bankbetet.ToString() + " Ft", new Vector2(fopanelX + 2 * 30, fopanelY + panelfont.MeasureString(jatekosok[aktualis].Bankbetet.ToString() + " Ft").X + 15), Color.White * a, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(panelfont, "Kp.", new Vector2(fopanelX + 30, fopanelY + fopanel.Height - 10), Color.White * a, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(panelfont, "Bank", new Vector2(fopanelX + 2 * 30, fopanelY + fopanel.Height - 10), Color.White * a, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(jatekosok[aktualis].FizetestKaphat ? igenikon : nemikon, new Vector2(fopanelX + 3 * 30, fopanelY + fopanel.Height - 10 - igenikon.Height), Color.White * a);
                spriteBatch.Draw(felszereles, new Vector2(felszerelesX, felszerelesY), Color.White * a);
            }
            else
            {
                spriteBatch.DrawString(panelfont, jatekosok[aktualis].Nev, new Vector2(fopanelX, fopanelY + fopanel.Height / 2 + panelfont.MeasureString(jatekosok[aktualis].Nev).X / 2), Color.White * a, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(panelfont, "JÁTÉK VÉGE", new Vector2(((fopanelX + fopanel.Width) - (fopanelX + 30)) / 2 + fopanelX + 30 - panelfont.MeasureString("JÁTÉK VÉGE").Y / 2, fopanelY + fopanel.Height / 2 + panelfont.MeasureString("JÁTÉK VÉGE").X / 2), Color.White * a, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
        }

        private void JatekosBeallitasokRajzolas(float a, float cs)
        {
            spriteBatch.Draw(menuhatter, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X - (menuhatter.Width - GraphicsDevice.Viewport.TitleSafeArea.Width) / 2, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White * a);
            for (int i = 1; i <= aktivjatekos; i++)
            {
                jatekospanelX = GraphicsDevice.Viewport.TitleSafeArea.X + 10;
                jatekospanelY = GraphicsDevice.Viewport.TitleSafeArea.Y + 10;
                szovegmezoX = GraphicsDevice.Viewport.TitleSafeArea.Y + 10 + jatekospanel.Width - szovegmezo.Width - 10;
                szovegmezoY = jatekospanelY + jatekospanel.Height - szovegmezo.Height - minusz.Height - 20;

                spriteBatch.Draw(jatekospanel, new Vector2(jatekospanelX + (i - 1) * (jatekospanel.Width + 20), jatekospanelY), Color.White * a);
                if (nevek[i] == "")
                {
                    spriteBatch.Draw(szovegmezohiba, new Vector2(szovegmezoX + (i - 1) * (jatekospanel.Width + 20), szovegmezoY), Color.White * a);
                }
                else
                {
                    spriteBatch.Draw(szovegmezo, new Vector2(szovegmezoX + (i - 1) * (jatekospanel.Width + 20), szovegmezoY), Color.White * a);
                }
                if (i > 2)
                {
                    minuszX = GraphicsDevice.Viewport.TitleSafeArea.X + 10;
                    minuszY = GraphicsDevice.Viewport.TitleSafeArea.Height - 10 - minusz.Height;
                    spriteBatch.Draw(minusz, new Vector2(minuszX + (i - 1) * (minusz.Width + 20), minuszY), Color.White * a);
                }
                spriteBatch.DrawString(panelfont, "Név", new Vector2(jatekospanelX + (i - 1) * (jatekospanel.Width + 20), jatekospanelY + jatekospanel.Height - minusz.Height - 10), Color.Black * a, (float)(Math.PI / -2), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                spriteBatch.DrawString(panelfont, "Szín", new Vector2(jatekospanelX + (i - 1) * (jatekospanel.Width + 20), jatekospanelY + 100), Color.Black * a, (float)(Math.PI / -2), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(BabuNormal[szinek[i]], new Vector2(jatekospanelX + (i - 1) * (jatekospanel.Width + 20) + jatekospanel.Width - BabuNormal[szinek[i]].Width * 2, jatekospanelY), null, Color.White * a, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
                spriteBatch.DrawString(ujjatekfont, nevek[i], new Vector2(szovegmezoX + (i - 1) * (jatekospanel.Width + 20) + szovegmezo.Width - ujjatekfont.MeasureString(nevek[i]).Y, jatekospanelY + jatekospanel.Height - minusz.Height - 20), Color.Black * a, (float)(Math.PI / -2), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
            }
            if (aktivjatekos < 6)
            {
                pluszX = GraphicsDevice.Viewport.TitleSafeArea.X + aktivjatekos * (jatekospanel.Width + 20) + 10;
                pluszY = GraphicsDevice.Viewport.TitleSafeArea.Y + 10;
                if (aktivjatekos == 5)
                {
                    spriteBatch.Draw(plusz, new Vector2(pluszX, pluszY - cs), Color.White * a);
                }
                else
                {
                    spriteBatch.Draw(plusz, new Vector2(pluszX + cs, pluszY), Color.White * a);
                }
            }

            startX = GraphicsDevice.Viewport.TitleSafeArea.Width - start.Width - 10;
            startY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + 15;
            visszaX = GraphicsDevice.Viewport.TitleSafeArea.Width - vissza.Width - 10;
            visszaY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - vissza.Height - 15;
            spriteBatch.Draw(start, new Vector2(startX, startY), Color.White * a);
            spriteBatch.Draw(vissza, new Vector2(visszaX, visszaY), Color.White * a);
        }

        private void BeallitasokRajzolas(float a)
        {
            beallitaspanelX = GraphicsDevice.Viewport.TitleSafeArea.X + 10;
            beallitaspanelY = GraphicsDevice.Viewport.TitleSafeArea.Y + scroll.Height + 10;
            scrollX = GraphicsDevice.Viewport.TitleSafeArea.X + scrollmozgas;
            scrollY = GraphicsDevice.Viewport.TitleSafeArea.Y;

            if (scrollX > GraphicsDevice.Viewport.TitleSafeArea.Width - scroll.Width)
            {
                scrollX = GraphicsDevice.Viewport.TitleSafeArea.Width - scroll.Width;
                scrollmozgas = GraphicsDevice.Viewport.TitleSafeArea.Width - scroll.Width;
            }
            if (scrollX < GraphicsDevice.Viewport.TitleSafeArea.X)
            {
                scrollX = GraphicsDevice.Viewport.TitleSafeArea.X;
                scrollmozgas = GraphicsDevice.Viewport.TitleSafeArea.X;
            }

            mentesX = beallitaspanelX + 21 * (beallitaspanel.Width + 5) + 10 - scrollmozgas;
            mentesY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + megse.Height/2 + 15;
            alapertekX = beallitaspanelX + 21 * (beallitaspanel.Width + 5) + 10 - scrollmozgas;
            alapertekY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - megse.Height/2 - alapertek.Height - 15;
            megseX = beallitaspanelX + 21 * (beallitaspanel.Width + 5) + 10 - scrollmozgas;
            megseY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - megse.Height / 2;

            spriteBatch.Draw(menuhatter, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X - (menuhatter.Width - GraphicsDevice.Viewport.TitleSafeArea.Width) / 2, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White * a);
            spriteBatch.Draw(scroll, new Vector2(scrollX, scrollY), Color.White * a);
            spriteBatch.Draw(megse, new Vector2(megseX, megseY), Color.White * a);
            spriteBatch.Draw(mentes, new Vector2(mentesX, mentesY), Color.White * a);
            spriteBatch.Draw(alapertek, new Vector2(alapertekX, alapertekY), Color.White * a);

            for (int i = 1; i <= 21; i++)
            {
                X = beallitaspanelX + (i - 1) * (beallitaspanel.Width + 5) - scrollmozgas;
                if (X <= GraphicsDevice.Viewport.TitleSafeArea.Width &&
                    X >= GraphicsDevice.Viewport.TitleSafeArea.X - beallitaspanel.Width)
                {
                    spriteBatch.Draw(beallitaspanel, new Vector2(X, beallitaspanelY), Color.White * a);
                    spriteBatch.Draw(beallitasokszovegmezo, new Vector2(X, beallitaspanelY), Color.White * a);
                    spriteBatch.DrawString(panelfont, cimkek[i], new Vector2(X + beallitaspanel.Width - panelfont.MeasureString(cimkek[i]).Y, beallitaspanelY + beallitaspanel.Height - 10), Color.Black * a, (float)(Math.PI / -2), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                    String ertek = "";
                    if (i < 15)
                    {
                        ertek = ertekek[i] + " Ft";
                    }
                    switch (i)
                    {
                        case 15:
                        case 16:
                        case 19:
                        case 20: ertek = ertekek[i] + " %"; break;
                        case 17: ertek = ertekek[i] + " db"; break;
                        case 18:
                        case 21: ertek = ertekek[i] + " körönként"; break;
                    }
                    spriteBatch.DrawString(ujjatekfont, ertek, new Vector2(X + beallitaspanel.Width - ujjatekfont.MeasureString(ertek).Y, beallitaspanelY + ujjatekfont.MeasureString(ertek).X + 10), Color.Black * a, (float)(Math.PI / -2), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                }
            }
        }

        private void Mentes()
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            FileStream stream = store.OpenFile("mentes.txt", FileMode.Create, FileAccess.Write);
            TextWriter tw = new StreamWriter(stream);

            tw.WriteLine(jatekkor);
            tw.WriteLine(aktualis);

            foreach (var j in jatekosok)
            {
                tw.WriteLine("P" + j.Key);
                tw.WriteLine(j.Value.Nev);
                tw.WriteLine(j.Value.Szin);
                tw.WriteLine(j.Value.Pozicio.Sorszam);
                tw.WriteLine(j.Value.Penz);
                tw.WriteLine(j.Value.Bankbetet);
                tw.WriteLine(j.Value.Allapot);
                tw.WriteLine(j.Value.Hanyszor);
                tw.WriteLine(j.Value.Aktiv);
                tw.WriteLine(j.Value.ReszletDB);
                tw.WriteLine(j.Value.Reszlet);
                tw.WriteLine(j.Value.Torlesztesig);
                tw.WriteLine(j.Value.CsakHatos);
                tw.WriteLine(j.Value.FizetestKaphat);
                tw.WriteLine(j.Value.Lakas);
                tw.WriteLine(j.Value.Auto);
                tw.WriteLine(j.Value.LakasBiztositas);
                tw.WriteLine(j.Value.AutoBiztositas);
                tw.WriteLine(j.Value.Mosogep);
                tw.WriteLine(j.Value.Konyhabutor);
                tw.WriteLine(j.Value.TV);
                tw.WriteLine(j.Value.PC);
                tw.WriteLine(j.Value.Huto);
                tw.WriteLine(j.Value.Agy);
                tw.WriteLine(j.Value.Szekreny);
                tw.WriteLine(j.Value.Garazs);
                tw.WriteLine(j.Value.MosogepKupon);
                tw.WriteLine(j.Value.KonyhabutorKupon);
                tw.WriteLine(j.Value.TVKupon);
                tw.WriteLine(j.Value.PCKupon);
                tw.WriteLine(j.Value.HutoKupon);
                tw.WriteLine(j.Value.AgyKupon);
                tw.WriteLine(j.Value.SzekrenyKupon);
            }
            tw.WriteLine("K");
            foreach (var h in tabla.Huzottak)
            {
                tw.WriteLine(h.Value);
            }
            tw.WriteLine(tabla.Kihuzott);
            tw.WriteLine(tabla.Fizetes);
            tw.WriteLine(tabla.Kezdo);
            tw.WriteLine(tabla.IngatlanirodaJutalek);
            tw.WriteLine(tabla.ReszletKamat);
            tw.WriteLine(tabla.ReszletDB);
            tw.WriteLine(tabla.ReszletKor);
            tw.WriteLine(tabla.Kezdoreszlet);
            tw.WriteLine(tabla.LakasAr);
            tw.WriteLine(tabla.GarazsAr);
            tw.WriteLine(tabla.AutoAr);
            tw.WriteLine(tabla.LakasBiztositasAr);
            tw.WriteLine(tabla.AutoBiztositasAr);
            tw.WriteLine(tabla.MosogepAr);
            tw.WriteLine(tabla.TVAr);
            tw.WriteLine(tabla.PCAr);
            tw.WriteLine(tabla.HutoAr);
            tw.WriteLine(tabla.KonyhabutorAr);
            tw.WriteLine(tabla.AgyAr);
            tw.WriteLine(tabla.SzekrenyAr);
            tw.WriteLine(tabla.BetetKamat);
            tw.WriteLine(tabla.BetetKamatKorDB);

            this.Allapot = Konstansok.JATEK;
            tw.Close();
        }

        private void Betoltes()
        {
            try
            {
                IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
                FileStream stream = store.OpenFile("mentes.txt", FileMode.Open, FileAccess.Read);
                TextReader tr = new StreamReader(stream);

                jatekkor = Convert.ToInt32(tr.ReadLine());
                aktualis = Convert.ToInt32(tr.ReadLine());
                aktualispanel = aktualis;
                aktivjatekos = 0;

                int i = 1;
                while (tr.ReadLine()[0] == 'P')
                {
                    jatekosok.Add(i, new Jatekos("", 1, tabla));
                    jatekosok[i].Nev = tr.ReadLine();
                    jatekosok[i].Szin = Convert.ToInt32(tr.ReadLine());
                    jatekosok[i].Pozicio = tabla.Mezok[Convert.ToInt32(tr.ReadLine())];
                    tabla.Mezok[6].Jatekosok.Remove(jatekosok[i]);
                    jatekosok[i].Pozicio.Jatekosok.Add(jatekosok[i]);
                    jatekosok[i].Penz = Convert.ToInt32(tr.ReadLine());
                    jatekosok[i].Bankbetet = Convert.ToInt32(tr.ReadLine());
                    jatekosok[i].Allapot = Convert.ToInt32(tr.ReadLine());
                    jatekosok[i].Hanyszor = Convert.ToInt32(tr.ReadLine());
                    jatekosok[i].Aktiv = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].ReszletDB = Convert.ToInt32(tr.ReadLine());
                    jatekosok[i].Reszlet = Convert.ToInt32(tr.ReadLine());
                    jatekosok[i].Torlesztesig = Convert.ToInt32(tr.ReadLine());
                    jatekosok[i].CsakHatos = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].FizetestKaphat = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].Lakas = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].Auto = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].LakasBiztositas = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].AutoBiztositas = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].Mosogep = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].Konyhabutor = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].TV = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].PC = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].Huto = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].Agy = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].Szekreny = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].Garazs = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].MosogepKupon = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].KonyhabutorKupon = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].TVKupon = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].PCKupon = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].HutoKupon = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].AgyKupon = Convert.ToBoolean(tr.ReadLine());
                    jatekosok[i].SzekrenyKupon = Convert.ToBoolean(tr.ReadLine());
                    i++;
                }
                foreach (var j in jatekosok)
                {
                    if (j.Value.Aktiv)
                    {
                        aktivjatekos++;
                    }
                }
                for (int j = 1; j <= tabla.Kartyak.Count; j++)
                {
                    tabla.Huzottak[j] = Convert.ToBoolean(tr.ReadLine());
                }

                tabla.Kihuzott = Convert.ToInt32(tr.ReadLine());
                tabla.Fizetes = Convert.ToInt32(tr.ReadLine());
                tabla.Kezdo = Convert.ToInt32(tr.ReadLine());
                tabla.IngatlanirodaJutalek = Convert.ToInt32(tr.ReadLine());
                tabla.ReszletKamat = Convert.ToInt32(tr.ReadLine());
                tabla.ReszletDB = Convert.ToInt32(tr.ReadLine());
                tabla.ReszletKor = Convert.ToInt32(tr.ReadLine());
                tabla.Kezdoreszlet = Convert.ToInt32(tr.ReadLine());
                tabla.LakasAr = Convert.ToInt32(tr.ReadLine());
                tabla.GarazsAr = Convert.ToInt32(tr.ReadLine());
                tabla.AutoAr = Convert.ToInt32(tr.ReadLine());
                tabla.LakasBiztositasAr = Convert.ToInt32(tr.ReadLine());
                tabla.AutoBiztositasAr = Convert.ToInt32(tr.ReadLine());
                tabla.MosogepAr = Convert.ToInt32(tr.ReadLine());
                tabla.TVAr = Convert.ToInt32(tr.ReadLine());
                tabla.PCAr = Convert.ToInt32(tr.ReadLine());
                tabla.HutoAr = Convert.ToInt32(tr.ReadLine());
                tabla.KonyhabutorAr = Convert.ToInt32(tr.ReadLine());
                tabla.AgyAr = Convert.ToInt32(tr.ReadLine());
                tabla.SzekrenyAr = Convert.ToInt32(tr.ReadLine());
                tabla.BetetKamat = Convert.ToInt32(tr.ReadLine());
                tabla.BetetKamatKorDB = Convert.ToInt32(tr.ReadLine());

                this.Allapot = Konstansok.JATEK;
                tr.Close();
            }
            catch
            {
                this.Allapot = Konstansok.START;
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            #region start képernyõ

            if (this.Allapot == Konstansok.START)
            {
                if (alpha < 1 && halvanyul == 0)
                {
                    alpha += alphamertek;
                }

                float beallitasokX = GraphicsDevice.Viewport.TitleSafeArea.Width - beallitasok.Width - 25;
                float beallitasokY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - beallitasok.Height / 2;
                float betoltesX = beallitasokX - 25 - betoltes.Width;
                float betoltesY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - betoltes.Height / 2;
                float ujjatekX = betoltesX - 25 - ujjatek.Width;
                float ujjatekY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - ujjatek.Height / 2;
                
                spriteBatch.Draw(menuhatter, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X - (menuhatter.Width - GraphicsDevice.Viewport.TitleSafeArea.Width) / 2, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White * alpha);
                spriteBatch.Draw(ujjatek, new Vector2(ujjatekX, ujjatekY), Color.White * alpha);
                spriteBatch.Draw(betoltes, new Vector2(betoltesX, betoltesY), Color.White * alpha);
                spriteBatch.Draw(beallitasok, new Vector2(beallitasokX, beallitasokY), Color.White * alpha);
                

                if (alpha >= 1)
                {
                    nevek.Clear();
                    jatekosok.Clear();
                    szinek.Clear();
                    valasztottszinek.Clear();
                    for (int i = 1; i <= tabla.Huzottak.Count; i++)
                    {
                        tabla.Huzottak[i] = false;
                    }
                    for (int i = 1; i <= tabla.Mezok.Count; i++)
                    {
                        tabla.Mezok[i].Jatekosok.Clear();
                    }

                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Pressed &&
                        touchX >= ujjatekX &&
                        touchX <= ujjatekX + ujjatek.Width &&
                        touchY >= ujjatekY &&
                        touchY <= ujjatekY + ujjatek.Height)
                    {
                        halvanyul = Konstansok.JATEKOSOK;

                        valasztottszinek.Add(1, true);
                        valasztottszinek.Add(2, true);
                        valasztottszinek.Add(3, false);
                        valasztottszinek.Add(4, false);
                        valasztottszinek.Add(5, false);
                        valasztottszinek.Add(6, false);
                        valasztottszinek.Add(7, false);

                        nevek.Add(1, "");
                        nevek.Add(2, "");

                        aktivjatekos = 2;

                        szinek.Add(1, 1);
                        szinek.Add(2, 2);
                    }
                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Pressed &&
                        touchX >= betoltesX &&
                        touchX <= betoltesX + betoltes.Width &&
                        touchY >= betoltesY &&
                        touchY <= betoltesY + betoltes.Height)
                    {
                        halvanyul = Konstansok.BETOLT;
                    }
                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Pressed &&
                        touchX >= beallitasokX &&
                        touchX <= beallitasokX + beallitasok.Width &&
                        touchY >= beallitasokY &&
                        touchY <= beallitasokY + beallitasok.Height)
                    {
                        halvanyul = Konstansok.BEALLITASOK;
                    }
                }
                if (halvanyul > 0)
                {
                    if (alpha > 0)
                    {
                        alpha -= alphamertek;
                    }
                    else
                    {
                        this.Allapot = halvanyul;
                        halvanyul = 0;
                    }
                }
            }

            #endregion

            #region játék

            if (this.Allapot == Konstansok.JATEK)
            {
                #region dobásra várás

                if (jatekosok[aktualis].Allapot == Konstansok.DOBAS)
                {
                    if (alpha < 1 && halvanyul == 0)
                    {
                        alpha += alphamertek;
                    }

                    tablascale = 1.0f;
                    tablaXMozgat = 0;
                    tablaYMozgat = 0;

                    this.TablaNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);
                    this.BabukNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);
                    
                    if (alpha >= 1)
                    {
                        tablazoom = 10.0f;
                        tablascalemertek = 0.25f;
                        lepesXMozgas = 0;
                        lepesYMozgas = 0;
                        lepesszam = 0;
                        mozdulas = 0;
                        dob = 0;
                        sorszam = 0;

                        if (felszerelesablakscale > 0)
                        {
                            felszerelesablakscale -= ablakscalemertek;
                        }

                        spriteBatch.Draw(felszerelesablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - felszerelesablak.Width * felszerelesablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - felszerelesablak.Height * felszerelesablakscale / 2), null, Color.White, 0, new Vector2(0, 0), felszerelesablakscale, SpriteEffects.None, 0);

                        if (visszakerdezoablakscale <= 0)
                        {
                            if (jatekosok[aktualis].Hanyszor > 0)
                            {
                                if (touch.Count > 0 &&
                                    touch[0].State == TouchLocationState.Pressed &&
                                    touchX >= fopanelfulekX &&
                                    touchX <= (fopanelfulekX + FopanelFulek[jatekosok[aktualis].Szin].Width) &&
                                    touchY <= fopanelfulekY &&
                                    touchY > (fopanelfulekY - jatekosok.Count * FopanelFulek[jatekosok[aktualis].Szin].Height))
                                {
                                    aktualispanel = (int)((fopanelfulekY - touchY) / FopanelFulek[jatekosok[aktualis].Szin].Height + 1);
                                }

                                if (touch.Count > 0 &&
                                    touch[0].State == TouchLocationState.Pressed &&
                                    touchX >= dobasX &&
                                    touchX <= dobasX + dobas.Width &&
                                    touchY >= dobasY &&
                                    touchY <= dobasY + dobas.Height)
                                {
                                    dob = jatekosok[aktualis].Dobas();
                                    //dob = 6;
                                    jatekosok[aktualis].Allapot = Konstansok.DOBASABLAK;
                                    lepesek = jatekosok[aktualis].Lepesek(dob);
                                    varakozasstart = gameTime.TotalGameTime;
                                }

                                if (touch.Count > 0 &&
                                    touch[0].State == TouchLocationState.Pressed &&
                                    touchX >= felszerelesX &&
                                    touchX <= felszerelesX + felszereles.Width &&
                                    touchY >= felszerelesY &&
                                    touchY <= felszerelesY + felszereles.Height)
                                {
                                    
                                    jatekosok[aktualis].Allapot = Konstansok.FELSZERELES;
                                }
                                if (touch.Count > 0 &&
                                    touch[0].State == TouchLocationState.Pressed &&
                                    touchX >= mentesX &&
                                    touchX <= mentesX + mentes.Width &&
                                    touchY >= mentesY &&
                                    touchY <= mentesY + mentes.Height)
                                {
                                    this.Allapot = Konstansok.MENTES;
                                }


                                if (touch.Count > 0 &&
                                    touch[0].State == TouchLocationState.Pressed &&
                                    touchX >= kilepesX &&
                                    touchX <= kilepesX + kilepes.Width &&
                                    touchY >= kilepesY &&
                                    touchY <= kilepesY + kilepes.Height)
                                {

                                    halvanyul = Konstansok.START;
                                }
                            }
                            else
                            {
                                jatekosok[aktualis].Allapot = Konstansok.KIMARADABLAK;
                                varakozasstart = gameTime.TotalGameTime;
                            }
                        }
                        else
                        {
                            jatekosok[aktualis].Allapot = Konstansok.KORVEGE;
                        }
                    }

                    if (halvanyul > 0)
                    {
                        if (alpha > 0)
                        {
                            alpha -= alphamertek;
                        }
                        else
                        {
                            this.Allapot = halvanyul;
                            halvanyul = 0;
                        }
                    }

                }
                #endregion

                #region választási lehetõségek

                if (jatekosok[aktualis].Allapot == Konstansok.VALASZTAS)
                {
                    if (jatekosok[aktualis].CsakHatos && dob != 6)
                    {
                        jatekosok[aktualis].Hanyszor--;
                        jatekosok[aktualis].Allapot = Konstansok.KORVEGE;
                    }
                    else
                    {
                        this.TablaNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);

                        if (lepesablakscale > 0.0f)
                        {
                            lepesablakscale -= ablakscalemertek;
                        }
                        foreach (Mezo m in lepesek)
                        {
                            if (villog)
                            {
                                spriteBatch.Draw(MezoOpcio[m.Sorszam], new Vector2(m.XPos, m.YPos), Color.White);
                            }
                            else
                            {
                                spriteBatch.Draw(MezoNormal[m.Sorszam], new Vector2(m.XPos, m.YPos), Color.White);
                            }
                        }
                        this.BabukNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);

                        spriteBatch.Draw(lepesablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - lepesablak.Width * lepesablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - lepesablak.Height * lepesablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), lepesablakscale, SpriteEffects.None, 0);

                        if (lepesablakscale <= 0)
                        {
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed)
                            {
                                foreach (Mezo m in lepesek)
                                {
                                    if (touchX >= m.XPos && 
                                        touchX <= m.XPos + MezoNormal[m.Sorszam].Height && 
                                        touchY >= m.YPos &&
                                        touchY <= m.YPos + MezoNormal[m.Sorszam].Width)
                                    {
                                        mezo = m;
                                        mezohatasszoveg = jatekosok[aktualis].Megnez(m);
                                    }
                                }
                            }
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= fopanelfulekX &&
                                touchX <= (fopanelfulekX + FopanelFulek[jatekosok[aktualis].Szin].Width) &&
                                touchY <= fopanelfulekY &&
                                touchY > (fopanelfulekY - jatekosok.Count * FopanelFulek[jatekosok[aktualis].Szin].Height))
                            {
                                aktualispanel = (int)((fopanelfulekY - touchY) / FopanelFulek[jatekosok[aktualis].Szin].Height + 1);
                            }
                        }
                    }
                }
                #endregion

                #region lehetõség megnézése

                if (jatekosok[aktualis].Allapot == Konstansok.MEZOMEGNEZES)
                {
                    this.TablaNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);

                    foreach (Mezo m in lepesek)
                    {
                        spriteBatch.Draw(MezoOpcio[m.Sorszam], new Vector2(m.XPos, m.YPos), Color.White);
                    }

                    this.BabukNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);

                    if (lepesablakscale < 1)
                    {
                        lepesablakscale += ablakscalemertek;
                    }
                    spriteBatch.Draw(lepesablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - lepesablak.Width * lepesablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - lepesablak.Height * lepesablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), lepesablakscale, SpriteEffects.None, 0);

                    if (lepesablakscale >= 1.0f)
                    {
                        spriteBatch.DrawString(panelfont, mezo.Szoveg, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - lepesablak.Width * lepesablakscale / 2 + 50, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + panelfont.MeasureString(mezo.Szoveg).X / 2), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        if (panelfont.MeasureString(mezohatasszoveg).X > lepesablak.Height - 25 - 25)
                        {
                            float scale = (lepesablak.Height - 25 - 25) / panelfont.MeasureString(mezohatasszoveg).X;
                            spriteBatch.DrawString(panelfont, mezohatasszoveg, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - lepesablak.Width * lepesablakscale / 2 + 150, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + panelfont.MeasureString(mezohatasszoveg).X * scale / 2), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), scale, SpriteEffects.None, 0);
                        }
                        else
                        {
                            spriteBatch.DrawString(panelfont, mezohatasszoveg, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - lepesablak.Width * lepesablakscale / 2 + 150, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + panelfont.MeasureString(mezohatasszoveg).X / 2), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        }
                        spriteBatch.Draw(ok, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + lepesablak.Width / 2 - ok.Width - 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + 25), Color.White);
                        spriteBatch.Draw(vissza, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + lepesablak.Width / 2 - vissza.Width - 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - vissza.Height - 25), Color.White);
                        if (touch.Count > 0 &&
                            touch[0].State == TouchLocationState.Pressed &&
                            touchX >= (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + lepesablak.Width / 2 - ok.Width - 25) &&
                            touchX <= (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + lepesablak.Width / 2 - 25) &&
                            touchY >= (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + 25) &&
                            touchY <= (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + 25 + ok.Height))
                        {
                            jatekosok[aktualis].Allapot = Konstansok.KOZELITES;
                        }
                        if (touch.Count > 0 &&
                            touch[0].State == TouchLocationState.Pressed &&
                            touchX >= (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + lepesablak.Width / 2 - vissza.Width - 25) &&
                            touchX <= (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + lepesablak.Width / 2 - 25) &&
                            touchY >= (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - vissza.Height - 25) &&                                          
                            touchY <= (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - 25))
                        {
                            jatekosok[aktualis].Allapot = Konstansok.VALASZTAS;
                            villogasstart = gameTime.TotalGameTime;
                        }
                    }

                }
                #endregion

                #region mezõre közelítés

                if (jatekosok[aktualis].Allapot == Konstansok.KOZELITES)
                {

                    if (lepesablakscale > 0.0f)
                    {
                        this.TablaNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);
                        this.BabukNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);
                        lepesablakscale -= ablakscalemertek;
                    }
                    spriteBatch.Draw(lepesablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - lepesablak.Width * lepesablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - lepesablak.Height * lepesablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), lepesablakscale, SpriteEffects.None, 0);
                    if (lepesablakscale <= 0.0f)
                    {
                        if (tablascale < tablazoom)
                        {
                            tablascale += tablascalemertek;

                            tablaXMozgat += ((GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - MezoNormal[jatekosok[aktualis].Pozicio.Sorszam].Width * tablazoom / 2) - (jatekosok[aktualis].Pozicio.XPos * tablazoom - (GraphicsDevice.Viewport.TitleSafeArea.Width * (tablazoom - 1) / 2))) / ((tablazoom-1) / tablascalemertek);
                            tablaYMozgat += ((GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - MezoNormal[jatekosok[aktualis].Pozicio.Sorszam].Height * tablazoom / 2) - (jatekosok[aktualis].Pozicio.YPos * tablazoom - (GraphicsDevice.Viewport.TitleSafeArea.Height * (tablazoom - 1) / 2))) / ((tablazoom-1) / tablascalemertek);

                        }
                        this.TablaNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);
                        this.BabukNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);



                        if (tablascale >= tablazoom)
                        {
                            lepessor = jatekosok[aktualis].Lepessor(mezo, dob);
                            jatekosok[aktualis].Allapot = Konstansok.MOZGAS;
                        }
                    }
                }

                #endregion

                #region lépés animálása

                if (jatekosok[aktualis].Allapot == Konstansok.MOZGAS)
                {
                    if (lepesszam < lepessor.Length)
                    {
                        if (mozdulas < 20)
                        {
                            switch (lepessor[lepesszam])
                            {
                                case 'L': lepesXMozgas -= 17.5f; break;
                                case 'F': lepesXMozgas += 17.5f; break;
                                case 'J': lepesYMozgas += 17.5f; break;
                                case 'B': lepesYMozgas -= 17.5f; break;
                            }
                            mozdulas++;
                        }
                        else
                        {
                            mozdulas = 0;
                            lepesszam++;
                        }

                        this.TablaNagyRajzolas(tablazoom, lepesXMozgas, lepesYMozgas, alpha);
                        this.BabukNagyRajzolas(tablazoom, lepesXMozgas, lepesYMozgas, alpha);
                    }
                    else
                    {
                        TablaNagyRajzolas(tablazoom, lepesXMozgas, lepesYMozgas, alpha);
                        BabukNagyRajzolas(tablazoom, lepesXMozgas, lepesYMozgas, alpha);

                        sorszam = jatekosok[aktualis].Lep(mezo);
                        if (jatekosok[aktualis].Hanyszor > 0)
                        {
                            tablaXMozgat = ((jatekosok[aktualis].Pozicio.XPos * tablazoom - (GraphicsDevice.Viewport.TitleSafeArea.Width * (tablazoom - 1) / 2)) - (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - MezoNormal[jatekosok[aktualis].Pozicio.Sorszam].Width * tablazoom / 2)) * (-1);
                            tablaYMozgat = ((jatekosok[aktualis].Pozicio.YPos * tablazoom - (GraphicsDevice.Viewport.TitleSafeArea.Height * (tablazoom - 1) / 2)) - (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - MezoNormal[jatekosok[aktualis].Pozicio.Sorszam].Height * tablazoom / 2)) * (-1);
                        }
                    }
                }

                #endregion

                #region vásárlás ablak

                if (jatekosok[aktualis].Allapot == Konstansok.VASARLASABLAK)
                {
                    if (sorszam == 7)
                    {
                        jatekosok[aktualis].Vasarol(sorszam, 0);
                        jatekosok[aktualis].Allapot = 0;
                    }
                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);

                    if (vasarlasablakscale < 1)
                    {
                        vasarlasablakscale += ablakscalemertek;
                    }

                    tovabbX = GraphicsDevice.Viewport.TitleSafeArea.Width / 2;
                    tovabbY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + vasarlasablak.Height / 2 - tovabb.Height - 25;
                    vasarlasablakX = GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - vasarlasablak.Width * vasarlasablakscale / 2;
                    vasarlasablakY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - vasarlasablak.Height * vasarlasablakscale / 2;


                    spriteBatch.Draw(vasarlasablak, new Vector2(vasarlasablakX, vasarlasablakY), null, Color.White, 0f, new Vector2(0, 0), vasarlasablakscale, SpriteEffects.None, 0);
                    if (vasarlasablakscale >= 1.0f)
                    {
                        if (visszakerdezoablakscale > 0)
                        {
                            visszakerdezoablakscale -= ablakscalemertek;
                        }
                        if (valaszablakscale > 0)
                        {
                            valaszablakscale -= ablakscalemertek;
                        }
                        if (felszerelesablakscale > 0)
                        {
                            felszerelesablakscale -= ablakscalemertek;
                        }

                        this.VasarlasablakRajzolas(sorszam);

                        if (visszakerdezoablakscale <= 0 && valaszablakscale <= 0 && felszerelesablakscale <= 0)
                        {
                            //spriteBatch.Draw(megveszem, new Vector2(vasarlasablakX + vasarlasablak.Width - megveszem.Width - 25, vasarlasablakY + 25 + keret + i * (panelfontmagassag + koz)), Color.White);
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= (vasarlasablakX + vasarlasablak.Width - megveszem.Width - 25) &&
                                touchX <= (vasarlasablakX + vasarlasablak.Width - 25) &&
                                touchY >= (vasarlasablakY + 25 + keret) &&                         
                                touchY < (vasarlasablakY + 25 + keret + termekdb * megveszem.Height))
                            {
                                termeksorszam = (int)((touchY - (vasarlasablakY + 25 + keret)) / megveszem.Height);

                                jatekosok[aktualis].Allapot = Konstansok.VASARLASVISSZAKERDEZES;
                            }

                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= (tovabbX) &&
                                touchX <= (tovabbX + tovabb.Width) &&
                                touchY >= (tovabbY) &&
                                touchY <= (tovabbY + tovabb.Height))
                            {
                                jatekosok[aktualis].Allapot = Konstansok.VASARLASABLAKBEZARAS;
                            }
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + vasarlasablak.Width / 2 - 25 - felszerelesnagy.Width) &&
                                touchX <= (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + vasarlasablak.Width / 2 - 25 + felszerelesnagy.Width) &&
                                touchY >= (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + vasarlasablak.Height / 2 - felszerelesnagy.Height - 25) &&
                                touchY <= (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + vasarlasablak.Height / 2 + felszerelesnagy.Height - 25))
                            {
                                aktualispanel = aktualis;
                                jatekosok[aktualis].Allapot = Konstansok.FELSZERELES;
                            }
                        }
                    }
                }

                #endregion

                #region vásárlás visszakérdezés

                if (jatekosok[aktualis].Allapot == Konstansok.VASARLASVISSZAKERDEZES)
                {
                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);
                    if (visszakerdezoablakscale < 1)
                    {
                        visszakerdezoablakscale += ablakscalemertek;
                    }

                    spriteBatch.Draw(vasarlasablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - vasarlasablak.Width * lepesablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - vasarlasablak.Height * lepesablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), lepesablakscale, SpriteEffects.None, 0);
                    this.VasarlasablakRajzolas(sorszam);

                    spriteBatch.Draw(visszakerdezoablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - visszakerdezoablak.Width * visszakerdezoablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - visszakerdezoablak.Height * visszakerdezoablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), visszakerdezoablakscale, SpriteEffects.None, 0);

                    if (visszakerdezoablakscale >= 1)
                    {
                        String kerdes = "Biztosan szeretnéd megvenni?";
                        String szoveg = jatekosok[aktualis].VasarolVisszakerdez(sorszam, termeksorszam + 1);
                        spriteBatch.DrawString(panelfont, kerdes, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - panelfont.MeasureString(kerdes).X / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - visszakerdezoablak.Height / 2 + 25), Color.White);
                        spriteBatch.DrawString(panelfont, szoveg, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - panelfont.MeasureString(szoveg).X / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - visszakerdezoablak.Height / 2 + 75), Color.White);
                        spriteBatch.Draw(igen, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - igen.Width - 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + visszakerdezoablak.Height / 2 - igen.Height - 25), Color.White);
                        spriteBatch.Draw(nem, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + visszakerdezoablak.Height / 2 - nem.Height - 25), Color.White);
                        if (touch.Count > 0 &&
                            touch[0].State == TouchLocationState.Pressed &&
                            touchX >= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + 25 &&
                            touchX <= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + 25 + nem.Width &&
                            touchY >= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + visszakerdezoablak.Height / 2 - nem.Height - 25 &&
                            touchY <= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + visszakerdezoablak.Height / 2 - 25)
                        {
                            jatekosok[aktualis].Allapot = Konstansok.VASARLASABLAK;
                        }
                        if (touch.Count > 0 &&
                            touch[0].State == TouchLocationState.Pressed &&
                            touchX >= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - igen.Width - 25 &&
                            touchX <= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - 25 &&
                            touchY >= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + visszakerdezoablak.Height / 2 - igen.Height - 25 &&
                            touchY <= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + visszakerdezoablak.Height / 2 - 25)
                        {
                            vasarlasvalasz = jatekosok[aktualis].Vasarol(sorszam, termeksorszam + 1);
                            //tw.WriteLine(jatekosok[aktualis].Nev + " vásárol " + szoveg);
                            jatekosok[aktualis].Allapot = Konstansok.VASARLAS;
                        }
                    }
                }

                #endregion

                #region automatikus mezõre lépés

                if (jatekosok[aktualis].Allapot == Konstansok.MEZORELEPES)
                {
                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);
                    lepesszam = 0;
                    lepesXMozgas = 0;
                    lepesYMozgas = 0;
                    switch (jatekosok[aktualis].Pozicio.Sorszam)
                    {
                        case 47: lepessor = "LLLLLJJJLLLLBBBB"; mezo = tabla.Mezok[88]; break;
                        case 75: lepessor = "LLLL"; mezo = tabla.Mezok[88]; break;
                        case 76: lepessor = "FFFFFBBBBFFFFFBBBB"; mezo = tabla.Mezok[10]; break;
                        case 72: lepessor = "FFFFFFFFFFJJFFFF"; mezo = tabla.Mezok[6]; break;
                    }
                    jatekosok[aktualis].Allapot = Konstansok.MOZGAS;
                }

                #endregion

                #region vásárlásablak bezárása

                if (jatekosok[aktualis].Allapot == Konstansok.VASARLASABLAKBEZARAS)
                {
                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);
                    if (vasarlasablakscale > 0)
                    {
                        vasarlasablakscale -= ablakscalemertek;
                        spriteBatch.Draw(vasarlasablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - vasarlasablak.Width * vasarlasablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - vasarlasablak.Height * vasarlasablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), vasarlasablakscale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        jatekosok[aktualis].Allapot = 0;
                    }
                }
                #endregion

                #region vásárlás

                if (jatekosok[aktualis].Allapot == Konstansok.VASARLAS)
                {
                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);

                    if (visszakerdezoablakscale > 0)
                    {
                        visszakerdezoablakscale -= ablakscalemertek;
                    }

                    switch (vasarlasvalasz)
                    {
                        case 1: vasarlasvalaszszoveg = "Nincs még lakásod!"; break;
                        case 2: vasarlasvalaszszoveg = "Ilyened már van!"; break;
                        case 3: vasarlasvalaszszoveg = "Sikeres vásárlás!"; break;
                        case 4: vasarlasvalaszszoveg = "Nincs elég pénzed!"; break;
                        case 5: vasarlasvalaszszoveg = "Nincs még garázsod!"; break;
                        case 6: vasarlasvalaszszoveg = "Nincs még autód!"; break;
                    }

                    spriteBatch.Draw(vasarlasablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - vasarlasablak.Width * lepesablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - vasarlasablak.Height * lepesablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), lepesablakscale, SpriteEffects.None, 0);
                    this.VasarlasablakRajzolas(sorszam);

                    spriteBatch.Draw(visszakerdezoablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - visszakerdezoablak.Width * visszakerdezoablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - visszakerdezoablak.Height * visszakerdezoablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), visszakerdezoablakscale, SpriteEffects.None, 0);
                    if (visszakerdezoablakscale <= 0)
                    {
                        if (valaszablakscale < 1.0f)
                        {
                            valaszablakscale += ablakscalemertek;
                        }
                        spriteBatch.Draw(valaszablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - valaszablak.Width * valaszablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - valaszablak.Height * valaszablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), valaszablakscale, SpriteEffects.None, 0);

                        if (valaszablakscale >= 1.0f)
                        {
                            spriteBatch.DrawString(panelfont, vasarlasvalaszszoveg, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - panelfont.MeasureString(vasarlasvalaszszoveg).X / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - valaszablak.Height / 2 + 25), Color.White);
                            spriteBatch.Draw(tovabb, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - tovabb.Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + valaszablak.Height / 2 - tovabb.Height - 25), Color.White);
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - tovabb.Width / 2 &&
                                touchX <= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + tovabb.Width / 2 &&
                                touchY >= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + valaszablak.Height / 2 - tovabb.Height - 25 &&
                                touchY <= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + valaszablak.Height / 2 - 25)
                            {
                                //tw.WriteLine(jatekosok[aktualis].Nev + " " + vasarlasvalaszszoveg);
                                jatekosok[aktualis].Allapot = Konstansok.VASARLASABLAK;
                            }
                        }
                    }

                }

                #endregion

                #region szerencsekártya húzás

                if (jatekosok[aktualis].Allapot == Konstansok.KARTYAHUZAS)
                {
                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);
                    if (szerencsekartyaablakscale < 1)
                    {
                        szerencsekartyaablakscale += ablakscalemertek;
                    }
                    spriteBatch.Draw(szerencsekartyaablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - szerencsekartyaablak.Width * szerencsekartyaablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - szerencsekartyaablak.Height * szerencsekartyaablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), szerencsekartyaablakscale, SpriteEffects.None, 0);
                    if (szerencsekartyaablakscale >= 1.0f)
                    {
                        this.SzerencsekartyaHatasKiiras(sorszam);

                        if (jatekosok[aktualis].Felolvas(tabla.Kartyak[sorszam]))
                        {
                            spriteBatch.Draw(vasarolok, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - vasarolok.Width - 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - vasarolok.Height - 25), Color.White);
                            spriteBatch.Draw(tovabb, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - tovabb.Height - 25), Color.White);
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - vasarolok.Width - 25 &&
                                touchX <= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + vasarolok.Width - 25 &&
                                touchY >= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - vasarolok.Height - 25 &&
                                touchY <= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - 25)
                            {
                                szerencsekartyavalasz = jatekosok[aktualis].KartyaVegrehajt(tabla.Kartyak[sorszam]);
                                jatekosok[aktualis].Allapot = Konstansok.KARTYAVALASZ;
                            }
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + 25 &&
                                touchX <= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + 25 + tovabb.Width &&
                                touchY >= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - tovabb.Height - 25 &&
                                touchY <= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - 25)
                            {
                                jatekosok[aktualis].KartyaVegrehajt(tabla.Kartyak[sorszam]);
                                jatekosok[aktualis].Allapot = Konstansok.KARTYAABLAKBEZARAS;
                            }
                        }
                        else
                        {
                            spriteBatch.Draw(tovabb, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - tovabb.Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - tovabb.Height - 25), Color.White);
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - tovabb.Width / 2 &&
                                touchX <= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + tovabb.Width / 2 &&
                                touchY >= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - tovabb.Height - 25 &&
                                touchY <= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - 25)
                            {
                                jatekosok[aktualis].KartyaVegrehajt(tabla.Kartyak[sorszam]);
                                jatekosok[aktualis].Allapot = Konstansok.KARTYAABLAKBEZARAS;
                                if (jatekosok[aktualis].Hanyszor > 0)
                                {
                                    tablaXMozgat = ((jatekosok[aktualis].Pozicio.XPos * tablazoom - (GraphicsDevice.Viewport.TitleSafeArea.Width * (tablazoom - 1) / 2)) - (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - MezoNormal[jatekosok[aktualis].Pozicio.Sorszam].Width * tablazoom / 2)) * (-1);
                                    tablaYMozgat = ((jatekosok[aktualis].Pozicio.YPos * tablazoom - (GraphicsDevice.Viewport.TitleSafeArea.Height * (tablazoom - 1) / 2)) - (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - MezoNormal[jatekosok[aktualis].Pozicio.Sorszam].Height * tablazoom / 2)) * (-1);
                                }
                            }
                        }
                    }
                }

                #endregion

                #region kártya ablak bezárás

                if (jatekosok[aktualis].Allapot == Konstansok.KARTYAABLAKBEZARAS)
                {
                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);
                    if (szerencsekartyaablakscale > 0)
                    {
                        szerencsekartyaablakscale -= ablakscalemertek;
                    }
                    spriteBatch.Draw(szerencsekartyaablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - szerencsekartyaablak.Width * szerencsekartyaablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - szerencsekartyaablak.Height * szerencsekartyaablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), szerencsekartyaablakscale, SpriteEffects.None, 0);
                    if (szerencsekartyaablakscale <= 0)
                    {
                        jatekosok[aktualis].Allapot = Konstansok.KORVEGE;
                    }
                }

                #endregion

                #region kártya válasz

                if (jatekosok[aktualis].Allapot == Konstansok.KARTYAVALASZ)
                {
                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);

                    spriteBatch.Draw(szerencsekartyaablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - szerencsekartyaablak.Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - szerencsekartyaablak.Height / 2), null, Color.White, 0f, new Vector2(0, 0), szerencsekartyaablakscale, SpriteEffects.None, 0);
                    this.SzerencsekartyaHatasKiiras(sorszam);
                    spriteBatch.Draw(vasarolok, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - vasarolok.Width - 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - vasarolok.Height - 25), Color.White);
                    spriteBatch.Draw(tovabb, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - tovabb.Height - 25), Color.White);

                    if (valaszablakscale < 1)
                    {
                        valaszablakscale += ablakscalemertek;
                    }
                    spriteBatch.Draw(valaszablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - valaszablak.Width * valaszablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - valaszablak.Height * valaszablakscale / 2), null, Color.White, 0, new Vector2(0, 0), valaszablakscale, SpriteEffects.None, 0);
                    switch (szerencsekartyavalasz)
                    {
                        case 1: szerencsekartyavalaszszoveg = "Nincs még lakásod!"; break;
                        case 2: szerencsekartyavalaszszoveg = "Ilyened már van!"; break;
                        case 3: szerencsekartyavalaszszoveg = "Sikeres vásárlás!"; break;
                        case 4: szerencsekartyavalaszszoveg = "Nincs elég pénzed!"; break;
                        case 5: szerencsekartyavalaszszoveg = "Nincs még garázsod!"; break;
                        case 6: szerencsekartyavalaszszoveg = "Nincs még autód!"; break;
                    }

                    if (valaszablakscale >= 1.0f)
                    {
                        spriteBatch.DrawString(panelfont, szerencsekartyavalaszszoveg, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - panelfont.MeasureString(szerencsekartyavalaszszoveg).X / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - valaszablak.Height / 2 + 25), Color.White);
                        spriteBatch.Draw(tovabb, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - tovabb.Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + valaszablak.Height / 2 - tovabb.Height - 25), Color.White);
                        if (touch.Count > 0 &&
                            touch[0].State == TouchLocationState.Pressed &&
                            touchX >= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - tovabb.Width / 2 &&
                            touchX <= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + tovabb.Width / 2 &&
                            touchY >= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + valaszablak.Height / 2 - tovabb.Height - 25 &&
                            touchY <= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + valaszablak.Height / 2 - 25)
                        {
                            jatekosok[aktualis].Allapot = Konstansok.KARTYABEZAR;
                        }
                    }


                }

                #endregion

                #region ablakok bezárása

                if (jatekosok[aktualis].Allapot == Konstansok.KARTYABEZAR)
                {
                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);
                    if (valaszablakscale <= 0)
                    {
                        if (szerencsekartyaablakscale > 0)
                        {
                            szerencsekartyaablakscale -= ablakscalemertek;
                        }
                    }
                    else
                    {
                        valaszablakscale -= ablakscalemertek;
                    }
                    spriteBatch.Draw(szerencsekartyaablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - szerencsekartyaablak.Width * szerencsekartyaablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - szerencsekartyaablak.Height * szerencsekartyaablakscale / 2), null, Color.White, 0f, new Vector2(0, 0), szerencsekartyaablakscale, SpriteEffects.None, 0);
                    if (szerencsekartyaablakscale >= 1)
                    {
                        this.SzerencsekartyaHatasKiiras(sorszam);
                        spriteBatch.Draw(vasarolok, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - vasarolok.Width - 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - vasarolok.Height - 25), Color.White);
                        spriteBatch.Draw(tovabb, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + szerencsekartyaablak.Height / 2 - tovabb.Height - 25), Color.White);
                    }
                    spriteBatch.Draw(valaszablak, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - valaszablak.Width * valaszablakscale / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - valaszablak.Height * valaszablakscale / 2), null, Color.White, 0, new Vector2(0, 0), valaszablakscale, SpriteEffects.None, 0);
                    if (valaszablakscale >= 1)
                    {
                        spriteBatch.DrawString(panelfont, szerencsekartyavalaszszoveg, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - panelfont.MeasureString(szerencsekartyavalaszszoveg).X / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - valaszablak.Height / 2 + 25), Color.White);
                        spriteBatch.Draw(tovabb, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - tovabb.Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + valaszablak.Height / 2 - tovabb.Height - 25), Color.White);
                    }
                    if (szerencsekartyaablakscale <= 0)
                    {
                        jatekosok[aktualis].Allapot = 0;
                    }
                }

                #endregion

                #region felszerelés ablak

                if (jatekosok[aktualis].Allapot == Konstansok.FELSZERELES)
                {
                    if (vasarlasablakscale >= 1)
                    {
                        this.TablaNagyRajzolas(tablascale, 0, 0, alpha);
                        this.BabukNagyRajzolas(tablascale, 0, 0, alpha);

                        spriteBatch.Draw(vasarlasablak, new Vector2(vasarlasablakX, vasarlasablakY), null, Color.White, 0f, new Vector2(0, 0), vasarlasablakscale, SpriteEffects.None, 0);

                        this.VasarlasablakRajzolas(sorszam);

                        if (touch.Count > 0 &&
                            touch[0].State == TouchLocationState.Pressed &&
                            touchX >= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + felszerelesablak.Width / 2 - vissza.Width - 25 &&
                            touchX <= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + felszerelesablak.Width / 2 - 25 &&
                            touchY >= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - vissza.Height / 2 &&
                            touchY <= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + vissza.Height / 2)
                        {
                            jatekosok[aktualis].Allapot = Konstansok.VASARLASABLAK;
                        }

                    }
                    else
                    {
                        this.TablaNormalRajzolas(tablascale, 0, 0, alpha);
                        this.BabukNormalRajzolas(tablascale, 0, 0, alpha);

                        if (touch.Count > 0 &&
                            touch[0].State == TouchLocationState.Pressed &&
                            touchX >= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + felszerelesablak.Width / 2 - vissza.Width - 25 &&
                            touchX <= GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + felszerelesablak.Width / 2 - 25 &&
                            touchY >= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - vissza.Height / 2 &&
                            touchY <= GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + vissza.Height / 2)
                        {
                            jatekosok[aktualis].Allapot = Konstansok.DOBAS;
                        }

                    }
                    if (felszerelesablakscale < 1)
                    {
                        felszerelesablakscale += ablakscalemertek;
                    }
                    float felszerelesablakX = GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - felszerelesablak.Width * felszerelesablakscale / 2;
                    float felszerelesablakY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - felszerelesablak.Height * felszerelesablakscale / 2;
                    spriteBatch.Draw(felszerelesablak, new Vector2(felszerelesablakX, felszerelesablakY), null, Color.White, 0, new Vector2(0, 0), felszerelesablakscale, SpriteEffects.None, 0);
                    if (felszerelesablakscale >= 1)
                    {
                        int i = 1;
                        Texture2D ikon;
                        spriteBatch.DrawString(panelfont, jatekosok[aktualispanel].Nev, new Vector2(felszerelesablakX + 25, felszerelesablakY + felszerelesablak.Height / 2 + panelfont.MeasureString(jatekosok[aktualispanel].Nev).X / 2), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);

                        spriteBatch.DrawString(panelfont, "Lakás", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].Lakas ? igenikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

                        spriteBatch.DrawString(panelfont, "Garázs", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].Garazs ? igenikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        spriteBatch.DrawString(panelfont, "Autó", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].Auto ? igenikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        spriteBatch.DrawString(panelfont, "Lakásbiztosítás", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].LakasBiztositas ? igenikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        spriteBatch.DrawString(panelfont, "Autó biztosítás", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].AutoBiztositas ? igenikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        spriteBatch.DrawString(panelfont, "Ágy", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].Agy ? igenikon : jatekosok[aktualispanel].AgyKupon ? kuponikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        spriteBatch.DrawString(panelfont, "Hûtõ", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].Huto ? igenikon : jatekosok[aktualispanel].HutoKupon ? kuponikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        spriteBatch.DrawString(panelfont, "Konyhabútor", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].Konyhabutor ? igenikon : jatekosok[aktualispanel].KonyhabutorKupon ? kuponikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        spriteBatch.DrawString(panelfont, "Mosógép", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].Mosogep ? igenikon : jatekosok[aktualispanel].MosogepKupon ? kuponikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        spriteBatch.DrawString(panelfont, "Számítógép", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].PC ? igenikon : jatekosok[aktualispanel].PCKupon ? kuponikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        spriteBatch.DrawString(panelfont, "Szekrény", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].Szekreny ? igenikon : jatekosok[aktualispanel].SzekrenyKupon ? kuponikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        spriteBatch.DrawString(panelfont, "TV", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        ikon = jatekosok[aktualispanel].TV ? igenikon : jatekosok[aktualispanel].TVKupon ? kuponikon : nemikon;
                        spriteBatch.Draw(ikon, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + igenikon.Height), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        
                        if (jatekosok[aktualis].ReszletDB > 0)
                        {
                            spriteBatch.DrawString(panelfont, "Még " + jatekosok[aktualis].ReszletDB+" részlet", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                            String szoveg = jatekosok[aktualis].ReszletDB + " x " + jatekosok[aktualis].Reszlet + " Ft";
                            spriteBatch.DrawString(panelfont, szoveg, new Vector2(felszerelesablakX + 25 + i++ * (panelfontmagassag + 2), felszerelesablakY + 25 + panelfont.MeasureString(szoveg).X), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                            spriteBatch.DrawString(panelfont, "Következõ", new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + felszerelesablak.Height * felszerelesablakscale - 25), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                            if (jatekosok[aktualis].Torlesztesig == 0)
                            {
                                szoveg = "A kör végén";    
                            }
                            else
                            {
                                szoveg = jatekosok[aktualis].Torlesztesig + " kör múlva";
                            }
                            spriteBatch.DrawString(panelfont, szoveg, new Vector2(felszerelesablakX + 25 + i * (panelfontmagassag + 2), felszerelesablakY + 25 + panelfont.MeasureString(szoveg).X), Color.White, (float)(Math.PI / -2), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        }

                        spriteBatch.Draw(vissza, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 + felszerelesablak.Width / 2 - vissza.Width - 25, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - vissza.Height / 2), Color.White);
                    }
                }

                #endregion

                #region dobás értéke

                if (jatekosok[aktualis].Allapot == Konstansok.DOBASABLAK)
                {
                    this.TablaNormalRajzolas(tablascale, 0, 0, alpha);
                    this.BabukNormalRajzolas(tablascale, 0, 0, alpha);

                    if (dobasablakscale < 1 && gameTime.TotalGameTime < (varakozasstart + varakozas))
                    {
                        dobasablakscale += ablakscalemertek;
                        varakozasstart = gameTime.TotalGameTime;
                    }
                    float dobasablakX = GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - dobasablak.Width * dobasablakscale / 2;
                    float dobasablakY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - dobasablak.Height * dobasablakscale / 2;
                    spriteBatch.Draw(dobasablak, new Vector2(dobasablakX, dobasablakY), null, Color.White, 0, new Vector2(0, 0), dobasablakscale, SpriteEffects.None, 0);

                    if (dobasablakscale >= 1 && gameTime.TotalGameTime < (varakozasstart + varakozas))
                    {
                        spriteBatch.DrawString(gamefont, dob.ToString(), new Vector2(dobasablakX + dobasablak.Width / 2 - gamefont.MeasureString(dob.ToString()).Y / 2, dobasablakY + dobasablak.Height / 2 + gamefont.MeasureString(dob.ToString()).X / 2), Color.White, (float)Math.PI / -2, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                    }
                    if (gameTime.TotalGameTime >= (varakozasstart + varakozas))
                    {
                        dobasablakscale -= ablakscalemertek;
                    }
                    if (dobasablakscale <= 0 && gameTime.TotalGameTime >= (varakozasstart + varakozas))
                    {
                        jatekosok[aktualis].Allapot = Konstansok.VALASZTAS;
                        villogasstart = gameTime.TotalGameTime;
                    }
                }

                #endregion

                #region kimarad

                if (jatekosok[aktualis].Allapot == Konstansok.KIMARADABLAK)
                {
                    this.TablaNormalRajzolas(tablascale, 0, 0, alpha);
                    this.BabukNormalRajzolas(tablascale, 0, 0, alpha);

                    if (kimaradablakscale < 1 && gameTime.TotalGameTime < (varakozasstart + varakozas))
                    {
                        kimaradablakscale += ablakscalemertek;
                        varakozasstart = gameTime.TotalGameTime;
                    }
                    float kimaradablakX = GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - kimaradablak.Width * kimaradablakscale / 2;
                    float kimaradablakY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - kimaradablak.Height * kimaradablakscale / 2;
                    spriteBatch.Draw(kimaradablak, new Vector2(kimaradablakX, kimaradablakY), null, Color.White, 0, new Vector2(0, 0), kimaradablakscale, SpriteEffects.None, 0);

                    if (kimaradablakscale >= 1 && gameTime.TotalGameTime < (varakozasstart + varakozas))
                    {
                        String szoveg = jatekosok[aktualis].Nev + " kimarad!";
                        spriteBatch.DrawString(panelfont, szoveg, new Vector2(kimaradablakX + kimaradablak.Width / 2 - panelfont.MeasureString(szoveg).Y / 2, kimaradablakY + kimaradablak.Height / 2 + panelfont.MeasureString(szoveg).X / 2), Color.White, (float)Math.PI / -2, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                    }
                    if (gameTime.TotalGameTime >= (varakozasstart + varakozas))
                    {
                        kimaradablakscale -= ablakscalemertek;
                    }
                    if (kimaradablakscale <= 0 && gameTime.TotalGameTime >= (varakozasstart + varakozas))
                    {
                        jatekosok[aktualis].Allapot = 0;
                    }
                }

                #endregion

                #region kör vége
                if (jatekosok[aktualis].Allapot == Konstansok.KORVEGE)
                {
                    if (jatekosok[aktualis].NyertE())
                    {
                        halvanyul = Konstansok.VEGE;
                        nyertes = jatekosok[aktivjatekos].Nev;
                    }
                    else
                    {
                        if (jatekosok[aktualis].Hanyszor < 1)
                        {
                            if (dob == 0)
                            {
                                this.TablaNormalRajzolas(tablascale, 0, 0, alpha);
                                this.BabukNormalRajzolas(tablascale, 0, 0, alpha);
                            }
                            else
                            {
                                if (jatekosok[aktualis].CsakHatos && sorszam == 0)
                                {
                                    this.TablaNormalRajzolas(tablascale, 0, 0, alpha);
                                    this.BabukNormalRajzolas(tablascale, 0, 0, alpha);
                                }
                                else
                                {
                                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);
                                }
                            }
                            if (!jatekosok[aktualis].Aktiv)
                            {
                                jatekosok[aktualis].Allapot = Konstansok.KIESETT;
                                varakozasstart = gameTime.TotalGameTime;
                            }
                            else
                            {
                                jatekosok[aktualis].Allapot = Konstansok.KOVETKEZO;
                                varakozasstart = gameTime.TotalGameTime;
                            }
                        }
                        else
                        {
                            if (tablascale > 1)
                            {
                                tablascale -= tablascalemertek;

                                tablaXMozgat += ((jatekosok[aktualis].Pozicio.XPos * tablazoom - (GraphicsDevice.Viewport.TitleSafeArea.Width * (tablazoom - 1) / 2)) - (GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - MezoNormal[jatekosok[aktualis].Pozicio.Sorszam].Width * tablazoom / 2)) / ((tablazoom - 1) / tablascalemertek);
                                tablaYMozgat += ((jatekosok[aktualis].Pozicio.YPos * tablazoom - (GraphicsDevice.Viewport.TitleSafeArea.Height * (tablazoom - 1) / 2)) - (GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - MezoNormal[jatekosok[aktualis].Pozicio.Sorszam].Height * tablazoom / 2)) / ((tablazoom - 1) / tablascalemertek);
                            }
                            this.TablaNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);
                            this.BabukNormalRajzolas(tablascale, tablaXMozgat, tablaYMozgat, alpha);
                            if (tablascale <= 1)
                            {
                                jatekosok[aktualis].Allapot = Konstansok.DOBAS;
                                aktualispanel = aktualis;
                            }
                        }
                    }
                    if (halvanyul > 0)
                    {
                        if (alpha > 0)
                        {
                            alpha -= alphamertek;
                        }
                        else
                        {
                            this.Allapot = halvanyul;
                            halvanyul = 0;
                        }
                    }
                }

                #endregion

                #region kiesett

                if (jatekosok[aktualis].Allapot == Konstansok.KIESETT)
                {
                    this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                    this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);

                    if (kiesettablakscale < 1 && gameTime.TotalGameTime < (varakozasstart + varakozas))
                    {
                        kiesettablakscale += ablakscalemertek;
                        varakozasstart = gameTime.TotalGameTime;
                    }
                    float kiesettablakX = GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - kiesettablak.Width * kiesettablakscale / 2;
                    float kiesettablakY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - kiesettablak.Height * kiesettablakscale / 2;
                    spriteBatch.Draw(kiesettablak, new Vector2(kiesettablakX, kiesettablakY), null, Color.White, 0, new Vector2(0, 0), kiesettablakscale, SpriteEffects.None, 0);
                    if (kiesettablakscale >= 1 && gameTime.TotalGameTime < (varakozasstart + varakozas))
                    {
                        spriteBatch.DrawString(panelfont, jatekosok[aktualis].Nev + " pénze elfogyott!", new Vector2(kiesettablakX + kiesettablak.Width / 2 - panelfont.MeasureString(jatekosok[aktualis].Nev + " pénze elfogyott!").Y, kiesettablakY + kiesettablak.Height / 2 + panelfont.MeasureString(jatekosok[aktualis].Nev + " pénze elfogyott!").X / 2), Color.White, (float)Math.PI / -2, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                        spriteBatch.DrawString(panelfont, "A játékának vége!", new Vector2(kiesettablakX + kiesettablak.Width / 2, kiesettablakY + kiesettablak.Height / 2 + panelfont.MeasureString("A játékának vége!").X / 2), Color.White, (float)Math.PI / -2, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                    }
                    if (kiesettablakscale > 0 && gameTime.TotalGameTime >= (varakozasstart + varakozas))
                    {
                        kiesettablakscale -= ablakscalemertek;
                    }

                    if (kiesettablakscale <= 0 && gameTime.TotalGameTime >= (varakozasstart + varakozas))
                    {
                        aktivjatekos--;
                        jatekosok[aktualis].Allapot = Konstansok.KOVETKEZO;
                        varakozasstart = gameTime.TotalGameTime;
                    }
                }

                #endregion

                #region következõ játékos

                if (jatekosok[aktualis].Allapot == Konstansok.KOVETKEZO)
                {
                    if (dob == 0)
                    {
                        this.TablaNormalRajzolas(tablascale, 0, 0, alpha);
                        this.BabukNormalRajzolas(tablascale, 0, 0, alpha);
                    }
                    else
                    {
                        this.TablaNagyRajzolas(tablazoom, 0, 0, alpha);
                        this.BabukNagyRajzolas(tablazoom, 0, 0, alpha);
                    }

                    if (aktivjatekos > 1)
                    {
                        if (kovetkezoablakscale < 1 && gameTime.TotalGameTime < (varakozasstart + varakozas))
                        {
                            kovetkezoablakscale += ablakscalemertek;
                            varakozasstart = gameTime.TotalGameTime;
                        }
                        float kovetkezoablakX = GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - kovetkezoablak.Width * kovetkezoablakscale / 2;
                        float kovetkezoablakY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - kovetkezoablak.Height * kovetkezoablakscale / 2;
                        spriteBatch.Draw(kovetkezoablak, new Vector2(kovetkezoablakX, kovetkezoablakY), null, Color.White, 0, new Vector2(0, 0), kovetkezoablakscale, SpriteEffects.None, 0);
                        if (kovetkezoablakscale >= 1 && gameTime.TotalGameTime < (varakozasstart + varakozas))
                        {
                            String szoveg = "";
                            int i = 1;
                            while (true)
                            {
                                kov = (aktualis + i) % jatekosok.Count;
                                if (kov == 0)
                                {
                                    kov = jatekosok.Count;
                                }
                                if (jatekosok[kov].Aktiv)
                                {
                                    szoveg = jatekosok[kov].Nev + " következik!";
                                    break;
                                }
                                else
                                {
                                    i++;
                                }
                            }
                            spriteBatch.DrawString(panelfont, szoveg, new Vector2(kovetkezoablakX + kovetkezoablak.Width / 2 - panelfont.MeasureString(szoveg).Y / 2, kovetkezoablakY + kovetkezoablak.Height / 2 + panelfont.MeasureString(szoveg).X / 2), Color.White, (float)Math.PI / -2, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                        }
                        if (kovetkezoablakscale > 0 && gameTime.TotalGameTime >= (varakozasstart + varakozas))
                        {
                            kovetkezoablakscale -= ablakscalemertek;
                        }
                        if (kovetkezoablakscale <= 0 && gameTime.TotalGameTime >= (varakozasstart + varakozas))
                        {
                            if (jatekosok[aktualis].Aktiv)
                            {
                                if (jatekosok[aktualis].ReszletDB > 0)
                                {
                                    if (!jatekosok[aktualis].Torleszt())
                                    {
                                        aktivjatekos--;
                                        jatekosok[aktivjatekos].Allapot = Konstansok.KIESETT;
                                        varakozasstart = gameTime.TotalGameTime;
                                    }
                                }
                                jatekosok[aktualis].Allapot = Konstansok.DOBAS;
                                jatekosok[aktualis].Hanyszor++;
                            }
                            else
                            {
                                jatekosok[aktualis].Pozicio.Jatekosok.Remove(jatekosok[aktualis]);
                                jatekosok[aktualis].Pozicio = null;
                            }
                            if (kov < aktualis)
                            {
                                jatekkor++;
                            }
                            aktualis = kov;

                            if (jatekkor % tabla.BetetKamatKorDB == 0)
                            {
                                jatekosok[aktualis].Penz += jatekosok[aktualis].Bankbetet * tabla.BetetKamat / 100;
                            }
                            aktualispanel = aktualis;
                        }
                    }
                    else
                    {
                        foreach (var j in jatekosok)
                        {
                            if (j.Value.Aktiv)
                            {
                                nyertes = j.Value.Nev;
                            }
                        }
                        halvanyul = Konstansok.VEGE;
                    }

                    if (halvanyul > 0)
                    {
                        if (alpha > 0)
                        {
                            alpha -= alphamertek;
                        }
                        else
                        {
                            this.Allapot = halvanyul;
                            halvanyul = 0;
                        }
                    }
                }

                #endregion

            }
            #endregion

            #region játék vége

            if (this.Allapot == Konstansok.VEGE)
            {
                if (alpha < 1 && halvanyul == 0)
                {
                    alpha += alphamertek;
                }

                float beallitasokX = GraphicsDevice.Viewport.TitleSafeArea.Width - beallitasok.Width - 25;
                float beallitasoky = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - beallitasok.Height / 2;
                float ujjatekX = beallitasokX - 25 - ujjatek.Width;
                float ujjatekY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - ujjatek.Height / 2;
                float nyertesablakX = ujjatekX - 25 - nyertesablak.Width;
                float nyertesablakY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - ujjatek.Height / 2;


                spriteBatch.Draw(menuhatter, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X - (menuhatter.Width - GraphicsDevice.Viewport.TitleSafeArea.Width) / 2, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White * alpha);
                spriteBatch.Draw(nyertesablak, new Vector2(nyertesablakX, nyertesablakY), Color.White * alpha);
                spriteBatch.Draw(ujjatek, new Vector2(ujjatekX, ujjatekY), Color.White * alpha);
                spriteBatch.Draw(beallitasok, new Vector2(beallitasokX, beallitasoky), Color.White * alpha);
                String szoveg = nyertes + " gyõzõtt!";
                spriteBatch.DrawString(panelfont, szoveg, new Vector2(nyertesablakX + nyertesablak.Width / 2 - panelfont.MeasureString(szoveg).Y / 2, nyertesablakY + nyertesablak.Height / 2 + panelfont.MeasureString(szoveg).X / 2), Color.Black * alpha, (float)(Math.PI / -2), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);

                if (alpha >= 1)
                {
                    nevek.Clear();
                    jatekosok.Clear();
                    szinek.Clear();
                    valasztottszinek.Clear();
                    for (int i = 1; i <= tabla.Huzottak.Count; i++)
                    {
                        tabla.Huzottak[i] = false;
                    }
                    for (int i = 1; i <= tabla.Mezok.Count; i++)
                    {
                        tabla.Mezok[i].Jatekosok.Clear();
                    }

                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Pressed &&
                        touchX >= ujjatekX &&
                        touchX <= ujjatekX + ujjatek.Width &&
                        touchY >= ujjatekY &&
                        touchY <= ujjatekY + ujjatek.Height)
                    {
                        halvanyul = Konstansok.JATEKOSOK;
                        valasztottszinek.Add(1, true);
                        valasztottszinek.Add(2, true);
                        valasztottszinek.Add(3, false);
                        valasztottszinek.Add(4, false);
                        valasztottszinek.Add(5, false);
                        valasztottszinek.Add(6, false);
                        valasztottszinek.Add(7, false);

                        nevek.Add(1, "");
                        nevek.Add(2, "");

                        aktivjatekos = 2;

                        szinek.Add(1, 1);
                        szinek.Add(2, 2);
                    }

                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Pressed &&
                        touchX >= beallitasokX &&
                        touchX <= beallitasokX + beallitasok.Width &&
                        touchY >= beallitasoky &&
                        touchY <= beallitasoky + beallitasok.Height)
                    {
                        halvanyul = Konstansok.BEALLITASOK;
                    }
                }
                if (halvanyul > 0)
                {
                    if (alpha > 0)
                    {
                        alpha -= alphamertek;
                    }
                    else
                    {
                        this.Allapot = halvanyul;
                        halvanyul = 0;
                    }
                }
            }

            #endregion

            #region játékos beállítások

            if (this.Allapot == Konstansok.JATEKOSOK)
            {
                if (alpha < 1 && halvanyul == 0)
                {
                    alpha += alphamertek;
                }

                this.JatekosBeallitasokRajzolas(alpha, csuszas);

                if (alpha >= 1)
                {
                    if (!hozzaad)
                    {
                        if (aktivjatekos < 6)
                        {
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= pluszX &&
                                touchX <= pluszX + plusz.Width &&
                                touchY >= pluszY &&
                                touchY <= pluszY + plusz.Height)
                            {
                                hozzaad = true;
                            }
                        }


                        for (int i = 1; i <= aktivjatekos; i++)
                        {
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= szovegmezoX + (i - 1) * (jatekospanel.Width + 20) &&
                                touchX <= szovegmezoX + (i - 1) * (jatekospanel.Width + 20) + szovegmezo.Width &&
                                touchY >= szovegmezoY &&
                                touchY <= szovegmezoY + szovegmezo.Height)
                            {
                                nagybetu = false;
                                this.Allapot = Konstansok.JATEKOSNEVIRAS;
                                jatekossorszam = i;
                                jatekosnev = nevek[i];
                                billhatterX = GraphicsDevice.Viewport.TitleSafeArea.X;
                                billhatterY = GraphicsDevice.Viewport.TitleSafeArea.Height;
                                szovegmezohatterX = GraphicsDevice.Viewport.TitleSafeArea.X;
                                szovegmezohatterY = GraphicsDevice.Viewport.TitleSafeArea.X - szovegmezohatter.Height;
                            }
                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= jatekospanelX + (i - 1) * (jatekospanel.Width + 20) + jatekospanel.Width - BabuNormal[szinek[i]].Width * 2 &&
                                touchX <= jatekospanelX + (i - 1) * (jatekospanel.Width + 20) + jatekospanel.Width &&
                                touchY >= jatekospanelY &&
                                touchY <= jatekospanelY + BabuNormal[szinek[i]].Height * 2)
                            {
                                jatekossorszam = i;
                                this.Allapot = Konstansok.JATEKOSSZIN;
                                valasztottszinek[szinek[i]] = false;
                            }

                            if (i > 2)
                            {
                                if (touch.Count > 0 &&
                                    touch[0].State == TouchLocationState.Pressed &&
                                    touchX >= minuszX + (i - 1) * (minusz.Width + 20) &&
                                    touchX <= minuszX + (i - 1) * (minusz.Width + 20) + minusz.Width &&
                                    touchY >= minuszY &&
                                    touchY <= minuszY + minusz.Height)
                                {
                                    valasztottszinek[i] = false;
                                    for (int j = i; j < nevek.Count; j++)
                                    {
                                        nevek[j] = nevek[j + 1];
                                        szinek[j] = szinek[j + 1];
                                    }
                                    nevek.Remove(aktivjatekos);
                                    szinek.Remove(aktivjatekos);
                                    aktivjatekos--;
                                }
                            }
                        }

                        if (touch.Count > 0 &&
                            touch[0].State == TouchLocationState.Pressed &&
                            touchX >= startX &&
                            touchX <= startX + start.Width &&
                            touchY >= startY &&
                            touchY <= startY + start.Height)
                        {
                            bool mindenok = true;
                            foreach (var v in nevek)
                            {
                                if (v.Value == "")
                                {
                                    mindenok = false;
                                }
                            }
                            if (mindenok)
                            {
                                for (int i = 1; i <= aktivjatekos; i++)
                                {
                                    jatekosok.Add(i, new Jatekos(nevek[i], szinek[i], tabla));
                                }
                                halvanyul = Konstansok.JATEK;
                            }
                        }

                        if (touch.Count > 0 &&
                            touch[0].State == TouchLocationState.Pressed &&
                            touchX >= visszaX &&
                            touchX <= visszaX + vissza.Width &&
                            touchY >= visszaY &&
                            touchY <= visszaY + vissza.Height)
                        {
                            halvanyul = Konstansok.START;
                        }
                    }
                    else
                    {
                        if (hozzaad)
                        {
                            if (aktivjatekos == 5)
                            {
                                if (csuszas < (plusz.Height))
                                {
                                    csuszas += (plusz.Height) / 20;
                                }
                            }
                            else
                            {
                                if (csuszas < (plusz.Width + 20))
                                {
                                    csuszas += (plusz.Width + 20) / 20;
                                }
                            }
                            if ((csuszas >= (plusz.Width + 20) && aktivjatekos < 5) || (csuszas >= (plusz.Height) && aktivjatekos == 5))
                            {
                                aktivjatekos++;
                                nevek.Add(aktivjatekos, "");
                                int x = 0;
                                foreach (var v in valasztottszinek)
                                {
                                    if (!v.Value)
                                    {
                                        x = v.Key;
                                        break;
                                    }
                                }
                                valasztottszinek[x] = true;
                                szinek.Add(aktivjatekos, x);
                                hozzaad = false;
                                csuszas = 0;
                            }
                        }
                    }
                }
                if (halvanyul > 0)
                {
                    if (alpha > 0)
                    {
                        alpha -= alphamertek;
                    }
                    else
                    {
                        if (halvanyul == Konstansok.START)
                        {
                            nevek.Clear();
                            szinek.Clear();
                            valasztottszinek.Clear();
                        }
                        this.Allapot = halvanyul;
                        halvanyul = 0;
                    }
                }
            }

            #endregion

            #region játék beállítások

            if (this.Allapot == Konstansok.BEALLITASOK)
            {
                if (alpha < 1 && halvanyul == 0)
                {
                    alpha += alphamertek;
                }
                this.BeallitasokRajzolas(alpha);
                if (alpha >= 1)
                {
                    for (int i = 1; i <= 21; i++)
                    {
                        X = beallitaspanelX + (i - 1) * (beallitaspanel.Width + 5) - scrollmozgas;

                        if (touch.Count > 0 &&
                            touch[0].State == TouchLocationState.Pressed &&
                            touchX >= X &&
                            touchX <= X + beallitasokszovegmezo.Width &&
                            touchY >= beallitaspanelY &&
                            touchY <= beallitaspanelY + beallitasokszovegmezo.Height)
                        {
                            this.Allapot = Konstansok.SZAMIRAS;
                            mezosorszam = i;
                            mezoertek = ertekek[i].ToString();
                            mezocimke = cimkek[i];
                            maxszamjegy = 7;
                            if (i == 21 || i == 18)
                            {
                                maxszamjegy = 1;
                            }
                            if (i >= 15 && i <= 20 && i != 18)
                            {
                                maxszamjegy = 2;
                            }
                            szamlaphatterX = GraphicsDevice.Viewport.TitleSafeArea.Width;
                            szamlaphatterY = GraphicsDevice.Viewport.TitleSafeArea.Y;
                            szammezohatterX = GraphicsDevice.Viewport.TitleSafeArea.X - szammezohatter.Width;
                            szammezohatterY = GraphicsDevice.Viewport.TitleSafeArea.Y;
                        }
                    }

                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Pressed &&
                        touchX >= alapertekX &&
                        touchX <= alapertekX + alapertek.Width &&
                        touchY >= alapertekY &&
                        touchY <= alapertekY + alapertek.Height)
                    {
                        ertekek[1] = Konstansok.Kezdo;
                        ertekek[2] = Konstansok.Fizetes;
                        ertekek[3] = Konstansok.LakasAr;
                        ertekek[4] = Konstansok.GarazsAr;
                        ertekek[5] = Konstansok.AutoAr;
                        ertekek[6] = Konstansok.AgyAr;
                        ertekek[7] = Konstansok.HutoAr;
                        ertekek[8] = Konstansok.MosogepAr;
                        ertekek[9] = Konstansok.KonyhabutorAr;
                        ertekek[10] = Konstansok.PCAr;
                        ertekek[11] = Konstansok.SzekrenyAr;
                        ertekek[12] = Konstansok.TVAr;
                        ertekek[13] = Konstansok.LakasBiztositasAr;
                        ertekek[14] = Konstansok.AutoBiztositasAr;
                        ertekek[15] = Konstansok.IngatlanirodaJutalek;
                        ertekek[16] = Konstansok.ReszletKamat;
                        ertekek[17] = Konstansok.ReszletDB;
                        ertekek[18] = Konstansok.ReszletKor;
                        ertekek[19] = Konstansok.Kezdoreszlet;
                        ertekek[20] = Konstansok.BetetKamat;
                        ertekek[21] = Konstansok.BetetKamatKorDB;
                    }

                    if (touch.Count > 0 &&
                       touch[0].State == TouchLocationState.Pressed &&
                       touchX >= megseX &&
                       touchX <= megseX + megse.Width &&
                       touchY >= megseY &&
                       touchY <= megseY + megse.Height)
                    {
                        ertekek[1] = tabla.Kezdo;
                        ertekek[2] = tabla.Fizetes;
                        ertekek[3] = tabla.LakasAr;
                        ertekek[4] = tabla.GarazsAr;
                        ertekek[5] = tabla.AutoAr;
                        ertekek[6] = tabla.AgyAr;
                        ertekek[7] = tabla.HutoAr;
                        ertekek[8] = tabla.MosogepAr;
                        ertekek[9] = tabla.KonyhabutorAr;
                        ertekek[10] = tabla.PCAr;
                        ertekek[11] = tabla.SzekrenyAr;
                        ertekek[12] = tabla.TVAr;
                        ertekek[13] = tabla.LakasBiztositasAr;
                        ertekek[14] = tabla.AutoBiztositasAr;
                        ertekek[15] = tabla.IngatlanirodaJutalek;
                        ertekek[16] = tabla.ReszletKamat;
                        ertekek[17] = tabla.ReszletDB;
                        ertekek[18] = tabla.ReszletKor;
                        ertekek[19] = tabla.Kezdoreszlet;
                        ertekek[20] = tabla.BetetKamat;
                        ertekek[21] = tabla.BetetKamatKorDB;

                        halvanyul = Konstansok.START;
                    }

                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Pressed &&
                        touchX >= mentesX &&
                        touchX <= mentesX + mentes.Width &&
                        touchY >= mentesY &&
                        touchY <= mentesY + mentes.Height)
                    {
                        tabla.Kezdo = ertekek[1];
                        tabla.Fizetes = ertekek[2];
                        tabla.LakasAr = ertekek[3];
                        tabla.GarazsAr = ertekek[4];
                        tabla.AutoAr = ertekek[5];
                        tabla.AgyAr = ertekek[6];
                        tabla.HutoAr = ertekek[7];
                        tabla.KonyhabutorAr = ertekek[8];
                        tabla.MosogepAr = ertekek[9];
                        tabla.PCAr = ertekek[10];
                        tabla.SzekrenyAr = ertekek[11];
                        tabla.TVAr = ertekek[12];
                        tabla.LakasBiztositasAr = ertekek[13];
                        tabla.AutoBiztositasAr = ertekek[14];
                        tabla.IngatlanirodaJutalek = ertekek[15];
                        tabla.ReszletKamat = ertekek[16];
                        tabla.ReszletDB = ertekek[17];
                        tabla.ReszletKor = ertekek[18];
                        tabla.Kezdoreszlet = ertekek[19];
                        tabla.BetetKamat = ertekek[20];
                        tabla.BetetKamatKorDB = ertekek[21];

                        IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
                        FileStream stream = store.OpenFile("ertekek.txt", FileMode.Create, FileAccess.Write);
                        TextWriter tw = new StreamWriter(stream);

                        for (int i = 1; i <= 21; i++)
                        {
                            tw.WriteLine(ertekek[i]);
                        }
                        tw.Close();

                        halvanyul = Konstansok.START;
                    }

                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Pressed &&
                        touchX >= scrollX &&
                        touchX <= scrollX + scroll.Width &&
                        touchY >= scrollY &&
                        touchY <= scrollY + scroll.Height)
                    {
                        scrolltavolsag = touchX - scrollX;
                    }
                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Moved &&
                        touchX >= scrollX &&
                        touchX <= scrollX + scroll.Width &&
                        touchY >= scrollY &&
                        touchY <= scrollY + scroll.Height &&
                        scrollX + scroll.Width <= GraphicsDevice.Viewport.TitleSafeArea.Width &&
                        scrollX >= GraphicsDevice.Viewport.TitleSafeArea.X)
                    {
                        scrollmozgas = touchX - scrolltavolsag;
                    }
                }
                if (halvanyul > 0)
                {
                    if (alpha > 0)
                    {
                        alpha -= alphamertek;
                    }
                    else
                    {
                        this.Allapot = halvanyul;
                        halvanyul = 0;
                    }
                }
            }

            #endregion

            #region játékosnév írás

            if (this.Allapot == Konstansok.JATEKOSNEVIRAS)
            {
                this.JatekosBeallitasokRajzolas(alpha, 0);
                if (billhatterY > (GraphicsDevice.Viewport.TitleSafeArea.Height - billhatter.Height))
                {
                    billhatterY -= billhatter.Height / (billmertek - 1);
                    szovegmezohatterY += szovegmezohatter.Height / (billmertek - 1);
                }
                spriteBatch.Draw(szovegmezohatter, new Vector2(szovegmezohatterX, szovegmezohatterY), Color.White);
                spriteBatch.Draw(billhatter, new Vector2(billhatterX, billhatterY), Color.White);
                if (billhatterY <= (GraphicsDevice.Viewport.TitleSafeArea.Height - billhatter.Height))
                {
                    spriteBatch.Draw(billszovegmezo, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - billszovegmezo.Width / 2, szovegmezohatter.Height / 2 - billszovegmezo.Height / 2), Color.White);
                    String kar = "";
                    for (int i = 1; i <= 4; i++)
                    {
                        for (int j = 1; j <= 13; j++)
                        {
                            if (i == 2 && j == 13)
                            {
                                spriteBatch.Draw(backspace, new Vector2(billhatterX + 10 + (j - 1) * karakter.Width, billhatterY + 25 + (i - 1) * karakter.Height), Color.White);
                                continue;
                            }
                            if (i == 3 && j == 13)
                            {
                                spriteBatch.Draw(enter, new Vector2(billhatterX + 10 + (j - 1) * karakter.Width, billhatterY + 25 + (i - 1) * karakter.Height), Color.White);
                                continue;
                            }
                            if (i == 4 && j == 13)
                            {
                                spriteBatch.Draw(shift, new Vector2(billhatterX + 10 + (j - 1) * karakter.Width, billhatterY + 25 + (i - 1) * karakter.Height), Color.White);
                                break;
                            }
                            if (nagybetu)
                            {
                                kar = karakterek[i + 4][j - 1];
                            }
                            else
                            {
                                kar = karakterek[i][j - 1];
                            }
                            if (kar != "")
                            {
                                spriteBatch.Draw(karakter, new Vector2(billhatterX + 10 + (j - 1) * karakter.Width, billhatterY + 25 + (i - 1) * karakter.Height), Color.White);
                                spriteBatch.DrawString(billfont, kar, new Vector2(billhatterX + 10 + (j - 1) * karakter.Width + karakter.Width / 2 - billfont.MeasureString(kar).X / 2, billhatterY + 25 + (i - 1) * karakter.Height + karakter.Height - billfont.MeasureString("0").Y), Color.Black);
                            }
                        }
                    }
                    spriteBatch.Draw(space, new Vector2(billhatterX + 10 + 3 * karakter.Width, billhatterY + 25 + 4 * karakter.Height), Color.White);

                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Pressed && 
                        touchX >= billhatterX + 10 &&
                        touchX <= billhatterX + 10 + 13 * karakter.Width &&
                        touchY >= billhatterY + 25 &&
                        touchY <= billhatterY + 25 + 5 * karakter.Height)
                    {
                        int x = (int)((touchX - billhatterX - 10) / karakter.Width);
                        int y = (int)((touchY - billhatterY - 25) / karakter.Height + 1);

                        if (x == 12 && y > 1)
                        {
                            if (y == 3)
                            {
                                nevek[jatekossorszam] = jatekosnev;
                                this.Allapot = Konstansok.JATEKOSNEVIRASBEZAR;
                            }
                            if (y == 4)
                            {
                                nagybetu = !nagybetu;
                            }
                            if (y == 2)
                            {
                                String temp = "";
                                for (int i = 0; i < jatekosnev.Length - 1; i++)
                                {
                                    temp += jatekosnev[i];
                                }
                                jatekosnev = temp;
                            }
                        }
                        else
                        {
                            if (jatekosnev.Length < 8)
                            {
                                if (y < 5)
                                {
                                    if (nagybetu)
                                    {
                                        jatekosnev += karakterek[y + 4][x];
                                        nagybetu = false;
                                    }
                                    else
                                    {
                                        jatekosnev += karakterek[y][x];
                                    }
                                }
                                else
                                {
                                    if (x >= 3 && x <= 8)
                                    {
                                        jatekosnev += " ";
                                    }
                                }
                            }
                        }
                    }
                    spriteBatch.DrawString(ujjatekfont, jatekosnev, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - billszovegmezo.Width / 2 + 10, szovegmezohatter.Height / 2 - billszovegmezo.Height / 2), Color.Black);
                }
            }

            #endregion

            #region játékosnév írás bezárása

            if (this.Allapot == Konstansok.JATEKOSNEVIRASBEZAR)
            {
                this.JatekosBeallitasokRajzolas(alpha, 0);
                if (billhatterY < GraphicsDevice.Viewport.TitleSafeArea.Height )
                {
                    billhatterY += billhatter.Height / 30;
                    szovegmezohatterY -= szovegmezohatter.Height / 30;
                }
                spriteBatch.Draw(szovegmezohatter, new Vector2(szovegmezohatterX, szovegmezohatterY), Color.White);
                spriteBatch.Draw(billhatter, new Vector2(billhatterX, billhatterY), Color.White);
                if (billhatterY >= GraphicsDevice.Viewport.TitleSafeArea.Height)
                {
                    this.Allapot = Konstansok.JATEKOSOK;
                }
            }

            #endregion

            #region szín választás

            if (this.Allapot == Konstansok.JATEKOSSZIN)
            {
                this.JatekosBeallitasokRajzolas(alpha, 0);
                if (szinablakscale < 1)
                {
                    szinablakscale += ablakscalemertek;
                }
                float szinvalasztoablakX = GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - szinvalasztoablak.Width * szinablakscale / 2;
                float szinvalasztoablakY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - szinvalasztoablak.Height * szinablakscale / 2;

                spriteBatch.Draw(szinvalasztoablak, new Vector2(szinvalasztoablakX, szinvalasztoablakY ), null, Color.White, 0, new Vector2(0, 0), szinablakscale, SpriteEffects.None, 0);
               
                if (szinablakscale >= 1)
                {
                    int i = 0;
                    foreach (var v in valasztottszinek)
                    {
                        if (!v.Value)
                        {
                            spriteBatch.Draw(BabuNormal[v.Key], new Vector2(szinvalasztoablakX + 25 + (i) * (25 + BabuNormal[v.Key].Width * 2), szinvalasztoablakY + 25), null, Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);

                            if (touch.Count > 0 &&
                                touch[0].State == TouchLocationState.Pressed &&
                                touchX >= szinvalasztoablakX + 25 + i * (25 + BabuNormal[v.Key].Width * 2) &&
                                touchX <= szinvalasztoablakX + 25 + i * (25 + BabuNormal[v.Key].Width * 2) + BabuNormal[v.Key].Width * 2 &&
                                touchY >= szinvalasztoablakY + 25 &&
                                touchY <= szinvalasztoablakY + 25 + BabuNormal[v.Key].Height * 2)
                            {
                                szinek[jatekossorszam] = v.Key;
                                valasztottszinek[v.Key] = true;
                                this.Allapot = Konstansok.JATEKOSSZINBEZAR;
                                break;
                            }
                            i++;
                        } 
                    }
                }
            }

            #endregion

            #region szín választás bezárás

            if (this.Allapot == Konstansok.JATEKOSSZINBEZAR)
            {
                this.JatekosBeallitasokRajzolas(alpha, 0);
                if (szinablakscale > 0)
                {
                    szinablakscale -= ablakscalemertek;
                }
                float szinvalasztoablakX = GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - szinvalasztoablak.Width * szinablakscale / 2;
                float szinvalasztoablakY = GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - szinvalasztoablak.Height * szinablakscale / 2;

                spriteBatch.Draw(szinvalasztoablak, new Vector2(szinvalasztoablakX, szinvalasztoablakY), null, Color.White, 0, new Vector2(0, 0), szinablakscale, SpriteEffects.None, 0);
                if (szinablakscale <= 0)
                {
                    this.Allapot = Konstansok.JATEKOSOK;
                }
            }

            #endregion

            #region mentés

            if (this.Allapot == Konstansok.MENTES)
            {
                this.Mentes();
            }

            #endregion

            #region betöltés

            if (this.Allapot == Konstansok.BETOLT)
            {
                this.Betoltes();
            }

            #endregion

            #region szám írás

            if (this.Allapot == Konstansok.SZAMIRAS)
            {
                this.BeallitasokRajzolas(alpha);
                if (szamlaphatterX > (GraphicsDevice.Viewport.TitleSafeArea.Width - szamlaphatter.Width))
                {
                    szamlaphatterX -= szamlaphatter.Width / 30;
                    szammezohatterX += szammezohatter.Width / 30;
                }
                spriteBatch.Draw(szammezohatter, new Vector2(szammezohatterX, szammezohatterY), Color.White);
                spriteBatch.Draw(szamlaphatter, new Vector2(szamlaphatterX, szamlaphatterY), Color.White);
                if (szamlaphatterX <= (GraphicsDevice.Viewport.TitleSafeArea.Width - szamlaphatter.Width))
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        for (int j = 1; j <= 4; j++)
                        {
                            if (j == 4 && i != 2)
                            {
                                if (i == 1)
                                {
                                    spriteBatch.Draw(enternagy, new Vector2(szamlaphatterX + 25 + (j - 1) * szam.Width, szamlaphatterY + 15 + (i - 1) * szam.Height), Color.White);
                                }
                                if (i == 3)
                                {
                                    spriteBatch.Draw(backspacenagy, new Vector2(szamlaphatterX + 25 + (j - 1) * szam.Width, szamlaphatterY + 15 + (i - 1) * szam.Height), Color.White);
                                }
                            }
                            else
                            {
                                spriteBatch.Draw(szam, new Vector2(szamlaphatterX + 25 + (j - 1) * szam.Width, szamlaphatterY + 15 + (i - 1) * szam.Height), Color.White);

                                spriteBatch.DrawString(szamfont, szamok[i][j-1], new Vector2(szamlaphatterX + 25 + j * szam.Width - szamfont.MeasureString(szamok[i][j-1]).Y, szamlaphatterY + 15 + (i - 1) * szam.Height + szam.Height / 2 + szamfont.MeasureString(szamok[i][j-1]).X / 2), Color.Black, (float)(Math.PI / -2), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                            }
                            
                        }
                    }
                    spriteBatch.DrawString(ujjatekfont, mezocimke, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 25, GraphicsDevice.Viewport.TitleSafeArea.Height - 25), Color.Black, (float)(Math.PI / -2), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(szovegmezo, new Vector2(szammezohatter.Width - szovegmezo.Width - 10, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - szovegmezo.Height / 2), Color.White);

                    if (touch.Count > 0 &&
                        touch[0].State == TouchLocationState.Pressed &&
                        touchX >= szamlaphatterX + 25 &&
                        touchX <= szamlaphatterX + 25 + 4 * szam.Width &&
                        touchY >= szamlaphatterY + 15 &&
                        touchY <= szamlaphatterY + 15 + 3 * szam.Height)
                    {
                        int x = (int)((touchX - szamlaphatterX - 25) / szam.Width);
                        int y = (int)((touchY - szamlaphatterY - 15) / szam.Height + 1);

                        if (x == 3 && y !=2)
                        {
                            if (y == 1)
                            {
                                ertekek[mezosorszam] = Convert.ToInt32(mezoertek);
                                this.Allapot = Konstansok.SZAMIRASBEZARAS;
                            }
                            if (y == 3)
                            {
                                String temp = "";
                                for (int i = 0; i < mezoertek.Length - 1; i++)
                                {
                                    temp += mezoertek[i];
                                }
                                mezoertek = temp;
                            }
                        }
                        else
                        {
                            if (mezoertek.Length < maxszamjegy)
                            {
                                mezoertek += szamok[y][x];
                            }
                        }
                    }
                    spriteBatch.DrawString(ujjatekfont, mezoertek, new Vector2(szammezohatter.Width - 10 - ujjatekfont.MeasureString(mezoertek).Y, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 + ujjatekfont.MeasureString(mezoertek).X / 2), Color.Black, (float)(Math.PI / -2), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                }
            }

            #endregion

            #region szám írás bezárása

            if (this.Allapot == Konstansok.SZAMIRASBEZARAS)
            {
                this.BeallitasokRajzolas(alpha);
                if (szamlaphatterX < GraphicsDevice.Viewport.TitleSafeArea.Width)
                {
                    szamlaphatterX += szamlaphatter.Width / 30;
                    szammezohatterX -= szammezohatter.Width / 30;
                }
                spriteBatch.Draw(szammezohatter, new Vector2(szammezohatterX, szammezohatterY), Color.White);
                spriteBatch.Draw(szamlaphatter, new Vector2(szamlaphatterX, szamlaphatterY), Color.White);
                if (szamlaphatterX >= GraphicsDevice.Viewport.TitleSafeArea.Width)
                {
                    this.Allapot = Konstansok.BEALLITASOK;
                }
            }

            #endregion

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}