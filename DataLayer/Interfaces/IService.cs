using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;

//Interfaces related to CRUD operations
interface IServiceGetById<T>
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
