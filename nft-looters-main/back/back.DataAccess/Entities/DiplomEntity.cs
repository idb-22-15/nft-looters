using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.DataAccess.Entities
{
    public class DiplomEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid IssuerId { get; set; }

        public string? Image { get; set; }

        public Guid UserId { get; set; }

        public string OrganisationName { get; set; }

        public string Faculty { get; set;}

        public string Specialization { get; set; }

        public DateTime DateOfIssue { get; set; }


    }

}
