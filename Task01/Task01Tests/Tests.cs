using System;
using NUnit.Framework;

using Task01;

namespace Task01Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test001()
        {
            Matrix m = new Matrix(2,2);
            m.SetRow(0, new []{4, 5});
            m.SetRow(1, new []{2, 5});
            MatrixProcess.SortByNonDecreasingSum(ref m);
            Assert.AreEqual(2, m.Get(0,0));
            Assert.AreEqual(5, m.Get(0,1));
        }
        
        [Test]
        public void Test002()
        {
            Matrix m = new Matrix(2,3);
            m.SetRow(0, new []{2, 1, 2});
            m.SetRow(1, new []{2, 2, 1});
            MatrixProcess.SortByNonDecreasingSum(ref m);
            Assert.AreEqual(1, m.Get(0,1));
        }
        
        [Test]
        public void Test003()
        {
            Matrix m = new Matrix(3,3);
            m.SetRow(0, new []{3, 1, 1});
            m.SetRow(1, new []{1, 3, 1});
            m.SetRow(2, new []{1, 1, 3});
            MatrixProcess.SortByNonDecreasingSum(ref m);
            Assert.AreEqual(3, m.Get(2,2));
            Assert.AreEqual(3, m.Get(0,0));
        }
        
        [Test]
        public void Test004()
        {
            Matrix m = new Matrix(3,3);
            m.SetRow(0, new []{2, 1, 1});
            m.SetRow(1, new []{2, 4, 1});
            m.SetRow(2, new []{1, 1, 3});
            MatrixProcess.SortByNonDecreasingSum(ref m);
            Assert.AreEqual(2, m.Get(0,0));
            Assert.AreEqual(4, m.Get(2,1));
        }
        
        [Test]
        public void Test005()
        {
            Matrix m = new Matrix(5,5);
            m.SetRow(0, new []{2, 12, 4, 5, 8}); // 31
            m.SetRow(1, new []{22, 16, -2, 33, 1}); // 70
            m.SetRow(2, new []{0, 4, 20, 1, 3}); //28
            m.SetRow(3, new []{1, 1, 1, 99, -5}); //97
            m.SetRow(4, new []{10, 21, 30, 7, -5}); //63
            MatrixProcess.SortByNonDecreasingSum(ref m);
            Assert.AreEqual(0, m.Get(0,0));
            Assert.AreEqual(2, m.Get(1,0));
            Assert.AreEqual(10, m.Get(2,0));
            Assert.AreEqual(22, m.Get(3,0));
            Assert.AreEqual(1, m.Get(4,0));
        }
        
    }
}