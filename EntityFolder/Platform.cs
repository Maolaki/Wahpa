using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityEngine
{
    internal class Platform : Triggerable
    {
        private int MOVE_DISTANCE_X { get; set; }
        private int MOVE_DISTANCE_Y { get; set; }
        private int movedX {  get; set; }
        private int movedY { get; set; }
        private bool isMoveableX {  get; set; }
        private bool isMoveableY { get; set; }

        public Platform(int coordX, int coordY, int platformSize, bool isMoveableX = false, bool isMoveableY = false,
            int moveDistanceX = 0, int moveDistanceY = 0, bool isTriggerable = true)
        {
            this.sprite.Position = new Vector2f(coordX, coordY);
            this.isMoveableX = isMoveableX;
            this.isMoveableY = isMoveableY;
            this.isTriggerable = isTriggerable;
            this.MOVE_DISTANCE_X = moveDistanceX;
            this.MOVE_DISTANCE_Y = moveDistanceY;
            this.movedX = 0;
            this.movedY = 0;
            this.triggerableOffsetX = 0;
            this.triggerableOffsetY = -1;

            switch(platformSize)
            {
                case 9:
                    this.sprite.Texture = Data.platformSize9;
                    this.triggerZoneSizeX = 16 * 9;
                    this.triggerZoneSizeY = 1;

                    break;
                case 5:
                    this.sprite.Texture = Data.platformSize5;
                    this.triggerZoneSizeX = 16 * 5;
                    this.triggerZoneSizeY = 1;

                    break;
                default:
                    break;
            }
        }

        public override void Update()
        {
            if (isMoveableX)
            {
                if (movedX < MOVE_DISTANCE_X)
                {
                    if (movedX > 0)
                    {
                        sprite.Position += new Vector2f(1,0);
                    }
                    else
                    {
                        sprite.Position -= new Vector2f(1, 0);
                    }

                    movedX++;
                }
                else
                {
                    movedX = -movedX;
                }
            }

            if (isMoveableY)
            {
                if (movedY < MOVE_DISTANCE_Y)
                {
                    if (movedY > 0)
                    {
                        sprite.Position += new Vector2f(0, 1);
                    }
                    else
                    {
                        sprite.Position -= new Vector2f(0, 1);
                    }

                    movedY++;
                }
                else
                {
                    movedX = -movedX;
                }
            }
        }
    }
}
