using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TargemTest;

namespace TestsProject
{
    [TestFixture]
    public class MyListTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void Test(FuncHelper<int> funcHelper)
        {
            var list = new List<int>();
            var myList = new MyList<int>();

            var actual = funcHelper.func(myList);
            var expected = funcHelper.func(list);
            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCaseSource(nameof(TestCasesCorrect))]
        public void TestCorrect(FuncCorrectHelper<int> funcHelper)
        {
            var list = new List<int>();
            var myList = new MyList<int>();

            var actual = funcHelper.func(myList);
            var expected = funcHelper.func(list);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CorrectClearList()
        {
            var list = new MyList<int> {1, 2, 3};
            
            list.Clear();
            
            Assert.That(list.Count, Is.EqualTo(0));
        } 

        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(
                    new FuncHelper<int>(
                        list =>
                        {
                            list.Add(1);
                            list.Add(2);
                            list.Add(3);
                            return list.ToArray();
                        })).SetName("Add");

                yield return new TestCaseData(
                    new FuncHelper<int>(
                        list =>
                        {
                            list.Add(1);
                            list.Add(2);
                            list.Add(3);
                            list.RemoveAt(2);
                            return list.ToArray();
                        })).SetName("RemoveAt");

                yield return new TestCaseData(
                    new FuncHelper<int>(
                        list =>
                        {
                            list.Add(1);
                            list.Add(2);
                            list.Add(3);
                            list.Insert(1, 100);
                            return list.ToArray();
                        })).SetName("Insert");

                yield return new TestCaseData(
                    new FuncHelper<int>(
                        list =>
                        {
                            list.Clear();
                            return list.ToArray();
                        })).SetName("Clear");

                yield return new TestCaseData(
                    new FuncHelper<int>(
                        list =>
                        {
                            list.Add(1);
                            list.Add(2);
                            list.Add(3);
                            list.Remove(0);
                            return list.ToArray();
                        })).SetName("Remove");

                yield return new TestCaseData(
                    new FuncHelper<int>(
                        list =>
                        {
                            for (var i = 0; i < 10000; i++)
                                list.Add(1);
                            for (var i = 0; i < 10000; i++)
                                list.RemoveAt(0);
                            return list.ToArray();
                        })).SetName("ManyAddAndRemove");
            }
        }

        public static IEnumerable TestCasesCorrect
        {
            get
            {
                yield return new TestCaseData(
                    new FuncCorrectHelper<int>(
                        list =>
                        {
                            list.Add(1);
                            list.Add(2);
                            list.Add(3);
                            return list.IndexOf(1);
                        })).SetName("IndexOf");

                yield return new TestCaseData(
                    new FuncCorrectHelper<int>(
                        list =>
                        {
                            list.Add(1);
                            list.Add(2);
                            list.Add(3);
                            return list.Contains(2);
                        })).SetName("Contains");

                yield return new TestCaseData(
                    new FuncCorrectHelper<int>(
                        list =>
                        {
                            var temp = new int[3];
                            list.Add(1);
                            list.Add(2);
                            list.Add(3);
                            list.CopyTo(temp, 0);
                            return temp;
                        })).SetName("CopyTo");
            }
        }

        [Test]
        public void NotCorrectUsing()
        {
            var myList = new MyList<int>();

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var _ = myList[0];
            });

            Assert.Throws<IndexOutOfRangeException>(() => myList.RemoveAt(0));
            Assert.Throws<IndexOutOfRangeException>(() => myList.Insert(2, 10));
            Assert.Throws<IndexOutOfRangeException>(() => myList.Insert(2, 10));
        }
    }

    public class FuncHelper<T>
    {
        public Func<IList<T>, T[]> func;

        public FuncHelper(Func<IList<T>, T[]> func)
        {
            this.func = func;
        }
    }

    public class FuncCorrectHelper<T>
    {
        public Func<IList<T>, object> func;

        public FuncCorrectHelper(Func<IList<T>, object> func)
        {
            this.func = func;
        }
    }
}