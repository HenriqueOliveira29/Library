using Supermarket.Data.Entities;
using Supermarket.Data.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<PaginateList<ApplicationUser>> GetAll(List<Parameter> searchParameters, List<Parameter> sortingParameters, int currentPage = 1, int pageSize = 5);

        Task<MessageHelper<ApplicationUser>> GetById(string id);

        Task<MessageHelper<List<ApplicationRole>>> GetRoles();

        Task<MessageHelper<bool>> CheckIfUserExists(string id); 
    }
}
