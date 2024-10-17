using back.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


namespace back.DataAccess.Entities
{
    public class UserEntity : CommonUser
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
        public OrganizationEntity? Organisation { get; set; }

        public string? JobTitle { get; set; }
    }
}
