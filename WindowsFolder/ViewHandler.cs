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

        private static int startX { get; set; }
        private static int startY { get; set; }
        private static int sizeX { get; set; }
        private static int sizeY { get; set; }
        public static View? view { get; private set; }
        private static Hero? hero { get; set; }

        //////////////////////////////////////////////////////////////////////

        public static void Draw()
        {
            for (int x = 0; x < tileViewMap?.GetLength(0); x++)
            {
                for (int y = 0; y < tileViewMap.GetLength(1); y++)
                {
                    if (tileViewMap[x, y].sprite != null)
                        MainWindow.window.Draw(tileViewMap[x, y].sprite);

                }
            }

            MainWindow.window.Draw(hero?.sprite);
        }

        public static void Update()
        {
            hero?.Update();
            MainWindow.window.SetView(new View(hero.sprite.Position, new Vector2f(1 * (MainWindow.window.Size.X / 192), 1 * (MainWindow.window.Size.Y / 108))));
        }

        //////////////////////////////////////////////////////////////////////////

        public static void ChangeRoom()
        {
            if (hero?.coordinateX < 0)
            {
                // переход влево
                int chunkCoordinateY = hero.coordinateY / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX - 1, startY + chunkCoordinateY, 128);
            }
            else if (hero?.coordinateX > chunkViewMap?.GetLength(0) * 11)
            {
                // переход вправо
                int chunkCoordinateY = hero.coordinateY / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX + 1, startY + chunkCoordinateY, -128);
            }
            else if (hero?.coordinateY < 0)
            {
                // переход вверх
                int chunkCoordinateX = hero.coordinateX / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX, startY - 1, 0, 32);
            }
            else if (hero?.coordinateY > chunkViewMap?.GetLength(1) * 11)
            {
                // переход вниз
                int chunkCoordinateX = hero.coordinateX / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX, startY + sizeY, 0, -128);
            }

            UpdateTileViewMap();
        }

        //////////////////////////////////////////////////////////////////////
        private static void UpdateTileViewMap()
        {
            tileViewMap = new Tile[chunkViewMap.GetLength(0) * 11 + 6, chunkViewMap.GetLength(1) * 11 + 6];

            for (int i = 0; i < chunkViewMap.GetLength(0); i++)
            {
                for (int j = 0; j < chunkViewMap.GetLength(1); j++)
                {
                    char[,] charTileArray = chunkViewMap[i, j].charTileArray;

                    for (int i1 = 0; i1 < charTileArray.GetLength(0); i1++)
                    {
                        for (int j1 = 0; j1 < charTileArray.GetLength(1); j1++)
                        {
                            if (charTileArray[i1, j1] != ' ')
                            {
                                tileViewMap[i * 11 + i1, j * 11 + j1] = new Tile(i, j, chunkViewMap[i, j], Data.LEVEL1_WALL_TEXTURES);
                                tileViewMap[i * 11 + i1, j * 11 + j1].sprite.Position = new Vector2f(i * Data.tileSize, j * Data.tileSize);
                            }
                            else
                            {
                                tileViewMap[i * 11 + i1, j * 11 + j1] = new Tile();
                            }
                        }
                    }

                }
            }

            UpdateTileViewMapBorder();
        }

        private static void UpdateTileViewMapBorder()
        {
            for (int j = 0; j < tileViewMap.GetLength(1); j++)
            {
                if (tileViewMap[3, j].type == TileType.wall)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        tileViewMap[i, j].SetTile(TileType.wall, Path.Combine(Data.LEVEL1_WALL_TEXTURES, "tile1.png"));
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    { 
                        tileViewMap[i, j].SetTile(TileType.empty);
                    }
                }

                if (tileViewMap[tileViewMap.GetLength(0) - 3, j].type == TileType.wall)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        tileViewMap[tileViewMap.GetLength(0) - i, j].SetTile(TileType.wall, Path.Combine(Data.LEVEL1_WALL_TEXTURES, "tile1.png"));
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        tileViewMap[tileViewMap.GetLength(0) - i, j].SetTile(TileType.empty);
                    }
                }
                
            }
            
            for (int i = 3; i < tileViewMap.GetLength(0); i++)
            {
                if (tileViewMap[i, 3].type == TileType.wall)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tileViewMap[i, j].SetTile(TileType.wall, Path.Combine(Data.LEVEL1_WALL_TEXTURES, "tile1.png"));
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tileViewMap[i, j].SetTile(TileType.empty);
                    }
                }

                if (tileViewMap[i, tileViewMap.GetLength(1) - 3].type == TileType.wall)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tileViewMap[i, tileViewMap.GetLength(1) - j].SetTile(TileType.wall, Path.Combine(Data.LEVEL1_WALL_TEXTURES, "tile1.png"));
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tileViewMap[i, tileViewMap.GetLength(1) - j].SetTile(TileType.empty);
                    }
                }
            }
        }

        private static void UpdateChunkViewMap(int chunkX, int chunkY, int heroCoordinateXSpawnPosition, int heroCoordinateYSpawnPosition = 0)
        {
            startX = chunkX;
            startY = chunkY;

            sizeX = 1;
            sizeY = 1;

            SetStartXY();
            chunkViewMap = new Chunk[sizeX, sizeY];

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    chunkViewMap[i, j] = MapEngine.chunkMap[startX + i, startY + j];
                }
            }

            hero.coordinateX = (chunkX - startX) * 11 * 16 + 80 + heroCoordinateXSpawnPosition;
            hero.coordinateY = (chunkY - startX) * 11 * 16 + 112 + heroCoordinateYSpawnPosition;
        }

        private static void SetStartXY()
        {
            while (MapEngine.chunkMap[startX, startY].borderLeft == false)
            {
                startX--;
                sizeX++;
            }
            while (MapEngine.chunkMap[startX, startY].borderUp == false)
            {
                startY--;
                sizeY++;
            }
        }

        
    }
}
