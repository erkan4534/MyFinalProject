using Bussiness.Abstract;
using Bussiness.BusinessAspects.Autofac;
using Bussiness.Constants;
using Bussiness.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Busines;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ValidationException = FluentValidation.ValidationException;

namespace Bussiness.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //business codes
            //validation

            //  ValidationTool.Validate(new ProductValidator(), product);
           

           IResult result=  BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                   CheckIfProductCountofCategoryCorrect(product.CategoryId), CheckIfCategoryLimitExceeded());

            if (result!=null)
            {
                return result;
            }

            _productDal.Add(product);

             return new SuccessResult(Messages.ProductAdded);
          
        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //is kodlari
            //Yetkisi var mi?

          //  if (DateTime.Now.Hour == 23)
            //{
               // return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<Product>>(_productDal.GelAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GelAll(p => p.CategoryId == id));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GelAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailDtos()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {

            if (CheckIfProductCountofCategoryCorrect(product.CategoryId).Success)
            {
                _productDal.Add(product);

                return new SuccessResult(Messages.ProductAdded);
            };

            return new ErrorResult();

        }

        //select count(*) from product where categoriId=1
        private IResult CheckIfProductCountofCategoryCorrect(int categoryId)
        {
            var result = _productDal.GelAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string  productName)
        {
            var result = _productDal.GelAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }

      
    }


}
