using Prison.App.Business;
using Prison.App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data
{
    public class Repository:IRepository
    {
        public ICollection<Detainee> Detainees { get {

                return new List<Detainee>() { new Detainee { LastName = "Tesla", FirstName = "Nikola", Detentions = new List<Detention> { new Detention { DetentionDate = DateTime.Parse("27-04-2018") } } },
                                              new Detainee { LastName = "Einstein", FirstName = "Albert", Detentions = new List<Detention> { new Detention { DetentionDate = DateTime.Parse("28-04-2018") } } },
                                              new Detainee { LastName = "Felini", FirstName = "Federico", Detentions = new List<Detention> { new Detention { DetentionDate = DateTime.Parse("28-04-2018") } } }};
            }  }

        public ICollection<Detention> Detentions { get { return new List<Detention>() { new Detention { DetentionDate = DateTime.Parse("27-04-2018") },
                                                                                        new Detention { DetentionDate = DateTime.Parse("28-04-2018") },
                                                                                        new Detention { DetentionDate = DateTime.Parse("28-04-2018") }}; } } 

        public ICollection<Employee> Employees { get { return new List<Employee>() { new Employee { LastName = "Ment", FirstName = "Ivan",Position="Vertuxai" },
                                                                                     new Employee { LastName = "Shunyavka", FirstName = "Vasya",Position="Vertuxai2" },
                                                                                     new Employee { LastName = "Popov", FirstName = "Igor",Position="Accountant" }}; } }

        public void ErrorMethod()
        {
            throw new StackOverflowException();
        }
    }
}
