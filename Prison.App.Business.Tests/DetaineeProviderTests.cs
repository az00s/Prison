﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            _cacheService.Setup(c => c.Get<IEnumerable<Detainee>>(It.IsAny<string>())).Returns<IEnumerable<Detainee>>(null);


            //act
            List<Detainee> resultList = _detaineeProvider.GetAllDetainees().ToList();


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

            Assert.AreEqual(2, resultList[1].DetaineeID);
            Assert.AreEqual("Doe", resultList[1].LastName);
            Assert.AreEqual("John", resultList[1].FirstName);
            Assert.AreEqual("empty", resultList[1].MiddleName);
            Assert.AreEqual("somewhere", resultList[1].WorkPlace);
            Assert.AreEqual("someplace", resultList[1].ResidenceAddress);
            Assert.AreEqual("some data", resultList[1].AdditionalData);
            Assert.AreEqual(DateTime.MaxValue, resultList[1].BirstDate);
            Assert.AreEqual(null, resultList[1].ImagePath);
            Assert.AreEqual(2, resultList[1].MaritalStatusID);
            Assert.AreEqual(null, resultList[1].PhoneNumbers);
            Assert.AreEqual(null, resultList[1].Detentions);

            _cacheService.Verify(c => c.Get<IEnumerable<Detainee>>(It.IsAny<string>()), Times.Once);
            _detaineeRepository.Verify(r => r.GetAllDetainees(), Times.Once);


        }

        [TestMethod]
        public void GetDetaineeByID_ArgumentInt2_DetaineeWithID2Returned()
        {
            //arrange
            int id = 2;
            _detaineeRepository.Setup(m => m.GetDetaineeByID(It.IsAny<int>())).Returns(_testlist[1]);
            _cacheService.Setup(c => c.Get<Detainee>(It.IsAny<string>())).Returns<Detainee>(null);

            //act
            Detainee result = _detaineeProvider.GetDetaineeByID(id);

            //assert
            Assert.AreEqual(id,result.DetaineeID);
            _cacheService.Verify(c => c.Get<Detainee>(It.IsAny<string>()), Times.Once);
            _detaineeRepository.Verify(r => r.GetDetaineeByID(It.IsAny<int>()), Times.Once);

        }

        [TestMethod]
        public void GetDetaineesByDate_inputValidDate03052018_DetaineeListReturned()
        {
            //arrange
            DateTime date = new DateTime(2018,5,3);
            _detaineeRepository.Setup(m => m.GetDetaineesByDate(It.IsAny<DateTime>())).Returns(_testlist);
            _cacheService.Setup(c => c.Get<IEnumerable<Detainee>>(It.IsAny<string>())).Returns<IEnumerable<Detainee>>(null);

            //act
            List<Detainee> result = _detaineeProvider.GetDetaineesByDate(date).ToList();

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count>0);
            _cacheService.Verify(c => c.Get<IEnumerable<Detainee>>(It.IsAny<string>()), Times.Once);
            _detaineeRepository.Verify(r => r.GetDetaineesByDate(It.IsAny<DateTime>()), Times.Once);
        }

        [TestMethod]
        public void GetAllMaritalStatuses_StatusesListReturned()
        {
            //arrange
            _detaineeRepository.Setup(m => m.GetAllMaritalStatuses()).Returns(_mStatusTestList);
            _cacheService.Setup(c => c.Get<IEnumerable<MaritalStatus>>(It.IsAny<string>())).Returns<IEnumerable<MaritalStatus>>(null);


            //act
            List<MaritalStatus> resultList = _detaineeProvider.GetAllMaritalStatuses().ToList();


            //assert
            Assert.AreEqual(_mStatusTestList[0].StatusID, resultList[0].StatusID);
            Assert.AreEqual(_mStatusTestList[0].StatusName, resultList[0].StatusName);
            Assert.AreEqual(_mStatusTestList[1].StatusID, resultList[1].StatusID);
            Assert.AreEqual(_mStatusTestList[1].StatusName, resultList[1].StatusName);

            _cacheService.Verify(c => c.Get<IEnumerable<MaritalStatus>>(It.IsAny<string>()), Times.Once);
            _detaineeRepository.Verify(r => r.GetAllMaritalStatuses(), Times.Once);
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
            _detaineeRepository.Setup(m => m.GetDetaineesByParams(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_testlist);
            
            //act
            List<Detainee> result = _detaineeProvider.GetDetaineesByParams(DetentionDate,FirstName, LastName,MiddleName,ResidenceAddress).ToList();

            //assert
            Assert.AreEqual(FirstName, result[2].FirstName);
            Assert.AreEqual(LastName, result[2].LastName);
            Assert.AreEqual(MiddleName, result[2].MiddleName);
            Assert.AreEqual(ResidenceAddress, result[2].ResidenceAddress);
            Assert.AreEqual(DetentionDate, result[2].Detentions.ToList()[0].DetentionDate.ToShortDateString());

            _detaineeRepository.Verify(r => r.GetDetaineesByParams(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
