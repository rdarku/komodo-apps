using System;

namespace ClaimsData
{
    public class Claim
    {
        public int ClaimID { get; set; }

        public ClaimType ClaimType { get; set; }

        public string Description { get; set; }

        public Decimal ClaimAmount { get; set; }

        public DateTime DateOfIncident { get; set; }

        public DateTime DateOfClaim { get; set; }

        public bool IsValid { get; set; }
    }
}
