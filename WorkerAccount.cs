using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiparkLibrary
{
    public class WorkerAccount:Account
    {
        private event AccountStateHandler SickLeaveEvent;
        private event AccountStateHandler GetOutOfSickLeaveEvent;
        private event AccountStateHandler DaysOff;
        private event AccountStateHandler DaysExtra;
        private const double _wagePerHour = 150;
        private const double _wageAdditional= 5;
        protected internal int Worklife { get; private set; }
        protected internal int NormalWorkDaysPerWeek { get; private set; }
        protected internal int WorkDaysThisWeek { get; private set; }
        protected internal int WorkHoursPerDay { get; private set; }
        protected internal bool SickLeave { get; private set; } = false;
        protected internal double WeekSalary { get; private set; }
        private void SetWeekSalary()
        {
            if(!SickLeave)
                WeekSalary = (WorkDaysThisWeek) * WorkHoursPerDay * (_wagePerHour + _wageAdditional * Worklife);
            else
                WeekSalary= NormalWorkDaysPerWeek * WorkHoursPerDay * (_wagePerHour + _wageAdditional * Worklife);
        }

        public WorkerAccount(string name, string password, int age, int worklife, int workHoursPerDay, int workDaysPerWeek) : base(name, password, age) 
        {
            Worklife=worklife;
            NormalWorkDaysPerWeek = workDaysPerWeek;
            WorkDaysThisWeek = NormalWorkDaysPerWeek;
            WorkHoursPerDay = workHoursPerDay;
            SetWeekSalary();
        }
        public override void ShowInformation()
        {
            DateTime today = DateTime.Now;
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                WorkDaysThisWeek = NormalWorkDaysPerWeek;
            }
            string ill = "No";
            if (SickLeave)
            {
                ill = "Yes";
            }
            SetWeekSalary();
            Console.WriteLine($" Worker`s name: {Name} \n Age: {Age} \n Worklife: {Worklife} whole years\n" +
                    $" Working days per week: {NormalWorkDaysPerWeek}\n Number of work days this week: {WorkDaysThisWeek}\n Work hours per day: {WorkHoursPerDay} " +
                    $"\n Salary per week: {WeekSalary:f2} UAH \n Sick leave: {ill}");
        }
        public void TakeSickLeave(bool sick, AccountStateHandler display)
        {
            bool previousStatus = SickLeave;
            SickLeave = sick;
            if (SickLeave != previousStatus) {
                if (SickLeave)
                {
                    SickLeaveEvent += display;
                    Console.ForegroundColor = ConsoleColor.Green;
                    SickLeaveEvent?.Invoke(this, new AccountEventArgs("You have taken sick leave."));
                    Console.ResetColor();
                    SickLeaveEvent -= display;
                }
                if (!SickLeave)
                {
                    GetOutOfSickLeaveEvent += display;
                    Console.ForegroundColor = ConsoleColor.Green;
                    GetOutOfSickLeaveEvent?.Invoke(this, new AccountEventArgs("Great! You get healthy and ready to work well as you did before!"));
                    Console.ResetColor();
                    GetOutOfSickLeaveEvent -= display;
                } 
            }
        }
        public void TakeDaysOff(int days, AccountStateHandler display)
        {
            if (days < 0)
            {
                throw new AccountException("You can`t take this number of days for your rest");
            }
            if (days > WorkDaysThisWeek)
            {
                throw new AccountException("You can't have more day for rest than you actually work");
            }
            if (SickLeave)
            {
                throw new AccountException("In the period of sick leave you can`t change your schedule.");
            }
            WorkDaysThisWeek -= days;
            SetWeekSalary();
            DaysOff += display;
            Console.ForegroundColor = ConsoleColor.Green;
            DaysOff?.Invoke(this, new AccountEventArgs("You have taken days off succesfully!"));
            Console.ResetColor();
            DaysOff -= display;
            
        }
        public void TakeMoreWorkDays(int days, AccountStateHandler display)
        {
            if (days < 1)
            {
                throw new AccountException("Incorrect number for extra days for working.");
            }
            else if (WorkDaysThisWeek + days > 7)
            {
                throw new AccountException("You can`t work more than 7 days per week.");
            }
            else if (SickLeave)
            {
                throw new AccountException("In the period of sick leave you can`t change your schedule.");
            }
            else
            {
                WorkDaysThisWeek += days;
            }
            SetWeekSalary();
            DaysExtra += display;
            Console.ForegroundColor = ConsoleColor.Green;
            DaysExtra?.Invoke(this, new AccountEventArgs("You have taken extra days for working succesfully!"));
            Console.ResetColor();
            DaysExtra -= display;
        }
        
    }
}
