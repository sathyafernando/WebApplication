using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationTestApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public int ImageUrl { get; set; }
        public int CreatedTime { get; set; }
        public int AuthorName { get; set; }
        public int TotalLikes { get; set; }
    }
}
