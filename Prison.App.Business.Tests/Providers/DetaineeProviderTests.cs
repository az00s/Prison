using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prison.App.Data.Repositories;
using Prison.App.Common.Entities;
using System.Collections.Generic;
using Prison.App.Business.Providers.Impl;
using Prison.App.Business.Services;
using System.Linq;
using System;

namespace Prison.App.Business.Tests
{
    [TestClass]
    public class DetaineeProviderTests
    {
        Mock<IDetaineeRepository> _detaineeRepository;

        Mock<ICachingService> _cacheService;

        DetaineeProvider _detaineeProvider;

        List<Detainee> _testlist;

        List<MaritalStatus> _mStatusTestList;

        Detainee _detainee;

        [TestInitialize]
        public void TestInitialize()
        {
            _detaineeRepository = new Mock<IDetaineeRepository>();
            _cacheService = new Mock<ICachingService>();
            _detaineeProvider = new DetaineeProvider(_detaineeRepository.Object, _cacheService.Object);

            _testlist = new List<Detainee> {
                new Detainee {
                    DetaineeID =1,
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

            _detainee = new Detainee {
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
        public void GetAllDetainees_DetaineeListReturned()
        {
            //arrange
            _detaineeRepository.Setup(m => m.GetAllDetainees()).Returns(_testlist);
            _cacheService.Setup(c => c.Get<IReadOnlyCollection<Detainee>>(It.IsAny<string>())).Returns<IReadOnlyCollection<Detainee>>(null);


            //act
            List<Detainee> resultList = _detaineeProvider.GetAllDetainees().ToList();


            //assert
            CollectionAssert.AreEqual(_testlist,resultList);
            _cacheService.Verify(c => c.Get<IReadOnlyCollection<Detainee>>(It.IsAny<string>()), Times.Once);
            _detaineeRepository.Verify(r => r.GetAllDetainees(), Times.Once);
        }

        [TestMethod]
        public void GetDetaineeByID_ValidId_DetaineeReturned()
        {
            //arrange
            int id = 3;
            _detaineeRepository.Setup(m => m.GetDetaineeByID(It.IsAny<int>())).Returns(_detainee);

            //act
            Detainee result = _detaineeProvider.GetDetaineeByID(id);

            //assert
            Assert.AreEqual(id,result.DetaineeID);
            _detaineeRepository.Verify(r => r.GetDetaineeByID(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDetaineeByID_InvalidId_ExceptionReturned()
        {
            //arrange
            int id = -1;

            //act
            _detaineeProvider.GetDetaineeByID(id);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetDetaineeByID_ValidId_ExceptionReturned()
        {
            //arrange
            int id = 2;

            //act
            _detaineeProvider.GetDetaineeByID(id);
        }

        [TestMethod]
        public void GetDetaineesByDate_ValidDate_DetaineeListReturned()
        {
            //arrange
            DateTime date = new DateTime(2018,5,3);
            _detaineeRepository.Setup(m => m.GetDetaineesByDate(It.IsAny<DateTime>())).Returns(_testlist);

            //act
            List<Detainee> resultList = _detaineeProvider.GetDetaineesByDate(date).ToList();

            //assert
            Assert.IsNotNull(resultList);
            Assert.IsTrue(resultList.Count>0);
            CollectionAssert.AreEqual(_testlist,resultList);
            _detaineeRepository.Verify(r => r.GetDetaineesByDate(It.IsAny<DateTime>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDetaineesByDate_InvalidDate_ExceptionReturned()
        {
            //arrange
            DateTime date = new DateTime(2019, 5, 3);

            //act
            _detaineeProvider.GetDetaineesByDate(date);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetDetaineesByDate_ValidDate_ExceptionReturned()
        {
            //arrange
            DateTime date = new DateTime(2018, 5, 3);

            //act
            _detaineeProvider.GetDetaineesByDate(date);
        }

        [TestMethod]
        public void GetAllMaritalStatuses_StatusesListReturned()
        {
            //arrange
            _detaineeRepository.Setup(m => m.GetAllMaritalStatuses()).Returns(_mStatusTestList);

            //act
            List<MaritalStatus> resultList = _detaineeProvider.GetAllMaritalStatuses().ToList();


            //assert
            CollectionAssert.AreEqual(_mStatusTestList,resultList);
            _detaineeRepository.Verify(r => r.GetAllMaritalStatuses(), Times.Once);
        }

        [TestMethod]
        public void Find_ValidArguments_DetaineeListReturned()
        {
            //arrange
            //Arguments:
            string FirstName = "John";
            string LastName = "Doe";
            string MiddleName = "";
            string ResidenceAddress = "someAddress";
            string DetentionDate = "03.05.2018";
            _testlist.Add(_detainee);
            _detaineeRepository.Setup(m => m.Find(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_testlist);
            
            //act
            List<Detainee> result = _detaineeProvider.Find(DetentionDate,FirstName, LastName,MiddleName,ResidenceAddress).ToList();

            //assert
            Assert.AreEqual(FirstName, result[2].FirstName);
            Assert.AreEqual(LastName, result[2].LastName);
            Assert.AreEqual(MiddleName, result[2].MiddleName);
            Assert.AreEqual(ResidenceAddress, result[2].ResidenceAddress);
            Assert.AreEqual(DetentionDate, result[2].Detentions.ToList()[0].DetentionDate.ToShortDateString());

            _detaineeRepository.Verify(r => r.Find(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void GetDetaineesByParams_InvalidArguments_NullReturned()
        {
            //arrange
            string DetentionDate = "-----";
            _detaineeRepository.Setup(m => m.Find(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(()=>null);

            //act
            var result = _detaineeProvider.Find(DetentionDate, null, null, null, null);

            //assert
            Assert.IsNull(result);

            _detaineeRepository.Verify(r => r.Find(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
