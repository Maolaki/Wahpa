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
            MapGenerator.Map = new Chunk[5,5];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    MapGenerator.Map[i, j] = new Chunk();
                    MapGenerator.Map[i, j].x = i;
                    MapGenerator.Map[i, j].y = j;
                }
            }

            rightChanksUpdate = new List<Chunk>();
            leftChanksUpdate = new List<Chunk>();
            upChanksUpdate = new List<Chunk>();
            downChanksUpdate = new List<Chunk>();

            /////////////////////////////////////////////////////////

            BasicGenerationMethods.BuildRoom(0,0,RoomSize.one_one);

            //Chunk spawn = MapGenerator.Map[14, 14];

            //spawn.chunkPixelArray = BasicGenerationMethods.ReadMapPattern(Data.LEVEL1_ONE_ONE);



            //rightChanksUpdate.Add(spawn);
            //leftChanksUpdate.Add(spawn);
            //upChanksUpdate.Add(spawn);
            //downChanksUpdate.Add(spawn);

            //for (int i = 0; i < 2; i++)
            //    GenerateChunks();
        }

        public static void GenerateChunks()
        {
            List<Chunk> rightChanksUpdateCopy = new List<Chunk>(rightChanksUpdate);
            List<Chunk> leftChanksUpdateCopy = new List<Chunk>(leftChanksUpdate);
            List<Chunk> upChanksUpdateCopy = new List<Chunk>(upChanksUpdate);
            List<Chunk> downChanksUpdateCopy = new List<Chunk>(downChanksUpdate);

            foreach (Chunk chunk in rightChanksUpdateCopy)
            {
                if (MapGenerator.Map[chunk.x + 1, chunk.y].type == ChunkType.main)
                {
                    for (int i = 1; i < 11; i++)
                        chunk.chunkPixelArray[10, i] = '0';
                    for (int i = 1; i < 11; i++)
                        MapGenerator.Map[chunk.x + 1, chunk.y].chunkPixelArray[0, i] = '0';
                }
                else if (BasicGenerationMethods.BuildRoomRight(chunk.x + 1, chunk.y, true))
                {
                    for (int i = 1; i < 11; i++)
                        chunk.chunkPixelArray[10, i] = '0';

                    rightChanksUpdate.Remove(chunk);
                }
            }

            foreach (Chunk chunk in leftChanksUpdateCopy)
            {
                if (MapGenerator.Map[chunk.x - 1, chunk.y].type == ChunkType.main)
                {
                    for (int i = 1; i < 11; i++)
                        chunk.chunkPixelArray[0, i] = '0';
                    for (int i = 1; i < 11; i++)
                        MapGenerator.Map[chunk.x - 1, chunk.y].chunkPixelArray[10, i] = '0';
                }
                else if (BasicGenerationMethods.BuildRoomLeft(chunk.x - 1, chunk.y, true))
                {
                    for (int i = 1; i < 11; i++)
                        chunk.chunkPixelArray[0, i] = '0';

                    leftChanksUpdate.Remove(chunk);
                }
            }

            foreach (Chunk chunk in upChanksUpdateCopy)
            {
                if (MapGenerator.Map[chunk.x, chunk.y - 1].type == ChunkType.main)
                {
                    for (int i = 1; i < 11; i++)
                        chunk.chunkPixelArray[i, 0] = '0';
                    for (int i = 1; i < 11; i++)
                        MapGenerator.Map[chunk.x, chunk.y - 1].chunkPixelArray[i, 10] = '0';
                }
                else if (BasicGenerationMethods.BuildRoomUp(chunk.x, chunk.y - 1, true))
                {
                    for (int i = 1; i < 11; i++)
                        chunk.chunkPixelArray[i, 0] = '0';

                    upChanksUpdate.Remove(chunk);
                }
            }

            foreach (Chunk chunk in downChanksUpdateCopy)
            {
                if (MapGenerator.Map[chunk.x, chunk.y + 1].type == ChunkType.main)
                {
                    for (int i = 1; i < 11; i++)
                        chunk.chunkPixelArray[i, 10] = '0';
                    for (int i = 1; i < 11; i++)
                        MapGenerator.Map[chunk.x, chunk.y + 1].chunkPixelArray[i, 0] = '0';
                }
                else if (BasicGenerationMethods.BuildRoomDown(chunk.x, chunk.y + 1, true))
                {
                    for (int i = 1; i < 11; i++)
                        chunk.chunkPixelArray[i, 10] = '0';

                    downChanksUpdate.Remove(chunk);
                }
            }
        }

    }
}
