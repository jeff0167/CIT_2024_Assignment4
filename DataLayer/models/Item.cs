using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace DataLayer
{
    public abstract class Item : ICloneable
    {
        public abstract int GetId();
        public abstract void SetId(int id);
        
        public object Clone() 
        {
            return this.MemberwiseClone();
        }
    }
}
