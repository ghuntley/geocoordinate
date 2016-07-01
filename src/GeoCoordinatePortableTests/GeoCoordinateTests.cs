using System;
//using System.Device.Location;
using GeoCoordinatePortable;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeoCoordinatePortableTests
{
    [TestClass]
    public class GeoCoordinateTests
    {
        private GeoCoordinate _UnitUnderTest;

        [TestMethod]
        public void GeoCoordinate_ConstructorWithDefaultValues_DoesNotThrow()
        {
            _UnitUnderTest = new GeoCoordinate(Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN);
        }

        [TestMethod]
        public void GeoCoordinate_ConstructorWithParameters_ReturnsInstanceWithExpectedValues()
        {
            var latitude = 42D;
            var longitude = 44D;
            var altitude = 46D;
            var horizontalAccuracy = 48D;
            var verticalAccuracy = 50D;
            var speed = 52D;
            var course = 54D;
            var isUnknown = false;
            _UnitUnderTest = new GeoCoordinate(latitude, longitude, altitude, horizontalAccuracy, verticalAccuracy, speed, course);

            Assert.AreEqual(latitude, _UnitUnderTest.Latitude);
            Assert.AreEqual(longitude, _UnitUnderTest.Longitude);
            Assert.AreEqual(altitude, _UnitUnderTest.Altitude);
            Assert.AreEqual(horizontalAccuracy, _UnitUnderTest.HorizontalAccuracy);
            Assert.AreEqual(verticalAccuracy, _UnitUnderTest.VerticalAccuracy);
            Assert.AreEqual(speed, _UnitUnderTest.Speed);
            Assert.AreEqual(course, _UnitUnderTest.Course);
            Assert.AreEqual(isUnknown, _UnitUnderTest.IsUnknown);
        }

        [TestMethod]
        public void GeoCoordinate_DefaultConstructor_ReturnsInstanceWithDefaultValues()
        {
            Assert.AreEqual(Double.NaN, _UnitUnderTest.Altitude);
            Assert.AreEqual(Double.NaN, _UnitUnderTest.Course);
            Assert.AreEqual(Double.NaN, _UnitUnderTest.HorizontalAccuracy);
            Assert.IsTrue(_UnitUnderTest.IsUnknown);
            Assert.AreEqual(Double.NaN, _UnitUnderTest.Latitude);
            Assert.AreEqual(Double.NaN, _UnitUnderTest.Longitude);
            Assert.AreEqual(Double.NaN, _UnitUnderTest.Speed);
            Assert.AreEqual(Double.NaN, _UnitUnderTest.VerticalAccuracy);
        }

        [TestMethod]
        public void GeoCoordinate_EqualsOperatorWithNullParameters_DoesNotThrow()
        {
            GeoCoordinate first = null;
            GeoCoordinate second = null;
            Assert.IsTrue(first == second);

            first = new GeoCoordinate();
            Assert.IsFalse(first == second);

            first = null;
            second = new GeoCoordinate();
            Assert.IsFalse(first == second);
        }

        [TestMethod]
        public void GeoCoordinate_EqualsTwoInstancesWithDifferentValuesExceptLongitudeAndLatitude_ReturnsTrue()
        {
            var first = new GeoCoordinate(11, 12, 13, 14, 15, 16, 17);
            var second = new GeoCoordinate(11, 12, 14, 15, 16, 17, 18);

            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        public void GeoCoordinate_EqualsTwoInstancesWithSameValues_ReturnsTrue()
        {
            var first = new GeoCoordinate(11, 12, 13, 14, 15, 16, 17);
            var second = new GeoCoordinate(11, 12, 13, 14, 15, 16, 17);

            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        public void GeoCoordinate_EqualsWithOtherTypes_ReturnsFalse()
        {
            var something = new Nullable<Int32>(42);
            Assert.IsFalse(_UnitUnderTest.Equals(something));
        }

        [TestMethod]
        public void GeoCoordinate_GetDistanceTo_ReturnsExpectedDistance()
        {
            var start = new GeoCoordinate(1, 1);
            var end = new GeoCoordinate(5, 5);
            var distance = start.GetDistanceTo(end);
            var expected = 629060.759879635;
            var delta = distance - expected;

            Assert.IsTrue(delta < 1e-8);
        }

        [TestMethod]
        public void GeoCoordinate_GetDistanceToWithNaNCoordinates_ThrowsArgumentException()
        {
            Assertations.Assert.Throws<ArgumentException>(() => new GeoCoordinate(Double.NaN, 1).GetDistanceTo(new GeoCoordinate(5, 5)));
            Assertations.Assert.Throws<ArgumentException>(() => new GeoCoordinate(1, Double.NaN).GetDistanceTo(new GeoCoordinate(5, 5)));
            Assertations.Assert.Throws<ArgumentException>(() => new GeoCoordinate(1, 1).GetDistanceTo(new GeoCoordinate(Double.NaN, 5)));
            Assertations.Assert.Throws<ArgumentException>(() => new GeoCoordinate(1, 1).GetDistanceTo(new GeoCoordinate(5, Double.NaN)));
        }

        [TestMethod]
        public void GeoCoordinate_GetHashCode_OnlyReactsOnLongitudeAndLatitude()
        {
            _UnitUnderTest.Latitude = 2;
            _UnitUnderTest.Longitude = 3;
            var firstHash = _UnitUnderTest.GetHashCode();

            _UnitUnderTest.Altitude = 4;
            _UnitUnderTest.Course = 5;
            _UnitUnderTest.HorizontalAccuracy = 6;
            _UnitUnderTest.Speed = 7;
            _UnitUnderTest.VerticalAccuracy = 8;
            var secondHash = _UnitUnderTest.GetHashCode();

            Assert.AreEqual(firstHash, secondHash);
        }

        [TestMethod]
        public void GeoCoordinate_GetHashCode_SwitchingLongitudeAndLatitudeReturnsSameHashCodes()
        {
            _UnitUnderTest.Latitude = 2;
            _UnitUnderTest.Longitude = 3;
            var firstHash = _UnitUnderTest.GetHashCode();

            _UnitUnderTest.Latitude = 3;
            _UnitUnderTest.Longitude = 2;
            var secondHash = _UnitUnderTest.GetHashCode();

            Assert.AreEqual(firstHash, secondHash);
        }

        [TestMethod]
        public void GeoCoordinate_IsUnknownIfLongitudeAndLatitudeIsNaN_ReturnsTrue()
        {
            _UnitUnderTest.Longitude = 1;
            _UnitUnderTest.Latitude = Double.NaN;
            Assert.IsFalse(_UnitUnderTest.IsUnknown);

            _UnitUnderTest.Longitude = Double.NaN;
            _UnitUnderTest.Latitude = 1;
            Assert.IsFalse(_UnitUnderTest.IsUnknown);

            _UnitUnderTest.Longitude = Double.NaN;
            _UnitUnderTest.Latitude = Double.NaN;
            Assert.IsTrue(_UnitUnderTest.IsUnknown);
        }

        [TestMethod]
        public void GeoCoordinate_NotEqualsOperatorWithNullParameters_DoesNotThrow()
        {
            GeoCoordinate first = null;
            GeoCoordinate second = null;
            Assert.IsFalse(first != second);

            first = new GeoCoordinate();
            Assert.IsTrue(first != second);

            first = null;
            second = new GeoCoordinate();
            Assert.IsTrue(first != second);
        }

        [TestMethod]
        public void GeoCoordinate_SetAltitude_ReturnsCorrectValue()
        {
            Assert.AreEqual(_UnitUnderTest.Altitude, Double.NaN);

            _UnitUnderTest.Altitude = 0;
            Assert.AreEqual(0, _UnitUnderTest.Altitude);

            _UnitUnderTest.Altitude = Double.MinValue;
            Assert.AreEqual(Double.MinValue, _UnitUnderTest.Altitude);

            _UnitUnderTest.Altitude = Double.MaxValue;
            Assert.AreEqual(Double.MaxValue, _UnitUnderTest.Altitude);
        }

        [TestMethod]
        public void GeoCoordinate_SetCourse_ThrowsOnInvalidValues()
        {
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => _UnitUnderTest.Course = -0.1);
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => _UnitUnderTest.Course = 360.1);
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => new GeoCoordinate(Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, -0.1));
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => new GeoCoordinate(Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, 360.1));
        }

        [TestMethod]
        public void GeoCoordinate_SetHorizontalAccuracy_ThrowsOnInvalidValues()
        {
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => _UnitUnderTest.HorizontalAccuracy = -0.1);
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => new GeoCoordinate(Double.NaN, Double.NaN, Double.NaN, -0.1, Double.NaN, Double.NaN, Double.NaN));
        }

        [TestMethod]
        public void GeoCoordinate_SetHorizontalAccuracyToZero_ReturnsNaNInProperty()
        {
            _UnitUnderTest = new GeoCoordinate(Double.NaN, Double.NaN, Double.NaN, 0, Double.NaN, Double.NaN, Double.NaN);
            Assert.AreEqual(Double.NaN, _UnitUnderTest.HorizontalAccuracy);
        }

        [TestMethod]
        public void GeoCoordinate_SetLatitude_ThrowsOnInvalidValues()
        {
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => _UnitUnderTest.Latitude = 90.1);
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => _UnitUnderTest.Latitude = -90.1);
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => new GeoCoordinate(90.1, Double.NaN));
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => new GeoCoordinate(-90.1, Double.NaN));
        }

        [TestMethod]
        public void GeoCoordinate_SetLongitude_ThrowsOnInvalidValues()
        {
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => _UnitUnderTest.Longitude = 180.1);
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => _UnitUnderTest.Longitude = -180.1);
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => new GeoCoordinate(Double.NaN, 180.1));
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => new GeoCoordinate(Double.NaN, -180.1));
        }

        [TestMethod]
        public void GeoCoordinate_SetSpeed_ThrowsOnInvalidValues()
        {
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => _UnitUnderTest.Speed = -0.1);
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => new GeoCoordinate(Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, -1, Double.NaN));
        }

        [TestMethod]
        public void GeoCoordinate_SetVerticalAccuracy_ThrowsOnInvalidValues()
        {
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => _UnitUnderTest.VerticalAccuracy = -0.1);
            Assertations.Assert.Throws<ArgumentOutOfRangeException>(() => new GeoCoordinate(Double.NaN, Double.NaN, Double.NaN, Double.NaN, -0.1, Double.NaN, Double.NaN));
        }

        [TestMethod]
        public void GeoCoordinate_SetVerticalAccuracyToZero_ReturnsNaNInProperty()
        {
            _UnitUnderTest = new GeoCoordinate(Double.NaN, Double.NaN, Double.NaN, Double.NaN, 0, Double.NaN, Double.NaN);
            Assert.AreEqual(Double.NaN, _UnitUnderTest.VerticalAccuracy);
        }

        [TestMethod]
        public void GeoCoordinate_ToString_ReturnsLongitudeAndLatitude()
        {
            Assert.AreEqual("Unknown", _UnitUnderTest.ToString());

            _UnitUnderTest.Latitude = 1;
            _UnitUnderTest.Longitude = Double.NaN;
            Assert.AreEqual("1, NaN", _UnitUnderTest.ToString());

            _UnitUnderTest.Latitude = Double.NaN;
            _UnitUnderTest.Longitude = 1;
            Assert.AreEqual("NaN, 1", _UnitUnderTest.ToString());
        }

        [TestInitialize]
        public void Initialize()
        {
            _UnitUnderTest = new GeoCoordinate();
        }
    }
}