using AppCore;
using AppCore.Entity;
using System.Collections.Generic;

namespace Library.DAL.Entities
{
    public class SubCategory : Audit, IEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category CategoryFK { get; set; }
        public ICollection<Book> Books { get; set; }
        public bool isDeleted { get; set; }
    }
}
