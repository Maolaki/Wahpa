using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityEngine
{
    internal class Crystal : Triggerable
    {
        public Crystal(int coordX, int coordY, int sizeX, int sizeY)
        {
            this.coordinateX = coordX;
            this.coordinateY = coordY;
            this.sprite.Texture = Data.GUIDict["Crystal"];
            this.triggerZoneSizeX = sizeX;
            this.triggerZoneSizeY = sizeY;
            this.sprite.Position = new Vector2f(coordX, coordY);
            this.triggerZoneSizeX = sizeX;
            this.triggerZoneSizeY = sizeY;
        }

        public override void Update()
        {
        }
    }
}
