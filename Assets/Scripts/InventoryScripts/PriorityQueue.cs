using System;
using System.Collections.Generic;

namespace InventoryScripts
{
    public class PriorityQueue<T>
    {
        private readonly List<T> list = new List<T>();

        public int Length => list.Count;

        public bool Contains(T i) => list.Contains(i);
        
        public T Peek()
        {
            try
            {
                return list[0];
            }
            catch
            {
                throw new NullReferenceException();
            }
        }
        
        public void Enqueue(T i)
        {
            list.Add(i);
            list.Sort();
        }
        public void Dequeue(T i)
        {
            list.Remove(i);
            list.Sort();
        }
    }
}