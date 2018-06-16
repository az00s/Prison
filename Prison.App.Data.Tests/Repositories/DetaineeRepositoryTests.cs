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
            int id = 3;
            _detaineeContext.Setup(m => m.GetDetaineeByID(It.IsAny<int>())).Returns(_detainee);

            //act
            Detainee result = _detaineeRepository.GetDetaineeByID(id);

            //assert
            Assert.AreEqual(id, result.DetaineeID);
            _detaineeContext.Verify(r => r.GetDetaineeByID(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetDetaineeByID_InvalidId_NullReturned()
        {
            //arrange
            int id = -3;

            //act
            Detainee result = _detaineeRepository.GetDetaineeByID(id);

            //assert
            Assert.IsNull(result);
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
            CollectionAssert.AreEqual(_testlist,resultList);
            _detaineeContext.Verify(r => r.GetAllDetainees(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetAllDetainees_ExceptionReturned()
        {
            //arrange
            _detaineeContext.Setup(m => m.GetAllDetainees()).Throws<InvalidCastException>();

            //act
            _detaineeRepository.GetAllDetainees();

        }

        [TestMethod]
        public void GetDetaineesByDate_ValidDate_DetaineeListReturned()
        {
            //arrange
            DateTime date = new DateTime(2018, 5, 3);
            _detaineeContext.Setup(m => m.GetDetaineesByDate(It.IsAny<DateTime>())).Returns(_testlist);

            //act
            var result = _detaineeRepository.GetDetaineesByDate(date);

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsInstanceOfType(result,typeof(List<Detainee>));
            _detaineeContext.Verify(r => r.GetDetaineesByDate(It.IsAny<DateTime>()), Times.Once);
        }

        [TestMethod]
        public void GetDetaineesByDate_InvalidDate_NullReturned()
        {
            //arrange
            DateTime date = new DateTime(2019, 5, 3);

            //act
            var result = _detaineeRepository.GetDetaineesByDate(date);

            //assert
            Assert.IsNull(result);
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
            CollectionAssert.AreEqual(_mStatusTestList,resultList);
            _detaineeContext.Verify(r => r.GetAllMaritalStatuses(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetAllMaritalStatuses_ExceptionReturned()
        {
            //arrange
            _detaineeContext.Setup(m => m.GetAllMaritalStatuses()).Throws<InvalidOperationException>();

            //act
            _detaineeRepository.GetAllMaritalStatuses();
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
            List<Detainee> result = _detaineeRepository.Find(DetentionDate, FirstName, LastName, MiddleName, ResidenceAddress).ToList();

            //assert
            Assert.AreEqual(FirstName, result[2].FirstName);
            Assert.AreEqual(LastName, result[2].LastName);
            Assert.AreEqual(MiddleName, result[2].MiddleName);
            Assert.AreEqual(ResidenceAddress, result[2].ResidenceAddress);
            Assert.AreEqual(DetentionDate, result[2].Detentions.ToList()[0].DetentionDate.ToShortDateString());

            _detaineeContext.Verify(r => r.Find(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void GetDetaineesByParams_InvalidArguments_NullReturned()
        {
            //arrange
            //Arguments:
            string FirstName = "-2";
            string LastName = "^&";
            string MiddleName = null;
            string ResidenceAddress = "___";
            string DetentionDate = "+";

            //act
            var result=_detaineeRepository.Find(DetentionDate, FirstName, LastName, MiddleName, ResidenceAddress);

            //assert
            Assert.IsNull(result);
            _detaineeContext.Verify(r => r.Find(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        }

    }
}
