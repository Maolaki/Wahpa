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

    internal class Chunk
    {
        public int x {  get; set; }
        public int y { get; set; }
        public ChunkType type {  get; set; }
        public bool BorderUp { get; set; }
        public bool BorderDown { get; set; }
        public bool BorderLeft { get; set; }
        public bool BorderRight { get; set; }
        public bool ExitUp { get; set; }
        public bool ExitDown { get; set; }
        public bool ExitLeft { get; set; }
        public bool ExitRight { get; set; }
        public RoomSize size { get; set; }
        public char[,] chunkPixelArray { get; set; }
        public CircleShape shape { get; set; }

        public Chunk()
        {
            x = 0;
            y = 0;
            type = ChunkType.empty;
            BorderUp = false;
            BorderDown = false;
            BorderLeft = false;
            BorderRight = false;
            ExitUp = false;
            ExitDown = false;
            ExitLeft = false;
            ExitRight = false;
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
