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
        protected int triggerableOffsetX { get; set; } = 0;
        protected int triggerableOffsetY { get; set; } = 0;
        protected bool isTriggerable { get; set; } = true;

        public static bool CheckCollission(Triggerable entityOne, Triggerable entityTwo)
        {
            if (!entityOne.isTriggerable || !entityTwo.isTriggerable
                || entityOne.coordinateX + entityOne.triggerableOffsetX > entityTwo.coordinateX + entityTwo.triggerableOffsetX + entityTwo.triggerZoneSizeX
                || entityOne.coordinateX + entityOne.triggerableOffsetX + entityOne.triggerZoneSizeX < entityTwo.coordinateX + entityTwo.triggerableOffsetX
                || entityOne.coordinateY + entityOne.triggerableOffsetY > entityTwo.coordinateY + entityTwo.triggerableOffsetY + entityTwo.triggerZoneSizeY
                || entityOne.coordinateY + entityOne.triggerableOffsetY + entityOne.triggerZoneSizeY < entityTwo.coordinateY + entityTwo.triggerableOffsetY)
            {
                return false;
            }
            return true;
        }

        public abstract void Update();

    }
}