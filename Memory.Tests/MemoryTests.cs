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
            Assert.AreEqual(1, number);
        }

        [TestMethod]
        public void CheckIfImagesAreTheSame()
        {
            SecondWindow secondWindow = new SecondWindow();
            PrivateObject obj = new PrivateObject(secondWindow);

            Image image1 = new Image();
            image1.Name = "image1";
            Image image2 = new Image();
            image2.Name = "image2";

            obj.SetArrayElement("_randomDucks", @".\Resources\duck1.jpg", 0);
            obj.SetArrayElement("_randomDucks", @".\Resources\duck2.jpg", 1);

            bool areTheSame = (bool) obj.Invoke("AreImagesTheSame", image1, image2);
            Assert.IsTrue(areTheSame);
        }
    }
}
