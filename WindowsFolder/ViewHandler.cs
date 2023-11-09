using EntityEngine;
using MapGen;
using SFML.Graphics;
using SFML.System;

namespace WindowEngine
{
    internal class ViewHandler
    {
        private Chunk[,] chunkViewMap { get; set; }
        public Tile[,] tileViewMap { get; private set; }

        private int startX { get; set; }
        private int startY { get; set; }
        private int sizeX { get; set; }
        private int sizeY { get; set; }
        public View view { get; private set; }
        private Hero hero { get; set; }

        //////////////////////////////////////////////////////////////////////

        public void Draw()
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

        public void Update()
        {
            hero.Update();
            MainWindow.window.SetView(new View(hero.sprite.Position, new Vector2f(1 * (MainWindow.window.Size.X / 192), 1 * (MainWindow.window.Size.Y / 108))));
        }

        //////////////////////////////////////////////////////////////////////////

        public void ChangeRoom()
        {
            if (hero.coordinateX < 0)
            {
                // переход влево
                int chunkCoordinateY = hero.coordinateY / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX - 1, startY + chunkCoordinateY, 128);
            }
            else if (hero.coordinateX > chunkViewMap.GetLength(0) * 11)
            {
                // переход вправо
                int chunkCoordinateY = hero.coordinateY / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX + 1, startY + chunkCoordinateY, -128);
            }
            else if (hero.coordinateY < 0)
            {
                // переход вверх
                int chunkCoordinateX = hero.coordinateX / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX, startY - 1, 0, 32);
            }
            else if (hero.coordinateY > chunkViewMap.GetLength(1) * 11)
            {
                // переход вниз
                int chunkCoordinateX = hero.coordinateX / (Data.LEVEL1_CHUNK_SIZE * Data.tileSize);
                UpdateChunkViewMap(startX, startY + sizeY, 0, -128);
            }

            UpdateTileViewMap();
        }

        //////////////////////////////////////////////////////////////////////
        private void UpdateTileViewMap()
        {
            tileViewMap = new Tile[chunkViewMap.GetLength(0) * 11 + 6, chunkViewMap.GetLength(1) * 11 + 6];

            for (int x = 0; x < chunkViewMap.GetLength(0); x++)
            {
                for (int y = 0; y < chunkViewMap.GetLength(1); y++)
                {
                    if (chunkViewMap[x / 11, y / 11].type != ChunkType.empty && chunkViewMap[x / 11, y / 11].chunkPixelArray[x % 11, y % 11] != ' ')
                    {
                        tileViewMap[x + 3, y + 3] = new Tile();
                        tileViewMap[x + 3, y + 3].SetTile(chunkViewMap[x / 11, y / 11].chunkPixelArray[x % 11, y % 11]);
                        tileViewMap[x + 3, y + 3].sprite.Position = new Vector2f(x * Data.tileSize, y * Data.tileSize);
                    }
                    else
                    {
                        tileViewMap[x + 3, y + 3] = new Tile();
                    }
                }
            }

            UpdateTileViewMapBorder();
        }

        private void UpdateTileViewMapBorder()
        {
            for (int j = 0; j < tileViewMap.GetLength(1); j++)
            {
                if (tileViewMap[3, j].type == TileType.wall)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        tileViewMap[i, j].SetTile('#');
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        tileViewMap[i, j].SetTile('0');
                    }
                }

                if (tileViewMap[tileViewMap.GetLength(0) - 3, j].type == TileType.wall)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        tileViewMap[tileViewMap.GetLength(0) - i, j].SetTile('#');
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        tileViewMap[tileViewMap.GetLength(0) - i, j].SetTile('0');
                    }
                }
                
            }
            
            for (int i = 3; i < tileViewMap.GetLength(0); i++)
            {
                if (tileViewMap[i, 3].type == TileType.wall)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tileViewMap[i, j].SetTile('#');
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tileViewMap[i, j].SetTile('0');
                    }
                }

                if (tileViewMap[i, tileViewMap.GetLength(1) - 3].type == TileType.wall)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tileViewMap[i, tileViewMap.GetLength(1) - j].SetTile('#');
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tileViewMap[i, tileViewMap.GetLength(1) - j].SetTile('0');
                    }
                }
            }
        }

        private void UpdateChunkViewMap(int chunkX, int chunkY, int heroCoordinateXSpawnPosition, int heroCoordinateYSpawnPosition = 0)
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

        private void SetStartXY()
        {
            while (MapEngine.chunkMap[startX, startY].BorderLeft == false)
            {
                startX--;
                sizeX++;
            }
            while (MapEngine.chunkMap[startX, startY].BorderUp == false)
            {
                startY--;
                sizeY++;
            }
        }

        
    }
}
