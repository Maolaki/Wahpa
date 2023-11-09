using System;

namespace MapGen
{
    internal static class Level1Generator
    {
        public static void Generate()
        {
            MapEngine.chunkMap = new Chunk[Data.LEVEL1_SIZEX, Data.LEVEL1_SIZEY];

            for (int i = 0; i < Data.LEVEL1_SIZEX; i++)
            {
                for (int j = 0; j < Data.LEVEL1_SIZEY; j++)
                {
                    MapEngine.chunkMap[i, j] = new Chunk();
                    MapEngine.chunkMap[i, j].x = i;
                    MapEngine.chunkMap[i, j].y = j;
                }
            }

            /////////////////////////////////////////////////////////

            BasicGenerationMethods.BuildRoom(24,24,RoomSize.three_two);

            //Chunk spawn = MapGenerator.chunkMap[14, 14];

            for (int i = 0; i < 3; i++)
                GenerateChunks();
        }

        public static void GenerateChunks()
        {
            List<Chunk> rightChanksUpdateCopy = new List<Chunk>(MapEngine.rightChanksUpdate);

            foreach (Chunk chunk in rightChanksUpdateCopy)
            {
                if (MapEngine.chunkMap[chunk.x + 1, chunk.y].type == ChunkType.main)
                {
                    BasicGenerationMethods.CreateExit(chunk, 1);
                }
                else if (BasicGenerationMethods.BuildRoomRight(chunk.x + 1, chunk.y, true))
                {
                    BasicGenerationMethods.CreateExit(chunk, 1);
                }
                else
                {
                    MapEngine.rightChanksUpdate.Remove(chunk);
                }
            }

            List<Chunk> leftChanksUpdateCopy = new List<Chunk>(MapEngine.leftChanksUpdate);

            foreach (Chunk chunk in leftChanksUpdateCopy)
            {
                if (MapEngine.chunkMap[chunk.x - 1, chunk.y].type == ChunkType.main)
                {
                    BasicGenerationMethods.CreateExit(chunk, 3);
                }
                else if (BasicGenerationMethods.BuildRoomLeft(chunk.x - 1, chunk.y, true))
                {
                    BasicGenerationMethods.CreateExit(chunk, 3);
                }
                else
                {
                    MapEngine.leftChanksUpdate.Remove(chunk);
                }
            }

            List<Chunk> upChanksUpdateCopy = new List<Chunk>(MapEngine.upChanksUpdate);

            foreach (Chunk chunk in upChanksUpdateCopy)
            {
                if (MapEngine.chunkMap[chunk.x, chunk.y - 1].type == ChunkType.main)
                {
                    BasicGenerationMethods.CreateExit(chunk, 2);
                }
                else if (BasicGenerationMethods.BuildRoomUp(chunk.x, chunk.y - 1, true))
                {
                    BasicGenerationMethods.CreateExit(chunk, 2);
                }
                else
                {
                    MapEngine.upChanksUpdate.Remove(chunk);
                }
            }

            List<Chunk> downChanksUpdateCopy = new List<Chunk>(MapEngine.downChanksUpdate);

            foreach (Chunk chunk in downChanksUpdateCopy)
            {
                if (MapEngine.chunkMap[chunk.x, chunk.y + 1].type == ChunkType.main)
                {
                    BasicGenerationMethods.CreateExit(chunk, 4);
                }
                else if (BasicGenerationMethods.BuildRoomDown(chunk.x, chunk.y + 1, true))
                {
                    BasicGenerationMethods.CreateExit(chunk, 4);
                }
                else
                {
                    MapEngine.downChanksUpdate.Remove(chunk);
                }
            }
        }

    }
}
