using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiparkLibrary
{
    public class Order
    {
        private const double _additionalPriceTripWithChild = 35;
        private const double _additionalPriceConditioner = 10;
        private const double _additionalPriceAnimalTransportaion = 15;
        private const double _startPrice = 50;
        private const double _tariffPerKm = 7;
        private int _porchPlaceFrom;
        private Street _placeTo;
        private string _comment;
        private DateTime _taxiTime;
        protected internal Street PlaceFrom { get; private set; }
        protected internal DateTime OrderTime { get; private set; }
        protected internal double Distance { get; private set; }
        protected internal double Price { get; private set; }
        protected internal double Option { get; private set; } = 0;
        protected internal bool TripWithChild { get; private set; }
        protected internal bool Conditioner { get; private set; }
        protected internal bool AnimalTransportation { get; private set; }
        protected internal double Discount { get; private set; } = 0;
        protected internal int PorchPlaceFrom 
        { 
            get 
            { 
                return _porchPlaceFrom; 
            }
            private set
            {
                if (PlaceFrom.NumberOfPorches < value | value < 1)
                {
                    throw new OrderException("The porch`s number doesn't exist!");
                }
                _porchPlaceFrom = value;
            }
        }
        protected internal Street PlaceTo 
        {
            get
            {
                return _placeTo;
            }
            private set
            {
                if (value == PlaceFrom)
                {
                    throw new OrderException("You have chosen same depature and arrival location. Please try make an order again ");
                }
                _placeTo = value;
            }
        }
        protected internal string Comment 
        {
            get
            {
                return _comment;
            }
            private set
            {
                if (value == "0")
                {
                    _comment = null;
                }
                else
                {
                    _comment = value;
                }
            }
        }
        protected internal DateTime TaxiTime 
        { 
            get 
            {
                return _taxiTime;
            }
            private set
            {
                
                if (DateTime.Compare(value, OrderTime)<=0)
                {
                    throw new OrderException("The date is not correct");
                }
                _taxiTime = value;
            }
        }
        protected internal Order(Street from, Street to, int porch,  bool tripWithChild, bool conditioner, bool animalTransportation, DateTime time, double discount, string comment = null)
        {
            OrderTime = DateTime.Now;
            PlaceFrom = from;
            PlaceTo = to;
            PorchPlaceFrom = porch;
            Comment = comment;
            TaxiTime = time;
            Discount = discount;
            SetDistance();
            TripWithChild = tripWithChild;
            Conditioner = conditioner;
            AnimalTransportation = animalTransportation;
            CalculateOption();
            CalculatePrice();
        }
        private void CalculateOption()
        {
            if (TripWithChild)
            {
                Option += _additionalPriceTripWithChild;
            }
            if (Conditioner)
            {
                Option += _additionalPriceConditioner;
            }
            if (AnimalTransportation)
            {
                Option += _additionalPriceAnimalTransportaion;
            }
        }
        private void CalculatePrice()
        {
            Price = _startPrice + Distance * _tariffPerKm + Option;
            Price -=Price * Discount;
        }
        private void SetDistance()
        {
            Distance = Math.Sqrt(Math.Pow(PlaceFrom.X - PlaceTo.X, 2) + Math.Pow(PlaceFrom.Y - PlaceTo.Y, 2));
        }

        protected internal void ShowOrderInformation()
        {
            Console.WriteLine($"=========Information about order ({OrderTime})=========\nFrom: {PlaceFrom.Name}, porch №{PorchPlaceFrom}\nTo: {PlaceTo.Name}\nTime: {TaxiTime}\nDistance between: {Distance:f3} km");
            if (Comment != null)
            {
                Console.WriteLine($"Comment for driver: {Comment}");
            }
            string TripWithChildStr = "No";
            string ConditionerStr = "No";
            string AnimalTransportationStr = "No";

            if (TripWithChild)
            {
                TripWithChildStr="Yes(+35 UAH)" ;
            }
            if (Conditioner)
            {
                ConditionerStr = "Yes(+10 UAH)";
            }
            if(AnimalTransportation)
            {
                AnimalTransportationStr = "Yes(+15 UAH)";
            }
            Console.WriteLine($"Trip with children: {TripWithChildStr}");
            Console.WriteLine($"Conditioner: {ConditionerStr}");
            Console.WriteLine($"Animal transportation: {AnimalTransportationStr}");
            if (Discount == 0)
            {
                Console.WriteLine($"Price: {Price:f2} UAH");
            }
            else
            {
                Console.WriteLine($"Price: {Price:f2} UAH with {Discount * 100}% discount.");
            }
        }
    }
}
