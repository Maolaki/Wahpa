using SFML.Graphics;

namespace EntityEngine
{
    internal abstract class Triggerable
    {
        public Sprite sprite = new Sprite();
        public int coordinateX { get; set; }
        public int coordinateY { get; set; }
        protected int triggerZoneSizeX { get; set; }
        protected int triggerZoneSizeY { get; set; }
        protected bool isTriggerable { get; set; } = true;

        public static bool CheckCollission(Triggerable entityOne, Triggerable entityTwo)
        {
            if (!entityOne.isTriggerable || !entityTwo.isTriggerable
                || entityOne.coordinateX > entityTwo.coordinateX + entityTwo.triggerZoneSizeX
                || entityOne.coordinateX + entityOne.triggerZoneSizeX < entityTwo.coordinateX
                || entityOne.coordinateY > entityTwo.coordinateY + entityTwo.triggerZoneSizeY
                || entityOne.coordinateY + entityOne.triggerZoneSizeY < entityTwo.coordinateY)
            {
                return false;
            }
            return true;
        }

    }
}
