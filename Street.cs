using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiparkLibrary
{
    public class Street
    {
        private int _porches;
        protected internal int X { get; private set; }
        protected internal int Y { get; private set; }
        protected internal string Name { get; set; }
        protected internal int NumberOfPorches {
            get 
            {
                return _porches;
            }
            private set 
            { 
                if (value < 1)
                { 
                    throw new StreetException("Number of porches can't be less than 1."); 
                }
                _porches = value;
            } 
        }
        protected internal Street(string str, int porches, int x, int y)
        {
            Name = str;
            X = x;
            Y = y;
            NumberOfPorches=porches;
        }
    }
}
