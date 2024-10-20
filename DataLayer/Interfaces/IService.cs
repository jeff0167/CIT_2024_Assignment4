using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;

interface IServiceGetById<T> // each one should be in it's own file, but easier to see them all here as there are so few and they inherit
{
    T GetById(int id);
}
interface IServiceGetAll<T>
{
    IList<T> GetAll();
}

interface IServiceGetByName<T>
{
    IList<T> GetByName(string name);
}

interface IServiceCreate<T>
{
    T Create(T item);
}

interface IServiceDelete<T>
{
    bool Delete(int id);
}
interface IServiceUpdate<T>
{
    bool Update(int id, T item);
}

interface IServiceCRUD<T> : IServiceGetById<T>, IServiceGetAll<T>, IServiceCreate<T>, IServiceUpdate<T>, IServiceDelete<T> 
{

}
