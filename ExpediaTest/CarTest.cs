using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestThatCarDoesGetLocationFromTheDatabase()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            String location = "San Francisco";
            String anotherLocation = "New York";

            Expect.Call(mockDatabase.getCarLocation(7)).Return(location);
            Expect.Call(mockDatabase.getCarLocation(509)).Return(anotherLocation);

            mocks.ReplayAll();

            Car target = new Car(10);
            target.Database = mockDatabase;

            String result;

            result = target.getCarLocation(7);
            Assert.AreEqual(location, result);

            result = target.getCarLocation(509);
            Assert.AreEqual(anotherLocation, result);

            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestThatCarHasCorrectMileage()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();

            Expect.Call(mockDatabase.Miles).PropertyBehavior();

            mocks.ReplayAll();

            mockDatabase.Miles = 230;
            
            Car target = new Car(10);
            target.Database = mockDatabase;

            int mileage = target.Mileage;

            Assert.AreEqual(230, mileage);

            mocks.VerifyAll();
        }
	}
}
