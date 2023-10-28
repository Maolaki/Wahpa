using SFML.Graphics;
using SFML.System;
using System.ComponentModel;
using WindowEngine;

namespace MapGen
{
    enum RoomSize
    {
        empty,
        one_one,
        one_five,
        two_one,
        two_two,
        three_two,
        four_two,
        five_five
    }

    internal static class MapGenerator
    {
        public static List<Chunk> rightChanksUpdate { get; set; } = new List<Chunk>();
        public static List<Chunk> leftChanksUpdate { get; set; } = new List<Chunk>();
        public static List<Chunk> upChanksUpdate { get; set; } = new List<Chunk>();
        public static List<Chunk> downChanksUpdate { get; set; } = new List<Chunk>();
        public static Chunk[,] chunkMap { get; set; }
        public static Tile[,] mainLayer { get; set; }

        public static void LoadHub()
        {
            char[,] charArray = BasicGenerationMethods.ReadMapPattern("..\\..\\..\\Maps\\Hub.txt");

            mainLayer = new Tile[charArray.GetLength(0), charArray.GetLength(1)];

            for (int x = 0; x < mainLayer.GetLength(0); x++)
            {
                for (int y = 0; y < mainLayer.GetLength(1); y++)
                {
                    mainLayer[x, y] = new Tile();
                    mainLayer[x, y].SetTile(charArray[x, y]);
                    mainLayer[x, y].sprite.Position = new Vector2f(x * Data.tileSize, y * Data.tileSize);
                }
            }
        }

        public static void LoadLevel(int level)
        {
            switch(level)
            {
                case 1:
                    Level1Generator.Generate();
                    UpdateMap();
                    
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }

        }

        public static void UpdateMap()
        {
            mainLayer = new Tile[chunkMap.GetLength(0) * 11, chunkMap.GetLength(1) * 11];

            for (int x = 0; x < mainLayer.GetLength(0); x++)
            {
                for (int y = 0; y < mainLayer.GetLength(1); y++)
                {
                    if (chunkMap[(int)Math.Floor(x / (double)11), (int)Math.Floor(y / (double)11)].type != ChunkType.empty && chunkMap[(int)Math.Floor(x / (double)11), (int)Math.Floor(y / (double)11)].chunkPixelArray[x % 11, y % 11] != ' ')
                    {
                        mainLayer[x, y] = new Tile();
                        mainLayer[x, y].SetTile(chunkMap[(int)Math.Floor(x / (double)11), (int)Math.Floor(y / (double)11)].chunkPixelArray[x % 11, y % 11]);
                        mainLayer[x, y].sprite.Position = new Vector2f(x * Data.tileSize, y * Data.tileSize);
                    }
                    else
                    {
                        mainLayer[x, y] = new Tile();
                    }
                }
            }
        }


    }
}
