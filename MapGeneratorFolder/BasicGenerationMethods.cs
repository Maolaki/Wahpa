using SFML.Graphics;
using System.Collections.Generic;

namespace MapGen
{
    internal static class BasicGenerationMethods
    {
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

        public static void SetBorderAndArray((int SizeX, int SizeY, int Chance, String pixelArray) roomDimensions, int i, int j, int startX, int startY)
        {
            if (i == 0 && i == roomDimensions.SizeX - 1 && j == 0 && j == roomDimensions.SizeY - 1)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.up_down_right_left;
                Level1Generator.upChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.downChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }

            else if (i == 0 && i == roomDimensions.SizeX - 1 && j == 0)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.up_right_left;
                Level1Generator.upChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }
            else if (i == 0 && i == roomDimensions.SizeX - 1 && j == roomDimensions.SizeY - 1)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.down_right_left;
                Level1Generator.downChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }
            else if (i == 0 && j == 0 && j == roomDimensions.SizeY - 1)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.up_down_right;
                Level1Generator.upChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.downChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }
            else if (i == roomDimensions.SizeX - 1 && j == 0 && j == roomDimensions.SizeY - 1)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.up_down_left;
                Level1Generator.upChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.downChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }

            else if (i == 0 && j == 0)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.up_left;
                Level1Generator.upChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }
            else if (i == 0 && j == roomDimensions.SizeY - 1)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.down_left;
                Level1Generator.downChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }
            else if (i == roomDimensions.SizeX - 1 && j == 0)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.up_right;
                Level1Generator.upChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }
            else if (i == roomDimensions.SizeX - 1 && j == roomDimensions.SizeY - 1)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.down_right;
                Level1Generator.downChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
                Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }

            else if (i == 0)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.left;
                Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }
            else if (i == roomDimensions.SizeX - 1)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.right;
                Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }
            else if (j == 0)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.up;
                Level1Generator.upChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }
            else if (j == roomDimensions.SizeY - 1)
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.down;
                Level1Generator.downChanksUpdate.Add(MapGenerator.Map[startX + i, startY + j]);
            }

            else
            {
                MapGenerator.Map[startX + i, startY + j].border = RoomBorder.none;
            }
        }

        public static void SetColumn(int x, int y1, int y2, Texture texture, TileType tileType)
        {
            for (int i = y1; i != y2; i++)
            {
                MapGenerator.mainLayer[x, i].sprite.Texture = texture;
                MapGenerator.mainLayer[x, i].type = tileType;
            }
        }

        public static void SetLine(int x1, int x2, int y, Texture texture, TileType tileType)
        {
            for (int i = x1; i != x2; i++)
            {
                MapGenerator.mainLayer[i, y].sprite.Texture = texture;
                MapGenerator.mainLayer[i, y].type = tileType;
            }
        }

        public static void FillPart(int x1, int x2, int y1, int y2, Texture texture, TileType tileType)
        {
            for (int i = x1; i != x2; i++)
            {
                for (int j = y1; j != y2; j++)
                {
                    if (MapGenerator.mainLayer[i, j].genType == TileGenType.freeGen)
                    {
                        MapGenerator.mainLayer[i, j].sprite.Texture = texture;
                        MapGenerator.mainLayer[i, j].genType = TileGenType.wallGen;
                        MapGenerator.mainLayer[i, j].type = TileType.wall;

                    }

                }
            }
        }

        ///////////////////////////////////////////////// 
        // Check Code 
        // 3е слово указывает но то, куда относительно чанка идет проверка, проверяется вниз

        public static void CheckXYRight(int x, int y, out int SizeX, out int SizeYUp, out int SizeYDown)
        {
            Chunk currectChunk = MapGenerator.Map[x, y];
            int maxJUp = 4;
            int maxJDown = 4;

            for (int i = 1; i <= 5; i++)
            {
                if (MapGenerator.Map[x + i - 1, y].type != ChunkType.empty)
                {
                    SizeX = i;
                    SizeYUp = maxJUp;
                    SizeYDown = maxJDown;
                    return;
                }

                for (int j = 0; j < 4; j++)
                {
                    if (MapGenerator.Map[x + i - 1, y - j - 1].type != ChunkType.empty)
                    {
                        if (maxJUp > j)
                            maxJUp = j;

                        break;
                    }
                }

                for (int j = 0; j < 4; j++)
                {
                    if (MapGenerator.Map[x + i - 1, y + j + 1].type != ChunkType.empty)
                    {
                        if (maxJDown > j)
                            maxJDown = j;

                        break;
                    }
                }
            }

            SizeX = 5;
            SizeYUp = maxJUp;
            SizeYDown = maxJDown;
        }

        public static void CheckXYLeft(int x, int y, out int SizeX, out int SizeYUp, out int SizeYDown)
        {
            Chunk currectChunk = MapGenerator.Map[x, y];
            int maxJUp = 4;
            int maxJDown = 4;

            for (int i = 1; i <= 5; i++)
            {
                if (MapGenerator.Map[x - i + 1, y].type != ChunkType.empty)
                {
                    SizeX = i;
                    SizeYUp = maxJUp;
                    SizeYDown = maxJDown;
                    return;
                }

                for (int j = 0; j < 4; j++)
                {
                    if (MapGenerator.Map[x - i + 1, y - j - 1].type != ChunkType.empty)
                    {
                        if (maxJUp > j)
                            maxJUp = j;

                        break;
                    }
                }

                for (int j = 0; j < 4; j++)
                {
                    if (MapGenerator.Map[x - i + 1, y + j + 1].type != ChunkType.empty)
                    {
                        if (maxJDown > j)
                            maxJDown = j;

                        break;
                    }
                }
            }

            SizeX = 5;
            SizeYUp = maxJUp;
            SizeYDown = maxJDown;
        }

        public static void CheckXYUp(int x, int y, out int SizeY, out int SizeXRight, out int SizeXLeft)
        {
            Chunk currectChunk = MapGenerator.Map[x, y];
            int maxIRight = 4;
            int maxILeft = 4;

            for (int j = 1; j <= 5; j++)
            {
                if (MapGenerator.Map[x, y - j + 1].type != ChunkType.empty)
                {
                    SizeY = j;
                    SizeXRight = maxIRight;
                    SizeXLeft = maxILeft;
                    return;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (MapGenerator.Map[x + i + 1, y - j + 1].type != ChunkType.empty)
                    {
                        if (maxIRight > i)
                            maxIRight = i;

                        break;
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    if (MapGenerator.Map[x - i - 1, y - j + 1].type != ChunkType.empty)
                    {
                        if (maxILeft > i)
                            maxILeft = i;

                        break;
                    }
                }
            }

            SizeY = 5;
            SizeXRight = maxIRight;
            SizeXLeft = maxILeft;
        }

        public static void CheckXYDown(int x, int y, out int SizeY, out int SizeXRight, out int SizeXLeft)
        {
            Chunk currectChunk = MapGenerator.Map[x, y];
            int maxIRight = 4;
            int maxILeft = 4;

            for (int j = 1; j <= 5; j++)
            {
                if (MapGenerator.Map[x, y + j - 1].type != ChunkType.empty)
                {
                    SizeY = j;
                    SizeXRight = maxIRight;
                    SizeXLeft = maxILeft;
                    return;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (MapGenerator.Map[x + i + 1, y + j - 1].type != ChunkType.empty)
                    {
                        if (maxIRight > i)
                            maxIRight = i;

                        break;
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    if (MapGenerator.Map[x - i - 1, y + j - 1].type != ChunkType.empty)
                    {
                        if (maxILeft > i)
                            maxILeft = i;

                        break;
                    }
                }
            }

            SizeY = 5;
            SizeXRight = maxIRight;
            SizeXLeft = maxILeft;
        }

        ////////////////////////////////////////
        /// Choose Code

        public static Dictionary<RoomSize, (int SizeX, int SizeY, int Chance, String pixelArray)> roomSizesRightLeft = new Dictionary<RoomSize, (int SizeX, int SizeY, int Chance, String pixelArray)>
        {
            { RoomSize.empty, (0, 0, 60, Data.LEVEL1_ONE_ONE) },
            { RoomSize.one_one, (1, 1, 1, Data.LEVEL1_ONE_ONE) },
            { RoomSize.two_one, (2, 1 , 10 , Data.LEVEL1_TWO_ONE) },
            { RoomSize.two_two, (2, 2, 5, Data.LEVEL1_TWO_TWO) },
            { RoomSize.three_two, (3, 2 , 8 , Data.LEVEL1_THREE_TWO ) },
            { RoomSize.four_two, (4, 2 , 8 , Data.LEVEL1_FOUR_TWO) },
            { RoomSize.five_five, (5, 5, 4, Data.LEVEL1_FIVE_FIVE) }
        };

        public static Dictionary<RoomSize, (int SizeX, int SizeY, int Chance, String pixelArray)> roomSizesUpDown = new Dictionary<RoomSize, (int SizeX, int SizeY, int Chance, String pixelArray)>
        {
            { RoomSize.empty, (0, 0, 60, Data.LEVEL1_ONE_ONE) },
            { RoomSize.one_five, (1, 5, 2, Data.LEVEL1_ONE_FIVE) },
            { RoomSize.two_two, (2, 2, 5, Data.LEVEL1_TWO_TWO) },
            { RoomSize.four_two, (4, 2, 8, Data.LEVEL1_FOUR_TWO) },
            { RoomSize.five_five, (5, 5, 4, Data.LEVEL1_FIVE_FIVE) }
        };

        public static RoomSize ChooseRoomRightLeft(int x, int y, out int SizeYUp, out int SizeYDown, bool isRight)
        {
            Random random = new Random();
            int SizeX;
            int SizeY;

            if (isRight)
                CheckXYRight(x, y, out SizeX, out SizeYUp, out SizeYDown);
            else
                CheckXYLeft(x, y, out SizeX, out SizeYUp, out SizeYDown);

            SizeY = SizeYUp + SizeYDown + 1;

            var availableRoomSizes = roomSizesRightLeft.Where(rs => SizeX >= rs.Value.SizeX && SizeY >= rs.Value.SizeY).ToList();

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

        public static RoomSize ChooseRoomUpDown(int x, int y, out int SizeXRight, out int SizeXLeft, bool isUp)
        {
            Random random = new Random();
            int SizeX;
            int SizeY;

            if (isUp)
                CheckXYUp(x, y, out SizeY, out SizeXRight, out SizeXLeft);
            else
                CheckXYDown(x, y, out SizeY, out SizeXRight, out SizeXLeft);

            SizeX = SizeXRight + SizeXLeft + 1;

            var availableRoomSizes = roomSizesUpDown.Where(rs => SizeX >= rs.Value.SizeX && SizeY >= rs.Value.SizeY).ToList();

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
                    MapGenerator.Map[startX + i, startY + j].type = ChunkType.main;
                    MapGenerator.Map[startX + i, startY + j].size = roomSize;

                    int psevdoI;
                    if (i > 0)
                        psevdoI = i * 11;
                    else
                        psevdoI = 0;

                    int psevdoJ;
                    if (j > 0)
                        psevdoJ = j * 11;
                    else
                        psevdoJ = 0;

                    char[,] chunkPixelArray = new char[11, 11];

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkPixelArray[k, l] = patternArray[psevdoI + k, psevdoJ + l];
                        }
                    }

                    MapGenerator.Map[startX + i, startY + j].chunkPixelArray = chunkPixelArray;

                    SetBorderAndArray(roomDimensions, i, j, startX, startY);

                }
            }
        }

        public static bool BuildRoomRight(int startX, int startY, bool isMain)
        {
            int SizeYUp;
            int SizeYDown;
            ChunkType chunkType = isMain ? ChunkType.main : ChunkType.extra;

            RoomSize roomSize = ChooseRoomRightLeft(startX, startY, out SizeYUp, out SizeYDown, true);

            if (roomSize == RoomSize.empty)
            {
                return false;
            }

            var roomDimensions = roomSizesRightLeft[roomSize];

            List<int> possibleStartPoints = new List<int>();
            for (int i = 0; i < roomDimensions.SizeY; i++)
            {
                if (i + SizeYDown + 1 >= roomDimensions.SizeY)
                {
                    possibleStartPoints.Add(startY + i);
                }
            }

            Random random = new Random();
            int randomStartY = possibleStartPoints[random.Next(possibleStartPoints.Count)];

            char[,] patternArray = ReadMapPattern(roomDimensions.pixelArray);
            char[,] chunkPixelArray = new char[11, 11];

            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                for (int j = 0; j < roomDimensions.SizeY; j++)
                {
                    MapGenerator.Map[startX + i, randomStartY - j].type = chunkType;
                    MapGenerator.Map[startX + i, randomStartY - j].size = roomSize;

                    int psevdoI;
                    if (i > 0)
                        psevdoI = i * 11 - 1;
                    else
                        psevdoI = 0;

                    int psevdoJ;
                    if (j > 0)
                        psevdoJ = j * 11 - 1;
                    else
                        psevdoJ = 0;

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkPixelArray[k, l] = patternArray[psevdoI + k, psevdoJ + l];
                        }
                    }

                    MapGenerator.Map[startX + i, randomStartY - j].chunkPixelArray = chunkPixelArray;

                    if (i == 0)
                        Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[startX + i, randomStartY - j]);
                    if (i == roomDimensions.SizeX - 1)
                        Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[startX + i, randomStartY - j]);
                    if (j == 0)
                        Level1Generator.upChanksUpdate.Add(MapGenerator.Map[startX + i, randomStartY - j]);
                    if (j == roomDimensions.SizeY - 1)
                        Level1Generator.downChanksUpdate.Add(MapGenerator.Map[startX + i, randomStartY - j]);

                }
            }

            for (int i = 1; i < 11; i++)
                MapGenerator.Map[startX, startY].chunkPixelArray[0,i] = '0';


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
                return false;
            }

            var roomDimensions = roomSizesRightLeft[roomSize];

            List<int> possibleStartPoints = new List<int>();
            for (int i = 0; i < roomDimensions.SizeY; i++)
            {
                if (i + SizeYDown + 1 >= roomDimensions.SizeY)
                {
                    possibleStartPoints.Add(startY + i);
                }
            }

            Random random = new Random();
            int randomStartY = possibleStartPoints[random.Next(possibleStartPoints.Count)];

            char[,] patternArray = ReadMapPattern(roomDimensions.pixelArray);
            char[,] chunkPixelArray = new char[11, 11];

            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                for (int j = 0; j < roomDimensions.SizeY; j++)
                {
                    MapGenerator.Map[startX + i - roomDimensions.SizeX + 1, randomStartY - j].type = chunkType;
                    MapGenerator.Map[startX + i - roomDimensions.SizeX + 1, randomStartY - j].size = roomSize;

                    int psevdoI;
                    if (i > 0)
                        psevdoI = i * 11 - 1;
                    else
                        psevdoI = 0;

                    int psevdoJ;
                    if (j > 0)
                        psevdoJ = j * 11 - 1;
                    else
                        psevdoJ = 0;

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkPixelArray[k, l] = patternArray[psevdoI + k, psevdoJ + l];
                        }
                    }

                    MapGenerator.Map[startX + i - roomDimensions.SizeX + 1, randomStartY - j].chunkPixelArray = chunkPixelArray;

                    if (i == 0)
                        Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[startX + i - roomDimensions.SizeX + 1, randomStartY - j]);
                    if (i == roomDimensions.SizeX - 1)
                        Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[startX + i - roomDimensions.SizeX + 1, randomStartY - j]);
                    if (j == 0)
                        Level1Generator.upChanksUpdate.Add(MapGenerator.Map[startX + i - roomDimensions.SizeX + 1, randomStartY - j]);
                    if (j == roomDimensions.SizeY - 1)
                        Level1Generator.downChanksUpdate.Add(MapGenerator.Map[startX + i - roomDimensions.SizeX + 1, randomStartY - j]);

                }
            }

            for (int i = 1; i < 11; i++)
                MapGenerator.Map[startX, startY].chunkPixelArray[10,i] = '0';


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
                return false;
            }

            var roomDimensions = roomSizesUpDown[roomSize];

            List<int> possibleStartPoints = new List<int>();
            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                if (i + SizeXRight + 1 >= roomDimensions.SizeX)
                {
                    possibleStartPoints.Add(startX - i);
                }
            }

            Random random = new Random();
            int randomStartX = possibleStartPoints[random.Next(possibleStartPoints.Count)];

            char[,] patternArray = ReadMapPattern(roomDimensions.pixelArray);
            char[,] chunkPixelArray = new char[11, 11];

            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                for (int j = roomDimensions.SizeY - 1; j >= 0; j--)
                {
                    MapGenerator.Map[randomStartX + i, startY - j].type = chunkType;
                    MapGenerator.Map[randomStartX + i, startY - j].size = roomSize;

                    int psevdoI;
                    if (i > 0)
                        psevdoI = i * 11 - 1;
                    else
                        psevdoI = 0;

                    int psevdoJ;
                    if (roomDimensions.SizeY - 1 - j > 0)
                        psevdoJ = (roomDimensions.SizeY - 1 - j) * 11 - 1;
                    else
                        psevdoJ = 0;

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkPixelArray[k, l] = patternArray[psevdoI + k, psevdoJ + l];
                        }
                    }

                    MapGenerator.Map[randomStartX + i, startY - j].chunkPixelArray = chunkPixelArray;

                    if (i == 0)
                        Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[randomStartX + i, startY - j]);
                    if (i == roomDimensions.SizeX - 1)
                        Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[randomStartX + i, startY - j]);
                    if (j == roomDimensions.SizeY - 1)
                        Level1Generator.upChanksUpdate.Add(MapGenerator.Map[randomStartX + i, startY - j]);
                    if (j == 0)
                        Level1Generator.downChanksUpdate.Add(MapGenerator.Map[randomStartX + i, startY - j]);

                }
            }

            for (int i = 1; i < 11; i++)
                MapGenerator.Map[startX, startY].chunkPixelArray[i, 10] = '0';


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
                return false;
            }

            var roomDimensions = roomSizesUpDown[roomSize];

            List<int> possibleStartPoints = new List<int>();
            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                if (i + SizeXRight + 1 >= roomDimensions.SizeX)
                {
                    possibleStartPoints.Add(startX - i);
                }
            }

            Random random = new Random();
            int randomStartX = possibleStartPoints[random.Next(possibleStartPoints.Count)];

            char[,] patternArray = ReadMapPattern(roomDimensions.pixelArray);
            char[,] chunkPixelArray = new char[11, 11];

            for (int i = 0; i < roomDimensions.SizeX; i++)
            {
                for (int j = 0; j < roomDimensions.SizeY; j++)
                {
                    MapGenerator.Map[randomStartX + i, startY + j].type = chunkType;
                    MapGenerator.Map[randomStartX + i, startY + j].size = roomSize;

                    int psevdoI;
                    if (i > 0)
                        psevdoI = i * 11 - 1;
                    else
                        psevdoI = 0;

                    int psevdoJ;
                    if (j > 0)
                        psevdoJ = j * 11 - 1;
                    else
                        psevdoJ = 0;

                    for (int k = 0; k < 11; k++)
                    {
                        for (int l = 0; l < 11; l++)
                        {
                            chunkPixelArray[k, l] = patternArray[psevdoI + k, psevdoJ + l];
                        }
                    }

                    MapGenerator.Map[randomStartX + i, startY + j].chunkPixelArray = chunkPixelArray;

                    if (i == 0)
                        Level1Generator.leftChanksUpdate.Add(MapGenerator.Map[randomStartX + i, startY + j]);
                    if (i == roomDimensions.SizeX - 1)
                        Level1Generator.rightChanksUpdate.Add(MapGenerator.Map[randomStartX + i, startY + j]);
                    if (j == 0)
                        Level1Generator.upChanksUpdate.Add(MapGenerator.Map[randomStartX + i, startY + j]);
                    if (j == roomDimensions.SizeY - 1)
                        Level1Generator.downChanksUpdate.Add(MapGenerator.Map[randomStartX + i, startY + j]);

                }
            }

            for (int i = 1; i < 11; i++)
                MapGenerator.Map[startX, startY].chunkPixelArray[i, 0] = '0';


            return true;
        }

    }
}
