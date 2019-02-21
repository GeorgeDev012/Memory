using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Memory.Tests
{
    [TestClass]
    public class MemoryTests
    {
        [TestMethod]
        public void CheckIfCorrectNumberFromName()
        {
            SecondWindow secondWindow = new SecondWindow();
            PrivateObject obj = new PrivateObject(secondWindow);

            Image image1 = new Image();
            image1.Name = "image1";

            var number = obj.Invoke("GetNumberFromName", image1);
            Assert.AreEqual(number, 1);
        }

        //[TestMethod]
        //public void CheckIfImagesAreTheSame()
        //{
        //    SecondWindow secondWindow = new SecondWindow();
        //    PrivateObject obj = new PrivateObject(secondWindow);

        //    Image image1 = new Image();
        //    image1.Name = "image1";
        //    Image image2 = new Image();
        //    image2.Name = "image2";

        //    obj.Invoke("AreImagesTheSame", image1, image2);
            
        //}
    }
}
