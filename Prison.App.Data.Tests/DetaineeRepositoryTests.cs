using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prison.App.Common.Entities;
using Prison.App.Data.DataContext;
using Prison.App.Data.Repositories;

namespace Prison.App.Data.Tests
{
    [TestClass]
    public class DetaineeRepositoryTests
    {
        Mock<IDetaineeDataContext> _detaineeContext;

        DetaineeRepository _detaineeRepository;

        List<Detainee> _testlist;

        Detainee _detainee;

        List<MaritalStatus> _mStatusTestList;

        [TestInitialize]
        public void TestInitialize()
        {
            _detaineeContext = new Mock<IDetaineeDataContext>();

            _detaineeRepository = new DetaineeRepository(_detaineeContext.Object);

            _testlist = new List<Detainee> {
                new Detainee {
                    DetaineeID =8,
                    LastName ="Doe",
                    FirstName ="John",
                    MiddleName ="empty",
                    WorkPlace ="somewhere",
                    ResidenceAddress ="someplace",
                    AdditionalData ="some data",
                    BirstDate =DateTime.MinValue,
                    MaritalStatusID =3,
                    ImagePath =null,
                    Detentions =new List<Detention>
                    {
                        new Detention { DetentionDate=new DateTime(2018,5,3)}
                    },
                    PhoneNumbers =null
                },
                new Detainee {
                    DetaineeID =2,
                    LastName ="Doe",
                    FirstName ="John",
                    MiddleName ="empty",
                    WorkPlace ="somewhere",
                    ResidenceAddress ="someplace",
                    AdditionalData ="some data",
                    BirstDate =DateTime.MaxValue,
                    MaritalStatusID =2,
                    ImagePath =null,
                    Detentions =null,
                    PhoneNumbers =null
                } };
            
            _mStatusTestList = new List<MaritalStatus>
            {
                new MaritalStatus { StatusID=1,StatusName="Married"},
                new MaritalStatus { StatusID=2,StatusName="Single"}
            };

            _detainee = new Detainee
            {
                DetaineeID = 3,
                LastName = "Doe",
                FirstName = "John",
                MiddleName = "",
                WorkPlace = "somewhere",
                ResidenceAddress = "someAddress",
                AdditionalData = "some data",
                BirstDate = DateTime.MinValue,
                MaritalStatusID = 3,
                ImagePath = null,
                Detentions = new List<Detention>
                    {
                        new Detention { DetentionDate=new DateTime(2018,5,3)}
                    },
                PhoneNumbers = null
            };

        }

        [TestMethod]
        public void GetDetaineeByID_ValidId_DetaineeReturned()
        {
            //arrange
            int id = 2;
            _detaineeContext.Setup(m => m.GetDetaineeByID(It.IsAny<int>())).Returns(_testlist[1]);

            //act
            Detainee result = _detaineeRepository.GetDetaineeByID(id);

            //assert
            Assert.AreEqual(id, result.DetaineeID);
            _detaineeContext.Verify(r => r.GetDetaineeByID(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetAllDetainees_DetaineeListReturned()
        {
            //arrange
            _detaineeContext.Setup(m => m.GetAllDetainees()).Returns(_testlist);


            //act
            List<Detainee> resultList = _detaineeRepository.GetAllDetainees().ToList();


            //assert
            Assert.AreEqual(_testlist[0].DetaineeID, resultList[0].DetaineeID);
            Assert.AreEqual(_testlist[0].LastName, resultList[0].LastName);
            Assert.AreEqual(_testlist[0].FirstName, resultList[0].FirstName);
            Assert.AreEqual(_testlist[0].MiddleName, resultList[0].MiddleName);
            Assert.AreEqual(_testlist[0].WorkPlace, resultList[0].WorkPlace);
            Assert.AreEqual(_testlist[0].ResidenceAddress, resultList[0].ResidenceAddress);
            Assert.AreEqual(_testlist[0].AdditionalData, resultList[0].AdditionalData);
            Assert.AreEqual(_testlist[0].BirstDate, resultList[0].BirstDate);
            Assert.AreEqual(_testlist[0].ImagePath, resultList[0].ImagePath);
            Assert.AreEqual(_testlist[0].MaritalStatusID, resultList[0].MaritalStatusID);
            Assert.AreEqual(_testlist[0].PhoneNumbers, resultList[0].PhoneNumbers);
            Assert.AreEqual(_testlist[0].Detentions.ToList()[0].DetentionDate, resultList[0].Detentions.ToList()[0].DetentionDate);

            Assert.AreEqual(_testlist[1].DetaineeID, resultList[1].DetaineeID);
            Assert.AreEqual(_testlist[1].LastName, resultList[1].LastName);
            Assert.AreEqual(_testlist[1].FirstName, resultList[1].FirstName);
            Assert.AreEqual(_testlist[1].MiddleName, resultList[1].MiddleName);
            Assert.AreEqual(_testlist[1].WorkPlace, resultList[1].WorkPlace);
            Assert.AreEqual(_testlist[1].ResidenceAddress, resultList[1].ResidenceAddress);
            Assert.AreEqual(_testlist[1].AdditionalData, resultList[1].AdditionalData);
            Assert.AreEqual(_testlist[1].BirstDate, resultList[1].BirstDate);
            Assert.AreEqual(_testlist[1].ImagePath, resultList[1].ImagePath);
            Assert.AreEqual(_testlist[1].MaritalStatusID, resultList[1].MaritalStatusID);
            Assert.AreEqual(_testlist[1].PhoneNumbers, resultList[1].PhoneNumbers);
            Assert.AreEqual(_testlist[1].Detentions, resultList[1].Detentions);

            _detaineeContext.Verify(r => r.GetAllDetainees(), Times.Once);


        }

        [TestMethod]
        public void GetDetaineesByDate_ValidDate_DetaineeListReturned()
        {
            //arrange
            DateTime date = new DateTime(2018, 5, 3);
            _detaineeContext.Setup(m => m.GetDetaineesByDate(It.IsAny<DateTime>())).Returns(_testlist);

            //act
            List<Detainee> result = _detaineeRepository.GetDetaineesByDate(date).ToList();

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            _detaineeContext.Verify(r => r.GetDetaineesByDate(It.IsAny<DateTime>()), Times.Once);
        }

        [TestMethod]
        public void GetAllMaritalStatuses_StatusesListReturned()
        {
            //arrange
            _detaineeContext.Setup(m => m.GetAllMaritalStatuses()).Returns(_mStatusTestList);

            //act
            List<MaritalStatus> resultList = _detaineeRepository.GetAllMaritalStatuses().ToList();

            //assert
            Assert.AreEqual(_mStatusTestList[0].StatusID, resultList[0].StatusID);
            Assert.AreEqual(_mStatusTestList[0].StatusName, resultList[0].StatusName);
            Assert.AreEqual(_mStatusTestList[1].StatusID, resultList[1].StatusID);
            Assert.AreEqual(_mStatusTestList[1].StatusName, resultList[1].StatusName);

            _detaineeContext.Verify(r => r.GetAllMaritalStatuses(), Times.Once);
        }

        [TestMethod]
        public void GetDetaineesByParams_ValidArguments_DetaineeListReturned()
        {
            //arrange
            //Arguments:
            string FirstName = "John";
            string LastName = "Doe";
            string MiddleName = "";
            string ResidenceAddress = "someAddress";
            string DetentionDate = "03.05.2018";

            _testlist.Add(_detainee);
            _detaineeContext.Setup(m => m.Find(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_testlist);

            //act
            List<Detainee> result = _detaineeRepository.GetDetaineesByParams(DetentionDate, FirstName, LastName, MiddleName, ResidenceAddress).ToList();

            //assert
            Assert.AreEqual(FirstName, result[2].FirstName);
            Assert.AreEqual(LastName, result[2].LastName);
            Assert.AreEqual(MiddleName, result[2].MiddleName);
            Assert.AreEqual(ResidenceAddress, result[2].ResidenceAddress);
            Assert.AreEqual(DetentionDate, result[2].Detentions.ToList()[0].DetentionDate.ToShortDateString());

            _detaineeContext.Verify(r => r.Find(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

    }
}
