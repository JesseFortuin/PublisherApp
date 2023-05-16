using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher.Shared.Dtos
{
    public class AddBookDto
    {
        public string Title { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
