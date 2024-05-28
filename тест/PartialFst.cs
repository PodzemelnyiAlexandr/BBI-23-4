using Interfaces;
using System;
namespace Classes
{
    public partial class Festival : ICounter
    {
        private int peopleAmount = new Random().Next(15, 50);
        public int DailyFlow { get { if ((EndDate - StartDate).Days == 0) { return 0; } return int.Parse((totalAmount / (EndDate - StartDate).Days).ToString()); } }
        public decimal Revenue { get { return this.DailyRevenue * (EndDate - StartDate).Days; } }
        public decimal DailyRevenue { get { return ticketPrice * peopleAmount; } }
    }
}