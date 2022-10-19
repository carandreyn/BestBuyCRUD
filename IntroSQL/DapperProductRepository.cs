﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IntroSQL
{
    internal class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products");
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@productName, @productPrice, @productCategoryID);",
             new { productName = name, productPrice = price, @productCategoryID = categoryID });
        }

        public Product GetProduct(int id)
        {
            return _connection.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id;", new { id = id });

        }

        public void UpdateProduct(Product product)
        {
            _connection.Execute("UPDATE products " +
                "SET Name = @name, " +
                "Price = @price, " +
                "CategoryID = @catID, " +
                "OnSale = @onSale, " +
                "StockLevel = @stock " +
                "WHERE ProductID = @id;",
                new
                {
                    name = product.Name,
                    price = product.Price,
                    catid = product.CategoryID,
                    onSale = product.OnSale,
                    stock = product.StockLevel,
                    id = product.ProductID
                });
        }

        public void DeleteProduct(int id)
        {
            _connection.Execute("DELETE FROM sales WHERE ProductID = @id", new { id = id });
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @id", new { id = id });
            _connection.Execute("DELETE FROM products WHERE ProductID = @id", new { id = id });
        }
    }
}
