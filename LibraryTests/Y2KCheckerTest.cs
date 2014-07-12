using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;
using System.Fakes;
using Library;

namespace LibraryTests
{
    // See:
    // http://msdn.microsoft.com/en-us/library/hh549176.aspx

    [TestClass]
    public class Y2KCheckerTest
    {
        [TestMethod]
        public void Check_IsFalse()
        {
            // create a ShimsContext cleans up shims 
            using (ShimsContext.Create())
            {
                // hook delegate to the shim method to redirect DateTime.Now
                // to return January 2nd of 2000
                ShimDateTime.NowGet = () => new DateTime(2000, 1, 2);
                Y2KChecker.Check();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Check_IsTrue()
        {
            // create a ShimsContext cleans up shims 
            using (ShimsContext.Create())
            {
                // hook delegate to the shim method to redirect DateTime.Now
                // to return January 1st of 2000
                ShimDateTime.NowGet = () => new DateTime(2000, 1, 1);
                Y2KChecker.Check();
            }
        }
    }
}
