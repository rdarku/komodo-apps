using ClaimsData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ClaimsTests
{
    [TestClass()]
    public class ClaimsRepositoryTests
    {
        private ClaimsRepository _claimsRepository;
        private Claim uniqueNewClaim;

        [TestInitialize]
        public void Arrange()
        {
            _claimsRepository = new ClaimsRepository();

            uniqueNewClaim = new Claim() { 
                ClaimAmount = 40000m,
                ClaimID = 1,
                TypeOfClaim = ClaimType.Car,
                Description = "Collision on Hudson avenue",
                DateOfClaim = DateTime.Now,
                DateOfIncident = new DateTime(2020,9,11)
            };

            _claimsRepository.AddClaim(uniqueNewClaim);
        }

        [TestMethod()]
        public void GetAllClaimsTest_ShouldReturnCorrectData()
        {
            var allClaims = _claimsRepository.GetAllClaims();

            Assert.IsTrue(allClaims.Contains(uniqueNewClaim));
        }

        [TestMethod]
        public void RemoveClaim_ShouldReturnTrue()
        {
            Claim foundClaim = _claimsRepository.GetClaimByID(1);
            bool removedSuccessfully = _claimsRepository.RemoveClaim(foundClaim);
            Assert.IsTrue(removedSuccessfully);
        }

        [TestMethod]
        public void AddClaim_ShouldReturnTrue()
        {
            Claim newClaim = new Claim();

            bool added = _claimsRepository.AddClaim(newClaim);

            Assert.IsTrue(added);
        }

        [TestMethod]
        public void GetClaimByID_ShouldReturnCorretData()
        {
            Claim foundClaim = _claimsRepository.GetClaimByID(1);

            Assert.AreEqual(foundClaim, uniqueNewClaim);
        }

        [TestMethod]
        public void GetNextClaim_ShouldReturnCorrectClaim()
        {
            Claim nextClaim = _claimsRepository.GetNextClaim();

            Assert.AreEqual(nextClaim, uniqueNewClaim);
        }
    }
}