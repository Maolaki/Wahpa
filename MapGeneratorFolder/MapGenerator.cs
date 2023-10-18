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
        public static Chunk[,] Map { get; set; }
        public static Tile[,] mainLayer { get; set; }

        public static void LoadHub()
        {
            char[,] charArray = BasicGenerationMethods.ReadMapPattern("..\\..\\..\\Maps\\Hub.txt");

            mainLayer = new Tile[charArray.GetLength(0), charArray.GetLength(1)];

            for (int x = 0; x < mainLayer.GetLength(0); x++)
            {
                for (int y = 0; y < mainLayer.GetLength(1); y++)
                {
                    mainLayer[x, y] = new Tile(charArray[x, y]);
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
                    mainLayer = new Tile[Map.GetLength(0) * 11, Map.GetLength(1) * 11];

                    for (int x = 0; x < mainLayer.GetLength(0); x++)
                    {
                        for (int y = 0; y < mainLayer.GetLength(1); y++)
                        {
                            if (Map[(int)Math.Floor(x / (double)11), (int)Math.Floor(y / (double)11)].type != ChunkType.empty)
                                mainLayer[x, y] = new Tile(Map[(int)Math.Floor(x / (double)11), (int)Math.Floor(y / (double)11)].chunkPixelArray[x % 11, y % 11]);
                            else
                                mainLayer[x, y] = new Tile('0');

                            mainLayer[x, y].sprite.Position = new Vector2f(x * Data.tileSize, y * Data.tileSize);
                        }
                    }

                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }

        }


    }
}
