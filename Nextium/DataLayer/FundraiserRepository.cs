using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nextium.Models;

namespace Nextium.DataLayer
{
    public class FundraiserRepository
    {
        private readonly List<Fundraiser> _funds;

        public FundraiserRepository()
        {
            _funds = new List<Fundraiser>
            {
                new Fundraiser(){ Id=1, Description="Desc1", Goal=100000, AmountRaised=20000, Title="Title 1"},
                new Fundraiser(){ Id=2, Description="Desc2", Goal=200000, AmountRaised=50000, Title="Title 2"}
            };
        }


        public IEnumerable<Fundraiser> GetAll()
        {
            return _funds;
        }
    }
}
