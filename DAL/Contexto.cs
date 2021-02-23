using Microsoft.EntityFrameworkCore;
using RegistroPedidosBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroPedidosBlazor.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Suplidores> Suplidores { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Ordenes> Ordenes { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Suplidores--------------------------------------------------
            modelBuilder.Entity<Suplidores>().HasData(new Suplidores {
                SuplidorId = 1,
                Nombres = "Juanito Alimaña"
            });

            modelBuilder.Entity<Suplidores>().HasData(new Suplidores
            {
                SuplidorId = 2,
                Nombres = "Carmen Stuart"
            });

            modelBuilder.Entity<Suplidores>().HasData(new Suplidores
            {
                SuplidorId = 3,
                Nombres = "Pablo Escobar"
            });

            modelBuilder.Entity<Suplidores>().HasData(new Suplidores
            {
                SuplidorId = 4,
                Nombres = "David Guetta"
            });

            modelBuilder.Entity<Suplidores>().HasData(new Suplidores
            {
                SuplidorId = 5,
                Nombres = "Batata Corp."
            });

            modelBuilder.Entity<Suplidores>().HasData(new Suplidores
            {
                SuplidorId = 6,
                Nombres = "Nike"
            });

            //Productos--------------------------------------------------
            modelBuilder.Entity<Productos>().HasData(new Productos
            { 
                ProductoId = 1,
                Descripcion = "Agua",
                Costo = 15,
                Inventario = 25
            
            });
            
            modelBuilder.Entity<Productos>().HasData(new Productos
            { 
                ProductoId = 2,
                Descripcion = "Soda",
                Costo = 25,
                Inventario = 50
            });

            modelBuilder.Entity<Productos>().HasData(new Productos
            {
                ProductoId = 3,
                Descripcion = "Leche",
                Costo = 35,
                Inventario = 50
            });

            modelBuilder.Entity<Productos>().HasData(new Productos
            {
                ProductoId = 4,
                Descripcion = "Jugo Rica",
                Costo = 45,
                Inventario = 50
            });

            modelBuilder.Entity<Productos>().HasData(new Productos
            {
                ProductoId = 5,
                Descripcion = "Choco-Rica",
                Costo = 45,
                Inventario = 50
            });

            modelBuilder.Entity<Productos>().HasData(new Productos
            {
                ProductoId = 6,
                Descripcion = "Batata",
                Costo = 15,
                Inventario = 50
            });

            modelBuilder.Entity<Productos>().HasData(new Productos
            {
                ProductoId = 7,
                Descripcion = "Manzana",
                Costo = 55,
                Inventario = 50
            });
        }
    }
}
