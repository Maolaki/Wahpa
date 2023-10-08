using SFML.Graphics;
using SFML.Window;

namespace EntityEngine
{
    public class Hero : EntityPhysics
    {
        public int speed { get; set; }
        //private Texture texture;

        public Hero(int coordX, int coordY) : base(coordX, coordY, 32, 48, new Texture("..\\..\\..\\Texture\\heroStanding.png"))
        {
            this.speed = 1;

        }

        public void Move()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    this.MoveUpRight(this.speed);
                else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    this.MoveUpLeft(this.speed);
                else
                    this.MoveUp(this.speed);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    this.MoveDownRight(this.speed);
                else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    this.MoveDownLeft(this.speed);
                else
                    this.MoveDown(this.speed);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                this.MoveLeft(this.speed);
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                this.MoveRight(this.speed);
        }
    }
}
