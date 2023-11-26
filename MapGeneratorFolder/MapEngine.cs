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

    internal static class MapEngine
    {
        public static List<Chunk> rightChanksUpdate { get; set; } = new List<Chunk>();
        public static List<Chunk> leftChanksUpdate { get; set; } = new List<Chunk>();
        public static List<Chunk> upChanksUpdate { get; set; } = new List<Chunk>();
        public static List<Chunk> downChanksUpdate { get; set; } = new List<Chunk>();
        public static Chunk[,] chunkMap { get; set; }
        public static Room spawnRoom { get; set; }
        public static int heroCoordinateXSpawnPosition { get; set; }
        public static int heroCoordinateYSpawnPosition { get; set; }

        public static void LoadMap(in string mapPath)
        {
            char[,] mapArray = BasicGenerationMethods.ReadMapPattern(mapPath);

            for (int i = 0; i < mapArray.GetLength(0); i++)
            {
                for (int j = 0; j < mapArray.GetLength(1); j++)
                {
                    char[,] charChunkArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            charChunkArray[k, l] = mapArray[i * 11 + k, j * 11 + l];
                        }
                    }

                    chunkMap[i, j].charTileArray = charChunkArray;
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
