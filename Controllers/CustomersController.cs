using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly DevCarsDbContext _dbContext;
        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }





        //post api/customers
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomer addCustomer)
        {

            var customer = new Customer(addCustomer.FullName, addCustomer.Document, addCustomer.BirthDate);

            _dbContext.Customer.Add(customer);
            _dbContext.SaveChanges(); //persistir a operação

            return NoContent();
        }

        //post api/customers/2/orders
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrder addOrder)
        {


            var extraItems = addOrder.ExtraItems.Select(e => new ExtraOrderItem(e.Description, e.Price)).ToList();


            //para obter o preço do carro, buscar ele no banco
            var precoCar = _dbContext.Cars.SingleOrDefault(c => c.Id == addOrder.IdCar);

            //
            var order = new Order(addOrder.IdCar, addOrder.IdCustomer, precoCar.Price ,extraItems);


            //buscar o cliente para esse pedido
            // var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == addOrder.IdCustomer);
            // customer.Purchase(order);

            _dbContext.Order.Add(order);
            precoCar.SetAsSold();
            _dbContext.SaveChanges(); //persistir a operação



            //resposta http no swagger
            return CreatedAtAction(
                    nameof(GetOrder),
                    new {id = order.IdCustomer, orderid = order.Id },
                    addOrder
                );
        }

        //get pai/cusomers/1/orders/3
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {


            
                 
            var order = _dbContext.Order
                .Include(o => o.ExtraItems)  //include para buscar dados de tabelas com relaçao
                .SingleOrDefault(o => o.Id == orderid);


            if (order == null)
            {
                return NotFound();
            }



            var extraItems = order.ExtraItems.Select(e => e.Decription).ToList();

            var orderViemModel = new OrderDetailsViewModel( order.IdCar, order.IdCustomer, order.TotalCost,  extraItems);

            return Ok(orderViemModel);
        }
    }
}
