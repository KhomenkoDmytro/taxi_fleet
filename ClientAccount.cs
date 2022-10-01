using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiparkLibrary
{
    public class ClientAccount:Account
    {
        private event AccountStateHandler MadeOrder;
        private List<Order> OrderList { get; set; } = new List<Order>();
        protected internal double Discount { get; set; } = 0;
        public ClientAccount(string name, string password, int age) : base(name, password, age) { }
        private void SetDiscount()
        {
            Discount = 0.05;
        }
        protected internal int GetSizeOrderList()
        {
            return OrderList.Count;
        }
        protected internal List<Order> GetOrderList()
        {
            return OrderList;
        }
        public override void ShowInformation()
        {
            Console.WriteLine($" Username: {Name} \t Age: {Age} \t Discount: {Discount * 100}%");
        }
        public void MakeOrder(Taxipark taxipark, AccountStateHandler display)
        {
            Console.WriteLine("=========Online order=========");
            Console.WriteLine(" From: ");
            taxipark.ShowRoutes();
            int from = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" Porch: ");
            int porch = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" Comment for driver (0 - skip):");
            string comment = Convert.ToString(Console.ReadLine());
            Console.WriteLine(" To: ");
            taxipark.ShowRoutes(from);
            int to = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" Do you trip with child? (1 - YES, 0 - NO):");
            bool TripWithChild = Convert.ToBoolean(Convert.ToInt16(Console.ReadLine()));
            Console.WriteLine(" Do you have conditioner? (1 - YES, 0 - NO):");
            bool Conditioner = Convert.ToBoolean(Convert.ToInt16(Console.ReadLine()));
            Console.WriteLine(" Do you have animal with you? (1 - YES, 0 - NO):");
            bool AnimalTransportation = Convert.ToBoolean(Convert.ToInt16(Console.ReadLine()));
            Console.WriteLine(" Write time: (for example: '20:00' or '20:00 2 Jule') ");
            DateTime time = Convert.ToDateTime(Console.ReadLine());
                    
            int index1 = from - 1;
            int index2 = to - 1;
            if (index1 < 0 | index2 < 0 | taxipark.GetNumberRoutes() < index1 | taxipark.GetNumberRoutes() < index2)
            {
                throw new AccountException("The route doesn`t exist.");
            }
            Order NewOrder = new Order(taxipark.RoutesGuide[from - 1], taxipark.RoutesGuide[to - 1], porch, TripWithChild, Conditioner, AnimalTransportation, time, Discount, comment);
            OrderList.Add(NewOrder);
            if (GetSizeOrderList() >= 3)
            {
                SetDiscount();
            }
            NewOrder.ShowOrderInformation();
            if (MadeOrder == null)
            {
                MadeOrder += display;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            MadeOrder?.Invoke(this, new AccountEventArgs("You have made succesfully your order!"));
            Console.ResetColor();
        }
        public void OrderHistory()
        {
            Console.WriteLine("=================ORDER HISTORY=================");
            if (OrderList.Count == 0)
            {
                Console.WriteLine("No orders have been made yet.");
            }
            for (int i = 0; i < OrderList.Count; i++)
            {
                OrderList[i].ShowOrderInformation();
            }
        }
    }
}
