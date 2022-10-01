using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiparkLibrary
{
    public abstract class Account
    {
        private int _age;
        private string _name;
        private string _password;
        protected internal string Password 
        { 
            get 
            { 
                return _password;
            }
            private set
            {
                if (value.Length>18|value.Length<6)
                {
                    throw new AccountException("Password must have at least 6 symbols but not have more than 18 symbols.");
                }
                for (int i = 0; i < value.Length; i++)
                {
                    if (Char.IsWhiteSpace(value[i]))
                    {
                        throw new AccountException("Password must not consist any spaces!");
                    }
                }
                _password = value;
            }
        }

        protected internal string Name 
        { 
            get 
            {
                return _name;
            }
            private set 
            {
                if (value.Length > 30)
                {
                    throw new AccountException("Your name is too big! Your name must be from 1 to 30 letters!");
                }
                else if (value.Length == 0)
                {
                    throw new AccountException("Please, write your user name.");
                }
                for (int i = 0; i < value.Length; i++)
                {
                    if (Char.IsWhiteSpace(value[i]))
                    {
                        throw new AccountException("Your name mustn`t consist any spaces!");
                    }
                }
                _name = value;
            }
        }
        protected internal int Age 
        {
            get 
            { 
                return _age;
            }
            private set 
            { 
                if (value < 18)
                {
                    throw new AccountException("Sorry, our application for people who are older than 18 y.o.");
                }
                else if (value > 118)
                {
                    throw new AccountException("The oldest person of the world is 118 y.o.. Please, write your real age!");
                }
                else
                {
                    _age = value;
                }
            }
        }
        protected internal Account(string name, string password, int age)
        {
            Name = name;
            Password = password;
            Age = age;
        }
        public virtual void ShowInformation() 
        {
            Console.WriteLine($" Username: {Name} \t Age: {Age}");
        }

    }
}
