using System;

namespace GeoCoordinatePortableTests.Assertations
{
    public static class Assert
    {
        public static T Throws<T>(Action action) where T : Exception
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                var exOfT = ex as T;

                if (exOfT != null)
                    return exOfT;

                throw;
            }

            throw new Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException("Expected to throw " + typeof(T).Name + ".");
        }
    }
}
