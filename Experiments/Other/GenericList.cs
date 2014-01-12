using System;
using System.Collections.Generic;
using System.Linq;

namespace Experiments.Other
{
    public class GenericList<T>
    {
        private class Node
        {
            public Node(T t)
            {
                next = null;
                data = t;
            }

            private Node next;
            public Node Next
            {
                get { return next; }
                set { next = value; }
            }

            private T data;
            public T Data
            {
                get { return data; }
                set { data = value; }
            }
         }

        private Node head;
        public GenericList()
        {
            head = null;
        }

        public void AddHead(T t)
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
                count++;
            }
        }

        private int count = 0;
        public int Count
        {
            get
            {
                GetEnumerator();
                return count;
            }

            private set { count = value; }
        }
    }
}
