using System;

namespace MapGen
{
    internal static class Level1Generator
    {
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

            /////////////////////////////////////////////////////////

            BasicGenerationMethods.BuildRoom(24,24,RoomSize.one_one);
            //BasicGenerationMethods.BuildRoom(0, 0, RoomSize.one_one);

            //Chunk spawn = MapGenerator.chunkMap[14, 14];

            //spawn.chunkPixelArray = BasicGenerationMethods.ReadMapPattern(Data.LEVEL1_ONE_ONE);

            for (int i = 0; i < 1; i++)
                GenerateChunks();
        }

        public static void GenerateChunks()
        {
            List<Chunk> rightChanksUpdateCopy = new List<Chunk>(MapGenerator.rightChanksUpdate);

            foreach (Chunk chunk in rightChanksUpdateCopy)
            {
                if (MapGenerator.chunkMap[chunk.x + 1, chunk.y].type == ChunkType.main)
                {
                    BasicGenerationMethods.CreateExit(chunk, 1);
                }
                else if (BasicGenerationMethods.BuildRoomRight(chunk.x + 1, chunk.y, true))
                {
                    BasicGenerationMethods.CreateExit(chunk, 1);
                }
                else
                {
                    MapGenerator.rightChanksUpdate.Remove(chunk);
                }
            }

            List<Chunk> leftChanksUpdateCopy = new List<Chunk>(MapGenerator.leftChanksUpdate);

            foreach (Chunk chunk in leftChanksUpdateCopy)
            {
                if (MapGenerator.chunkMap[chunk.x - 1, chunk.y].type == ChunkType.main)
                {
                    BasicGenerationMethods.CreateExit(chunk, 3);
                }
                else if (BasicGenerationMethods.BuildRoomLeft(chunk.x - 1, chunk.y, true))
                {
                    BasicGenerationMethods.CreateExit(chunk, 3);
                }
                else
                {
                    MapGenerator.leftChanksUpdate.Remove(chunk);
                }
            }

            List<Chunk> upChanksUpdateCopy = new List<Chunk>(MapGenerator.upChanksUpdate);

            foreach (Chunk chunk in upChanksUpdateCopy)
            {
                if (MapGenerator.chunkMap[chunk.x, chunk.y - 1].type == ChunkType.main)
                {
                    BasicGenerationMethods.CreateExit(chunk, 2);
                }
                else if (BasicGenerationMethods.BuildRoomUp(chunk.x, chunk.y - 1, true))
                {
                    BasicGenerationMethods.CreateExit(chunk, 2);
                }
                else
                {
                    MapGenerator.upChanksUpdate.Remove(chunk);
                }
            }

            List<Chunk> downChanksUpdateCopy = new List<Chunk>(MapGenerator.downChanksUpdate);

            foreach (Chunk chunk in downChanksUpdateCopy)
            {
                if (MapGenerator.chunkMap[chunk.x, chunk.y + 1].type == ChunkType.main)
                {
                    BasicGenerationMethods.CreateExit(chunk, 4);
                }
                else if (BasicGenerationMethods.BuildRoomDown(chunk.x, chunk.y + 1, true))
                {
                    BasicGenerationMethods.CreateExit(chunk, 4);
                }
                else
                {
                    MapGenerator.downChanksUpdate.Remove(chunk);
                }
            }
        }

    }
}
