using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Prison.Service.Repositories
{
    public class Repository
    {
        IList<Blurb> _list;

        public IList<Blurb> List { get { return _list; } }

        public Repository()
        {
            _list = new List<Blurb> {
                new Blurb{ BlurbID=1,BlurbHeader="Nissan LEAF",BlurbContent="Зачем ждать, если вы прямо сейчас можете управлять полностью электрическим Leaf?",Image=GetByteArrFromImage("~/Images/Nissan.jpg") },
                new Blurb{ BlurbID=2,BlurbHeader="Tesla Model 3",BlurbContent="Жги резину, а не бензин!",Image=GetByteArrFromImage("~/Images/Tesla.jpg") },
                new Blurb{ BlurbID=3,BlurbHeader="BMW i3",BlurbContent="Сделай свой вклад в защиту окружающей среды.",Image=GetByteArrFromImage("~/Images/BMW.jpg")},
                new Blurb{ BlurbID=4,BlurbHeader="Volkswagen e-Golf",BlurbContent="Полный контроль над расходами.",Image=GetByteArrFromImage("~/Images/Volkswagen.jpg")},
                new Blurb{ BlurbID=5,BlurbHeader="Ford Focus Electric",BlurbContent="Навстречу переменам!",Image=GetByteArrFromImage("~/Images/Ford.jpg")},
                new Blurb{ BlurbID=6,BlurbHeader="Renault Zoe",BlurbContent="Renault. Машина для этой жизни.",Image=GetByteArrFromImage("~/Images/Renault.jpg")},
                new Blurb{ BlurbID=7,BlurbHeader="Volvo C30 Electric",BlurbContent="Ваша жизнь слишком дорога, чтобы ездить на чём-то другом.",Image=GetByteArrFromImage("~/Images/Volvo.jpg")},
                new Blurb{ BlurbID=8,BlurbHeader="Toyota RAV4 EV",BlurbContent="Если бы мы делали девушек,-они бы никогда не ломались.",Image=GetByteArrFromImage("~/Images/Toyota.jpg")},
                new Blurb{ BlurbID=9,BlurbHeader="Mercedes-Benz Generation EQ",BlurbContent="Электрический кроссовер будущего!",Image=GetByteArrFromImage("~/Images/Mercedes.jpg")},
                new Blurb{ BlurbID=10,BlurbHeader="Geely Emgrand EV300",BlurbContent="Попробуйте электрическое желе из Китая!"/*,Image=GetByteArrFromImage("~/Images/Geely.jpg")*/}
            };
        }

        public IEnumerable<Blurb> GetRandomElementsFromRep(int numOfElements)
        {
            if (numOfElements > _list.Count) throw new IndexOutOfRangeException("The requested number of items is greater than the items in the list!");

            if (numOfElements == _list.Count) return _list;

            List<Blurb> list=new List<Blurb>();

            Random rnd = new Random();

            int[] arrOfRandomNum = new int[numOfElements];

                for (int i = 0,num=0 ; i < numOfElements; i++)
                {
                    num = rnd.Next(0, _list.Count);

                    while (arrOfRandomNum.Contains(num)) 
                    {
                        num = rnd.Next(0, _list.Count);
                    }
                
                    list.Add(_list[num]);
                    arrOfRandomNum[i] = num;
                    
                }

            return list;          
        }

        public byte[] GetByteArrFromImage(string path)
        {
            //get the physical path
            string physicalPath = HttpContext.Current.Server.MapPath(path);
            //read and return byte[]
            return File.ReadAllBytes(physicalPath);
        }

    }
}