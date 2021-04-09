using DevCars.API.Entities;
using DevCars.API.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext : DbContext
    {


        // ORM - converter seu modelos de dados em uma tabela e colunas no banco de dados    
        //---------------------*******************--------------------------------------
        //TESTE COM DADOS EM MEMORIA PARA VER SE API ESTÁ FUNCIONANDO
        //public List<Car> Cars { get; set; }
        //public List<Customer> Customers { get; set; }


        public DevCarsDbContext(DbContextOptions<DevCarsDbContext> options) : base(options) //cosntrutor
        {

            /*
            Cars = new List<Car>
            {
                new Car(1, "HONDA", "CIVIC","Cinza", "123ABC", 2021, 100000, new DateTime(2021, 1, 1)),
                new Car(2, "TOYTOTA", "COROLLA","Preto", "456ABD", 2021, 100000, new DateTime(2021, 1, 1)),
                new Car(3, "CHEVROLLET", "ONIUX","Branco", "723AFC", 2021, 100000, new DateTime(2020, 1, 1)),
                new Car(4, "HYUNDAI", "HB20","Vermelho", "823AFR", 2021, 100000, new DateTime(2020, 1, 1)),
            };

            Customers = new List<Customer>()
            {
                new Customer(1, "EDSON MONTEIRO", "123456", new DateTime(1990, 1, 1)),
                new Customer(2, "EDSON LOIOLA", "456789", new DateTime(1990, 1, 1)),
                new Customer(3, "MONTEIRO", "987654", new DateTime(1990, 1, 1)),
                new Customer(4, "CARDOSO", "654321", new DateTime(1990, 1, 1))
            };
            */
            //---------------------*******************--------------------------------------
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ExtraOrderItem> ExtraOrderItem { get; set; }


        //para a migrations criar as tabelas no banco, desta forma as notations não suja o código nos models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Segunda forma - As mesma coisas comentadsa abaixo porem separado em pastas e organizadas (pasta Configurations)
           /*
            modelBuilder.ApplyConfiguration(new CarDbConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerDbConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDbConfiguration());
            modelBuilder.ApplyConfiguration(new ExtraOrderItemDbConfiguration());
           */


            //terceira forma de deixar o codigo mais limpo, é não precisar chamar cada configuration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

           //----------------------------------



            //Primeira forma

            //modelBuilder.Entity<Car>()
            //     .Property(c => c.Brand)
            //     .HasColumnName("Marca");//a propriedade fica com nome em ingles e a coluna do BD em pt.

            //modelBuilder.Entity<Car>()
            //     .Property(c => c.Brand)
            //     .HasDefaultValue("Padrão"); //Define valor padrão para propriedade


            //modelBuilder.Entity<Car>()
            //     .Property(c => c.Brand)
            //     .HasDefaultValueSql("getdate()"); //para função do sql


            //modelBuilder.Entity<Car>()
            //     .Property(c => c.Brand)
            //     .HasColumnType("VARCHAR(100)"); //DEFINIR tipo da coluna


            //modelBuilder.Entity<Car>()
            //    .ToTable("tb_Car");



            //modelBuilder.Entity<Customer>()
            //    .HasKey(c => c.Id);
            //modelBuilder.Entity<Customer>()
            //    .HasMany(c => c.Orders)
            //    .WithOne(o => o.Customer)
            //    .HasForeignKey(o => o.IdCustomer)
            //    .OnDelete(DeleteBehavior.Restrict);  //Restrict não deixa apagar os dados, Cascade apaga os dados e as referencias em outras tabelas


            //modelBuilder.Entity<Order>()
            //    .HasKey(o => o.Id);
            //modelBuilder.Entity<Order>()
            //    .HasMany(o => o.ExtraItems)
            //    .WithOne()
            //    .HasForeignKey(e => e.IdOrder)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Car) //um pedido tem um carro
            //    .WithOne() //um pedido tem um carro. Se eu vendi o carro, não posso vender para outra pessoa 
            //    .HasForeignKey<Order>(o => o.IdCar)
            //    .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<ExtraOrderItem>()
            //    .HasKey(e => e.Id);       


        }

    }
}
