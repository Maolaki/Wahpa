using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowEngine;
using static EntityEngine.HeroPhysics;

namespace EntityEngine
{
    internal class WalkedMonster : EntityPhysics
    {
        public WalkedMonster(int coordX, int coordY, int sizeX, int sizeY, string monsterType) : base(coordX, coordY, sizeX, sizeY, monsterType, 1)
        {
            this.direction = Direction.right;
            this.prevDirection = Direction.right;
        }

        public override void Attacked()
        {
            ViewHandler.hero.coins += Data.random.Next(1,5);
            ViewHandler.Enemies.Remove(this);
        }

        public override void Update()
        {
            if (direction == Direction.right)
            {
                MoveRight(1);

                if (CheckEndRight())
                {
                    direction = Direction.left;
                }
            }
            else if (direction == Direction.left)
            {
                MoveLeft(1);

                if (CheckEndLeft())
                {
                    direction = Direction.right;
                }
            }

            sprite.Position = new Vector2f(coordinateX, coordinateY);
            UpdateAnimation();

            if (direction == Direction.right && prevDirection == Direction.left)
            {
                sprite.Scale = new Vector2f(1f, 1f);
                sprite.Origin -= new Vector2f(16, 0);
                prevDirection = Direction.right;
            }
            else if (direction == Direction.left && prevDirection == Direction.right)
            {
                sprite.Scale = new Vector2f(-1f, 1f);
                sprite.Origin += new Vector2f(16, 0);
                prevDirection = Direction.left;
            }
        }
    }
}
