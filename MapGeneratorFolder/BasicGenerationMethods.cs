using SFML.Graphics;

namespace MapGen
{
    internal static class BasicGenerationMethods
    {
        public static void SetColumn(int x, int y1, int y2, Texture texture, TileType tileType)
        {
            for (int i = y1; i != y2; i++)
            {
                MapGenerator.mainLayer[x, i].sprite.Texture = texture;
                MapGenerator.mainLayer[x, i].type = tileType;
            }
        }

        public static void SetLine (int x1, int x2, int y, Texture texture, TileType tileType)
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

        public static void CheckXYRight(out int SizeX,out int SizeY, int x, int y)
        {
            Chunk currectChunk = MapGenerator.Map[x,y];
            int maxJ = 5;

            for (int i = 1; i <= 5; i++)
            {
                if (MapGenerator.Map[x + i - 1, y].type != ChunkType.empty)
                {
                    SizeX = i;
                    SizeY = maxJ;
                    return;
                }

                for (int j = 1; j <= 5; j++)
                {
                    if (MapGenerator.Map[x+i-1,y+j-1].type != ChunkType.empty)
                    {
                        if (maxJ > j)
                            maxJ = j;

                        break;
                    }
                }
            }

            SizeX = 5;
            SizeY = maxJ;
        }

        public static void CheckXYLeft(out int SizeX, out int SizeY, int x, int y)
        {
            Chunk currectChunk = MapGenerator.Map[x, y];
            int maxJ = 5;

            for (int i = 1; i <= 5; i++)
            {
                if (MapGenerator.Map[x - i + 1, y].type != ChunkType.empty)
                {
                    SizeX = i;
                    SizeY = maxJ;
                    return;
                }

                for (int j = 1; j <= 5; j++)
                {
                    if (MapGenerator.Map[x - i + 1, y + j - 1].type != ChunkType.empty)
                    {
                        if (maxJ > j)
                            maxJ = j;

                        break;
                    }
                }
            }

            SizeX = 5;
            SizeY = maxJ;
        }

        ////////////////////////////////////////
        /// Choose Code

        public static Dictionary<RoomSize, (int MinSizeX, int MinSizeY)> roomSizesUpDown = new Dictionary<RoomSize, (int MinSizeX, int MinSizeY)>
        {
            { RoomSize.empty, (0, 0) },
            { RoomSize.one_five, (1, 5) },
            { RoomSize.two_two, (2, 2) },
            { RoomSize.four_two, (4, 2) },
            { RoomSize.five_five, (5, 5) }
        };

        public static Dictionary<RoomSize, (int MinSizeX, int MinSizeY, int Chance)> roomSizesRightLeft = new Dictionary<RoomSize, (int MinSizeX, int MinSizeY, int Chance)>
        {
            { RoomSize.empty, (0, 0, 60) },
            { RoomSize.one_one, (1, 1, 1) },
            { RoomSize.two_one, (2, 1, 10) },
            { RoomSize.two_two, (2, 2, 5) },
            { RoomSize.three_two, (3, 2, 8) },
            { RoomSize.four_two, (4, 2, 8) },
            { RoomSize.five_five, (5, 5, 4) }
        };

        public static RoomSize ChooseRoomRightLeft(int x, int y, bool isRight)
        {
            Random random = new Random();
            int sizeX;
            int sizeY;

            if (isRight)
                CheckXYRight(out sizeX, out sizeY, x, y);
            else
                CheckXYLeft(out sizeX, out sizeY, x, y);

            var availableRoomSizes = roomSizesRightLeft.Where(rs => sizeX >= rs.Value.MinSizeX && sizeY >= rs.Value.MinSizeY).ToList();

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
        /// Create Code

        public static void CreateRoomRight(int x, int y)
        {
            Random random = new Random();
            RoomSize roomsize = ChooseRoomRightLeft(x,y, true);

            if (roomSizesRightLeft.TryGetValue(roomsize, out var sizes))
            {
                int sizeX = sizes.MinSizeX;
                int sizeY = sizes.MinSizeY;
            }
            else
                return;



            // Тут код с выбором шаблона и заполнением чанков шаблоном, настроек чанков


        }

    }
}
