using System.Collections.Generic;

namespace Claims.Data
{
    public class ClaimsRepository
    {
        private readonly Queue<Claim> _claims = new Queue<Claim>();

        public bool AddClaim(Claim claim)
        {
            int countBeforAdd = _claims.Count;
            _claims.Enqueue(claim);
            return _claims.Count > countBeforAdd;
        }

        public Queue<Claim> GetAllClaims()
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

        public Claim GetNextClaim()
        {
            return (_claims.Count > 0) ? _claims.Peek() : null;
           
        }

        public bool RemoveClaim(Claim claim)
        {
            var removedClaim = _claims.Dequeue();
            return (claim == removedClaim);
        }

        public bool UpdateClaim(int claimID, Claim claim)
        {
            Claim foundClaim = GetClaimByID(claimID);

            if(foundClaim != null)
            {
                foundClaim.ClaimAmount = claim.ClaimAmount;
                foundClaim.ClaimID = claim.ClaimID;
                foundClaim.TypeOfClaim = claim.TypeOfClaim;
                foundClaim.DateOfClaim = claim.DateOfClaim;
                foundClaim.DateOfIncident = claim.DateOfIncident;
                foundClaim.Description = claim.Description;

                return true;
            }
            
            return false;
        }
    }
}
