using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace Test
{
    [TestFixture]
    public class LinkedListTests
    {
        [Test]
        public void Create_SetValue_And_Next_Is_Null()
        {
            ListNode<int> node = new ListNode<int>(1);
            node.Data.Should().Be(1);
            node.Next.Should().Be(null);
        }

        [Test]
        public void Can_Add_First()
        {
            var myLinkedList = new MyLinkedList<int>();
            myLinkedList.AddFirst(1);
            myLinkedList.GetHead().Data.Should().Be(1);
        }

        [Test]
        public void Can_Add_Last()
        {
            var myLinkedList = new MyLinkedList<int>();
            myLinkedList.AddLast(1);
            myLinkedList.AddLast(2);
            myLinkedList.AddFirst(3);
            myLinkedList.GetLast().Data.Should().Be(2);
        }

        [Test]
        public void Can_Add_After()
        {
            var myLinkedList = new MyLinkedList<int>();
            myLinkedList.AddFirst(1);
            myLinkedList.AddLast(2);
            myLinkedList.AddLast(4);
            myLinkedList.AddAfter(myLinkedList.GetHead().Next, 3);
            myLinkedList.GetHead().Next.Next.Data.Should().Be(3);
        }

        [Test]
        public void Can_Add_Before()
        {
            var myLinkedList = new MyLinkedList<int>();
            myLinkedList.AddFirst(1);
            myLinkedList.AddLast(3);
            myLinkedList.AddLast(4);
            myLinkedList.AddBefore(myLinkedList.GetHead().Next, 2);
            myLinkedList.GetHead().Next.Data.Should().Be(2);
        }

        [Test]
        public void Can_Travel_All()
        {
            var myLinkedList = new MyLinkedList<int>();
            myLinkedList.AddFirst(1);
            myLinkedList.AddLast(2);
            myLinkedList.AddLast(3);

            var list = myLinkedList.ToList();
            list[0].Should().Be(1);
            list[1].Should().Be(2);
            list[2].Should().Be(3);
            list.Count.Should().Be(3);
        }
    }

    public class ListNode<T>
    {
        public T Data;
        public ListNode<T> Next;

        public ListNode(T data, ListNode<T> next = null)
        {
            Data = data;
            Next = next;
        }
    }

    public class MyLinkedList<T>
    {
        private ListNode<T> _head;

        public void AddFirst(T value)
        {
            var node = new ListNode<T>(value)
            {
                Next = _head
            };
            _head = node;
        }

        public ListNode<T> GetHead()
        {
            return _head;
        }

        public void AddLast(T value)
        {
            var node = new ListNode<T>(value);
            var p = _head;
            if (p == null)
            {
                _head = node;
                return;
            }

            while (p.Next != null)
            {
                p = p.Next;
            }

            p.Next = node;
        }

        public ListNode<T> GetLast()
        {
            var p = _head;
            if (p == null)
            {
                return null;
            }

            while (p.Next != null)
            {
                p = p.Next;
            }

            return p;
        }

        public void AddAfter(ListNode<T> node, T value)
        {
            var newNode = new ListNode<T>(value)
            {
                Next = node.Next
            };
            node.Next = newNode;
        }

        public void AddBefore(ListNode<T> node, T value)
        {
            var p = _head;
            while (p.Next != null)
            {
                if (p.Next == node)
                {
                    var newNode = new ListNode<T>(value);
                    p.Next = newNode;
                    newNode.Next = node;
                    break;
                }

                p = p.Next;
            }
        }

        public List<T> ToList()
        {
            var result = new List<T>();
            var p = _head;
            while (p != null)
            {
                result.Add(p.Data);
                p = p.Next;
            }

            return result;
        }
    }
}