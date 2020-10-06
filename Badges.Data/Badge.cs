using System.Collections.Generic;

namespace Badges.Data
{
    public class Badge
    {
        public int BadgeID { get; set; }

        public List<string> Doors { get; set; }

        public Badge()
        {
            Doors = new List<string>();
        }

        public Badge(int badgeID, List<string> doors)
        {
            BadgeID = badgeID;
            Doors = doors;
        }
    }
}
