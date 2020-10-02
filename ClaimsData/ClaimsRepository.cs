using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
