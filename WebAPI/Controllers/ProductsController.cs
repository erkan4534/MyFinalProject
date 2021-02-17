using Bussiness.Abstract;
using Bussiness.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        //naming convention
        //IoC Container  --Inversion of Control
        IProductService _productServic;

        public ProductsController(IProductService productServic)
        {
            _productServic = productServic;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
             //Swagger
             //Dependency chain --
             var result= _productServic.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

             return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productServic.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productServic.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
