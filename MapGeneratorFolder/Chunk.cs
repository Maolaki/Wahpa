namespace MapGen
{
    enum ChunkType
    {
        empty,
        main,
        extra
    }

    enum RoomConnection
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
        public ChunkType type {  get; set; }
        public Exit exit { get; set; }
        public RoomConnection connection { get; set; }
        public RoomSize size { get; set; }

        public Chunk()
        {
            type = ChunkType.empty;
            exit = Exit.none;
            connection = RoomConnection.none;
            size = RoomSize.empty;


        }
    }
}
