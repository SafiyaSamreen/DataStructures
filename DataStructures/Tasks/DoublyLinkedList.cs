using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private class Node
        {
            public T Value { get; set; }
            public Node Next { get; set; }
            public Node Prev { get; set; }

            public Node(T value)
            {
                Value = value;
                Next = null;
                Prev = null;
            }
        }

        private Node head;
        private Node tail;
        private int count;

        public int Length
        {
            get { return count; }
        }

        public void Add(T e)
        {
            Node newNode = new Node(e);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }

            count++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            Node newNode = new Node(e);

            if (index == 0)
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }
            else if (index == count)
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            else
            {
                Node current = head;

                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }

                newNode.Next = current.Next;
                newNode.Prev = current;
                current.Next.Prev = newNode;
                current.Next = newNode;
            }

            count++;
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            Node current = head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        public void Remove(T item)
        {
            Node current = head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    if (current == head)
                    {
                        head = current.Next;
                        if (head != null)
                        {
                            head.Prev = null;
                        }
                    }
                    else if (current == tail)
                    {
                        tail = current.Prev;
                        tail.Next = null;
                    }
                    else
                    {
                        current.Prev.Next = current.Next;
                        current.Next.Prev = current.Prev;
                    }

                    count--;
                }

                current = current.Next;
            }
        }

        public T RemoveAt(int index)
        {
            T deletedValue = default(T);
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            if (index == 0)
            {
                deletedValue = head.Value;
                head = head.Next;
                if (head != null)
                {
                    head.Prev = null;
                }
            }
            else if (index == count - 1)
            {
                deletedValue = tail.Value;
                tail = tail.Prev;
                tail.Next = null;
            }
            else
            {
                Node current = head;

                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }

                deletedValue = current.Value;
                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
            }

            count--;
            return deletedValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
