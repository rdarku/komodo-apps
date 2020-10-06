using Claims.Data;
using KomodoApps;
using System;
using System.Threading;

namespace Claims.UI
{
    public class ClaimsManager
    {
        private readonly ClaimsRepository _claimsRepository = new ClaimsRepository();

        private readonly IConsole _console;

        private bool _keepRunning = true;

        public ClaimsManager(IConsole console)
        {
            _console = console;
        }

        public void Run()
        {
            SeedClaims();
            RunMainMenu();
        }
        private void RunMainMenu()
        {
            while (_keepRunning)
            {
                PrintBanner();
                _console.WriteLine("Main menu\n" +
                    "================\n" +
                    "Please select an action from the action list below\n" +
                    "1. View all claims\n" +
                    "2. Take care of next claim\n" +
                    "3. Add a new claim\n" +
                    "4. Exit\n");

                string input = _console.ReadLine();

                switch (input.ToLower())
                {
                    case "1":
                        ViewAllClaims();
                        break;
                    case "2":
                        TakeCareOfNextClaim();
                        break;
                    case "3":
                        AddNewClaim();
                        break;
                    case "4":
                        ExitApp();
                        break;
                    default:
                        InvalidInputPrompt();
                        break;
                }
            }
        }

        private void InvalidInputPrompt()
        {
            _console.WriteLine("Invalid input!!! You need to choose between options 1, 2, 3 or 4\n" +
                "To try again with a valid input, enter [C] or any other key to abort.\n");
            var choice = _console.ReadLine();
            if (choice.ToLower() == "c")
            {
                RunMainMenu();
            }
            else
            {
                ReturnOrQuit();
            }
        }

        private void AddNewClaim()
        {
            Claim newClaim = new Claim();
            PrintBanner();

            _console.WriteLine("Add New Claim\n");

            SetClaimId(ref newClaim);
            SetClaimType(ref newClaim);
            SetClaimDescription(ref newClaim);
            SetClaimAmount(ref newClaim);
            SetDateOfIncident(ref newClaim);
            SetDateOfClaim(ref newClaim);
            SetClaimValidStatus(ref newClaim);

            if (_claimsRepository.AddClaim(newClaim))
            {
                _console.WriteLine("\nClaim Added Successfully!.");
            }
            else
            {
                _console.WriteLine("\nCould not Add New Claim.\nPlease try agaon later.\n");
            }

            ReturnOrQuit();
        }

        private void SetClaimId(ref Claim claim)
        {
            _console.WriteLine("\nEnter the claim id (Hint - must be number) :");

            string input = _console.ReadLine();

            if(int.TryParse(input, out int claimID))
            {
                claim.ClaimID = claimID;
            }
            else
            {
                _console.WriteLine("\nInvalid input! The Claim ID must be a number\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");
                string response = _console.ReadLine();
                if(response.ToLower() == "c")
                {
                    SetClaimId(ref claim);
                }
                else
                {
                    ReturnOrQuit();
                }
            }

        }

        private void SetClaimType(ref Claim claim)
        {
            _console.WriteLine("\nEnter the claim type (Car, Home or Theft) :");

            string input = _console.ReadLine();

            switch (input.ToLower())
            {
                case "car":
                    claim.TypeOfClaim = ClaimType.Car;
                    break;
                case "home":
                    claim.TypeOfClaim = ClaimType.Home;
                    break;
                case "theft":
                    claim.TypeOfClaim = ClaimType.Theft;
                    break;
                default:
                    _console.WriteLine("\nInvalid input! The Claim Type must be Car or Home or Theft\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");
                    string response = _console.ReadLine();
                    if (response.ToLower() == "c")
                    {
                        SetClaimType(ref claim);
                    }
                    else
                    {
                        ReturnOrQuit();
                    }
                    break;
            }

        }

        private void SetDateOfIncident(ref Claim claim)
        {
            _console.WriteLine("\n\tDate of incident\n");

            var year = GetYear();
            var month = GetMonth();
            var day = GetDay();

            claim.DateOfIncident = new DateTime(year, month, day);
        }

        private void SetDateOfClaim(ref Claim claim)
        {
            _console.WriteLine("\n\tDate of claim\n");

            var year = GetYear();
            var month = GetMonth();
            var day = GetDay();

            claim.DateOfClaim = new DateTime(year, month, day);
        }

        public int GetYear()
        {
            _console.WriteLine("Enter the year: ");
            string input = _console.ReadLine();
            int year = 0;
            if(int.TryParse(input, out year))
            {
                if(year > DateTime.Now.Year)
                {
                    _console.WriteLine("You cannot set a future date. Please Enter a valid year.");
                    return GetYear();
                }

                return year;
            }
            else
            {
                _console.WriteLine("Invalid input. Please Enter a valid year.");
                return GetYear();
            }
        }

        public int GetMonth()
        {
            _console.WriteLine("Enter the month [1 -12]: ");
            string input = _console.ReadLine();
            int month = 0;
            if (int.TryParse(input, out month))
            {
                if (month > 12)
                {
                    _console.WriteLine("Invalid input! Please Enter a valid month [ 1 - 12 ].");
                    return GetMonth();
                }
                return month;
            }
            else
            {
                _console.WriteLine("Invalid input! Please Enter a valid month.");
                return GetMonth();
            }
        }

        public int GetDay()
        {
            _console.WriteLine("Enter the Day [1 - 31]: ");
            string input = _console.ReadLine();
            int day = 0;
            if (int.TryParse(input, out day))
            {
                if (day > 31)
                {
                    _console.WriteLine("Invalid input! Please Enter a valid day [ 1 - 31 ].");
                    return GetDay();
                }
                return day;
            }
            else
            {
                _console.WriteLine("Invalid input. Please Enter a valid month.");
                return GetDay();
            }
        }

        private void SetClaimDescription(ref Claim claim)
        {
            _console.WriteLine("\nEnter the claim description :\n");

            string input = _console.ReadLine();
            claim.Description = input;
        }

        private void SetClaimValidStatus(ref Claim claim)
        {
            _console.WriteLine("\nIs the Claim Valid (y/n): ");

            bool valid = false;
            string input = _console.ReadLine();
            if(input.ToLower() == "y")
            {
                valid = true;
            }

            claim.IsValid = valid;
        }

        private void SetClaimAmount(ref Claim claim)
        {
            _console.WriteLine("\nEnter the Claim Amount :\n");

            var input = _console.ReadLine();
            if (decimal.TryParse(input, out decimal amount))
            {
                claim.ClaimAmount = amount;
            }
            else
            {
                _console.WriteLine("Invalid input!!! Please type a valid amount\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");
                var choice = _console.ReadLine();

                if (choice.ToLower() == "c")
                {
                    SetClaimAmount(ref claim);
                }
                else
                {
                    ReturnOrQuit();
                }
            }
        }

        private void TakeCareOfNextClaim()
        {
            Claim nextClaim = _claimsRepository.GetNextClaim();

            if(nextClaim != null)
            {
                DisplayClaimDetails(nextClaim);
                DealWithClaim(nextClaim);
            }
            else
            {
                _console.WriteLine("\n There are no pending claims to deal with\n");
                ReturnOrQuit();
            }
        }

        private void DealWithClaim(Claim claim)
        {
            _console.WriteLine("\nDo You want to deal with this claim now? (y/n)\n");

            string input = _console.ReadLine();

            if(input.ToLower() == "y")
            {
                if (_claimsRepository.RemoveClaim(claim))
                {
                    _console.WriteLine("\nClaim Successfully dealt with.");
                }
                else
                {
                    _console.WriteLine("\nCould not deal with this Claim.\n" +
                        "Please try agaon later.\n");
                }
            }
            else
            {
                _console.WriteLine("You chose not to deal with this claim\n");
            }

            ReturnOrQuit();
        }

        private void DisplayClaimDetails(Claim claim)
        {
            PrintBanner();
            string printOutput = "Next Claim\n" +
                 "================\n\n" +
                 $"ClaimID: {claim.ClaimID}\n" +
                 $"Description: {claim.Description}\n" +
                 $"Amount: {claim.ClaimAmount}\n" +
                 $"Date Of Incident: {claim.DateOfIncident}\n" +
                 $"Date Of Claim: {claim.DateOfClaim}\n" +
                 $"Is Valid: {claim.IsValid}\n";

            _console.WriteLine(printOutput);
        }

        private void ViewAllClaims()
        {
            PrintBanner();
            string printOutput = "Claims List\n" +
                "================\n\n" +
                string.Format("{0,-10}{1,-7}{2,-50}{3,-15}{4,-15}{5,-13}{6,-8}\n",
                    "ClaimID", "Type", "Description", "Amount", "Incident Date", "Claim Date", "IsValid");

            foreach(var claim in _claimsRepository.GetAllClaims())
            {
                printOutput += string.Format("{0,-10}{1,-7}{2,-50}{3,-15:C}{4,-15:d}{5,-13:d}{6,-8}\n",
                    claim.ClaimID, claim.TypeOfClaim, claim.Description, claim.ClaimAmount, claim.DateOfIncident, claim.DateOfClaim, claim.IsValid);
            }

            _console.WriteLine(printOutput);
            ReturnOrQuit();
        }

        private void ReturnOrQuit()
        {
            _console.WriteLine("\nHit [Enter] to return to the Main Menu or enter [Q] to Exit the program\n");

            var input = _console.ReadLine();
            switch (input.ToLower())
            {
                case "q":
                    ExitApp();
                    break;
                default:
                    RunMainMenu();
                    break;
            }
        }

        private void ExitApp()
        {
            _keepRunning = false;
            _console.WriteLine("\nThanks you for using the Komodo Claims Manager\n Have a great day!\n");
            Thread.Sleep(3000);
        }

        private void SeedClaims()
        {
            _claimsRepository.AddClaim(
                new Claim(1, ClaimType.Car, 
                "Collision on I70 South", 4500m, 
                new DateTime(20,9,11), 
                new DateTime(20,9,15),true)
            );
            _claimsRepository.AddClaim(
                new Claim(2, ClaimType.Car,
                "Car totalled after Semi collision", 7200m,
                new DateTime(20, 9, 12),
                new DateTime(20, 9, 14), true)
            );
            _claimsRepository.AddClaim(
                new Claim(3, ClaimType.Home,
                "Tree fell on roof during rain storm", 45000m,
                new DateTime(20, 9, 11),
                new DateTime(20, 9, 15), true)
            );
            _claimsRepository.AddClaim(
                new Claim(4, ClaimType.Theft,
                "Car got broken into during carnival celebration", 500m,
                new DateTime(20, 9, 11),
                new DateTime(20, 9, 15), true)
            );
            _claimsRepository.AddClaim(
                new Claim(5, ClaimType.Car,
                "Rear-end collision at 38th and High Street", 700m,
                new DateTime(20, 9, 11),
                new DateTime(20, 9, 15), true)
            );
        }

        private void PrintBanner()
        {
            _console.Clear();
            _console.WriteLine(@"                                _           ___ _       _               
  /\ /\___  _ __ ___   ___   __| | ___     / __\ | __ _(_)_ __ ___  ___ 
 / //_/ _ \| '_ ` _ \ / _ \ / _` |/ _ \   / /  | |/ _` | | '_ ` _ \/ __|
/ __ \ (_) | | | | | | (_) | (_| | (_) | / /___| | (_| | | | | | | \__ \
\/  \/\___/|_| |_| |_|\___/ \__,_|\___/  \____/|_|\__,_|_|_| |_| |_|___/
                                                                        
");
        }
    }
}
