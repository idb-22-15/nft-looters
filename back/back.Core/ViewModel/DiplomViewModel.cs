using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Core.ViewModel
{
    public class DiplomViewModel
    {
        public string? Image {  get; set; }

        public Guid UserId { get; set; }

        public string OrganisationName { get; set; }

        public string Faculty { get; set; }

        public string Specialization { get; set; }

        public DateTime DateOfIssue { get; set; }
    }
}
