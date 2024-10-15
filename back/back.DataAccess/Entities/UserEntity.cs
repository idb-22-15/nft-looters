using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.DataAccess.Entities
{
    public class UserEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Wallet {  get; set; }

        public Guid? OrganisationId { get; set; }
        public OrganisationEntity? Organisation { get; set; }

        public string? JobTitle { get; set; }
    }
}
