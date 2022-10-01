using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiparkLibrary
{
    public delegate void AccountStateHandler(object sender, AccountEventArgs argument);
    public class AccountEventArgs
    {
        public string Message { get; private set; }
        public AccountEventArgs(string message)
        {
            Message = message;
        }
    }
}
