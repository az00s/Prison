using Prison.Common;
using Prison.App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prison.Common.Entities;

namespace Prison.App.Data
{
    public class Repository : IRepository
    {
        public Repository()
        {
            Detainees = new List<Detainee>() { new Detainee { DetaineeID=1,LastName = "Tesla", FirstName = "Nikola", Detentions = new List<Detention> { new Detention { DetentionDate = DateTime.Parse("27-04-2018") } } },
                                              new Detainee { DetaineeID=2,LastName = "Einstein", FirstName = "Albert", Detentions = new List<Detention> { new Detention { DetentionDate = DateTime.Parse("28-04-2018") } } },
                                              new Detainee { DetaineeID=3,LastName = "Felini", FirstName = "Federico", Detentions = new List<Detention> { new Detention { DetentionDate = DateTime.Parse("28-04-2018") } } }};

            Detentions = new List<Detention>() { new Detention { DetentionID=1,DetentionDate = DateTime.Parse("27-04-2018") },
                                                new Detention { DetentionID=2,DetentionDate = DateTime.Parse("28-04-2018") },
                                                new Detention { DetentionID=3,DetentionDate = DateTime.Parse("28-04-2018") }};

            Employees= new List<Employee>() { new Employee { EmployeeID=1,LastName = "Mentov", FirstName = "Ivan", Position = "Overseer" },
                                            new Employee { EmployeeID=1,LastName = "Shunyavka", FirstName = "Vasya", Position = "Administrator" },
                                            new Employee { EmployeeID=1,LastName = "Popov", FirstName = "Igor", Position = "Accountant" }};

            PlacesOfDetention = new List<PlaceOfDetention> { new PlaceOfDetention { PlaceID=1,Address= "212011, г. Могилев, ул. Крупской, 99б" },
                                                            new PlaceOfDetention { PlaceID=2,Address= "213810, г. Бобруйск, ул. Советская, 7а" },
                                                            new PlaceOfDetention { PlaceID=3,Address= "213320, г. Быхов, ул. Авиационная, 11" }};
        }
    
    
        public ICollection<Detainee> Detainees { get; set; }

        public ICollection<Detention> Detentions { get; set; } 

        public ICollection<Employee> Employees { get; set; }

        public ICollection<PlaceOfDetention> PlacesOfDetention { get; set; }

       

        public void ErrorMethod()
        {
            throw new NullReferenceException();
        }
    }
}
