using Badges.Data;
using KomodoApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badge.UI
{
    public class BadgeManager
    {
        private BadgeRepository _badgeRepository = new BadgeRepository();

        private readonly IConsole _console;

        private bool _keepRunning = true;

        public BadgeManager(IConsole console)
        {
            _console = console;
        }

        public void Run()
        {
            SeedData();
        }

        private void SeedData()
        {
            _badgeRepository.AddBadge(
                new Badge()
            ) ;
        }
    }
}
