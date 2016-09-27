using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftAzure.Tutorial.RedisCache.HelloWorld
{
    public class BlogPost
    {
        private HashSet<string> _tags = new HashSet<string>();

        public int Id { get; set; }
        public string Title { get; set; }
        public string Score { get; set; }
        public ICollection<string> Tags
        {
            get { return this._tags; }
            set { this._tags = new HashSet<string>(value); }
        }

        public BlogPost(int id, string title, string score, ICollection<string> tags)
        {
            this.Id = id;
            this.Title = title;
            this.Score = score;
            this.Tags = tags;
        }
    }
}
