using System;
using System.Collections.Generic;
using Game;

public interface IPooleable<T>
{
    void Desactivate();
    event SimpleEventHandler<T> OnDesactivate;
}

namespace Game.pools
{
    public class Pool<T> where T : IPooleable<T>, new()
    {

        private List<T> inUse = new List<T>();
        private List<T> available = new List<T>();
        
        public List<T> InUse{ get => inUse; set => inUse = value; }
        public List<T> Available{ get => available; set => available = value; }

        public T Get()
        {
            T obj = default(T);

            if (available.Count > 0)
            {
                obj = available[0];
                available.Remove(obj);
            }
            else
            {
                obj = new T();
                obj.OnDesactivate += OnObjectDesactivateHandler;
            }

            inUse.Add(obj);
            return obj;
        }

        private void OnObjectDesactivateHandler(T obj)
        {
            Release(obj);
        }

        private void Release(T obj)
        {
            inUse.Remove(obj);
            available.Add(obj);
        }
    }
}