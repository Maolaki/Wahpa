using SFML.Graphics;
using System.Net.Http.Headers;

namespace MapGen
{
    enum ChunkType
    {
        empty,
        main,
        extra
    }

    enum RoomBorder
    {
        none,
        up,
        down,
        right,
        left,
        up_right,
        up_left,
        down_right,
        down_left,
        up_right_left,
        down_right_left,
        up_down_left,
        up_down_right,
        up_down_right_left
    }

    enum Exit
    {
        none,
        up,
        down,
        left,
        right,
        up_left,
        up_right,
        down_left,
        down_right
    }

    internal class Chunk
    {
        public int x {  get; set; }
        public int y { get; set; }
        public ChunkType type {  get; set; }
        public Exit exit { get; set; }
        public RoomBorder border { get; set; }
        public RoomSize size { get; set; }
        public char[,] chunkPixelArray { get; set; }
        public CircleShape shape { get; set; }

        public Chunk()
        {
            x = 0;
            y = 0;
            type = ChunkType.empty;
            exit = Exit.none;
            border = RoomBorder.none;
            size = RoomSize.empty;
            chunkPixelArray = new char[Data.LEVEL1_CHUNK_SIZE, Data.LEVEL1_CHUNK_SIZE];
            shape = new CircleShape();

            for (int i = 0; i < Data.LEVEL1_CHUNK_SIZE; i++)
            {
                for (int j = 0; j < Data.LEVEL1_CHUNK_SIZE; j++)
                {
                    chunkPixelArray[i, j] = '0';
                }
            }

        }
    }
}
