using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public int IdCar { get; private set; }
        public Car Car { get; private set; }
        public int IdCustomer { get; private set; }
        public Customer Customer { get; private set; }
        public decimal TotalCost { get; private set; }

        public List<ExtraOrderItem> ExtraItems { get; private set; }


        protected Order()
        {
            //consttutor sem parametro protected para o EF
        }


        public Order(int idCar, int idCustomer, decimal price, List<ExtraOrderItem> items)
        {
         
            IdCar = idCar;
            IdCustomer = idCustomer;
            TotalCost = items.Sum(i => i.Price) + price;

            ExtraItems = items;
        }
    }


   

    public class ExtraOrderItem
    {
        public int Id { get; private set; }
        public string Decription { get; set; }
        public decimal Price { get; set; }
        public int IdOrder { get; private set; }


        protected ExtraOrderItem()
        {
            //consttutor sem parametro protected para o EF
        }

        public ExtraOrderItem(string decription, decimal price)
        {
            Decription = decription;
            Price = price;
        }
    }
}

