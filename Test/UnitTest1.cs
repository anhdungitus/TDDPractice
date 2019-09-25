using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class UpdateableSpinTests
    {
        [Test]
        public void Wait_NoPulse_ReturnsFalse()
        {
            UpdateableSpin spin = new UpdateableSpin();
            bool wasPulsed = spin.Wait(TimeSpan.FromMilliseconds(10));
            Assert.IsFalse(wasPulsed);
        }

        [Test]
        public void Wait_Pulse_ReturnsTrue()
        {
            UpdateableSpin spin = new UpdateableSpin();
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                spin.Set();
            });
            bool wasPulsed = spin.Wait(TimeSpan.FromSeconds(10));
            Assert.IsTrue(wasPulsed);
        }

        [Test]
        public void Wait50Millisec_CallIsActuallyWaitingFor50Millisec()
        {
            var spin = new UpdateableSpin();

            Stopwatch watcher = new Stopwatch();
            watcher.Start();

            spin.Wait(TimeSpan.FromMilliseconds(50));

            watcher.Stop();

            TimeSpan actual = TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds);
            TimeSpan lefEpsilon = TimeSpan.FromMilliseconds(50 - 50 * 0.1);
            TimeSpan rightEpsilon = TimeSpan.FromMilliseconds(50 + 50 * 0.1);

            Assert.IsTrue(actual > lefEpsilon && actual < rightEpsilon);
        }

        [Test]
        public void Waith50millisec_UpdateAfter300Millisec_TotalWaitingIsApprox800Millisec()
        {
            var spin = new UpdateableSpin();
            var watcher = new Stopwatch();
            watcher.Start();

            const int timeOut = 500;
            const int spanBeforeUpdate = 300;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(spanBeforeUpdate);
                spin.UpdateTimeOut();
            });

            spin.Wait(TimeSpan.FromMilliseconds(timeOut));

            watcher.Stop();

            TimeSpan actual = TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds);
            const int expected = timeOut + spanBeforeUpdate;

            TimeSpan left = TimeSpan.FromMilliseconds(expected - expected * 0.1);
            TimeSpan right = TimeSpan.FromMilliseconds(expected + expected * 0.1);

            Assert.IsTrue(actual > left && actual < right);
        }
    }

    public class UpdateableSpin
    {
        private readonly object lockObj = new object();
        private bool shouldWait = true;
        private long executionStartingTime;

        public bool Wait(TimeSpan timeOut, int spinDuration = 0)
        {
            UpdateTimeOut();
            while (true)
            {
                lock (lockObj)
                {
                    if (!shouldWait)
                    {
                        return true;
                    }

                    if (DateTime.UtcNow.Ticks - executionStartingTime > timeOut.Ticks)
                        return false;
                }

                Thread.Sleep(spinDuration);
            }
        }

        public void Set()
        {
            lock (lockObj)
            {
                shouldWait = false;
            }
        }

        public void UpdateTimeOut()
        {
            lock (lockObj)
            {
                executionStartingTime = DateTime.UtcNow.Ticks;
            }
        }
    }
}