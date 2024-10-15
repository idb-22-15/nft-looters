using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.DataAccess.Entities
{
    public class CertificateEntity
    {
        public Guid Id { get; set; } 

        public DateTime CreatedAt { get; set; }

        public Guid IssuerId { get; set; }

        public string? Image { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

    }
}
