using System.Collections.Generic;

namespace Badges.Data
{
    public class BadgeRepository
    {
        private readonly Dictionary<int, List<string>> _badges = new Dictionary<int, List<string>>();

        public bool AddBadge(Badge badge)
        {
            int countBeforeAdd = _badges.Count;

            _badges.Add(badge.BadgeID, badge.Doors);

            return _badges.Count > countBeforeAdd;
        }

        public Dictionary<int,List<string>> GetAllBadges() => _badges;

        public Badge GetBadgeByID(int badgeID)
        {
            if(_badges.TryGetValue(badgeID, out List<string> doors))
            {
                return new Badge(badgeID, doors);
            }
            else
            {
                return null;
            }
        }
        
        public bool AddDoorToBadge(int badgeID, string doorName)
        {
            Badge foundBadge = GetBadgeByID(badgeID);

            if(foundBadge != null)
            {
                _badges[badgeID].Add(doorName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveDoorFromBadge(int badgeID, string doorName)
        {
            return _badges[badgeID].Remove(doorName);
        }

        public bool RemoveAllDoorsFromBadge(int badeID)
        {
            Badge foundBadge = GetBadgeByID(badeID);
            if(foundBadge != null)
            {
                if(foundBadge.Doors.Count >0)
                    _badges[badeID].Clear();

                return _badges[badeID].Count == 0;
            }
            return false;
        }
    }
}
