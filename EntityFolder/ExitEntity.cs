using SFML.System;

namespace EntityEngine
{
    internal class ExitEntity : Triggerable
    {
        public ExitEntity(int coordinateX, int coordinateY, int sizeX, int sizeY)
        {
            this.coordinateX = coordinateX;
            this.coordinateY = coordinateY;
            this.triggerZoneSizeX = sizeX;
            this.triggerZoneSizeY = sizeY;
            this.sprite.Origin = new Vector2f(0, 0);
            this.sprite.Position = new Vector2f(coordinateX, coordinateY);
        }
    }
}
