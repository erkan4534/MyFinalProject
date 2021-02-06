using Bussiness.Concrete;
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
            ProductManager productManager = null;

            productManager = new ProductManager(new EfProductDal());

            foreach (var product in productManager.GetAllByCategoryId(2))
            {
                Console.WriteLine(product.ProductName);
            }


            productManager = new ProductManager(new EfProductDal());

            foreach (var product in productManager.GetProductDetailDtos())
            {
                Console.WriteLine(product.ProductName +"/"+product.CategoryName);
            }

        }
    }
}
