using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiparkLibrary
{
    public class AccountException : Exception
    {
        public AccountException(string mes) : base(mes) { }
    }
    public class StreetException : Exception
    {
        public StreetException(string mes) : base(mes) { }
    }
    public class OrderException : Exception
    {
        public OrderException(string mes) : base(mes) { } 
    }
}
