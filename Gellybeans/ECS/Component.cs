using System;
using System.Collections.Generic;
using System.Text;

namespace Gellybeans.ECS
{
    public abstract class Component
    {
        private Entity? entity;
        public Entity Entity { get { return entity!; } }

        public bool isEnabled { get; set; } = true;
        
        public Component() { }
        public Component(Entity parent) 
        { 
            entity = parent;
        }
    
        public void SetEntity(Entity e)
        {
            if(entity == null)
            {
                entity = e; 
                return;
            }              
            return;
        }
    }
}
