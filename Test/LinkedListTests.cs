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
            ListNode<int> node = new ListNode<int>();
            node.Set(1);
            node.Get().Should().Be(1);
            node.Next().Should().Be(null);
        }
    }

    public class ListNode<T>
    {
        private T _data;
        private ListNode<T> _next;

        public void Set(T data)
        {
            _data = data;
        }

        public T Get()
        {
            return _data;
        }

        public ListNode<T> Next()
        {
            return _next;
        }
    }

    public class MyLinkedList
    {
    }
}