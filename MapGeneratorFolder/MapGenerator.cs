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

        public static void LoadHub(string[] map)
        {
            char[,] charArray = new char[map[0].Length, map.Length];
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[0].Length; j++)
                {
                    charArray[j, i] = map[i][j];
                }
            }

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
