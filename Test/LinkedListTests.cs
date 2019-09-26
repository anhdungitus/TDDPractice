using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
            myLinkedList.AddFirst(new ListNode<int>(1));
            myLinkedList.GetHead().Data.Should().Be(1);
        }

        [Test]
        public void Can_Add_Last()
        {
            var myLinkedList = new MyLinkedList<int>();
            myLinkedList.AddLast(new ListNode<int>(1));
            myLinkedList.GetLast().Data.Should().Be(1);
            myLinkedList.GetLast().Next.Should().Be(null);
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

        public void AddFirst(ListNode<T> listNode)
        {
            listNode.Next = _head;
            _head = listNode;
        }

        public ListNode<T> GetHead()
        {
            return _head;
        }

        public void AddLast(ListNode<T> listNode)
        {
            var p = _head;
            if (p == null)
            {
                _head = listNode;
                return;
            }

            while (p.Next != null)
            {
                p = p.Next;
            }

            p.Next = listNode;
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
    }
}