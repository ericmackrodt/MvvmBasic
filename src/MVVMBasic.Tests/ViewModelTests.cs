using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace MVVMBasic.Tests
{
    [TestClass]
    public class ViewModelTests
    {
        [TestInitialize]
        public void Initialization()
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }

        [TestMethod]
        public void Test1()
        {
            var notified = false;
            var propertyName = "Test";
            var propertyFired = "";
            var sut = new DummyViewModel();
            sut.PropertyChanged += (s, e) => {
                notified = true;
                propertyFired = e.PropertyName;
            };
            sut.Test.Value = "something";
            Assert.IsTrue(notified);
            Assert.AreEqual(propertyName, propertyFired);
        }
    }

    public class DummyViewModel : DynamicViewModel
    {
        [NotifyChange]
        public Property<string> Test { get; set; }
    }
}
