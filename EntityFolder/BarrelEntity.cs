using SFML.Graphics;

namespace EntityEngine
{
    internal class BarrelEntity : AttackableEntityTemplate
    {
        BarrelEntity(float sizeX ,Texture texture) : base(true , 1)
        { 

        }

        public override void Attacked()
        {
            // разрушение бочки
            
            // добавление лута
        }

        public override void Update()
        {
            
        }
    }
}
