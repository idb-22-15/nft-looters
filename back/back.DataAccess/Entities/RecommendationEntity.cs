using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.DataAccess.Entities
{
    public class RecommendationEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid IssuerId { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
    }
}
