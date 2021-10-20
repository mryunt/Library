using AppCore;
using AppCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Entities
{
    public class Book : Audit, IEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int SubCategoryId{ get; set; }
        public SubCategory SubCategoryFK{ get; set; }
        public bool isDeleted { get; set; }
    }
}
