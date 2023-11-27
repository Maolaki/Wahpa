using EntityEngine;
using MapGen;
using SFML.Graphics;
using SFML.System;

namespace WindowEngine
{
    internal static class ViewHandler
    {
        private static Chunk[,]? chunkViewMap { get; set; }
        public static Tile[,]? tileViewMap { get; private set; }
        private static Sprite background { get; set; }
        private static List<ExitEntity> exitArray { get; set; }
        private static int chunkStartX { get; set; }
        private static int chunkStartY { get; set; }
        private static int chunkSizeX { get; set; }
        private static int chunkSizeY { get; set; }
        private static int pixelSizeX { get; set; }
        private static int pixelSizeY { get; set; }
        private static View? view { get; set; }
        public static int viewSizeX { get; set; } = 336;
        public static int viewSizeY { get; set; } = 336;
        private static Hero? hero { get; set; }

        //////////////////////////////////////////////////////////////////////

        public static void Draw()
        {
            for (int x = 0; x < tileViewMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileViewMap.GetLength(1); y++)
                {
                    if (tileViewMap[x, y].sprite != null)
                        MainWindow.window.Draw(tileViewMap[x, y].sprite);

                }
            }

            MainWindow.window.Draw(hero.sprite);
        }

        public static void Start()
        {
            hero = new Hero(324, 136);
            MapEngine.LoadLevel(1);
            UpdateChunkViewMap(MapEngine.spawnRoom.startChunkX, MapEngine.spawnRoom.startChunkY, 5);
            UpdateTileViewMap();
            Tile.LoadBordersAndCorners(tileViewMap, Data.LEVEL1_WALL_TEXTURES);

            view = new View(hero.sprite.Position, new Vector2f(viewSizeX, viewSizeY));
        }

        public static void Update()
        {
            hero?.Update();
            CheckExitCollision();
            UpdateViewSize();
        }

        //////////////////////////////////////////////////////////////////////////

        public static void UpdateViewSize()
        {
            // Тут сделать установку размера экрана

            float centerPositionX;
            float centerPositionY;
            // Установка границ области просмотра, чтобы она не выходила за пределы карты
            if (viewSizeX / 2 > hero.sprite.Position.X)
            {
                centerPositionX = viewSizeX / 2;
            }
            else if (viewSizeX / 2 > pixelSizeX - hero.sprite.Position.X)
            {
                centerPositionX = pixelSizeX - viewSizeX / 2;
            }
            else 
            {
                centerPositionX = hero.sprite.Position.X;
            }

            if (viewSizeY / 2 > hero.sprite.Position.Y)
            {
                centerPositionY = viewSizeX / 2;
            }
            else if (viewSizeY / 2 > pixelSizeY - hero.sprite.Position.Y)
            {
                centerPositionY = pixelSizeY - viewSizeY / 2;
            }
            else 
            {
                centerPositionY = hero.sprite.Position.Y;
            }

            view.Center = new Vector2f(centerPositionX, centerPositionY);
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
