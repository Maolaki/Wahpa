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
        private static int startX { get; set; }
        private static int startY { get; set; }
        private static int sizeX { get; set; }
        private static int sizeY { get; set; }
        public static View? view { get; private set; }
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

        public static void TestStart()
        {
            hero = new Hero(324, 136);
            MapEngine.LoadLevel(1);
            UpdateChunkViewMap(MapEngine.spawnRoom.startChunkX, MapEngine.spawnRoom.startChunkY, 324, 136);
            UpdateTileViewMap();
            Tile.LoadBordersAndCorners(tileViewMap, Data.LEVEL1_WALL_TEXTURES);
        }

        public static void Update()
        {
            hero?.Update();
            CheckExitCollision();
            MainWindow.window.SetView(new View(hero.sprite.Position, new Vector2f(60 * (MainWindow.window.Size.X / 192), 60 * (MainWindow.window.Size.Y / 108))));
        }

        //////////////////////////////////////////////////////////////////////////

        public static void ChangeRoom()
        {
            ResetSprites();

            if (hero?.coordinateX <= 80)
            {
                // переход влево
                int chunkCoordinateY = (hero.coordinateY - 5 * 16 + 1) / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX - 1, startY + chunkCoordinateY, 0, 0);
            }
            else if (hero?.coordinateX + hero.sizeX >= (tileViewMap.GetLength(0) - 5) * 16)
            {
                // переход вправо
                int chunkCoordinateY = (hero.coordinateY - 5 * 16 + 1) / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX + chunkViewMap.GetLength(0), startY + chunkCoordinateY, 0, 0);
            }
            else if (hero?.coordinateY <= 80)
            {
                // переход вверх
                int chunkCoordinateX = (hero.coordinateX - 5 * 16 + 1) / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX + chunkCoordinateX, startY - 1, 0, 0);
            }
            else if (hero?.coordinateY + hero.sizeY >= (tileViewMap.GetLength(1) - 5) * 16)
            {
                // переход вниз
                int chunkCoordinateX = (hero.coordinateX - 5 * 16 + 1) / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX + chunkCoordinateX, startY + chunkViewMap.GetLength(1), 0, 0);
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
        }

        public static void CheckExitCollision()
        {
            foreach(ExitEntity exit in exitArray)
            {
                if (Triggerable.CheckCollission(exit, hero))
                {
                    ChangeRoom();
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
                        exitArray.Add(new ExitEntity(i * 16, j * 16, 16, 16));
                    }
                }

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
                        exitArray.Add(new ExitEntity((tileViewMap.GetLength(0) - 1 - i) * 16, j * 16, 16, 16));
                    }
                }
                
            }
            
            for (int i = 5; i < tileViewMap.GetLength(0); i++)
            {
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
                        exitArray.Add(new ExitEntity(i * 16, j * 16, 16, 16));
                    }
                }

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
                        exitArray.Add(new ExitEntity(i * 16, (tileViewMap.GetLength(1) - 1 - j) * 16, 16, 16));
                    }
                }
            }
        }

        private static void UpdateChunkViewMap(int chunkX, int chunkY, int heroCoordinateXSpawnPosition, int heroCoordinateYSpawnPosition) 
        {
            Room room = MapEngine.chunkMap[chunkX, chunkY].room;
            startX = room.startChunkX;
            startY = room.startChunkY;
           
            sizeX = room.sizeX;
            sizeY = room.sizeY;

            chunkViewMap = new Chunk[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    chunkViewMap[i, j] = MapEngine.chunkMap[startX + i, startY + j];
                }
            }

            //hero.coordinateX = (chunkX - startX) * 11 * 16 + 5 * 16 + 50 + heroCoordinateXSpawnPosition;
            //hero.coordinateY = (chunkY - startX) * 11 * 16 + 5 * 16 + 50 + heroCoordinateYSpawnPosition;
            hero.coordinateX = 100;
            hero.coordinateY = 100;
        }

        
    }
}
