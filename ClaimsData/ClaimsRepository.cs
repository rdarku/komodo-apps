using System.Collections.Generic;

namespace ClaimsData
{
    public class ClaimsRepository
    {
        private readonly List<Claim> _claims = new List<Claim>();

        public bool AddClaim(Claim claim)
        {
            int countBeforAdd = _claims.Count;
            _claims.Add(claim);
            return _claims.Count > countBeforAdd;
        }

        public List<Claim> GetAllClaims()
        {
            return _claims;
        }

        public Claim GetClaimByID(int claimID)
        {
            foreach (var claim in _claims)
            {
                if (claim.ClaimID == claimID)
                {
                    return claim;
                }
            }

            return null;
        }

        public bool RemoveClaim(Claim claim)
        {
            return _claims.Remove(claim);
        }

        public bool UpdateClaim(int claimID, Claim claim)
        {
            Claim foundClaim = GetClaimByID(claimID);

            if(foundClaim != null)
            {
                foundClaim.ClaimAmount = claim.ClaimAmount;
                foundClaim.ClaimID = claim.ClaimID;
                foundClaim.ClaimType = claim.ClaimType;
                foundClaim.DateOfClaim = claim.DateOfClaim;
                foundClaim.DateOfIncident = claim.DateOfIncident;
                foundClaim.Description = claim.Description;

                return true;
            }
            
            return false;
        }
    }
}
