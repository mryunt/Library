using AppCore;
using AppCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Entities
{
    public class Category : Audit, IEntity, ISoftDeleted
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public ICollection<SubCategory> SubCategories{ get; set; }
        public bool isDeleted { get; set; }
    }
}
