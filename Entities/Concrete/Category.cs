using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{

    //ciplak class kalmasin
    public class Category: IEntity
    {
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }

    }
}
