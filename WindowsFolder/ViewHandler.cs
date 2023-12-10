﻿using EntityEngine;
using MapGen;
using SFML.Graphics;
using SFML.System;
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
        public static List<AttackableEntityTemplate> EntityList = new List<AttackableEntityTemplate>();
        public static LevelBackground levelBackground;
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
        private static Hero? hero { get; set; }

        //////////////////////////////////////////////////////////////////////
        public static ProcessDelegate Update;
        public static ProcessDelegate Draw;

        public static void Start()
        {
            LoadLevelOne();
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
                LoadLevelOne();
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

            // ScreenSize label
            GUIDraw.Add(new Label(20, -12, 20, 3, "Screen Size:", Data.font1, 60, Color.Black));

            // FPS label
            GUIDraw.Add(new Label(7, -12, 50, 3, "FPS:", Data.font1, 60, Color.Black));


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
            ComboBox comboBox1 = new ComboBox(15, 12, 20, 3, textBox1);
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
                    ChangeScreenSize(combo.Text.ToString());
                };
            }

            // comboBox fps
            string[] textBox2 = { "60", "45", "30" };
            ComboBox comboBox2 = new ComboBox(15, 12, 50, 3, textBox2);
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
                    ChangeFPSLimit(combo.Text.ToString());
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

            GUIDraw.Add(new Label(30, 0, 10, 3, "Authorization", Data.font1, 60, Color.Black));

            GUIDraw.Add(new Label(10, -12, 25, 3, "Login:", Data.font1, 60, Color.Black));

            GUIDraw.Add(new Label(14, -12, 35, 3, "Password:", Data.font1, 60, Color.Black));

            TextField textField1 = new TextField(22, 15, 35, Data.GUIDict["TextField"], 3, false);
            GUIDraw.Add(textField1);
            GUIUpdates.Add(textField1);

            // login button
            Button button1 = new Button(15, -15, 70, Data.GUIDict["Button1"], 3);
            GUIDraw.Add(button1);
            GUIUpdates.Add(button1);
            button1.SetText("Log In", Data.font1, 60, Color.Black);
            button1.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadMainMenu();
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
                LoadMainMenu();
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

            // left button
            Button button2 = new Button(5, -20, 70, Data.GUIDict["ArrowLeft"], 3);
            GUIDraw.Add(button2);
            GUIUpdates.Add(button2);
            button2.SetText("", Data.font1, 60, Color.Black);
            button2.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadMainMenu();
            };

            // right button
            Button button3 = new Button(5, 20, 70, Data.GUIDict["ArrowRight"], 3);
            GUIDraw.Add(button3);
            GUIUpdates.Add(button3);
            button3.SetText("", Data.font1, 60, Color.Black);
            button3.DoClickFunc += () =>
            {
                Update -= UpdateGUIElements;
                Draw -= DrawGUIElements;
                LoadMainMenu();
            };
        }

        public static void LoadLevelOne()
        {
            hero = new Hero(324, 136);
            MapEngine.LoadLevel(1);
            UpdateChunkViewMap(MapEngine.spawnRoom.startChunkX, MapEngine.spawnRoom.startChunkY, 5);
            UpdateTileViewMap();
            Tile.LoadBordersAndCorners(tileViewMap, Data.LEVEL1_WALL_TEXTURES);

            view = new View(hero.sprite.Position, new Vector2f(viewSizeX, viewSizeY));

            levelBackground = new LevelBackground(Data.background4, 110);

            Update += hero.Update;
            Update += CheckExitCollision;
            Update += UpdatePlayViewSize;
            Update += levelBackground.Relocate;

            Draw += levelBackground.Draw;
            Draw += DrawMap;
            Draw += DrawEntities;
        }

        //////////////////////////////////////////////////////////////////////////

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
            MainWindow.window.Draw(hero.sprite);
        }

        public static void ChangeScreenSize(string text)
        {
            Console.WriteLine(text);
        }

        public static void ChangeFPSLimit(string text)
        {
            Console.WriteLine(text);
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
            view.Center = new SFML.System.Vector2f(temp.X, temp.Y);

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
            Tile.LoadBordersAndCorners(tileViewMap, Data.LEVEL1_WALL_TEXTURES);
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
            
            
        }

        
    }
}
