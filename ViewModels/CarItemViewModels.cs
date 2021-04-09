using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    public class CarItemViewModels
    {
 
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }

        public CarItemViewModels(int id, string brand, string model, decimal price)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Price = price;
        }

    }
}
