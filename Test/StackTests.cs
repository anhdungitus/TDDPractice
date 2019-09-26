using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Linq;

namespace Test
{
    /*
     * LIFO
     * Pop
     * Peek
     * Push
     */

    [TestFixture]
    public class StackTests
    {
        [Test]
        public void Empty_Stack_Should_Returns_True()
        {
            var myStack = new MyStack();
            myStack.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void Stack_Can_Push_Value()
        {
            var myStack = new MyStack();
            myStack.Push(1);
            myStack.IsEmpty().Should().BeFalse();
        }

        [Test]
        public void Stack_Can_Pop_Value()
        {
            var myStack = new MyStack();
            myStack.Push(1);
            myStack.Pop().Should().Be(1);
            myStack.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void Stack_Can_Peek_Value()
        {
            var myStack = new MyStack();
            myStack.Push(1);
            myStack.Peek().Should().Be(1);
            myStack.IsEmpty().Should().BeFalse();
        }

        [Test]
        public void Stack_Push_Pop_Peek_Count()
        {
            var myStack = new MyStack();
            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(3);

            myStack.Pop();
            myStack.Pop().Should().Be(2);
            myStack.Count().Should().Be(1);
        }

        [Test]
        public void Pop_Empty_Stack_Throw_Error()
        {
            var myStack = new MyStack();
            myStack.Invoking(s => s.Pop()).Should().Throw<InvalidOperationException>();
        }
    }

    public class MyStack
    {
        private int[] _stack;
        private int _count;

        public MyStack()
        {
            _count = 0;
            _stack = new int[_count];
        }

        public int Count()
        {
            return _count;
        }

        public bool IsEmpty()
        {
            return !_stack.Any();
        }

        public void Push(int number)
        {
            Array.Resize(ref _stack, ++_count);
            _stack[_count - 1] = number;
        }

        public int Pop()
        {
            if (_count <= 0)
                throw new InvalidOperationException();
            var value = _stack.LastOrDefault();
            Array.Resize(ref _stack, --_count);
            return value;
        }

        public int Peek()
        {
            return _stack.LastOrDefault();
        }
    }
}