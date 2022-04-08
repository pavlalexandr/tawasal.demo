using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class PaginatedView<T>
    {
        public IReadOnlyList<T> Data { get; set; }
        public int TotalPages { get; set; }
    }
}
