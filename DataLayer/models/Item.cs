using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace DataLayer
{
    //All classes that represent an item in the database should inherit from this class, apart from OrderDetails
    public abstract class Item : ICloneable //Implements interface ICloneable from System namespace
    {
        public abstract int GetId();
        public abstract void SetId(int id);
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
