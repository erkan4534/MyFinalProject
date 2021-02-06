using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //generic constraint
    //call: referans tip deemektir mesela int gelemez
    //Entity: IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    //new():new'lenebilir olmali
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        List<T> GelAll(Expression<Func<T,bool>> filter =null);
        T Get(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
