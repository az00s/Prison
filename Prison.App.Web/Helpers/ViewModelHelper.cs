using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prison.App.Web.Helpers
{
    public static class ViewModelHelper
    {
        public static IEnumerable<EmployeeIndexViewModel> ToEmployeeIndexViewModel(IEnumerable<Employee> list,IPositionProvider pos)
        {
            var Positions = pos.GetAllRecordsFromTable();
            List<EmployeeIndexViewModel> ResultList = new List<EmployeeIndexViewModel>();
            foreach (Employee item in list)
            {
                ResultList.Add(new EmployeeIndexViewModel
                {
                    EmployeeID = item.EmployeeID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MiddleName = item.MiddleName,
                    Position = Positions.First(p => p.PositionID == item.PositionID).PositionName
                });
            }

            return ResultList;
        }

        public static EmployeeIndexViewModel ToEmployeeIndexViewModel(Employee emp, IPositionProvider pos)
        {
            var Positions = pos.GetAllRecordsFromTable();
            EmployeeIndexViewModel Result = new EmployeeIndexViewModel
                {
                    EmployeeID = emp.EmployeeID,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    MiddleName = emp.MiddleName,
                    Position = Positions.First(p => p.PositionID == emp.PositionID).PositionName
                };
           
            return Result;
        }

        public static EmployeeEditViewModel ToEmployeeEditViewModel(Employee emp, IPositionProvider pos)
        {
            var Positions = pos.GetAllRecordsFromTable();
            EmployeeEditViewModel Result = new EmployeeEditViewModel
            {
                EmployeeID = emp.EmployeeID,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                MiddleName = emp.MiddleName,
                Positions = Positions,
                PositionID=emp.PositionID
            };

            return Result;
        }

        public static DetaineeDetailsViewModel ToDetaineeDetailsViewModel(Detainee dtn,IDetaineeProvider db)
        {
            var statuses = db.GetAllMaritalStatusesFromTable();

            DetaineeDetailsViewModel Result = new DetaineeDetailsViewModel
            {
                DetaineeID = dtn.DetaineeID,
                FirstName = dtn.FirstName,
                LastName = dtn.LastName,
                MiddleName = dtn.MiddleName,
                BirstDate = dtn.BirstDate.ToLongDateString(),
                MaritalStatus = statuses.First(s=>s.StatusID==dtn.MaritalStatusID).StatusName,
                ImagePath = dtn.ImagePath,
                WorkPlace=dtn.WorkPlace,
                ResidenceAddress=dtn.ResidenceAddress,
                AdditionalData=dtn.AdditionalData,
                Detentions=dtn.Detentions,
                PhoneNumbers=dtn.PhoneNumbers
            };

            return Result;
        }

        public static IEnumerable<DetaineeIndexViewModel> ToDetaineeIndexViewModel(IEnumerable<Detainee> list)
        {
            List<DetaineeIndexViewModel> ResultList = new List<DetaineeIndexViewModel>();

            foreach (Detainee item in list)
            {
                ResultList.Add(new DetaineeIndexViewModel
                {
                    DetaineeID = item.DetaineeID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MiddleName = item.MiddleName,
                    BirstDate = item.BirstDate.ToShortDateString(),
                    ImagePath=item.ImagePath
                });
            }

            

            return ResultList;
        }

        public static DetaineeEditViewModel ToDetaineeEditViewModel(Detainee dtn, IDetaineeProvider db)
        {
            var statuses = db.GetAllMaritalStatusesFromTable();

            DetaineeEditViewModel Result = new DetaineeEditViewModel
            {
                DetaineeID = dtn.DetaineeID,
                FirstName = dtn.FirstName,
                LastName = dtn.LastName,
                MiddleName = dtn.MiddleName,
                BirstDate = dtn.BirstDate.ToLocalTime(),
                MaritalStatus = statuses,
                MaritalStatusID=dtn.MaritalStatusID,
                ImagePath = dtn.ImagePath,
                WorkPlace = dtn.WorkPlace,
                ResidenceAddress = dtn.ResidenceAddress,
                AdditionalData = dtn.AdditionalData,
                Detentions = dtn.Detentions,
                PhoneNumbers = dtn.PhoneNumbers
            };

            return Result;
        }



    }
}