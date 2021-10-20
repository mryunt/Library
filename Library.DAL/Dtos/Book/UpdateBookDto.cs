using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Dtos
{
    public class UpdateBookDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int SubCategoryId { get; set; }
    }
}
