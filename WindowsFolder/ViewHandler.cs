using DatabaseEngine;
using EntityEngine;
using MapGen;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.CompilerServices;
using Wahpa.WindowsFolder;
using static SFML.Window.Mouse;

namespace WindowEngine
{
    internal static class ViewHandler
    {
        public delegate void ProcessDelegate();
        public static List<GUIElement> GUIDraw = new List<GUIElement>();
        public static List<IUpdate> GUIUpdates = new List<IUpdate>();
        public static List<AttackableEntityTemplate> Enemies = new List<AttackableEntityTemplate>();
        public static List<Crystal> Crystals = new List<Crystal>();
        public static List<SkillTemplate> Skills = new List<SkillTemplate>();
        public static LevelBackground levelBackground { get; set; }
        public static int levelType {  get; set; }
        private static Chunk[,]? chunkViewMap { get; set; }
        public static Tile[,]? tileViewMap { get; private set; }
        private static List<ExitEntity> exitArray { get; set; }
        private static int chunkStartX { get; set; }
        private static int chunkStartY { get; set; }
        private static int chunkSizeX { get; set; }
        private static int chunkSizeY { get; set; }
        public static int pixelSizeX { get; private set; }
        public static int pixelSizeY { get; private set; }
        private static View? view { get; set; }
        public static float viewSizeX { get; set; } = 336;
        public static float viewSizeY { get; set; } = 336;
        public static float screenSizeX { get; set; } = 336;
        public static float screenSizeY { get; set; } = 336;
        public static Hero? hero { get; set; }

        public static int choosenLevel { get; set; } = 1;
        public static int gameStatus { get; set; } = 0;

        //////////////////////////////////////////////////////////////////////
        public static ProcessDelegate Update;
        public static ProcessDelegate Draw;

        public static void Start()
        {
            LoadLoginMenu();
        }

        public static void LoadMainMenu()
        {
            GUIDraw.Clear();
            GUIUpdates.Clear();
            Update += UpdateGUIElements;
            Draw += DrawGUIElements;

            GUIDraw.Add(new Background(120, -2, -10, Data.background1));

            // start button
            Button button1 = new Button(15, 0, 40, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Play", Data.font1, 60, Color.Black);
            button1.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadChooseLevelMenu();
            };


            // settings button
            Button button2 = new Button(15, 0, 50, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button2);
            GUIUpdates.Add(button2);
            button2.SetText("Settings", Data.font1, 60, Color.Black);
            button2.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadSettingsMenu();
            };


            // rating button
            Button button3 = new Button(15, 0, 60, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button3);
            GUIUpdates.Add(button3);
            button3.SetText("Rating", Data.font1, 60, Color.Black);
            button3.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadRatingMenu();
            };


            // exit button
            Button button4 = new Button(15, 0, 70, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button4);
            GUIUpdates.Add(button4);
            button4.SetText("Exit", Data.font1, 60, Color.Black);
            button4.DoClickFunc += () =>
            {
                MainWindow.window.Close();
            };


            // logOut button
            Button button5 = new Button(10, 3, 87, Data.GUIDict["LogOut"], 1);
            GUIDraw.Add(button5);
            GUIUpdates.Add(button5);
            button5.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadLoginMenu();
            };


            view = new View(GUIDraw[0].Sprite.Position, (Vector2f)MainWindow.window.Size);
            MainWindow.window.SetView(view);
            UpdateGUIElements();
        }

        public static void LoadSettingsMenu()
        {
            GUIUpdates.Clear();
            GUIDraw.Clear();
            Update += UpdateGUIElements;
            Draw += DrawGUIElements;

            GUIDraw.Add(new Background(120, 0, 0, Data.background2));

            GUIDraw.Add(new Pane(75, 0, 10, Data.GUIDict["Pane1"], 3));

            GUIDraw.Add(new Label(21, -3, 13, 3, "Settings", Data.font1, 60, Color.Black));
            // ScreenSize label
            GUIDraw.Add(new Label(20, -12, 24, 3, "Screen Size:", Data.font1, 60, Color.Black));

            // FPS label
            GUIDraw.Add(new Label(7, -12, 47, 3, "FPS:", Data.font1, 60, Color.Black));


            // return button
            Button button1 = new Button(15, 0, 70, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Return", Data.font1, 60, Color.Black);
            button1.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadMainMenu();
            };

            // comboBox screenSize
            string[] textBox1 = { "1920x1080", "1024x768", "800x600" };
            ComboBox comboBox1 = new ComboBox(15, 12, 24, 3, textBox1);
            GUIDraw.Add(comboBox1);
            GUIUpdates.Add(comboBox1);
            comboBox1.SetText("1920x1080", Data.font1, 60, Color.Black);
            comboBox1.DoClickFunc += () =>
            {
                GUIDraw.Remove(comboBox1);
                GUIUpdates.Remove(comboBox1);

                foreach (ComboBox combo in comboBox1.boxes)
                {
                    GUIDraw.Add(combo);
                    GUIUpdates.Add(combo);
                }
            };
            foreach (ComboBox combo in comboBox1.boxes)
            {
                combo.DoClickFunc += () =>
                {
                    ChangeScreenSize(combo.Text.DisplayedString.ToString());
                };
            }

            // comboBox fps
            string[] textBox2 = { "60", "45", "30" };
            ComboBox comboBox2 = new ComboBox(15, 12, 47, 3, textBox2);
            GUIDraw.Add(comboBox2);
            GUIUpdates.Add(comboBox2);
            comboBox2.SetText("60", Data.font1, 60, Color.Black);
            comboBox2.DoClickFunc += () =>
            {
                GUIDraw.Remove(comboBox2);
                GUIUpdates.Remove(comboBox2);

                foreach (ComboBox combo in comboBox2.boxes)
                {
                    GUIDraw.Add(combo);
                    GUIUpdates.Add(combo);
                }
            };
            foreach (ComboBox combo in comboBox2.boxes)
            {
                combo.DoClickFunc += () =>
                {
                    ChangeFPSLimit(combo.Text.DisplayedString.ToString());
                };
            }

            view = new View(GUIDraw[0].Sprite.Position, (Vector2f)MainWindow.window.Size);
            MainWindow.window.SetView(view);
            UpdateGUIElements();
        }

        public static void LoadLoginMenu()
        {
            GUIUpdates.Clear();
            GUIDraw.Clear();
            Update += UpdateGUIElements;
            Draw += DrawGUIElements;

            GUIDraw.Add(new Background(120, 0, 0, Data.background2));

            GUIDraw.Add(new Pane(75, 0, 10, Data.GUIDict["Pane1"], 3));

            GUIDraw.Add(new Label(30, -3, 15, 3, "Authorization", Data.font1, 60, Color.Black));

            GUIDraw.Add(new Label(10, -12, 30, 3, "Login:", Data.font1, 60, Color.Black));

            GUIDraw.Add(new Label(14, -12, 50, 3, "Password:", Data.font1, 60, Color.Black));

            TextField textField1 = new TextField(22, 10, 31.5f, Data.GUIDict["TextField"], 3, false);
            GUIDraw.Add(textField1);
            GUIUpdates.Add(textField1);

            TextField textField2 = new TextField(22, 10, 50.5f, Data.GUIDict["TextField"], 3, true);
            GUIDraw.Add(textField2);
            GUIUpdates.Add(textField2);

            // login button
            Button button1 = new Button(15, -15, 70, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Log In", Data.font1, 60, Color.Black);
            button1.DoClickFunc += () =>
            {
                LogInFunction(textField1.text, textField2.text);
            };

            // register button
            Button button2 = new Button(15, 15, 70, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button2);
            GUIUpdates.Add(button2);
            button2.SetText("Registration", Data.font1, 60, Color.Black);
            button2.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadRegisterMenu();
            };

            // exit button
            Button button4 = new Button(15, 0, 90, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button4);
            GUIUpdates.Add(button4);
            button4.SetText("Exit", Data.font1, 60, Color.Black);
            button4.DoClickFunc += () =>
            {
                MainWindow.window.Close();
            };

            view = new View(GUIDraw[0].Sprite.Position, (Vector2f)MainWindow.window.Size);
            MainWindow.window.SetView(view);
            UpdateGUIElements();
        }

        public static void LoadRegisterMenu()
        {
            GUIUpdates.Clear();
            GUIDraw.Clear();
            Update += UpdateGUIElements;
            Draw += DrawGUIElements;

            GUIDraw.Add(new Background(120, 0, 0, Data.background2));

            GUIDraw.Add(new Pane(75, 0, 10, Data.GUIDict["Pane1"], 3));

            GUIDraw.Add(new Label(30, -4, 14, 3, "Registration", Data.font1, 60, Color.Black));

            GUIDraw.Add(new Label(10, -12, 30, 3, "Login:", Data.font1, 60, Color.Black));

            GUIDraw.Add(new Label(14, -12, 40, 3, "Password:", Data.font1, 60, Color.Black));

            GUIDraw.Add(new Label(20, -12, 50, 3, "Rep. Password:", Data.font1, 60, Color.Black));

            TextField textField1 = new TextField(22, 10, 31.5f, Data.GUIDict["TextField"], 3, false);
            GUIDraw.Add(textField1);
            GUIUpdates.Add(textField1);

            TextField textField2 = new TextField(22, 10, 40.5f, Data.GUIDict["TextField"], 3, true);
            GUIDraw.Add(textField2);
            GUIUpdates.Add(textField2);

            TextField textField3 = new TextField(22, 10, 50.5f, Data.GUIDict["TextField"], 3, true);
            GUIDraw.Add(textField3);
            GUIUpdates.Add(textField3);

            // reg in button
            Button button1 = new Button(15, -15, 70, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Reg In", Data.font1, 60, Color.Black);
            button1.DoClickFunc += () =>
            {
                RegisterFunction(textField1.text, textField2.text, textField3.text);
            };

            // register button
            Button button2 = new Button(15, 15, 70, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button2);
            GUIUpdates.Add(button2);
            button2.SetText("Authorization", Data.font1, 60, Color.Black);
            button2.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadLoginMenu();
            };

            // exit button
            Button button4 = new Button(15, 0, 90, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button4);
            GUIUpdates.Add(button4);
            button4.SetText("Exit", Data.font1, 60, Color.Black);
            button4.DoClickFunc += () =>
            {
                MainWindow.window.Close();
            };

            view = new View(GUIDraw[0].Sprite.Position, (Vector2f)MainWindow.window.Size);
            MainWindow.window.SetView(view);
            UpdateGUIElements();
        }

        public static void LoadRatingMenu()
        {
            GUIUpdates.Clear();
            GUIDraw.Clear();
            Update += UpdateGUIElements;
            Draw += DrawGUIElements;

            GUIDraw.Add(new Background(120, 0, 0, Data.background2));

            GUIDraw.Add(new Pane(75, 0, 10, Data.GUIDict["Pane1"], 3));

            DataBase.DBGetRowsCount();
            DataBase.DBInitUsers();
            DataBase.page = 1;

            GUIDraw.Add(new Label(15, -2.5f, 14, 3, "Rating", Data.font1, 60, Color.Black));

            Label label1 = new Label(10, -12, 25, 3, "User1", Data.font1, 60, Color.Black);
            Label label2 = new Label(10, -12, 35, 3, "User2", Data.font1, 60, Color.Black);
            Label label3 = new Label(10, -12, 45, 3, "User3", Data.font1, 60, Color.Black);
            Label label4 = new Label(10, -12, 55, 3, "User4", Data.font1, 60, Color.Black);
            Label label5 = new Label(10, -12, 65, 3, "User5", Data.font1, 60, Color.Black);

            Label[] logins = new Label[] { label1, label2, label3, label4, label5 };

            foreach (Label label in logins)
            {
                GUIDraw.Add(label);
            }

            Label label6 = new Label(5, 12, 24, 3, "0", Data.font1, 60, Color.Black);
            Label label7 = new Label(5, 12, 34, 3, "0", Data.font1, 60, Color.Black);
            Label label8 = new Label(5, 12, 44, 3, "0", Data.font1, 60, Color.Black);
            Label label9 = new Label(5, 12, 54, 3, "0", Data.font1, 60, Color.Black);
            Label label10 = new Label(5, 12, 64, 3, "0", Data.font1, 60, Color.Black);

            Label[] coins = new Label[] { label6, label7, label8, label9, label10 };

            foreach (Label label in coins)
            {
                GUIDraw.Add(label);
            }

            UpdatePageRating(logins, coins);

            // return button
            Button button1 = new Button(15, 0, 80, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Return", Data.font1, 60, Color.Black);
            button1.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadMainMenu();
            };

            // left button
            Button button2 = new Button(5, -20, 70, Data.GUIDict["ArrowLeft"], 3);
            button2.SetText("", Data.font1, 60, Color.Black);


            // right button
            Button button3 = new Button(5, 20, 70, Data.GUIDict["ArrowRight"], 3);
            button3.SetText("", Data.font1, 60, Color.Black);

            if (DataBase.page * 5 < DataBase.userLen)
            {
                GUIDraw.Add(button3);
                GUIUpdates.Add(button3);
            }

            button2.DoClickFunc += () =>
            {
                if (DataBase.page == 2)
                {
                    GUIDraw.Remove(button2);
                    GUIUpdates.Remove(button2);
                }
                DataBase.page -= 1;
                if (DataBase.page * 5 < DataBase.userLen)
                {
                    GUIDraw.Add(button3);
                    GUIUpdates.Add(button3);
                }
                UpdatePageRating(logins, coins);
            };

            button3.DoClickFunc += () =>
            {
                if (DataBase.page == 1)
                {
                    GUIDraw.Add(button2);
                    GUIUpdates.Add(button2);
                }
                DataBase.page += 1;
                if (DataBase.page * 5 >= DataBase.userLen)
                {
                    GUIDraw.Remove(button3);
                    GUIUpdates.Remove(button3);
                }
                UpdatePageRating(logins, coins);
            };

        }

        public static void LoadAdminMenu()
        {
            GUIUpdates.Clear();
            GUIDraw.Clear();
            Update += UpdateGUIElements;
            Draw += DrawGUIElements;

            GUIDraw.Add(new Background(120, 0, 0, Data.background2));

            GUIDraw.Add(new Pane(75, 0, 10, Data.GUIDict["Pane1"], 3));

            DataBase.DBGetRowsCount();
            DataBase.DBInitUsers();
            DataBase.page = 1;

            GUIDraw.Add(new Label(20, -1, 14, 3, "Admin Panel", Data.font1, 60, Color.Black));

            Label label1 = new Label(10, -15, 25, 3, "User1", Data.font1, 60, Color.Black);
            Label label2 = new Label(10, -15, 35, 3, "User2", Data.font1, 60, Color.Black);
            Label label3 = new Label(10, -15, 45, 3, "User3", Data.font1, 60, Color.Black);
            Label label4 = new Label(10, -15, 55, 3, "User4", Data.font1, 60, Color.Black);
            Label label5 = new Label(10, -15, 65, 3, "User5", Data.font1, 60, Color.Black);

            Label[] logins = new Label[] { label1, label2, label3, label4, label5 };

            foreach (Label label in logins)
            {
                GUIDraw.Add(label);
            }

            Label label6 = new Label(10, 0, 24, 3, "0", Data.font1, 60, Color.Black);
            Label label7 = new Label(10, 0, 34, 3, "0", Data.font1, 60, Color.Black);
            Label label8 = new Label(10, 0, 44, 3, "0", Data.font1, 60, Color.Black);
            Label label9 = new Label(10, 0, 54, 3, "0", Data.font1, 60, Color.Black);
            Label label10 = new Label(10, 0, 64, 3, "0", Data.font1, 60, Color.Black);

            Label[] passwords = new Label[] { label6, label7, label8, label9, label10 };

            foreach (Label label in passwords)
            {
                GUIDraw.Add(label);
            }

            Label label11 = new Label(3, 15, 24, 3, "0", Data.font1, 60, Color.Black);
            Label label12 = new Label(3, 15, 34, 3, "0", Data.font1, 60, Color.Black);
            Label label13 = new Label(3, 15, 44, 3, "0", Data.font1, 60, Color.Black);
            Label label14 = new Label(3, 15, 54, 3, "0", Data.font1, 60, Color.Black);
            Label label15 = new Label(3, 15, 64, 3, "0", Data.font1, 60, Color.Black);

            Label[] coins = new Label[] { label11, label12, label13, label14, label15 };

            foreach (Label label in coins)
            {
                GUIDraw.Add(label);
            }

            Button buttonD1 = new Button(5, 25, 24, Data.GUIDict["Button6"], 3);
            Button buttonD2 = new Button(5, 25, 34, Data.GUIDict["Button6"], 3);
            Button buttonD3 = new Button(5, 25, 44, Data.GUIDict["Button6"], 3);
            Button buttonD4 = new Button(5, 25, 54, Data.GUIDict["Button6"], 3);
            Button buttonD5 = new Button(5, 25, 64, Data.GUIDict["Button6"], 3);

            Button[] buttons = new Button[] { buttonD1, buttonD2, buttonD3, buttonD4, buttonD5 };

            // left button
            Button button2 = new Button(5, -20, 73, Data.GUIDict["ArrowLeft"], 3);
            button2.SetText("", Data.font1, 60, Color.Black);


            // right button
            Button button3 = new Button(5, 20, 73, Data.GUIDict["ArrowRight"], 3);
            button3.SetText("", Data.font1, 60, Color.Black);

            buttonD1.DoClickFunc += () =>
            {
                DataBase.DBDeleteUser(label1.text.DisplayedString.ToString());
                DataBase.DBGetRowsCount();
                DataBase.DBInitUsers();
                UpdateAdminMenuPage(logins, passwords, coins, buttons, button2, button3);
            };
            buttonD2.DoClickFunc += () =>
            {
                DataBase.DBDeleteUser(label2.text.DisplayedString.ToString());
                DataBase.DBGetRowsCount();
                DataBase.DBInitUsers();
                UpdateAdminMenuPage(logins, passwords, coins, buttons, button2, button3);
            };
            buttonD3.DoClickFunc += () =>
            {
                DataBase.DBDeleteUser(label3.text.DisplayedString.ToString());
                DataBase.DBGetRowsCount();
                DataBase.DBInitUsers();
                UpdateAdminMenuPage(logins, passwords, coins, buttons, button2, button3);
            };
            buttonD4.DoClickFunc += () =>
            {
                DataBase.DBDeleteUser(label4.text.DisplayedString.ToString());
                DataBase.DBGetRowsCount();
                DataBase.DBInitUsers();
                UpdateAdminMenuPage(logins, passwords, coins, buttons, button2, button3);
            };
            buttonD5.DoClickFunc += () =>
            {
                DataBase.DBDeleteUser(label5.text.DisplayedString.ToString());
                DataBase.DBGetRowsCount();
                DataBase.DBInitUsers();
                UpdateAdminMenuPage(logins, passwords, coins, buttons, button2, button3);
            };

            UpdateAdminMenuPage(logins, passwords, coins, buttons, button2, button3);

            // return button
            Button button1 = new Button(15, 0, 80, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Return", Data.font1, 60, Color.Black);
            button1.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadLoginMenu();
            };

            if (DataBase.page * 5 < DataBase.userLen)
            {
                GUIDraw.Add(button3);
                GUIUpdates.Add(button3);
            }

            button2.DoClickFunc += () =>
            {
                DataBase.page -= 1;
                UpdateAdminMenuPage(logins, passwords, coins, buttons, button2, button3);
            };

            button3.DoClickFunc += () =>
            {
                DataBase.page += 1;
                UpdateAdminMenuPage(logins, passwords, coins, buttons, button2, button3);
            };


        }

        public static void LoadChooseLevelMenu()
        {
            GUIUpdates.Clear();
            GUIDraw.Clear();
            Update += UpdateGUIElements;
            Draw += DrawGUIElements;

            GUIDraw.Add(new Background(120, 0, 0, Data.background2));

            GUIDraw.Add(new Pane(75, 0, 10, Data.GUIDict["Pane1"], 3));

            GUIDraw.Add(new Label(30, -4, 14, 3, "Choose Level", Data.font1, 60, Color.Black));

            Button button1 = new Button(30, 0, 35, Data.GUIDict["Button2"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Wood House", Data.font1, 60, Color.White);
            button1.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                levelType = 1;
                LoadLevelOne();
            };

            Button button2 = new Button(30, 0, 55, Data.GUIDict["Button3"], 3);
            GUIDraw.Add(button2);
            GUIUpdates.Add(button2);
            button2.SetText("Stone Cave", Data.font1, 60, Color.White);
            button2.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                levelType = 2;
                LoadLevelOne();
            };

        }

        public static void LoadChooseClassMenu(string login, string password)
        {
            GUIUpdates.Clear();
            GUIDraw.Clear();
            Update += UpdateGUIElements;
            Draw += DrawGUIElements;

            GUIDraw.Add(new Background(120, 0, 0, Data.background2));

            GUIDraw.Add(new Pane(75, 0, 10, Data.GUIDict["Pane1"], 3));

            GUIDraw.Add(new Label(30, -4, 14, 3, "Choose Class", Data.font1, 60, Color.Black));

            Button button1 = new Button(30, 0, 35, Data.GUIDict["Button4"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Fire Shaman", Data.font1, 60, Color.White);
            button1.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                DataBase.DBAddUser(login, password, 1);
                DataBase.DBLogUser(login, password);
                LoadMainMenu();
            };

            Button button2 = new Button(30, 0, 55, Data.GUIDict["Button5"], 3);
            GUIDraw.Add(button2);
            GUIUpdates.Add(button2);
            button2.SetText("Ice Shaman", Data.font1, 60, Color.White);
            button2.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                DataBase.DBAddUser(login, password, 2);
                DataBase.DBLogUser(login, password);
                LoadMainMenu();
            };

        }

        public static void LoadLevelOne()
        {
            GUIUpdates.Clear();
            GUIDraw.Clear();
            gameStatus = 1;

            hero = new Hero(324, 136, GetHeroClass());
            MapEngine.LoadLevel(1);
            UpdateChunkViewMap(MapEngine.spawnRoom.startChunkX, MapEngine.spawnRoom.startChunkY, 5);
            UpdateTileViewMap();

            if (levelType == 1)
                Tile.LoadBordersAndCorners(tileViewMap, Data.LEVEL1_WALL_TEXTURES);
            else
                Tile.LoadBordersAndCorners(tileViewMap, Data.LEVEL2_WALL_TEXTURES);

            view = new View(hero.sprite.Position, new Vector2f(viewSizeX, viewSizeY));

            Heart heart1 = new Heart(1, 1, 2, Data.GUIDict["HeartFull"], 1, 1);
            Heart heart2 = new Heart(1, 1.6f, 2, Data.GUIDict["HeartFull"], 1, 3);
            Heart heart3 = new Heart(1, 2.2f, 2, Data.GUIDict["HeartFull"], 1, 5);
            GUIDraw.Add(heart1);
            GUIDraw.Add(heart2);
            GUIDraw.Add(heart3);
            GUIUpdates.Add(heart1);
            GUIUpdates.Add(heart2);
            GUIUpdates.Add(heart3);

            CoinCouter coinCouter = new CoinCouter(1, 15, 2, Data.GUIDict["Coin"], 1);

            GUIDraw.Add(coinCouter);
            GUIUpdates.Add(coinCouter);

            if (levelType == 1)
                levelBackground = new LevelBackground(Data.background4, 110);
            else
                levelBackground = new LevelBackground(Data.background5, 110);

            Update += UpdateGUIElements;
            Update += CheckExitCollision;
            Update += UpdatePlayViewSize;
            Update += levelBackground.Relocate;
            Update += UpdateEntities;

            Draw += levelBackground.Draw;
            Draw += DrawMap;
            Draw += DrawGUIElements;
            Draw += DrawEntities;
        }

        public static void LoadLoseScreen()
        {
            gameStatus = 0;

            View view = new View(new FloatRect(0, 0, 1920, 1080));
            MainWindow.window.SetView(view);

            GUIDraw.Clear();
            GUIUpdates.Clear();
            Enemies.Clear();
            Skills.Clear();
            Crystals.Clear();

            Update -= CheckExitCollision;
            Update -= UpdatePlayViewSize;
            Update -= levelBackground.Relocate;
            Update -= UpdateEntities;

            Draw -= levelBackground.Draw;
            Draw -= DrawMap;
            Draw -= DrawEntities;

            //Update += UpdateGUIElements;
            //Draw += DrawGUIElements;

            if (levelType == 1)
                GUIDraw.Add(new Background(120, 0, 0, Data.background4));
            else
                GUIDraw.Add(new Background(120, 0, 0, Data.background5));


            GUIDraw.Add(new Pane(75, 0, 10, Data.GUIDict["Pane1"], 3));

            GUIDraw.Add(new Label(30, -4, 14, 3, "Lose Screen", Data.font1, 60, Color.Black));

            GUIDraw.Add(new Label(40, 10, 55, 3, "You're character have " + DataBase.logUser.coins.ToString() + " coins!", Data.font1, 60, Color.Black));

            Button button1 = new Button(15, 0, 80, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Continue", Data.font1, 60, Color.Black);
            button1.DoClickFunc += () =>
            {
                DataBase.DBBlock(DataBase.logUser.login);

                LoadLoginMenu();
            };
        }

        public static void LoadWinScreen()
        {
            gameStatus = 0;

            View view = new View(new FloatRect(0, 0, 1920, 1080));
            MainWindow.window.SetView(view);

            GUIDraw.Clear();
            GUIUpdates.Clear();
            Enemies.Clear();
            Skills.Clear();
            Crystals.Clear();

            Update -= CheckExitCollision;
            Update -= UpdatePlayViewSize;
            Update -= levelBackground.Relocate;
            Update -= UpdateEntities;

            Draw -= levelBackground.Draw;
            Draw -= DrawMap;
            Draw -= DrawEntities;

            if (levelType == 1)
                GUIDraw.Add(new Background(120, 0, 0, Data.background4));
            else
                GUIDraw.Add(new Background(120, 0, 0, Data.background5));

            GUIDraw.Add(new Pane(75, 0, 10, Data.GUIDict["Pane1"], 3));

            GUIDraw.Add(new Label(30, -4, 14, 3, "Win Screen", Data.font1, 60, Color.Black));

            DataBase.DBAddCoins(hero.coins);
            DataBase.DBLogUser(DataBase.logUser.login, DataBase.logUser.password);

            GUIDraw.Add(new Label(40, 10, 55, 3, "You're character have " + DataBase.logUser.coins.ToString() + " coins!", Data.font1, 60, Color.Black));

            Button button1 = new Button(15, 0, 80, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Continue", Data.font1, 60, Color.Black);
            button1.DoClickFunc += () =>
            {
                GUIDraw.Clear();
                GUIUpdates.Clear();
                Enemies.Clear();
                Skills.Clear();
                Crystals.Clear();

                Update = null;
                Draw = null;

                LoadMainMenu();
            };
        }

        //////////////////////////////////////////////////////////////////////////

        public static Hero.HeroClass GetHeroClass()
        {
            if (DataBase.logUser.mageClass == 1)
            {
                return Hero.HeroClass.FireMage;
            }
            else
            {
                return Hero.HeroClass.IceMage;
            }
        }

        public static void EscFunction()
        {
           
        }

        public static void LogInFunction(string login, string password)
        {
            if (login == "admin" && password == "admin")
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadAdminMenu();
            }
            else if (DataBase.DBLogUser(login, password))
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadMainMenu();
            }

        }

        public static void RegisterFunction(string login, string password, string repPassword)
        {
            if (login != "admin" && password == repPassword && DataBase.DBCheckLogin(login))
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadChooseClassMenu(login, password);
            }
        }

        public static void UpdateAdminMenuPage(Label[] logins, Label[] passwords, Label[] coins, Button[] buttons, Button left, Button right)
        {
            for (int i = 0; i < 5; i++)
            {
                if (DataBase.userLen < DataBase.page * 5 + i - 4)
                {
                    logins[i].text.DisplayedString = "";
                    passwords[i].text.DisplayedString = "";
                    coins[i].text.DisplayedString = "";
                    GUIDraw.Remove(buttons[i]);
                    GUIUpdates.Remove(buttons[i]);
                    continue;
                }

                logins[i].text.DisplayedString = DataBase.users[DataBase.page * 5 - 5 + i].login;
                passwords[i].text.DisplayedString = DataBase.users[DataBase.page * 5 - 5 + i].password;
                if (DataBase.users[DataBase.page * 5 - 5 + i].access)
                    logins[i].text.Color = Color.Green;
                else
                    logins[i].text.Color = Color.Red;

                coins[i].text.DisplayedString = DataBase.users[DataBase.page * 5 - 5 + i].coins.ToString();
                GUIDraw.Remove(buttons[i]);
                GUIUpdates.Remove(buttons[i]);
                GUIDraw.Add(buttons[i]);
                GUIUpdates.Add(buttons[i]);


            }

            GUIDraw.Remove(right);
            GUIUpdates.Remove(right);
            GUIDraw.Add(left);
            GUIUpdates.Add(left);
            if (DataBase.page == 1)
            {
                GUIDraw.Remove(left);
                GUIUpdates.Remove(left);
            }

            GUIDraw.Remove(right);
            GUIUpdates.Remove(right);
            GUIDraw.Add(right);
            GUIUpdates.Add(right);
            if (DataBase.page * 5 >= DataBase.userLen)
            {
                GUIDraw.Remove(right);
                GUIUpdates.Remove(right);
            }
        }

        public static void UpdatePageRating(Label[] logins, Label[] coins)
        {
            for (int i = 0; i < 5; i++)
            {
                if (DataBase.userLen < DataBase.page * 5 + i - 4)
                {
                    logins[i].text.DisplayedString = "";
                    coins[i].text.DisplayedString = "";
                    continue;
                }

                logins[i].text.DisplayedString = DataBase.users[DataBase.page * 5 - 5 + i].login;
                if (DataBase.users[DataBase.page * 5 - 5 + i].access)
                    logins[i].text.Color = Color.Green;
                else
                    logins[i].text.Color = Color.Red;

                coins[i].text.DisplayedString = DataBase.users[DataBase.page * 5 - 5 + i].coins.ToString();
            }
        }

        public static void CastSkill(Hero.HeroClass heroClass)
        {
            switch(heroClass)
            {
                case Hero.HeroClass.FireMage:
                    Skills.Add(new SkillTemplate(hero.coordinateX + hero.sizeX, hero.coordinateY, 16, 12, "FireShamanBullet", hero.direction));
                    break;
                case Hero.HeroClass.IceMage:
                    Skills.Add(new SkillTemplate(hero.coordinateX + hero.sizeX, hero.coordinateY, 16, 12, "IceShamanBullet", hero.direction));
                    break;
            }
        }

        public static void DrawMap()
        {
            for (int x = 0; x < tileViewMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileViewMap.GetLength(1); y++)
                {
                    if (tileViewMap[x, y].sprite != null)
                        MainWindow.window.Draw(tileViewMap[x, y].sprite);

                }
            }
        }

        public static void DrawEntities()
        {
            foreach (Crystal crystal in Crystals)
            {
                MainWindow.window.Draw(crystal.sprite);
            }

            foreach (AttackableEntityTemplate entity in Enemies)
            {
                MainWindow.window.Draw(entity.sprite);
            }

            foreach (SkillTemplate skill in Skills)
            {
                MainWindow.window.Draw(skill.sprite);
            }

            MainWindow.window.Draw(hero.sprite);
        }
        public static void UpdateEntities()
        {
            if (gameStatus == 1)
            {
                hero.Update();

                List<AttackableEntityTemplate> enemiesCopy = new List<AttackableEntityTemplate>(Enemies);
                foreach (AttackableEntityTemplate entity in enemiesCopy)
                {
                    if (Triggerable.CheckCollission(hero, entity))
                    {
                        hero.Attacked();
                    }

                    entity.Update();
                }

                List<Crystal> crystalsCopy = new List<Crystal>(Crystals);
                foreach (Crystal crystal in crystalsCopy)
                {
                    if (Triggerable.CheckCollission(hero, crystal))
                    {
                        Crystals.Remove(crystal);
                        hero.crystals += 1;

                        if (hero.crystals > 2)
                        {
                            LoadWinScreen();
                        }
                    }
                }

                List<SkillTemplate> skillsCopy = new List<SkillTemplate>(Skills);
                foreach (SkillTemplate skill in skillsCopy)
                {
                    skill.Update();

                    foreach (AttackableEntityTemplate entity in enemiesCopy)
                    {
                        if (Triggerable.CheckCollission(entity, skill))
                        {
                            skill.Attack(entity);
                        }
                    }
                }
            }
            
        }

        public static void ChangeScreenSize(string text)
        {
            if (text == "1920x1080")
            {
                MainWindow.window.Close();
                MainWindow.StartChanged();
            }
            else
            {
                uint[] windowSize = Data.SizesDict[text];
                MainWindow.window.Close();
                MainWindow.StartChanged(windowSize[0], windowSize[1]);
            }
        }

        public static void ChangeFPSLimit(string text)
        {
            switch (text)
            {
                case "60":
                    MainWindow.window.SetFramerateLimit(60);
                    break;
                case "45":
                    MainWindow.window.SetFramerateLimit(45);
                    break;
                default:
                    MainWindow.window.SetFramerateLimit(30);
                    break;
            }
        }

        public static void UpdateGUIElements()
        {
            List<GUIElement> GUIDrawCopy = new List<GUIElement>(GUIDraw);
            foreach (GUIElement element in GUIDrawCopy)
            {
                element.Resize();
                element.Relocate();
            }

            List<IUpdate> GUIUpdatesCopy = new List<IUpdate>(GUIUpdates);
            foreach (IUpdate upd in GUIUpdatesCopy)
            {
                upd.Update();
            }
        }

        public static void DrawGUIElements()
        {
            // тута сделать нормальное копирование, синхронная для двух потоков
            List<GUIElement> gUIElementsCopy = new List<GUIElement>(GUIDraw);

            foreach(GUIElement element in gUIElementsCopy)
            {
                element.Draw();
            }
        }

        public static void UpdatePlayViewSize()
        {
            if (screenSizeY > screenSizeX)
            {
                viewSizeY = 336;
                viewSizeX = 336 * (screenSizeX / screenSizeY);
            }
            else
            {
                viewSizeX = 336;
                viewSizeY = 336 * (screenSizeY / screenSizeX);
            }

            view.Size = new Vector2f(viewSizeX, viewSizeY);

            float centerPositionX;
            float centerPositionY;

            // Установка границ области просмотра, чтобы она не выходила за пределы карты
            if (viewSizeX / 2 > hero.sprite.Position.X) // левая грань 
            {
                centerPositionX = viewSizeX / 2;
            }
            else if (viewSizeX / 2 > pixelSizeX - hero.sprite.Position.X) // правая грань
            {
                centerPositionX = pixelSizeX - viewSizeX / 2;
            }
            else 
            {
                centerPositionX = hero.sprite.Position.X;
            }

            if (viewSizeY / 2 > hero.sprite.Position.Y)
            {
                centerPositionY = viewSizeY / 2;
            }
            else if (viewSizeY / 2 > pixelSizeY - hero.sprite.Position.Y)
            {
                centerPositionY = pixelSizeY - viewSizeY / 2;
            }
            else 
            {
                centerPositionY = hero.sprite.Position.Y;
            }

            Vector2 targetPosition = new Vector2(centerPositionX, centerPositionY);

            Vector2 temp = Vector2.Lerp(new Vector2(view.Center.X, view.Center.Y), targetPosition, 0.1f);
            view.Center = new Vector2f(temp.X, temp.Y);

            MainWindow.window.SetView(view);
        }

        public static void ChangeRoom()
        {
            ResetSprites();

            if (hero?.coordinateX <= 80)
            {
                // переход влево
                int chunkCoordinateY = (hero.coordinateY - 5 * 16 + 1) / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(chunkStartX - 1, chunkStartY + chunkCoordinateY, 3);
            }
            else if (hero?.coordinateX + hero.sizeX >= (tileViewMap.GetLength(0) - 5) * 16)
            {
                // переход вправо
                int chunkCoordinateY = (hero.coordinateY - 5 * 16 + 1) / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(chunkStartX + chunkViewMap.GetLength(0), chunkStartY + chunkCoordinateY, 1);
            }
            else if (hero?.coordinateY <= 80)
            {
                // переход вверх
                int chunkCoordinateX = (hero.coordinateX - 5 * 16 + 1) / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(chunkStartX + chunkCoordinateX, chunkStartY - 1, 2);
            }
            else if (hero?.coordinateY + hero.sizeY >= (tileViewMap.GetLength(1) - 5) * 16)
            {
                // переход вниз
                int chunkCoordinateX = (hero.coordinateX - 5 * 16 + 1) / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(chunkStartX + chunkCoordinateX, chunkStartY + chunkViewMap.GetLength(1), 4);
            }

            UpdateTileViewMap();

            if (levelType == 1)
                Tile.LoadBordersAndCorners(tileViewMap, Data.LEVEL1_WALL_TEXTURES);
            else
                Tile.LoadBordersAndCorners(tileViewMap, Data.LEVEL2_WALL_TEXTURES);
        }

        public static void ResetSprites()
        {
            for (int i = 0; i < tileViewMap.GetLength(0); i++)
            {
                for (int j = 0; j < tileViewMap.GetLength(1); j++)
                {
                    tileViewMap[i, j].sprite = new Sprite();
                }
            }

            Enemies.Clear();
            Skills.Clear();
            Crystals.Clear();
            exitArray.Clear();
        }

        public static void CheckExitCollision()
        {
            foreach(ExitEntity exit in exitArray)
            {
                if (Triggerable.CheckCollission(exit, hero))
                {
                    ChangeRoom();
                    return;
                }
                    
            }
        }

        //////////////////////////////////////////////////////////////////////
        private static void UpdateTileViewMap()
        {
            tileViewMap = new Tile[chunkViewMap.GetLength(0) * 11 + 10, chunkViewMap.GetLength(1) * 11 + 10];

            for (int i = 0; i < chunkViewMap.GetLength(0); i++)
            {
                for (int j = 0; j < chunkViewMap.GetLength(1); j++)
                {
                    char[,] charTileArray = chunkViewMap[i, j].charTileArray;

                    for (int i1 = 0; i1 < charTileArray.GetLength(0); i1++)
                    {
                        for (int j1 = 0; j1 < charTileArray.GetLength(1); j1++)
                        {
                            if (charTileArray[i1, j1] == '#' || charTileArray[i1, j1] == '@')
                            {
                                tileViewMap[i * 11 + i1 + 5, j * 11 + j1 + 5] = new Tile(TileType.wall);
                                tileViewMap[i * 11 + i1 + 5, j * 11 + j1 + 5].sprite.Position = new Vector2f(i * 11 * 16 + (i1 + 5) * 16, j * 11 * 16 + (j1 + 5) * 16);
                            }
                            else if (charTileArray[i1, j1] == 'M')
                            {
                                tileViewMap[i * 11 + i1 + 5, j * 11 + j1 + 5] = new Tile(TileType.empty);
                                if (levelType == 1)
                                    Enemies.Add(new WalkedMonster((i * 11 + i1 + 5) * 16, (j * 11 + j1 + 5) * 16 - 8, 16, 24, "SkeletonRun"));
                                else
                                    Enemies.Add(new WalkedMonster((i * 11 + i1 + 5) * 16, (j * 11 + j1 + 5) * 16 - 12, 16, 28, "GolemRun"));
                            }
                            else if (charTileArray[i1, j1] == 'C')
                            {
                                tileViewMap[i * 11 + i1 + 5, j * 11 + j1 + 5] = new Tile(TileType.empty);
                                Crystals.Add(new Crystal((i * 11 + i1 + 5) * 16, (j * 11 + j1 + 5) * 16 - 8, 16, 24));
                            }
                            else
                            {
                                tileViewMap[i * 11 + i1 + 5, j * 11 + j1 + 5] = new Tile(TileType.empty);
                            }
                        }
                    }

                }
            }

            UpdateTileViewMapBorder();
        }

        private static void UpdateTileViewMapBorder()
        {
            exitArray = new List<ExitEntity> { };

            for (int j = 0; j < tileViewMap.GetLength(1); j++)
            {
                //левая грань
                if (tileViewMap[5, j] == null || tileViewMap[5, j].type == TileType.wall)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        tileViewMap[i, j] = new Tile(TileType.wall);
                        tileViewMap[i, j].sprite.Position = new Vector2f(i * 16, j * 16);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        tileViewMap[i, j] = new Tile(TileType.empty);
                        tileViewMap[i, j].sprite.Position = new Vector2f(i * 16, j * 16);

                        if (i == 0)
                        exitArray.Add(new ExitEntity(i * 16, j * 16, 16, 16));
                    }
                }

                // правая грань
                if (tileViewMap[tileViewMap.GetLength(0) - 6, j] == null || tileViewMap[tileViewMap.GetLength(0) - 6, j].type == TileType.wall)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        tileViewMap[tileViewMap.GetLength(0) - 1 - i, j] = new Tile(TileType.wall);
                        tileViewMap[tileViewMap.GetLength(0) - 1 - i, j].sprite.Position = new Vector2f((tileViewMap.GetLength(0) - 1 - i) * 16, j * 16);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        tileViewMap[tileViewMap.GetLength(0) - 1 - i, j] = new Tile(TileType.empty);
                        tileViewMap[tileViewMap.GetLength(0) - 1 - i, j].sprite.Position = new Vector2f((tileViewMap.GetLength(0) - 1 - i) * 16, j * 16);

                        if (i == 0)
                            exitArray.Add(new ExitEntity((tileViewMap.GetLength(0) - 1 - i) * 16, j * 16, 16, 16));
                    }
                }
                
            }
            
            for (int i = 5; i < tileViewMap.GetLength(0); i++)
            {
                // верхняя грань
                if (tileViewMap[i, 5] == null || tileViewMap[i, 5].type == TileType.wall)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        tileViewMap[i, j] = new Tile(TileType.wall);
                        tileViewMap[i, j].sprite.Position = new Vector2f(i * 16, j * 16);
                    }
                }
                else
                {
                    for (int j = 0; j < 5; j++)
                    {
                        tileViewMap[i, j] = new Tile(TileType.empty);
                        tileViewMap[i, j].sprite.Position = new Vector2f(i * 16, j * 16);

                        if (j == 0)
                            exitArray.Add(new ExitEntity(i * 16, j * 16, 16, 16));
                    }
                }

                // нижняя грань
                if (tileViewMap[i, tileViewMap.GetLength(1) - 6] == null || tileViewMap[i, tileViewMap.GetLength(1) - 6].type == TileType.wall)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        tileViewMap[i, tileViewMap.GetLength(1) - 1 - j] = new Tile(TileType.wall);
                        tileViewMap[i, tileViewMap.GetLength(1) - 1 - j].sprite.Position = new Vector2f(i * 16, (tileViewMap.GetLength(1) - 1 - j) * 16);
                    }
                }
                else
                {
                    for (int j = 0; j < 5; j++)
                    {
                        tileViewMap[i, tileViewMap.GetLength(1) - 1 - j] = new Tile(TileType.empty);
                        tileViewMap[i, tileViewMap.GetLength(1) - 1 - j].sprite.Position = new Vector2f(i * 16, (tileViewMap.GetLength(1) - 1 - j) * 16);

                        if (j == 0)
                            exitArray.Add(new ExitEntity(i * 16, (tileViewMap.GetLength(1) - 1 - j) * 16, 16, 16));
                    }
                }
            }
        }

        private static void UpdateChunkViewMap(int chunkX, int chunkY, int route)
        {
            Room room = MapEngine.chunkMap[chunkX, chunkY].room;
            chunkStartX = room.startChunkX;
            chunkStartY = room.startChunkY;

            chunkSizeX = room.sizeX;
            chunkSizeY = room.sizeY;
            pixelSizeX = (chunkSizeX * 11 * 16) + (10 * 16);
            pixelSizeY = (chunkSizeY * 11 * 16) + (10 * 16);

            chunkViewMap = new Chunk[chunkSizeX, chunkSizeY];
            for (int i = 0; i < chunkSizeX; i++)
            {
                for (int j = 0; j < chunkSizeY; j++)
                {
                    chunkViewMap[i, j] = MapEngine.chunkMap[chunkStartX + i, chunkStartY + j];
                }
            }

            switch (route)
            {
                case 1: // вправо
                    hero.coordinateY = (chunkY - chunkStartY) * 11 * 16 + (5 * 16) + (hero.coordinateY - 5 * 16) % (11 * 16);
                    hero.coordinateX = 32;
                    break;
                case 2: // вверх
                    hero.coordinateX = (chunkX - chunkStartX) * 11 * 16 + (5 * 16) + (hero.coordinateX - 5 * 16) % (11 * 16);
                    hero.coordinateY = (chunkSizeY * 11 * 16) + (5 * 16) + 25;
                    break;
                case 3: // влево
                    hero.coordinateY = (chunkY - chunkStartY) * 11 * 16 + (5 * 16) + (hero.coordinateY - 5 * 16) % (11 * 16);
                    hero.coordinateX = (chunkSizeX * 11 * 16) + (5 * 16) + 33;
                    break;
                case 4: // вниз
                    hero.coordinateX = (chunkX - chunkStartX) * 11 * 16 + (5 * 16) + (hero.coordinateX - 5 * 16) % (11 * 16);
                    hero.coordinateY = 24;
                    break;
                case 5: // центр
                    hero.coordinateY = (chunkSizeY / 2 * 11 * 16) + (5 * 16) - 8;
                    hero.coordinateX = (chunkSizeX / 2 * 11 * 16) + (5 * 16) - 12;
                    break;
            }

            view = new View(hero.sprite.Position, new Vector2f(viewSizeX, viewSizeY));
            MainWindow.window.SetView(view);
        }


    }
}
