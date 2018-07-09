using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Prison.AdvertismentService.Repositories
{
    internal class AdRepository
    {
        private IReadOnlyCollection<Blurb> List { get; }

        public AdRepository()
        {
            List = new List<Blurb> {
                new Blurb{ BlurbID=1,BlurbHeader="Nissan LEAF",BlurbContent="Зачем ждать, если вы прямо сейчас можете управлять полностью электрическим Leaf?",Image=GetByteArray("~/Images/Nissan.jpg") },
                new Blurb{ BlurbID=2,BlurbHeader="Tesla Model 3",BlurbContent="Жги резину, а не бензин!",Image=GetByteArray("~/Images/Tesla.jpg") },
                new Blurb{ BlurbID=3,BlurbHeader="BMW i3",BlurbContent="Сделай свой вклад в защиту окружающей среды.",Image=GetByteArray("~/Images/BMW.jpg")},
                new Blurb{ BlurbID=4,BlurbHeader="Volkswagen e-Golf",BlurbContent="Полный контроль над расходами.",Image=GetByteArray("~/Images/Volkswagen.jpg")},
                new Blurb{ BlurbID=5,BlurbHeader="Ford Focus Electric",BlurbContent="Навстречу переменам!",Image=GetByteArray("~/Images/Ford.jpg")},
                new Blurb{ BlurbID=6,BlurbHeader="Renault Zoe",BlurbContent="Renault. Машина для этой жизни.",Image=GetByteArray("~/Images/Renault.jpg")},
                new Blurb{ BlurbID=7,BlurbHeader="Volvo C30 Electric",BlurbContent="Ваша жизнь слишком дорога, чтобы ездить на чём-то другом.",Image=GetByteArray("~/Images/Volvo.jpg")},
                new Blurb{ BlurbID=8,BlurbHeader="Toyota RAV4 EV",BlurbContent="Если бы мы делали девушек,-они бы никогда не ломались.",Image=GetByteArray("~/Images/Toyota.jpg")},
                new Blurb{ BlurbID=9,BlurbHeader="Mercedes-Benz Generation EQ",BlurbContent="Электрический кроссовер будущего!",Image=GetByteArray("~/Images/Mercedes.jpg")},
                new Blurb{ BlurbID=10,BlurbHeader="Geely Emgrand EV300",BlurbContent="Попробуйте электрическое желе из Китая!",Image=GetByteArray("~/Images/Geely.jpg")}
            };
        }

        public IReadOnlyCollection<Blurb> GetRandomAd(int numOfElements)
        {
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

            var rnd = new Random();

            var arrOfRandomNum = new int[numOfElements];

                for (int i = 0,num=0 ; i < numOfElements; i++)
                {
                    num = rnd.Next(List.Count);

                    while (arrOfRandomNum.Contains(num)) 
                    {
                        num = rnd.Next(List.Count);
                    }
                
                    arrOfRandomNum[i] = num;
                }

            int index = -1;
            
            return List.Where(x => { ++index; return arrOfRandomNum.Contains(index) ? true : false; }).ToList();          
        }

        private byte[] GetByteArray(string ImagePath)
        {
            //get the physical path
            var physicalPath = HttpContext.Current.Server.MapPath(ImagePath);
            //read and return byte[]
            return File.ReadAllBytes(physicalPath);
        }
    }
}