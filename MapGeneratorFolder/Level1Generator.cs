using System;

namespace MapGen
{
    internal static class Level1Generator
    {
        public static List<Chunk> rightChanksUpdate { get; set; }
        public static List<Chunk> leftChanksUpdate { get; set; }
        public static List<Chunk> upChanksUpdate { get; set; }
        public static List<Chunk> downChanksUpdate { get; set; }

        public static void Generate()
        {
            MapGenerator.chunkMap = new Chunk[Data.LEVEL1_SIZEX, Data.LEVEL1_SIZEY];

            for (int i = 0; i < Data.LEVEL1_SIZEX; i++)
            {
                for (int j = 0; j < Data.LEVEL1_SIZEY; j++)
                {
                    MapGenerator.chunkMap[i, j] = new Chunk();
                    MapGenerator.chunkMap[i, j].x = i;
                    MapGenerator.chunkMap[i, j].y = j;
                }
            }

            rightChanksUpdate = new List<Chunk>();
            leftChanksUpdate = new List<Chunk>();
            upChanksUpdate = new List<Chunk>();
            downChanksUpdate = new List<Chunk>();

            /////////////////////////////////////////////////////////

            BasicGenerationMethods.BuildRoom(24,24,RoomSize.two_two);

            //Chunk spawn = MapGenerator.chunkMap[14, 14];

            //spawn.chunkPixelArray = BasicGenerationMethods.ReadMapPattern(Data.LEVEL1_ONE_ONE);

            for (int i = 0; i < 3; i++)
                GenerateChunks();
        }

        public static void GenerateChunks()
        {
            List<Chunk> rightChanksUpdateCopy = new List<Chunk>(rightChanksUpdate);
            List<Chunk> leftChanksUpdateCopy = new List<Chunk>(leftChanksUpdate);
            List<Chunk> upChanksUpdateCopy = new List<Chunk>(upChanksUpdate);
            List<Chunk> downChanksUpdateCopy = new List<Chunk>(downChanksUpdate);

            foreach (Chunk chunk in rightChanksUpdateCopy)
            {
                if (MapGenerator.chunkMap[chunk.x + 1, chunk.y].type == ChunkType.main)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        chunk.chunkPixelArray[10, i] = '0';
                        MapGenerator.chunkMap[chunk.x + 1, chunk.y].chunkPixelArray[0, i] = '0';
                    }

                    rightChanksUpdate.Remove(chunk);
                }
                else if (BasicGenerationMethods.BuildRoomRight(chunk.x + 1, chunk.y, true))
                {
                    for (int i = 1; i < 10; i++)
                    {
                        chunk.chunkPixelArray[10, i] = '0';
                        MapGenerator.chunkMap[chunk.x + 1, chunk.y].chunkPixelArray[0, i] = '0';
                    }

                    rightChanksUpdate.Remove(chunk);
                }
                else
                {
                    rightChanksUpdate.Remove(chunk);
                }
            }

            foreach (Chunk chunk in leftChanksUpdateCopy)
            {
                if (MapGenerator.chunkMap[chunk.x - 1, chunk.y].type == ChunkType.main)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        chunk.chunkPixelArray[0, i] = '0';
                        MapGenerator.chunkMap[chunk.x - 1, chunk.y].chunkPixelArray[10, i] = '0';
                    }

                    leftChanksUpdate.Remove(chunk);
                }
                else if (BasicGenerationMethods.BuildRoomLeft(chunk.x - 1, chunk.y, true))
                {
                    for (int i = 1; i < 10; i++)
                    {
                        chunk.chunkPixelArray[0, i] = '0';
                        MapGenerator.chunkMap[chunk.x - 1, chunk.y].chunkPixelArray[10, i] = '0';
                    }

                    leftChanksUpdate.Remove(chunk);
                }
                else
                {
                    leftChanksUpdate.Remove(chunk);
                }
            }

            foreach (Chunk chunk in upChanksUpdateCopy)
            {
                if (MapGenerator.chunkMap[chunk.x, chunk.y - 1].type == ChunkType.main)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        chunk.chunkPixelArray[i, 0] = '0';
                        MapGenerator.chunkMap[chunk.x, chunk.y - 1].chunkPixelArray[i, 10] = '0';
                    }

                    upChanksUpdate.Remove(chunk);
                }
                else if (BasicGenerationMethods.BuildRoomUp(chunk.x, chunk.y - 1, true))
                {
                    for (int i = 1; i < 10; i++)
                    {
                        chunk.chunkPixelArray[i, 0] = '0';
                        MapGenerator.chunkMap[chunk.x, chunk.y - 1].chunkPixelArray[i, 10] = '0';
                    }

                    upChanksUpdate.Remove(chunk);
                }
                else
                {
                    upChanksUpdate.Remove(chunk);
                }
            }

            foreach (Chunk chunk in downChanksUpdateCopy)
            {
                if (MapGenerator.chunkMap[chunk.x, chunk.y + 1].type == ChunkType.main)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        chunk.chunkPixelArray[i, 10] = '0';
                        MapGenerator.chunkMap[chunk.x, chunk.y + 1].chunkPixelArray[i, 0] = '0';
                    }

                    downChanksUpdate.Remove(chunk);
                }
                else if (BasicGenerationMethods.BuildRoomDown(chunk.x, chunk.y + 1, true))
                {
                    for (int i = 1; i < 10; i++)
                    {
                        chunk.chunkPixelArray[i, 10] = '0';
                        MapGenerator.chunkMap[chunk.x, chunk.y + 1].chunkPixelArray[i, 0] = '0';
                    }

                    downChanksUpdate.Remove(chunk);
                }
                else
                {
                    downChanksUpdate.Remove(chunk);
                }
            }
        }

    }
}
