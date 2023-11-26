using EntityEngine;
using SFML.Graphics;
using System.Collections.Generic;

namespace MapGen
{
    internal static class BasicGenerationMethods
    {
        public struct RoomInfo
        {
            public int sizeX;
            public int sizeY;
            public int chance;
            public string chunkTemplatePath;

            public RoomInfo(int sizeX, int sizeY, int chance, string chunkTemplatePath)
            {
                this.sizeX = sizeX;
                this.sizeY = sizeY;
                this.chance = chance;
                this.chunkTemplatePath = chunkTemplatePath;
            }
        }

        ///////////////////////////////////////////////////
        // Dictionaries

        public static Dictionary<RoomSize, RoomInfo> roomSizesRightLeft = new Dictionary<RoomSize, RoomInfo>
        {
            { RoomSize.empty, new (0, 0, 40, Data.LEVEL1_ONE_ONE) },
            { RoomSize.one_one, new (1, 1, 1, Data.LEVEL1_ONE_ONE) },
            { RoomSize.two_one, new (2, 1 , 3 , Data.LEVEL1_TWO_ONE) },
            { RoomSize.two_two, new (2, 2, 3, Data.LEVEL1_TWO_TWO) },
            { RoomSize.three_two, new (3, 2 , 3, Data.LEVEL1_THREE_TWO ) },
            { RoomSize.four_two, new (4, 2 , 3, Data.LEVEL1_FOUR_TWO) },
            { RoomSize.five_five, new (5, 5, 3, Data.LEVEL1_FIVE_FIVE) }
        };

        public static Dictionary<RoomSize, RoomInfo> roomSizesUpDown = new Dictionary<RoomSize, RoomInfo>
        {
            { RoomSize.empty, new (0, 0, 40, Data.LEVEL1_ONE_ONE) },
            { RoomSize.one_one, new (1, 1, 1, Data.LEVEL1_ONE_ONE) },
            { RoomSize.one_five, new (1, 5, 3, Data.LEVEL1_ONE_FIVE) },
            { RoomSize.two_two, new (2, 2, 3, Data.LEVEL1_TWO_TWO) },
            { RoomSize.four_two, new (4, 2, 3, Data.LEVEL1_FOUR_TWO) },
            { RoomSize.five_five, new (5, 5, 3, Data.LEVEL1_FIVE_FIVE) }
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

        public static void SetBorderAndRoom(RoomInfo roomDimensions, int startX, int startY)
        {
            Room room = new Room();
            room.startChunkX = startX;
            room.startChunkY = startY;
            room.sizeX = roomDimensions.sizeX;
            room.sizeY = roomDimensions.sizeY;

            for (int i = 0; i < roomDimensions.sizeX; i++)
            {
                for (int j = 0; j < roomDimensions.sizeY; j++)
                {
                    Chunk chunk = MapEngine.chunkMap[startX + i, startY + j];
                    chunk.room = room;

                    if (i == 0)
                    {
                        // влево
                        chunk.borderLeft = true;
                        MapEngine.leftChanksUpdate.Add(chunk);

                        if (MapEngine.chunkMap[startX + i - 1, startY + j].room != null && Data.random.Next(0, 4) == 0)
                            CreateExit(chunk, 3);
                    }

                    if (i == roomDimensions.sizeX - 1)
                    {
                        // вправо
                        chunk.borderRight = true;
                        MapEngine.rightChanksUpdate.Add(chunk);

                        if (MapEngine.chunkMap[startX + i + 1, startY + j].room != null && Data.random.Next(0, 4) == 0)
                            CreateExit(chunk, 1);
                    }

                    if (j == 0)
                    {
                        // вверх
                        chunk.borderUp = true;
                        MapEngine.upChanksUpdate.Add(chunk);

                        if (MapEngine.chunkMap[startX + i, startY + j - 1].room != null && Data.random.Next(0, 4) == 0)
                            CreateExit(chunk, 2);
                    }

                    if (j == roomDimensions.sizeY - 1)
                    {
                        // вниз
                        chunk.borderDown = true;
                        MapEngine.downChanksUpdate.Add(chunk);

                        if (MapEngine.chunkMap[startX + i, startY + j + 1].room != null && Data.random.Next(0, 4) == 0)
                            CreateExit(chunk, 4);
                    }


                }
            }
        }

        public static void CreateExit(Chunk chunk, int route)
        {
            Chunk secondChunk; 

            switch (route)
            {
                case 1:
                    secondChunk = MapEngine.chunkMap[chunk.coordinateX + 1, chunk.coordinateY];

                    for (int i = 1; i < 10; i++)
                    {
                        // вправо 
                        chunk.charTileArray[10, i] = '0';
                        secondChunk.charTileArray[0, i] = '0';
                        chunk.exitRight = true;
                        secondChunk.exitLeft = true;
                    }

                    MapEngine.rightChanksUpdate.Remove(chunk);
                    MapEngine.leftChanksUpdate.Remove(secondChunk);
                    break;
                case 2:
                    secondChunk = MapEngine.chunkMap[chunk.coordinateX, chunk.coordinateY - 1];

                    for (int i = 1; i < 10; i++)
                    {
                        // вверх
                        chunk.charTileArray[i, 0] = '0';
                        secondChunk.charTileArray[i, 10] = '0';
                        chunk.exitUp = true;
                        secondChunk.exitDown = true;
                    }

                    MapEngine.upChanksUpdate.Remove(chunk);
                    MapEngine.downChanksUpdate.Remove(secondChunk);
                    break;
                case 3:
                    secondChunk = MapEngine.chunkMap[chunk.coordinateX - 1, chunk.coordinateY];

                    for (int i = 1; i < 10; i++)
                    {
                        // влево
                        chunk.charTileArray[0, i] = '0';
                        secondChunk.charTileArray[10, i] = '0';
                        chunk.exitLeft = true;
                        secondChunk.exitRight = true;
                    }

                    MapEngine.leftChanksUpdate.Remove(chunk);
                    MapEngine.rightChanksUpdate.Remove(secondChunk);
                    break;
                case 4:
                    secondChunk = MapEngine.chunkMap[chunk.coordinateX, chunk.coordinateY + 1];

                    for (int i = 1; i < 10; i++)
                    {
                        // вниз
                        chunk.charTileArray[i, 10] = '0';
                        secondChunk.charTileArray[i, 0] = '0';
                        chunk.exitDown = true;
                        secondChunk.exitUp = true;
                    }

                    MapEngine.downChanksUpdate.Remove(chunk);
                    MapEngine.upChanksUpdate.Remove(secondChunk);
                    break;
            }

        }

        ///////////////////////////////////////////////// 
        // Check chunks 

        public static void CheckRightSpace(int startX, int startY, ref int sizeX, ref int sizeYUp, ref int sizeYDown)
        {
            sizeYUp = 4;
            sizeYDown = 4;

            for (int i = 0; i < 5; i++)
            {
                if (MapEngine.chunkMap[startX + i, startY].room != null)
                {
                    sizeX = i;
                    return;
                }

                for (int j = 1; j <= sizeYUp; j++)
                {
                    if (MapEngine.chunkMap[startX + i, startY - j].room != null)
                    {
                        sizeYUp = j - 1;

                        break;
                    }
                }

                for (int j = 1; j <= sizeYDown; j++)
                {
                    if (MapEngine.chunkMap[startX + i, startY + j].room != null)
                    {
                        sizeYDown = j - 1;

                        break;
                    }
                }
            }

            sizeX = 5;
        }

        public static void CheckLeftSpace(int startX, int startY, ref int sizeX, ref int sizeYUp, ref int sizeYDown)
        {
            sizeYUp = 4;
            sizeYDown = 4;

            for (int i = 0; i < 5; i++)
            {
                if (MapEngine.chunkMap[startX - i, startY].room != null)
                {
                    sizeX = i;
                    return;
                }

                for (int j = 1; j <= sizeYUp; j++)
                {
                    if (MapEngine.chunkMap[startX - i, startY - j].room != null)
                    {
                        sizeYUp = j - 1;

                        break;
                    }
                }

                for (int j = 1; j <= sizeYDown; j++)
                {
                    if (MapEngine.chunkMap[startX - i, startY + j].room != null)
                    {
                        sizeYDown = j - 1;

                        break;
                    }
                }
            }

            sizeX = 5;
        }

        public static void CheckUpSpace(int startX, int startY, ref int sizeY, ref int sizeXRight, ref int sizeXLeft)
        {
            sizeXRight = 4;
            sizeXLeft = 4;

            for (int j = 0; j < 5; j++)
            {
                if (MapEngine.chunkMap[startX, startY - j].room != null)
                {
                    sizeY = j;
                    return;
                }

                for (int i = 1; i <= sizeXRight; i++)
                {
                    if (MapEngine.chunkMap[startX + i, startY - j].room != null)
                    {
                        sizeXRight = i - 1;

                        break;
                    }
                }

                for (int i = 1; i <= sizeXLeft; i++)
                {
                    if (MapEngine.chunkMap[startX - i, startY - j].room != null)
                    {
                        sizeXLeft = i - 1;

                        break;
                    }
                }
            }

            sizeY = 5;
        }

        public static void CheckDownSpace(int startX, int startY, ref int sizeY, ref int sizeXRight, ref int sizeXLeft)
        {
            sizeXRight = 4;
            sizeXLeft = 4;

            for (int j = 0; j < 5; j++)
            {
                if (MapEngine.chunkMap[startX, startY + j].room != null)
                {
                    sizeY = j;
                    return;
                }

                for (int i = 1; i <= sizeXRight; i++)
                {
                    if (MapEngine.chunkMap[startX + i, startY + j].room != null)
                    {
                        sizeXRight = i - 1;

                        break;
                    }
                }

                for (int i = 1; i <= sizeXLeft; i++)
                {
                    if (MapEngine.chunkMap[startX - i, startY + j].room != null)
                    {
                        sizeXLeft = i - 1;

                        break;
                    }
                }
            }

            sizeY = 5;
        }

        ////////////////////////////////////////
        /// Choose room

        public static RoomSize ChooseRoomRightLeft(int startX, int startY, ref int sizeYUp, ref int sizeYDown, bool isRight)
        {
            int sizeX = 0;
            int sizeY;

            if (isRight)
                CheckRightSpace(startX, startY, ref sizeX, ref sizeYUp, ref sizeYDown);
            else
                CheckLeftSpace(startX, startY, ref sizeX, ref sizeYUp, ref sizeYDown);

            sizeY = sizeYUp + sizeYDown + 1;

            var availableRoomSizes = roomSizesRightLeft.Where(rs => sizeX >= rs.Value.sizeX && sizeY >= rs.Value.sizeY).ToList();

            int totalChance = availableRoomSizes.Sum(rs => rs.Value.chance);
            int randomChance = Data.random.Next(totalChance);

            foreach (var roomSize in availableRoomSizes)
            {
                if (randomChance < roomSize.Value.chance)
                    return roomSize.Key;

                randomChance -= roomSize.Value.chance;
            }

            throw new InvalidOperationException("Не удалось выбрать комнату");
        }

        public static RoomSize ChooseRoomUpDown(int startX, int startY, ref int sizeXRight, ref int sizeXLeft, bool isUp)
        {
            Random random = new Random();
            int sizeX;
            int sizeY = 0;

            if (isUp)
                CheckUpSpace(startX, startY, ref sizeY, ref sizeXRight, ref sizeXLeft);
            else
                CheckDownSpace(startX, startY, ref sizeY, ref sizeXRight, ref sizeXLeft);

            sizeX = sizeXRight + sizeXLeft + 1;

            var availableRoomSizes = roomSizesUpDown.Where(rs => sizeX >= rs.Value.sizeX && sizeY >= rs.Value.sizeY).ToList();

            int totalChance = availableRoomSizes.Sum(rs => rs.Value.chance);
            int randomChance = random.Next(totalChance);

            foreach (var roomSize in availableRoomSizes)
            {
                if (randomChance < roomSize.Value.chance)
                    return roomSize.Key;

                randomChance -= roomSize.Value.chance;
            }

            throw new InvalidOperationException("Не удалось выбрать комнату");
        }

        ////////////////////////////////////////////
        // Create Code

        public static void BuildRoom(int startX, int startY, RoomSize roomSize)
        {
            var roomDimensions = roomSizesRightLeft[roomSize];

            char[,] patternArray = ReadMapPattern(roomDimensions.chunkTemplatePath);

            for (int i = 0; i < roomDimensions.sizeX; i++)
            {
                for (int j = 0; j < roomDimensions.sizeY; j++)
                {
                    char[,] charChunkArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            charChunkArray[k, l] = patternArray[i * 11 + k, j * 11 + l];
                        }
                    }

                    MapEngine.chunkMap[startX + i, startY + j].charTileArray = charChunkArray;
                }
            }

            SetBorderAndRoom(roomDimensions, startX, startY);
            MapEngine.spawnRoom = MapEngine.chunkMap[startX, startY].room;
        }

        public static bool BuildRoomRight(int startX, int startY)
        {
            int SizeYUp = 0;
            int SizeYDown = 0;

            RoomSize roomSize = ChooseRoomRightLeft(startX, startY, ref SizeYUp, ref SizeYDown, true);

            if (roomSize == RoomSize.empty)
            {
                MapEngine.rightChanksUpdate.Remove(MapEngine.chunkMap[startX - 1,startY]);
                return false;
            }

            var roomDimensions = roomSizesRightLeft[roomSize];
            char[,] patternArray = ReadMapPattern(roomDimensions.chunkTemplatePath);

            List<int> possibleStartPoints = new List<int>();
            for (int j = 0; j < roomDimensions.sizeY && j <= SizeYUp; j++)
            {
                if (j + SizeYDown + 1 >= roomDimensions.sizeY)
                {
                    possibleStartPoints.Add(startY - j);
                }
            }

            int randomStartY = possibleStartPoints[Data.random.Next(possibleStartPoints.Count)];

            for (int i = 0; i < roomDimensions.sizeX; i++)
            {
                for (int j = 0; j < roomDimensions.sizeY; j++)
                {
                    char[,] chunkTileArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkTileArray[k, l] = patternArray[i * 11 + k, j * 11 + l];
                        }
                    }

                    MapEngine.chunkMap[startX + i, randomStartY + j].charTileArray = chunkTileArray;
                }
            }
            SetBorderAndRoom(roomDimensions, startX, randomStartY);

            return true;
        }

        public static bool BuildRoomLeft(int startX, int startY)
        {
            int SizeYUp = 0;
            int SizeYDown = 0;

            RoomSize roomSize = ChooseRoomRightLeft(startX, startY, ref SizeYUp, ref SizeYDown, false);

            if (roomSize == RoomSize.empty)
            {
                MapEngine.leftChanksUpdate.Remove(MapEngine.chunkMap[startX + 1, startY]);
                return false;
            }

            var roomDimensions = roomSizesRightLeft[roomSize];
            char[,] patternArray = ReadMapPattern(roomDimensions.chunkTemplatePath);

            List<int> possibleStartPoints = new List<int>();
            for (int j = 0; j < roomDimensions.sizeY && j <= SizeYUp; j++)
            {
                if (j + SizeYDown + 1 >= roomDimensions.sizeY)
                {
                    possibleStartPoints.Add(startY - j);
                }
            }

            int randomStartY = possibleStartPoints[Data.random.Next(possibleStartPoints.Count)];

            for (int i = 0; i < roomDimensions.sizeX; i++)
            {
                for (int j = 0; j < roomDimensions.sizeY; j++)
                {
                    char[,] chunkTileArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkTileArray[k, l] = patternArray[i * 11 + k, j * 11 + l];
                        }
                    }

                    MapEngine.chunkMap[startX + i - roomDimensions.sizeX + 1, randomStartY + j].charTileArray = chunkTileArray;
                }
            }
            SetBorderAndRoom(roomDimensions, startX - roomDimensions.sizeX + 1, randomStartY);

            return true;
        }

        public static bool BuildRoomUp(int startX, int startY)
        {
            int SizeXRight = 0;
            int SizeXLeft = 0;

            RoomSize roomSize = ChooseRoomUpDown(startX, startY, ref SizeXRight, ref SizeXLeft, true);

            if (roomSize == RoomSize.empty)
            {
                MapEngine.upChanksUpdate.Remove(MapEngine.chunkMap[startX, startY + 1]);
                return false;
            }

            var roomDimensions = roomSizesUpDown[roomSize];
            char[,] patternArray = ReadMapPattern(roomDimensions.chunkTemplatePath);

            List<int> possibleStartPoints = new List<int>();
            for (int i = 0; i < roomDimensions.sizeX && i <= SizeXLeft; i++)
            {
                if (i + SizeXRight + 1 >= roomDimensions.sizeX)
                {
                    possibleStartPoints.Add(startX - i);
                }
            }

            int randomStartX = possibleStartPoints[Data.random.Next(possibleStartPoints.Count)];

            for (int i = 0; i < roomDimensions.sizeX; i++)
            {
                for (int j = roomDimensions.sizeY - 1; j >= 0; j--)
                {
                    char[,] chunkTileArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkTileArray[k, l] = patternArray[i * 11 + k, (roomDimensions.sizeY - 1 - j) * 11 + l];
                        }
                    }

                    MapEngine.chunkMap[randomStartX + i, startY - j].charTileArray = chunkTileArray;
                }
            }
            SetBorderAndRoom(roomDimensions, randomStartX, startY - roomDimensions.sizeY + 1);

            return true;
        }

        public static bool BuildRoomDown(int startX, int startY)
        {
            int SizeXRight = 0;
            int SizeXLeft = 0;

            RoomSize roomSize = ChooseRoomUpDown(startX, startY, ref SizeXRight, ref SizeXLeft, false);

            if (roomSize == RoomSize.empty)
            {
                MapEngine.downChanksUpdate.Remove(MapEngine.chunkMap[startX, startY - 1]);
                return false;
            }

            var roomDimensions = roomSizesUpDown[roomSize];
            char[,] patternArray = ReadMapPattern(roomDimensions.chunkTemplatePath);

            List<int> possibleStartPoints = new List<int>();
            for (int i = 0; i < roomDimensions.sizeX && i <= SizeXLeft; i++)
            {
                if (i + SizeXRight + 1 >= roomDimensions.sizeX)
                {
                    possibleStartPoints.Add(startX - i);
                }
            }

            int randomStartX = possibleStartPoints[Data.random.Next(possibleStartPoints.Count)];

            for (int i = 0; i < roomDimensions.sizeX; i++)
            {
                for (int j = 0; j < roomDimensions.sizeY; j++)
                {

                    char[,] chunkTileArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkTileArray[k, l] = patternArray[i * 11 + k, j * 11 + l];
                        }
                    }

                    MapEngine.chunkMap[randomStartX + i, startY + j].charTileArray = chunkTileArray;
                }
            }
            SetBorderAndRoom(roomDimensions, randomStartX, startY);

            return true;
        }

    }
}
