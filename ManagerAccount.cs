using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiparkLibrary
{
    public class ManagerAccount:Account
    {
        public ManagerAccount(string name, string password, int age) : base(name, password, age) { }
        public void ShowWorkers(List<WorkerAccount> workers)
        {
            if (workers == null)
            {
                throw new AccountException("No workers have been registrated here.");
            }
            for (int i = 0; i < workers.Count; i++)
            {
                workers[i].ShowInformation();
                Console.WriteLine("=============================================");
            }
        }
        public void HistoryOfOrders(List<ClientAccount> clients)
        {
            if (clients == null)
            {
                throw new AccountException("No clients have been registrated here.");
            }
            for (int i = 0; i < clients.Count; i++)
            {
                int numberOfOrders = clients[i].GetSizeOrderList();
                List<Order> clientOrder=clients[i].GetOrderList();
                Console.WriteLine($"Clients: ");
                clients[i].ShowInformation();
                for (int j = 0; j < numberOfOrders; j++)
                {
                    clientOrder[j].ShowOrderInformation();
                }
            }
        }
    }
}
