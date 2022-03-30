using Supermarket.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.User
{
    public class RoleDTO
    {
        public string Id { get; init; }
        public string Name { get; init; }

        public RoleDTO()
        {

        }

        public RoleDTO(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public RoleDTO(ApplicationRole role)
        {
               this.Id=role.Id;
                this.Name=role.Name;
        }
    }
}
