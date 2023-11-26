using SFML.Graphics;

namespace EntityEngine
{
    internal abstract class SkillTemplate : Triggerable
    {
        protected List<Texture> boomAnimation;
        protected List<Texture> skillAnimation;
        protected bool isPlayerSkill;
        protected bool isActive;
        protected int damage;

        public SkillTemplate(string boomName, string skillName, bool isPlayerSkill, int damage = 1)
        {
            this.boomAnimation = Data.EntityDictionary[boomName];
            this.skillAnimation = Data.EntityDictionary[skillName];
            this.isPlayerSkill = isPlayerSkill;
            this.isActive = true;
            this.damage = damage;
        }

        public void Attack(AttackableEntityTemplate attackedEntity)
        {
            Invoke();
            attackedEntity.Attacked();
        }

        public void Invoke()
        {
            // сделать взрыв 
            // удалить из массива сущностей комнаты
        }

        public void Update()
        {
            // сделать обновление анимации и координат
        }
    }
}
