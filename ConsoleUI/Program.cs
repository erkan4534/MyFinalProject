﻿using Bussiness.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //DTO = Data Transformation Object
            ProductTest();

            //CategoryTest();


        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        
        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            var result = productManager.GetProductDetailDtos();

            //  var result = productManager.GetAll();

            if (result.Success==true)
            {
                foreach (var product in result.Data)
                {
                   Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

           /*      
            productManager = new ProductManager(new EfProductDal());

            foreach (var product in productManager.GetProductDetailDtos())
            {
            
            }
           */

        }
        
    }
}
