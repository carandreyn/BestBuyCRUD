using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace IntroSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            // Department repo
            var departmentRepo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Type a new Department name");
            var newDepartment = Console.ReadLine();

            departmentRepo.InsertDepartment(newDepartment);
            Console.WriteLine();
            var departments = departmentRepo.GetAllDepartments();

            foreach (var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }
            Console.WriteLine();

            // Product repo
            var productRepo = new DapperProductRepository(conn);
            Console.WriteLine("Type a new Product name");
            var newProductName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Type a new Product price");
            var newProductPrice = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Type a new category ID");
            var newProductCatId = int.Parse(Console.ReadLine());
            Console.WriteLine();

            productRepo.CreateProduct(newProductName, newProductPrice, newProductCatId);

            var productToUpdate = productRepo.GetProduct(940);
            productToUpdate.OnSale = false;
            productToUpdate.StockLevel = 101;
            productRepo.UpdateProduct(productToUpdate);

            var products = productRepo.GetAllProducts();

            foreach (var prod in products)
            {
                Console.WriteLine(prod.Name);
            }

            //productRepo.DeleteProduct(945);
            //productRepo.DeleteProduct(944);
            //productRepo.DeleteProduct(943);
            //productRepo.DeleteProduct(942);
            //productRepo.DeleteProduct(941);

        }
    }
}
