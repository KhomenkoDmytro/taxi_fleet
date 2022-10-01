using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiparkLibrary
{
    interface ITaxipark
    {
        public void CreateUserAccount(string name, string password, int age, AccountStateHandler display);
        public ClientAccount LogInClient(string name, string password, AccountStateHandler display);
        public WorkerAccount LogInWorker(string name, string password);
        public ManagerAccount LogInManager(string name, string password);
    }
}
