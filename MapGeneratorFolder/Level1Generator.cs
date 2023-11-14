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
                    MapEngine.chunkMap[i, j] = new Chunk(i, j);
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
                if (BasicGenerationMethods.BuildRoomRight(chunk.coordinateX + 1, chunk.coordinateY))
                {
                    BasicGenerationMethods.CreateExit(chunk, 1);
                }
            }

            List<Chunk> leftChanksUpdateCopy = new List<Chunk>(MapEngine.leftChanksUpdate);

            foreach (Chunk chunk in leftChanksUpdateCopy)
            {
                if (BasicGenerationMethods.BuildRoomLeft(chunk.coordinateX - 1, chunk.coordinateY))
                {
                    BasicGenerationMethods.CreateExit(chunk, 3);
                }
            }

            List<Chunk> upChanksUpdateCopy = new List<Chunk>(MapEngine.upChanksUpdate);

            foreach (Chunk chunk in upChanksUpdateCopy)
            {
                if (BasicGenerationMethods.BuildRoomUp(chunk.coordinateX, chunk.coordinateY - 1))
                {
                    BasicGenerationMethods.CreateExit(chunk, 2);
                }
            }

            List<Chunk> downChanksUpdateCopy = new List<Chunk>(MapEngine.downChanksUpdate);

            foreach (Chunk chunk in downChanksUpdateCopy)
            {
                if (BasicGenerationMethods.BuildRoomDown(chunk.coordinateX, chunk.coordinateY + 1))
                {
                    BasicGenerationMethods.CreateExit(chunk, 4);
                }
            }
        }

    }
}
