using Dapper;
using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{

    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private readonly DevCarsDbContext _dbContex;
        private readonly string _conncetionString;

        public CarsController(DevCarsDbContext dbContext, IConfiguration configuration)
        {
            _dbContex = dbContext;

            //outra forma de pegar a connection string
           // _conncetionString = _dbContex.Database.GetDbConnection().ConnectionString;

            _conncetionString = configuration.GetConnectionString("DevCarsCs");
        }



        //get api/cars
        [HttpGet]
        public IActionResult Get()
        {


            //Entity Frame Core
            //retornar lista de CarItemView
            //var listCars = _dbContex.Cars;
            //var carsViewModel = listCars
            //    .Select(c => new CarItemViewModels(c.Id, c.Brand, c.Model, c.Price))
            //    .ToList();
            //return Ok(carsViewModel);



            //Utilizando DAPPER - baixa performance
            using (var sqlConnetion = new SqlConnection(_conncetionString))
            {
               
                var query = "SELECT Id, Brand, Model, Price FROM Cars WHERE Status = 0";              

                var carsViewModel = sqlConnetion.Query<CarItemViewModels>(query);

                return Ok(carsViewModel);

            }            

        }


        //get api/cars/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
                    
            var car = _dbContex.Cars.SingleOrDefault(c => c.Id == id);

            //se não existir o carro retornar notfoun ()
            if (car == null)
            {
                return NotFound();
            }

            //se extistir retorna ok
            var carDetailsViewModel = new CarDetailsViewModel(
                car.Id,
                car.Brand,
                car.Model,
                car.Color,
                car.VinCode,
                car.Year,
                car.Price,               
                car.ProductionDate
                );
            return Ok(carDetailsViewModel);

        }

        //post api/cars

        /// <summary>
        /// Cadastrar um Carro
        /// </summary>
        /// <remarks>
        /// Requisão de Exemplo:
        /// {
        ///     "Brand": "Honda",
        ///     "model": "Civic",
        ///     "Vincode": "abc123",
        ///     "year" : 2021,
        ///     "color" : "Cinza,
        ///     "Price" : 10000
        ///     "productionDate": "2021-04-05"
        /// 
        /// }
        /// </remarks>
        /// <param name="car">Dados de um novo carro</param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Objeto criado com sucesso</response>
        /// <response code="400">Dados invalidos</response>



        [HttpPost]
        public IActionResult Post([FromBody] AddCar car) //vou receber via corpo de requisição
        {

            //se o cadastro funcionar, retornar created (201)

            // se os dados de entrada estiverem incorretos, retornar bad request (400)

            // se o cadastro funcionar , mas não tiver uma api de consulta, retornar (204 no content)

            //if (car.Model.Length > 50)
            //{
            //    return BadRequest("Modelo não pode ter mais de 50 caracteres");
            //}

            //if (car.Id == 0)
            //{
            //    return BadRequest("Erro ao cadastrar");
            //}


            var carro = new Car(car.Brand, car.Model, car.Color, car.VinCode, car.Year, car.Price, car.ProductionDate);

            _dbContex.Cars.Add(carro);
            _dbContex.SaveChanges(); //persistir a operação

            //resposta http no swagger
            return CreatedAtAction
                (
                    nameof(GetById),
                    new { id = car.Id },
                    car
                );
        }

        //put api/cars/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCar updateCar)
        {

            
               var car = _dbContex.Cars.SingleOrDefault(c => c.Id == id);

               //se não existir, retornar not found 404
               if (car == null)
               {
                   return NotFound();
               }            
              
            
            // car.Update(updateCar.Color, updateCar.Price);
            // _dbContex.SaveChanges(); //persistir a operação                  
           


            //Utilizando DAPPER - baixa performance
            using (var sqlConnetion = new SqlConnection(_conncetionString))
            {
                var query = "UPDATE Cars SET Color = @color, Price = @price WHERE Id = @id";

                sqlConnetion.Execute(query, new { color = updateCar.Color, price = updateCar.Price, car.Id });


            }

            return NoContent();


        }


        //delete api/cars/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
                   
            var car = _dbContex.Cars.SingleOrDefault(c => c.Id == id);

            //se não existir, retornar not found 404
            if (car == null)
            {
                return NotFound();
            }


            car.SetAsSuspenden(); //chamando o metodo que muda o status do carro
            _dbContex.SaveChanges(); //persistir a operação

            // se for sucesso, retornar no content 204
            return NoContent();
        }


    }
}
