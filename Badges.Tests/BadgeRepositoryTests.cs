using Badges.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Badges.Tests
{
    [TestClass]
    public class BadgeRepositoryTests
    {
        private BadgeRepository _badgeRepo;
        private Badge testBadge1;
        private Badge testBadge2;

        [TestInitialize]
        public void Arrange()
        {
            _badgeRepo = new BadgeRepository();
            testBadge1 = new Badge(1, new List<string>() { "A1", "A2" });
            testBadge2 = new Badge(12345, new List<string>() { "B1", "B2" });

            _badgeRepo.AddBadge(testBadge1);
        }

        [TestMethod]
        public void AddBadge_ShouldReturnTrue()
        {
            Assert.IsTrue(_badgeRepo.AddBadge(testBadge2));
        }

        [TestMethod]
        public void GetAllBadges_ShouldReturnAllBadges()
        {
            _badgeRepo.AddBadge(testBadge2);
            var allBadges = _badgeRepo.GetAllBadges();

            Assert.IsTrue(allBadges.ContainsKey(12345));
        }

        [TestMethod]
        public void GetBadgeByID_ShouldReturnCorrectBadge()
        {
            var foundBadge = _badgeRepo.GetBadgeByID(1);
            Assert.AreEqual(foundBadge.BadgeID, testBadge1.BadgeID);
        }

        [TestMethod]
        public void AddDoorToBadge_ShouldReturnTrue()
        {
            var addedDoorSuccess = _badgeRepo.AddDoorToBadge(1, "C1");
            Assert.IsTrue(addedDoorSuccess);
        }

        [TestMethod]
        public void RemoveDoorFromBadge_ShouldReturnTrue()
        {
            var removed = _badgeRepo.RemoveDoorFromBadge(1, "A1");
            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void RemoveAllDoorsFromBadge_ShouldReturnTrue()
        {
            var removed = _badgeRepo.RemoveAllDoorsFromBadge(1);
            Assert.IsTrue(removed);
        }
    }
}
