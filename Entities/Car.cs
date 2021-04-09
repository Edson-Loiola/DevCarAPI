using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Car
    {

        public int Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string Color { get; private set; }
        public string VinCode { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }
        public DateTime ProductionDate { get; private set; }

        public CarStatusEnum Status { get; private set; }
        public DateTime RegisteredAt { get; private set; }



        public Car()
        {
            //consttutor sem parametro protected para o EF
        }

        public Car(string brand, string model, string color, string vinCode, int year, decimal price, DateTime productionDate)
        {


            Brand = brand;
            Model = model;
            Color = color;
            VinCode = vinCode;
            Year = year;
            Price = price;
            ProductionDate = productionDate;

            Status = CarStatusEnum.Available;  //quando o carro for cadastrado já fica com o status disponivel
            RegisteredAt = DateTime.Now; //ao cadastrar ja seta a data e horario atual do cadastramento
        }


        //metodo que registringe atualizar apenas esses parametros
        public void Update(string color, decimal price)
        {
            Color = color;
            Price = price;
        }


        //quando o carro for deletado
        public void SetAsSuspenden()
        {
            Status = CarStatusEnum.Suspended;

        }


        //quando o carro for vendido
        public void SetAsSold()
        {
            Status = CarStatusEnum.Sold;
        }


    }
}
