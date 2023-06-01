using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher.Shared.Dtos
{
    public class AddBookWithCoverDto
    {
        public int AuthorId { get; set; }

        public DateTime PublishDate { get; set; }

        public string Title { get; set; }

        public string DesignIdeas { get; set; }
    }
}
