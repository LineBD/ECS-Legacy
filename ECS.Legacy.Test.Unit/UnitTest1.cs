using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;


namespace ECS.Legacy.Test.Unit
{
    [TestFixture]
    public class UnitTest1
    {
        private ECS _uut;
        private IHeater _heater;
        private ITempSensor _tempSensor;

        [SetUp]
        public void Setup()
        {
            _heater = Substitute.For<IHeater>();
            _tempSensor = Substitute.For<ITempSensor>();

            _uut = new ECS(25, _tempSensor,_heater);
        }

        [Test]
        public void RunSelfTest_TempSensorFails_SelfTestFails()
        {
            _tempSensor.RunSelfTest().Returns(false);
            _heater.RunSelfTest().Returns(true);
            NUnit.Framework.Assert.IsFalse(_uut.RunSelfTest());
        }

        [Test]
        public void Regulate_TempIrrelevant_TempSensorQueriedForTemp()
        {
            _uut.Regulate();
            _tempSensor.Received().GetTemp();
        }


    }
}
