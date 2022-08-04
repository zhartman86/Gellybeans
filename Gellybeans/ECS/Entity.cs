using System;
using System.Collections.Generic;

namespace EsoLib
{
    public class Entity
    {
        protected static int counter = 0; 

        public int Id       { get; protected set; }
        public string Name  { get; protected set; } = "";

        private List<Component> components = new List<Component>();

        public Entity()
        {
            Id = counter++;
        }

        public Component AddComponent(Component c)
        {
            for(int i = 0; i < components.Count; i++)
            {
                if(components[i].GetType() == c.GetType()) 
                    return null!;
            }
            
            c.SetEntity(this);
            components.Add(c);          
            return c;
        }
        
        public Component AddComponent<T>() where T : Component, new()
        {
            var c = new T();
            return AddComponent(c);
        }

        public Component GetComponent<T>() where T : Component
        {
            for(int i = 0; i < components.Count; i++)
            {
                if(components[i].GetType() == typeof(T))
                {
                    return components[i];
                }
            }
            return null!;
        }
    
        public List<Component> GetComponents(bool getDisabled = false)
        {
            var list = new List<Component>();
            for(int i = 0; i < components.Count; i++)
            {
                if(components[i].isEnabled) list.Add(components[i]);
                else if(getDisabled) list.Add(components[i]);
            }
            return list;
        }
    }
}
