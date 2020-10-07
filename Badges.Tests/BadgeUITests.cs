using System;
using System.Collections.Generic;
using Badges.UI;
using KomodoApps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Badges.Tests
{
    [TestClass]
    public class BadgeUITests
    {
        [TestMethod]
        public void AddBadge_ShouldAddNewBadgeToCollection()
        {
            var commandList = new List<string> { "1", "12345", "X1", "n", "q" };

            var console = new FakeConsole(commandList);
            var program = new BadgeManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Badge Added Successfully!"));
        }

        [TestMethod]
        public void ListAllBadges_ShouldReturnValidData()
        {
            var commandList = new List<string> { "3", "q"};

            var console = new FakeConsole(commandList);
            var program = new BadgeManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("3         C1, C2"));
        }

        [TestMethod]
        public void ExitApp_ShouldCloseTheApp()
        {
            var commandList = new List<string> { "4" };

            var console = new FakeConsole(commandList);
            var program = new BadgeManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Thank you for using the Komodo Key Bagde Manager\n Have a great day!"));
        }

        [TestMethod]
        public void AddDoorToBadge_ShouldAddANewDoorToBadge()
        {
            var commandList = new List<string> { "2", "1","1", "X5","n", "q" };

            var console = new FakeConsole(commandList);
            var program = new BadgeManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Door added to Badge Successfully!."));
        }

        [TestMethod]
        public void RemoveDoorFromBadge_ShouldRemoveDoorFromBadgeDoorsList()
        {
            var commandList = new List<string> { "2", "1", "2", "A1", "n", "q" };

            var console = new FakeConsole(commandList);
            var program = new BadgeManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Door removed from Badge Successfully!."));
        }

        [TestMethod]
        public void RemoveAllDoorsFromBadge_ShouldRemoveAllDoorsFromBadgeDoorsList()
        {
            var commandList = new List<string> { "2", "1", "3", "q" };

            var console = new FakeConsole(commandList);
            var program = new BadgeManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Doors removed from Badge Successfully!."));
        }
    }
}
