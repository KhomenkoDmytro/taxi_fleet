using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiparkLibrary
{
    public class Taxipark:ITaxipark
    {
        private event AccountStateHandler Created;
        private event AccountStateHandler SignedIn;
        protected internal ManagerAccount Manager { get; private set; } = new ManagerAccount("Admin", "admin0", 18);
        protected internal List<Street> RoutesGuide { get; private set; } = new List<Street>(10){ new Street("Mazepa street", 5, 0, 0), new Street("Shevchenko street", 10, 40, 0),
            new Street("Kotlyarevsky street", 1, 15, 100), new Street("Lesya Ukraiinka street", 4, -17, 30), new Street("Hulak-Artemovsky street",5, -24, -11),
            new Street("Skovoroda street", 5, 100, 100), new Street("Krushelnytska street", 3, 11, 17), new Street("Khvyliovyi street", 9, -50, -80), new Street("Chornovil street", 7, 50,-22)};
        protected internal List<WorkerAccount> WorkerAccounts { get; private set; } = new List<WorkerAccount>{ new WorkerAccount("Olexandr", "worker1", 21, 2, 5, 4),
            new WorkerAccount("Olexii", "worker2", 35, 10, 7, 6), new WorkerAccount ("Oleh", "worker3", 50, 20, 8, 5) };
        protected internal List<ClientAccount> ClientAccounts { get; private set; }
        public string Name { get; private set; }

        public Taxipark(string name)
        {
            Name = name;
        }
        public int GetNumberRoutes()
        {
            return RoutesGuide.Count;
        }
        public void CreateUserAccount(string name, string password, int age, AccountStateHandler display)
        {
            IsUserNameAlreadyTaken(name);
            ClientAccount newAccount = new ClientAccount(name, password, age);
            if (newAccount == null)
            {
                throw new AccountException("Error, try to create new account once more.");
            }
            if (ClientAccounts == null)
            {
                ClientAccounts = new List<ClientAccount> { newAccount };
            }
            else
            {
                ClientAccounts.Add(newAccount);
            }
            if (Created == null)
            {
                Created += display;
            }
            if (ClientAccounts[ClientAccounts.Count - 1] != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Created?.Invoke(this, new AccountEventArgs("Your account has been succesfully created!"));
                Console.ResetColor();
            }
        }
        public ClientAccount LogInClient(string name, string password, AccountStateHandler display)
        {
            ClientAccount Found = null;
            if (ClientAccounts != null)
            {
                for (int i = 0; i < ClientAccounts.Count; i++)
                {
                    if (ClientAccounts[i].Name == name & ClientAccounts[i].Password == password)
                    {
                        Found = ClientAccounts[i];
                    }
                }
            }
            if (SignedIn == null) 
            {
                SignedIn += display;
            }
            if (Found == null)
            {
                throw new AccountException("Your input of name or password was incorrect! Please, try to log in again.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                SignedIn?.Invoke(this, new AccountEventArgs("You have successfully signed into your account!"));
                Console.ResetColor();
                return Found;
            }
        }
        public WorkerAccount LogInWorker(string name, string password)
        {
            WorkerAccount Found = null;
            if (WorkerAccounts != null)
            {
                for (int i = 0; i < WorkerAccounts.Count; i++)
                {
                    if (WorkerAccounts[i].Name == name & WorkerAccounts[i].Password == password)
                    {
                        Found = WorkerAccounts[i];
                    }
                }
            }
            if (Found == null)
            {
                throw new AccountException("Your input of name or password was incorrect! Please, try to log in again.");
            }
            else
            {
                return Found;
            }
            
        }
        public ManagerAccount LogInManager(string name, string password)
        {
            if (Manager.Name != name)
            {
                throw new AccountException("Your input of name or password was incorrect! Please, try to log in again.");
            }
            else if(Manager.Password != password)
            {
                throw new AccountException("Your input of name or password was incorrect! Please, try to log in again.");
            }

            return Manager;
        }
        public void IsUserNameAlreadyTaken(string name)
        {
            if (ClientAccounts != null)
            {
                for (int i = 0; i < ClientAccounts.Count; i++)
                {
                    if (ClientAccounts[i].Name == name)
                    {
                        throw new AccountException("Your name is already taken. Please use different one.");
                    }
                }
            }
        }
       
        public void OptionalServices()
        {
            Console.WriteLine($" * Air conditioning - 10 UAH\n * Trip with child - 35 UAH \n * Animal transportation - 15 UAH");
        }
        public void ShowInformation()
        {
            Console.WriteLine("\n=================INFORMATION=================\nTariff: 7 UAH per km\n\n Routes:");
            ShowRoutes();
            Console.WriteLine("\nOptional services:");
            OptionalServices();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nIf you make more than 5 orders you will receive 5% discount!");
            Console.ResetColor();
        }
        protected internal void ShowRoutes(int from=-1)
        {
            for (int i = 0; i < RoutesGuide.Count; i++)
            {
                if (i != from-1)
                {
                    Console.Write($"{i + 1} - {RoutesGuide[i].Name}");
                    if (RoutesGuide[i].NumberOfPorches == 1)
                    {
                        Console.WriteLine($", porch №{RoutesGuide[i].NumberOfPorches}");
                    }
                    else
                    {
                        Console.WriteLine($", porches: №1-{RoutesGuide[i].NumberOfPorches}");
                    }
                }
            }
        }
        public List<WorkerAccount> GetWorkers()
        {
            return WorkerAccounts;
        }
        public List<ClientAccount> GetClients()
        {
            return ClientAccounts;
        }

    }   
}
