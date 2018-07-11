using Prison.AdvertismentService.Business;
using Prison.AdvertismentService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Prison.AdvertismentService.Services
{
    internal class AdService:IAdContract
    {
        private IAdProvider _adProvider;

        public AdService(IAdProvider adProvider)
        {
            _adProvider = adProvider;
        }

        public IEnumerable<Blurb> GetAd(int numOfElements)
        {
            
            var List = _adProvider.GetAll();

            if (numOfElements > List.Count)
            {
                throw new IndexOutOfRangeException("The requested number of items is greater than the items in the list!");
            }

            if (numOfElements < 1)
            {
                throw new ArgumentException("Invalid number of elements!");
            }

            if (numOfElements == List.Count)
            {
                return List;
            }

            var arrOfRandomNum = GetArrOfRandomNum(numOfElements, List.Count);

            var ResultList = arrOfRandomNum.Select(num => List.ElementAt(num)).ToList();

            if (ResultList.Count<1)
            {
                throw new FaultException("List of Blurbs is empty!");
            }
            return ResultList;
        }

        #region Helpers

        private int[] GetArrOfRandomNum(int numOfAd,int count)
        {
            var rnd = new Random();

            var arrOfRandomNum = new int[numOfAd];

            for (int i = 0, num = 0; i < numOfAd; i++)
            {
                num = rnd.Next(count);

                //for guarantee, that all num will be different
                while (arrOfRandomNum.Contains(num))
                {
                    num = rnd.Next(count);
                }

                arrOfRandomNum[i] = num;
            }

            return arrOfRandomNum;
        }

        #endregion
    }
}