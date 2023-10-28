using SFML.Graphics;
using System.Collections.Generic;

namespace MapGen
{
    internal static class BasicGenerationMethods
    {
        ///////////////////////////////////////////////////
        // Dictionaries
        public static Dictionary<RoomSize, (int SizeX, int SizeY, int Chance, String pixelArray)> roomSizesRightLeft = new Dictionary<RoomSize, (int SizeX, int SizeY, int Chance, String pixelArray)>
        {
            { RoomSize.empty, (0, 0, 0, Data.LEVEL1_ONE_ONE) },
            { RoomSize.one_one, (1, 1, 1, Data.LEVEL1_ONE_ONE) },
            { RoomSize.two_one, (2, 1 , 3 , Data.LEVEL1_TWO_ONE) },
            { RoomSize.two_two, (2, 2, 3, Data.LEVEL1_TWO_TWO) },
            { RoomSize.three_two, (3, 2 , 3, Data.LEVEL1_THREE_TWO ) },
            { RoomSize.four_two, (4, 2 , 3, Data.LEVEL1_FOUR_TWO) },
            { RoomSize.five_five, (5, 5, 3, Data.LEVEL1_FIVE_FIVE) }
        };

        public static Dictionary<RoomSize, (int SizeX, int SizeY, int Chance, String pixelArray)> roomSizesUpDown = new Dictionary<RoomSize, (int SizeX, int SizeY, int Chance, String pixelArray)>
        {
            { RoomSize.empty, (0, 0, 0, Data.LEVEL1_ONE_ONE) },
            { RoomSize.one_one, (1, 1, 1, Data.LEVEL1_ONE_ONE) },
            { RoomSize.one_five, (1, 5, 3, Data.LEVEL1_ONE_FIVE) },
            { RoomSize.two_two, (2, 2, 3, Data.LEVEL1_TWO_TWO) },
            { RoomSize.four_two, (4, 2, 3, Data.LEVEL1_FOUR_TWO) },
            { RoomSize.five_five, (5, 5, 3, Data.LEVEL1_FIVE_FIVE) }
        };

        //////////////////////////////////////////////////////
        // Basic Funs

        public static char[,] ReadMapPattern(String filePath)
        {
            string[] mapStringArray = File.ReadAllLines(filePath);

            char[,] charArray = new char[mapStringArray[0].Length, mapStringArray.Length];
            for (int i = 0; i < mapStringArray.Length; i++)
            {
                for (int j = 0; j < mapStringArray[0].Length; j++)
                {
                    charArray[j, i] = mapStringArray[i][j];
                }
            }

            return charArray;
        }

        public static void SetBorderAndArray((int SizeX, int SizeY, int Chance, String pixelArray) roomDimensions, int startX, int startY)
        {
            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                for (int j = 0; j < roomDimensions.SizeY; j++)
                {
                    if (i == 0)
                    {
                        // влево
                        Chunk chunk = MapGenerator.chunkMap[startX + i, startY + j];
                        chunk.BorderLeft = true;
                        MapGenerator.leftChanksUpdate.Add(chunk);

                        if (MapGenerator.chunkMap[startX + i - 1, startY + j].type == ChunkType.main)
                            CreateExit(chunk, 3);
                    }

                    if (i == roomDimensions.SizeX - 1)
                    {
                        // вправо
                        Chunk chunk = MapGenerator.chunkMap[startX + i, startY + j];
                        chunk.BorderRight = true;
                        MapGenerator.rightChanksUpdate.Add(chunk);

                        if (MapGenerator.chunkMap[startX + i + 1, startY + j].type == ChunkType.main)
                            CreateExit(chunk, 1);
                    }

                    if (j == 0)
                    {
                        // вверх
                        Chunk chunk = MapGenerator.chunkMap[startX + i, startY + j];
                        chunk.BorderUp = true;
                        MapGenerator.upChanksUpdate.Add(chunk);

                        if (MapGenerator.chunkMap[startX + i, startY + j - 1].type == ChunkType.main)
                            CreateExit(chunk, 2);
                    }

                    if (j == roomDimensions.SizeY - 1)
                    {
                        // вниз
                        Chunk chunk = MapGenerator.chunkMap[startX + i, startY + j];
                        chunk.BorderDown = true;
                        MapGenerator.downChanksUpdate.Add(chunk);

                        if (MapGenerator.chunkMap[startX + i, startY + j + 1].type == ChunkType.main)
                            CreateExit(chunk, 4);
                    }


                }
            }
        }

        public static void CreateExit(Chunk chunk, int route)
        {
            switch (route)
            {
                case 1:
                    for (int i = 1; i < 10; i++)
                    {
                        // вправо 
                        chunk.chunkPixelArray[10, i] = '@';
                        MapGenerator.chunkMap[chunk.x + 1, chunk.y].chunkPixelArray[0, i] = '@';
                        MapGenerator.rightChanksUpdate.Remove(chunk);
                        MapGenerator.leftChanksUpdate.Remove(MapGenerator.chunkMap[chunk.x + 1, chunk.y]);
                    }
                    break;
                case 2:
                    for (int i = 1; i < 10; i++)
                    {
                        // вверх
                        chunk.chunkPixelArray[i, 0] = '@';
                        MapGenerator.chunkMap[chunk.x, chunk.y - 1].chunkPixelArray[i, 10] = '@';
                        MapGenerator.upChanksUpdate.Remove(chunk);
                        MapGenerator.downChanksUpdate.Remove(MapGenerator.chunkMap[chunk.x, chunk.y - 1]);
                    }
                    break;
                case 3:
                    for (int i = 1; i < 10; i++)
                    {
                        // влево
                        chunk.chunkPixelArray[0, i] = '@';
                        MapGenerator.chunkMap[chunk.x - 1, chunk.y].chunkPixelArray[10, i] = '@';
                        MapGenerator.leftChanksUpdate.Remove(chunk);
                        MapGenerator.rightChanksUpdate.Remove(MapGenerator.chunkMap[chunk.x - 1, chunk.y]);
                    }
                    break;
                case 4:
                    for (int i = 1; i < 10; i++)
                    {
                        // вниз
                        chunk.chunkPixelArray[i, 10] = '@';
                        MapGenerator.chunkMap[chunk.x, chunk.y + 1].chunkPixelArray[i, 0] = '@';
                        MapGenerator.downChanksUpdate.Remove(chunk);
                        MapGenerator.upChanksUpdate.Remove(MapGenerator.chunkMap[chunk.x, chunk.y + 1]);
                    }
                    break;
            }

        }

        ///////////////////////////////////////////////// 
        // Check chunks 

        public static void CheckXYRight(int startX, int startY, out int sizeX, out int sizeYUp, out int sizeYDown)
        {
            int maxJUp = 4;
            int maxJDown = 4;

            for (int i = 0; i < 5; i++)
            {
                if (MapGenerator.chunkMap[startX + i, startY].type != ChunkType.empty)
                {
                    sizeX = i;
                    sizeYUp = maxJUp;
                    sizeYDown = maxJDown;
                    return;
                }

                for (int j = 1; j <= 4; j++)
                {
                    if (MapGenerator.chunkMap[startX + i, startY - j].type != ChunkType.empty)
                    {
                        if (maxJUp > j)
                            maxJUp = j - 1;

                        break;
                    }
                }

                for (int j = 1; j <= 4; j++)
                {
                    if (MapGenerator.chunkMap[startX + i, startY + j].type != ChunkType.empty)
                    {
                        if (maxJDown > j)
                            maxJDown = j - 1;

                        break;
                    }
                }
            }

            sizeX = 5;
            sizeYUp = maxJUp;
            sizeYDown = maxJDown;
        }

        public static void CheckXYLeft(int startX, int startY, out int sizeX, out int sizeYUp, out int sizeYDown)
        {
            int maxJUp = 4;
            int maxJDown = 4;

            for (int i = 0; i < 5; i++)
            {
                if (MapGenerator.chunkMap[startX - i, startY].type != ChunkType.empty)
                {
                    sizeX = i;
                    sizeYUp = maxJUp;
                    sizeYDown = maxJDown;
                    return;
                }

                for (int j = 1; j <= 4; j++)
                {
                    if (MapGenerator.chunkMap[startX - i, startY - j].type != ChunkType.empty)
                    {
                        if (maxJUp > j)
                            maxJUp = j - 1;

                        break;
                    }
                }

                for (int j = 1; j <= 4; j++)
                {
                    if (MapGenerator.chunkMap[startX - i, startY + j].type != ChunkType.empty)
                    {
                        if (maxJDown > j)
                            maxJDown = j - 1;

                        break;
                    }
                }
            }

            sizeX = 5;
            sizeYUp = maxJUp;
            sizeYDown = maxJDown;
        }

        public static void CheckXYUp(int startX, int startY, out int sizeY, out int sizeXRight, out int sizeXLeft)
        {
            int maxIRight = 4;
            int maxILeft = 4;

            for (int j = 0; j < 5; j++)
            {
                if (MapGenerator.chunkMap[startX, startY - j].type != ChunkType.empty)
                {
                    sizeY = j;
                    sizeXRight = maxIRight;
                    sizeXLeft = maxILeft;

                    return;
                }

                for (int i = 1; i <= 4; i++)
                {
                    if (MapGenerator.chunkMap[startX + i, startY - j].type != ChunkType.empty)
                    {
                        if (maxIRight >= i)
                            maxIRight = i - 1;

                        break;
                    }
                }

                for (int i = 1; i <= 4; i++)
                {
                    if (MapGenerator.chunkMap[startX - i, startY - j].type != ChunkType.empty)
                    {
                        if (maxILeft >= i)
                            maxILeft = i - 1;

                        break;
                    }
                }
            }

            sizeY = 5;
            sizeXRight = maxIRight;
            sizeXLeft = maxILeft;
        }

        public static void CheckXYDown(int startX, int startY, out int sizeY, out int sizeXRight, out int sizeXLeft)
        {
            int maxIRight = 4;
            int maxILeft = 4;

            for (int j = 0; j < 5; j++)
            {
                if (MapGenerator.chunkMap[startX, startY + j].type != ChunkType.empty)
                {
                    sizeY = j;
                    sizeXRight = maxIRight;
                    sizeXLeft = maxILeft;

                    return;
                }

                for (int i = 1; i <= 4; i++)
                {
                    if (MapGenerator.chunkMap[startX + i, startY + j].type != ChunkType.empty)
                    {
                        if (maxIRight > i)
                            maxIRight = i - 1;

                        break;
                    }
                }

                for (int i = 1; i <= 4; i++)
                {
                    if (MapGenerator.chunkMap[startX - i, startY + j].type != ChunkType.empty)
                    {
                        if (maxILeft > i)
                            maxILeft = i - 1;

                        break;
                    }
                }
            }

            sizeY = 5;
            sizeXRight = maxIRight;
            sizeXLeft = maxILeft;
        }

        ////////////////////////////////////////
        /// Choose room

        public static RoomSize ChooseRoomRightLeft(int startX, int startY, out int sizeYUp, out int sizeYDown, bool isRight)
        {
            Random random = new Random();
            int sizeX;
            int sizeY;

            if (isRight)
                CheckXYRight(startX, startY, out sizeX, out sizeYUp, out sizeYDown);
            else
                CheckXYLeft(startX, startY, out sizeX, out sizeYUp, out sizeYDown);

            sizeY = sizeYUp + sizeYDown + 1;

            var availableRoomSizes = roomSizesRightLeft.Where(rs => sizeX >= rs.Value.SizeX && sizeY >= rs.Value.SizeY).ToList();

            int totalChance = availableRoomSizes.Sum(rs => rs.Value.Chance);
            int randomChance = random.Next(totalChance);

            foreach (var roomSize in availableRoomSizes)
            {
                if (randomChance < roomSize.Value.Chance)
                    return roomSize.Key;

                randomChance -= roomSize.Value.Chance;
            }

            throw new InvalidOperationException("Не удалось выбрать комнату");
        }

        public static RoomSize ChooseRoomUpDown(int startX, int startY, out int sizeXRight, out int sizeXLeft, bool isUp)
        {
            Random random = new Random();
            int sizeX;
            int sizeY;

            if (isUp)
                CheckXYUp(startX, startY, out sizeY, out sizeXRight, out sizeXLeft);
            else
                CheckXYDown(startX, startY, out sizeY, out sizeXRight, out sizeXLeft);

            sizeX = sizeXRight + sizeXLeft + 1;

            var availableRoomSizes = roomSizesUpDown.Where(rs => sizeX >= rs.Value.SizeX && sizeY >= rs.Value.SizeY).ToList();

            int totalChance = availableRoomSizes.Sum(rs => rs.Value.Chance);
            int randomChance = random.Next(totalChance);

            foreach (var roomSize in availableRoomSizes)
            {
                if (randomChance < roomSize.Value.Chance)
                    return roomSize.Key;

                randomChance -= roomSize.Value.Chance;
            }

            throw new InvalidOperationException("Не удалось выбрать комнату");
        }

        ////////////////////////////////////////////
        // Create Code

        public static void BuildRoom(int startX, int startY, RoomSize roomSize)
        {
            var roomDimensions = roomSizesRightLeft[roomSize];

            char[,] patternArray = ReadMapPattern(roomDimensions.pixelArray);

            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                for (int j = 0; j < roomDimensions.SizeY; j++)
                {
                    MapGenerator.chunkMap[startX + i, startY + j].type = ChunkType.main;
                    MapGenerator.chunkMap[startX + i, startY + j].size = roomSize;

                    char[,] chunkPixelArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkPixelArray[k, l] = patternArray[i * 11 + k, j * 11 + l];
                        }
                    }

                    MapGenerator.chunkMap[startX + i, startY + j].chunkPixelArray = chunkPixelArray;
                }
            }

            SetBorderAndArray(roomDimensions, startX, startY);
        }

        public static bool BuildRoomRight(int startX, int startY, bool isMain)
        {
            int SizeYUp;
            int SizeYDown;
            ChunkType chunkType = isMain ? ChunkType.main : ChunkType.extra;

            RoomSize roomSize = ChooseRoomRightLeft(startX, startY, out SizeYUp, out SizeYDown, true);

            if (roomSize == RoomSize.empty)
            {
                MapGenerator.rightChanksUpdate.Remove(MapGenerator.chunkMap[startX,startY]);
                return false;
            }

            var roomDimensions = roomSizesRightLeft[roomSize];
            char[,] patternArray = ReadMapPattern(roomDimensions.pixelArray);

            List<int> possibleStartPoints = new List<int>();
            for (int i = 0; i < roomDimensions.SizeY && i <= SizeYUp; i++)
            {
                if (i + SizeYDown + 1 >= roomDimensions.SizeY)
                {
                    possibleStartPoints.Add(startY - i);
                }
            }

            Random random = new Random();
            int randomStartY = possibleStartPoints[random.Next(possibleStartPoints.Count)];

            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                for (int j = 0; j < roomDimensions.SizeY; j++)
                {
                    MapGenerator.chunkMap[startX + i, randomStartY + j].type = chunkType;
                    MapGenerator.chunkMap[startX + i, randomStartY + j].size = roomSize;

                    char[,] chunkTilesArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkTilesArray[k, l] = patternArray[i * 11 + k, j * 11 + l];
                        }
                    }

                    MapGenerator.chunkMap[startX + i, randomStartY + j].chunkPixelArray = chunkTilesArray;
                }
            }
            SetBorderAndArray(roomDimensions, startX, randomStartY);

            return true;
        }

        public static bool BuildRoomLeft(int startX, int startY, bool isMain)
        {
            int SizeYUp;
            int SizeYDown;
            ChunkType chunkType = isMain ? ChunkType.main : ChunkType.extra;

            RoomSize roomSize = ChooseRoomRightLeft(startX, startY, out SizeYUp, out SizeYDown, false);

            if (roomSize == RoomSize.empty)
            {
                MapGenerator.rightChanksUpdate.Remove(MapGenerator.chunkMap[startX, startY]);
                return false;
            }

            var roomDimensions = roomSizesRightLeft[roomSize];
            char[,] patternArray = ReadMapPattern(roomDimensions.pixelArray);

            List<int> possibleStartPoints = new List<int>();
            for (int i = 0; i < roomDimensions.SizeY && i <= SizeYUp; i++)
            {
                if (i + SizeYDown + 1 >= roomDimensions.SizeY)
                {
                    possibleStartPoints.Add(startY - i);
                }
            }

            Random random = new Random();
            int randomStartY = possibleStartPoints[random.Next(possibleStartPoints.Count)];

            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                for (int j = 0; j < roomDimensions.SizeY; j++)
                {
                    MapGenerator.chunkMap[startX + i - roomDimensions.SizeX + 1, randomStartY + j].type = chunkType;
                    MapGenerator.chunkMap[startX + i - roomDimensions.SizeX + 1, randomStartY + j].size = roomSize;

                    char[,] chunkPixelArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkPixelArray[k, l] = patternArray[i * 11 + k, j * 11 + l];
                        }
                    }

                    MapGenerator.chunkMap[startX + i - roomDimensions.SizeX + 1, randomStartY + j].chunkPixelArray = chunkPixelArray;
                }
            }
            SetBorderAndArray(roomDimensions, startX - roomDimensions.SizeX + 1, randomStartY);

            return true;
        }

        public static bool BuildRoomUp(int startX, int startY, bool isMain)
        {
            int SizeXRight;
            int SizeXLeft;
            ChunkType chunkType = isMain ? ChunkType.main : ChunkType.extra;

            RoomSize roomSize = ChooseRoomUpDown(startX, startY, out SizeXRight, out SizeXLeft, true);

            if (roomSize == RoomSize.empty)
            {
                MapGenerator.rightChanksUpdate.Remove(MapGenerator.chunkMap[startX, startY]);
                return false;
            }

            var roomDimensions = roomSizesUpDown[roomSize];
            char[,] patternArray = ReadMapPattern(roomDimensions.pixelArray);

            List<int> possibleStartPoints = new List<int>();
            for (int i = 0; i < roomDimensions.SizeX && i <= SizeXLeft; i++)
            {
                if (i + SizeXRight + 1 >= roomDimensions.SizeX)
                {
                    possibleStartPoints.Add(startX - i);
                }
            }

            Random random = new Random();
            int randomStartX = possibleStartPoints[random.Next(possibleStartPoints.Count)];

            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                for (int j = roomDimensions.SizeY - 1; j >= 0; j--)
                {
                    MapGenerator.chunkMap[randomStartX + i, startY - j].type = chunkType;
                    MapGenerator.chunkMap[randomStartX + i, startY - j].size = roomSize;

                    char[,] chunkPixelArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkPixelArray[k, l] = patternArray[i * 11 + k, (roomDimensions.SizeY - 1 - j) * 11 + l];
                        }
                    }

                    MapGenerator.chunkMap[randomStartX + i, startY - j].chunkPixelArray = chunkPixelArray;
                }
            }
            SetBorderAndArray(roomDimensions, randomStartX, startY - roomDimensions.SizeY + 1);

            return true;
        }

        public static bool BuildRoomDown(int startX, int startY, bool isMain)
        {
            int SizeXRight;
            int SizeXLeft;
            ChunkType chunkType = isMain ? ChunkType.main : ChunkType.extra;

            RoomSize roomSize = ChooseRoomUpDown(startX, startY, out SizeXRight, out SizeXLeft, false);

            if (roomSize == RoomSize.empty)
            {
                MapGenerator.rightChanksUpdate.Remove(MapGenerator.chunkMap[startX, startY]);
                return false;
            }

            var roomDimensions = roomSizesUpDown[roomSize];
            char[,] patternArray = ReadMapPattern(roomDimensions.pixelArray);

            List<int> possibleStartPoints = new List<int>();
            for (int i = 0; i < roomDimensions.SizeX && i <= SizeXLeft; i++)
            {
                if (i + SizeXRight + 1 >= roomDimensions.SizeX)
                {
                    possibleStartPoints.Add(startX - i);
                }
            }

            Random random = new Random();
            int randomStartX = possibleStartPoints[random.Next(possibleStartPoints.Count)];

            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                for (int j = 0; j < roomDimensions.SizeY; j++)
                {
                    MapGenerator.chunkMap[randomStartX + i, startY + j].type = chunkType;
                    MapGenerator.chunkMap[randomStartX + i, startY + j].size = roomSize;

                    char[,] chunkPixelArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkPixelArray[k, l] = patternArray[i * 11 + k, j * 11 + l];
                        }
                    }

                    MapGenerator.chunkMap[randomStartX + i, startY + j].chunkPixelArray = chunkPixelArray;
                }
            }
            SetBorderAndArray(roomDimensions, randomStartX, startY);

            return true;
        }

    }
}
