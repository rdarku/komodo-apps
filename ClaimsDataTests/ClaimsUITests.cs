using System;
using System.Collections.Generic;
using Claims.UI;
using KomodoApps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Claims.Tests
{
    [TestClass]
    public class ClaimsUITests
    {
        [TestMethod]
        public void GetAllClaimsTest_ShouldReturnCorrectData()
        {
            var commandList = new List<string> { "1", "x", "4" };

            var console = new FakeConsole(commandList);
            var program = new ClaimsManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Collision on I70 South"));
        }

        [TestMethod]
        public void RemoveClaim_ShouldReturnTrue()
        {
            var commandList = new List<string> { "2", "y", "x","4" };

            var console = new FakeConsole(commandList);
            var program = new ClaimsManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Claim Successfully dealt with"));
        }

        [TestMethod]
        public void AddClaim_ShouldReturnTrue()
        {
            var commandList = new List<string> { "3", "12", "Car","Collision on 3rd Ace","800","2020","4","12","2020","4","20","y","x", "4" };

            var console = new FakeConsole(commandList);
            var program = new ClaimsManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Claim Added Successfully!."));
        }

        [TestMethod]
        public void GetNextClaim_ShouldReturnCorrectClaim()
        {
            var commandList = new List<string> { "2", "n", "x", "4" };

            var console = new FakeConsole(commandList);
            var program = new ClaimsManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("You chose not to deal with this claim"));
        }
    }
}
