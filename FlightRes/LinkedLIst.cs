using System;
using System.Collections.Generic;

namespace FlightRes
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LinkedList<T>
    {
        private Node<T> head;
        private int count;

        public void Add(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node<T> current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            count++;
        }

        public bool Remove(Predicate<T> match)
        {
            Node<T> current = head;
            Node<T> previous = null;

            while (current != null)
            {
                if (match(current.Data))
                {
                    if (previous == null)
                    {
                        head = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }
                    count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }

        public List<T> GetAll()
        {
            List<T> result = new List<T>();
            Node<T> current = head;
            while (current != null)
            {
                result.Add(current.Data);
                current = current.Next;
            }
            return result;
        }

        public T Find(Predicate<T> match)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (match(current.Data))
                {
                    return current.Data;
                }
                current = current.Next;
            }
            return default(T);
        }

        public int Count => count;
    }
}