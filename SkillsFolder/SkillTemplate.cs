using SFML.Graphics;
using SFML.System;
using WindowEngine;
using static EntityEngine.EntityPhysics;

namespace EntityEngine
{
    internal class SkillTemplate : Triggerable
    {
        public Clock clock { get; private set; } = new Clock();
        public Time pDeltaTime { get; private set; }
        public float deltaTime { get; private set; } = 0;
        private Direction route { get; set; }
        public SkillTemplate(int coordX, int coordY, int sizeX, int sizeY, string skillAnim, Direction direction) : base()
        {
            this.coordinateX = coordX;
            this.coordinateY = coordY; 
            this.sprite.Position = new Vector2f(coordX, coordY);
            this.triggerZoneSizeX = sizeX;
            this.triggerZoneSizeY = sizeY;
            this.sprite.Texture = Data.GUIDict[skillAnim];
            this.route = direction;
            if (direction == Direction.left)
            {
                sprite.Scale = new Vector2f(-1f, 1f);
                sprite.Origin -= new Vector2f(16, 0);
            }
        }

        public void Attack(AttackableEntityTemplate attackedEntity)
        {
            ViewHandler.Enemies.Remove(attackedEntity);
            ViewHandler.Skills.Remove(this);
            ViewHandler.hero.coins += Data.random.Next(1,6);
        }

        public override void Update()
        {
            if (deltaTime > 1)
            {
                ViewHandler.Skills.Remove(this);
                deltaTime = 0;
            }
            else
            {
                pDeltaTime = clock.Restart();
                deltaTime += pDeltaTime.AsSeconds();
            }

            if (route == Direction.right)
            {
                sprite.Position += new Vector2f(1, 0);
                coordinateX += 1;
            }
            else
            {
                sprite.Position -= new Vector2f(1, 0);
                coordinateX -= 1;
            }

        }
    }
}
