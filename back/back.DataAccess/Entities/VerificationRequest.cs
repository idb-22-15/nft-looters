using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.DataAccess.Entities
{
    public class VerificationRequest
    {
        public Guid Id { get; set; }

        public Guid DiplomaId { get; set; }

        public Guid CertificateId { get; set; }

        public enum documentType
        {
            Certificate,
            Diploma,
        }

        public documentType DocumentType { get; set; }

        public Guid OrganisationId { get; set; }


    }
}
