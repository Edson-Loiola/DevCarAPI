using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.InputModels
{
    public class AddOrder
    {
        public int IdCar { get; set; }
        public int IdCustomer { get; set; }
        public List<ExtraItem> ExtraItems { get; set; }
    }


    public class ExtraItem
    {
        public string Description { get; set; }
        public decimal Price { get; set; }

    }

}
