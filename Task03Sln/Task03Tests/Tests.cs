using System;
using NUnit.Framework;
using Task03;

namespace Task03Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test000()
        {
            SimpleFraction f = new SimpleFraction(3,2);
            Assert.AreEqual(3, f.Nominator);
            Assert.AreEqual(2, f.Denominator);
        }        
        
        [Test]
        public void Test001()
        {
            SimpleFraction f = new SimpleFraction(3,2);
            f.Reduce();
            Assert.AreEqual(3, f.Nominator);
            Assert.AreEqual(2, f.Denominator);
        }
        
        [Test]
        public void Test002()
        {
            SimpleFraction f = new SimpleFraction(4,2);
            f.Reduce();
            Assert.AreEqual(2, f.Nominator);
            Assert.AreEqual(1, f.Denominator);
        }
        
        [Test]
        public void Test003()
        {
            SimpleFraction f = new SimpleFraction(5,6);
            f.Reduce();
            Assert.AreEqual(5, f.Nominator);
            Assert.AreEqual(6, f.Denominator);
        }
        
        [Test]
        public void Test004()
        {
            SimpleFraction f1 = new SimpleFraction(4,6);
            SimpleFraction f = f1.Reduced();
            Assert.AreEqual(2, f.Nominator);
            Assert.AreEqual(3, f.Denominator);
        }
        
        [Test]
        public void Test005()
        {
            SimpleFraction f1 = new SimpleFraction(4,6);
            SimpleFraction f2 = new SimpleFraction(2, 6);
            SimpleFraction add = (SimpleFraction) f1.Add(f2);
            Assert.AreEqual(1,add.Nominator);
            Assert.AreEqual(1,add.Denominator);
        }
        
        [Test]
        public void Test006()
        {
            SimpleFraction f1 = new SimpleFraction(1,3);
            SimpleFraction f2 = new SimpleFraction(1, 2);
            SimpleFraction add = (SimpleFraction) f1.Add(f2);
            Assert.AreEqual(5,add.Nominator);
            Assert.AreEqual(6,add.Denominator);
        }
        
        [Test]
        public void Test007()
        {
            SimpleFraction f1 = new SimpleFraction(1,3);
            SimpleFraction f2 = new SimpleFraction(1, 2);
            SimpleFraction sub = (SimpleFraction) f1.Sub(f2);
            Assert.AreEqual(-1,sub.Nominator);
            Assert.AreEqual(6,sub.Denominator);
        }
        
        [Test]
        public void Test008()
        {
            SimpleFraction f1 = new SimpleFraction(2,3);
            SimpleFraction f2 = new SimpleFraction(4, 2);
            SimpleFraction mul = (SimpleFraction) f1.Multiply(f2);
            Assert.AreEqual(8,mul.Nominator);
            Assert.AreEqual(6,mul.Denominator);
        }
        
        [Test]
        public void Test009()
        {
            SimpleFraction f1 = new SimpleFraction(5,6);
            SimpleFraction f2 = new SimpleFraction(2, 6);
            SimpleFraction div = (SimpleFraction) f1.Divide(f2);
            Assert.AreEqual(30,div.Nominator);
            Assert.AreEqual(12,div.Denominator);
        }
    }
}