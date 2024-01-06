using System;

namespace System.Collections.Generic
{
    public class Heap<T> where T :IEquatable<T>, IComparable<T>
    {
        int children { get; set; } = 2;

        List<T> items = new List<T>();

        public int Count => items.Count;

        public Heap(int children)
        {
            this.children = Math.Max(2, children);
        }

        public void Clear() => items.Clear();

        public void Enqueue(in T item)
        {
            int index = Count;

            items.Add(item);

            while (0 < index && 0 > items[index].CompareTo(items[ParentIndex(index)]))
            {
                Swap(index, ParentIndex(index));
                index = ParentIndex(index);
            }
        }

        public T Dequeue()
        {
            T item = items[0];

            items[0] = items[Count - 1];
            items.RemoveAt(Count - 1);

            int index = 0;
            int minIndex;

            do
            {
                minIndex = index;
                for (int n = 0; n < children; ++n)
                {
                    int childIndex = ChildIndex(index, n + 1);
                    if (childIndex < Count && 0 > items[childIndex].CompareTo(items[minIndex]))
                    {
                        minIndex = childIndex;
                    }
                }

                if (minIndex != index)
                {
                    Swap(minIndex, index);
                    index = minIndex;

                    minIndex = -1;
                }
            } while (index != minIndex);

            return item;
        }

        public T Peek() => items[0];

        int ParentIndex(int index) => (index - 1) / children;

        int ChildIndex(int index, int child) => index * children + child;

        void Swap(int a, int b)
        {
            T temp = items[a]; 
            items[a] = items[b]; 
            items[b] = temp;
        }
    }
}
