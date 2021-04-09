using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {

            builder
                .HasKey(c => c.Id); //define o id na tabela

            builder
                .Property(c => c.Brand)
              //  .IsRequired()  //obrigatorio
              //  .HasColumnName("Marca") //a propriedade fica com nome em ingles e a coluna do BD em pt.
                .HasColumnType("VARCHAR(100)")
                .HasDefaultValue("Padrão")
                .HasMaxLength(100);

            builder
                 .Property(c => c.ProductionDate)
                 .HasDefaultValueSql("getdate()"); //para função pegar a data

        }


    }
}
