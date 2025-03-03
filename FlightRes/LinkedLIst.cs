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
        private Node<T> tail;
        private int count;

        public void Add(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
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

        public void MergeSortBy(Func<T, IComparable> keySelector)
        {
            head = MergeSort(head, keySelector);

            // Update the tail pointer
            Node<T>? temp = head;
            while (temp?.Next != null)
            {
                temp = temp.Next;
            }
            tail = temp;
        }

        private Node<T>? MergeSort(Node<T>? head, Func<T, IComparable> keySelector)
        {
            if (head == null || head.Next == null)
                return head;

            Node<T>? middle = GetMiddle(head);
            Node<T>? nextToMiddle = middle?.Next;

            if (middle != null)
                middle.Next = null; // Split the list

            Node<T>? left = MergeSort(head, keySelector);
            Node<T>? right = MergeSort(nextToMiddle, keySelector);

            return MergeSortedLists(left, right, keySelector);
        }

        private Node<T>? GetMiddle(Node<T>? head)
        {
            if (head == null) return null;

            Node<T>? slow = head, fast = head;
            while (fast?.Next != null && fast.Next.Next != null)
            {
                slow = slow?.Next;
                fast = fast.Next.Next;
            }
            return slow;
        }

        private Node<T>? MergeSortedLists(Node<T>? left, Node<T>? right, Func<T, IComparable> keySelector)
        {
            if (left == null) return right;
            if (right == null) return left;

            if (keySelector(left.Data).CompareTo(keySelector(right.Data)) <= 0)
            {
                left.Next = MergeSortedLists(left.Next, right, keySelector);
                return left;
            }
            else
            {
                right.Next = MergeSortedLists(left, right.Next, keySelector);
                return right;
            }
        }
    }
}
