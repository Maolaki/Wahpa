using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityEngine
{
    internal abstract class SkillTemplate : ITriggerable
    {
        protected Data.EntityAnimation boomAnimation;
        protected Data.EntityAnimation skillAnimation;
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
