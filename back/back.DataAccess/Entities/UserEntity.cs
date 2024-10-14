using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.DataAccess.Entities
{
    internal class UserEntity
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }
    }
}
