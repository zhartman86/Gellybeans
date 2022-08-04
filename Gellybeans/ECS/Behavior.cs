using System;
using System.Collections.Generic;
using System.Text;

namespace EsoLib
{
    public abstract class Behavior
    {
        public Entity Entity { get; set; }

        public Behavior(Entity entity)
        {
            Entity = entity;
        }

        public virtual void Update(float deltaTime) { }
    }
}
