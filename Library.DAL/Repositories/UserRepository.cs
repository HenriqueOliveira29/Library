using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Repository;
using Supermarket.Data.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<MessageHelper<bool>> CheckIfUserExists(string id)
        {
            MessageHelper<bool> response = new MessageHelper<bool>();

            try{ 
               
                bool userExist = await _context.ApplicationUsers.AnyAsync(x => x.Id == id);

                response.Sucess = true;
                response.obj = userExist;
            
            } catch (Exception ex) {
                response.Sucess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<PaginateList<ApplicationUser>> GetAll(List<Parameter> searchParameters, List<Parameter> sortingParameters, int currentPage = 1, int pageSize = 5)
        {
            var response = new PaginateList<ApplicationUser>();

            try
            {
                searchParameters = Parameter.VerParametros(new string[] { "name", "email", "role" }, searchParameters);


                IQueryable<ApplicationUser> query = _context.ApplicationUsers
                    .Include(u => u.UserRoles)
                    .ThenInclude(u => u.Role)
                    .AsQueryable();

                foreach (var searchParameter in searchParameters)
                {
                    if (searchParameter.Value != null)
                    {
                        switch (searchParameter.Name)
                        {
                            case "name":
                                query = query.Where(r => r.Name.Contains(searchParameter.Value));
                                break;

                            case "email":
                                query = query.Where(r => r.Email.Contains(searchParameter.Value));
                                break;

                            case "role":
                                query = query.Where(r => r.UserRoles.Select(ur => ur.Role.Name.ToLower()).Contains(searchParameter.Value.ToLower()));
                                break;
                        }
                    }
                }
                var totalRecords = await query.CountAsync();
                response.TotalRecords = totalRecords;

                var numberOfItemsToSkip = pageSize * (currentPage - 1);

                //Se existir por menos um parâmetro de ordenação, ordenamos por esse campo
                if (sortingParameters != null && sortingParameters.Count > 0)
                {
                    switch (sortingParameters[0].Name)
                    {
                        case "name":
                            query = sortingParameters[0].Value == "DESC" ? query.OrderByDescending(a => a.Name) : query.OrderBy(a => a.Name);
                            break;

                        case "email":
                            query = sortingParameters[0].Value == "DESC" ? query.OrderByDescending(a => a.Email) : query.OrderBy(a => a.Email);
                            break;

                        case "roles":
                            query = sortingParameters[0].Value == "DESC" ?
                                query.OrderByDescending(a => a.UserRoles.OrderByDescending(ur => ur.Role.Name).First().Role.Name) :
                                query.OrderBy(a => a.UserRoles.OrderByDescending(ur => ur.Role.Name).First().Role.Name);
                            break;
                    }
                }

                //Ficamos apenas com o nº de registos pretendidos
                query = query.Skip(numberOfItemsToSkip);
                query = query.Take(pageSize);

                var list = await query.ToListAsync();

                response.Items = list;
                response.CurrentPage = currentPage;
                response.PageSize = pageSize;

                response.Success = true;
                response.Message = null;

            }
            catch (Exception ex) {
                    response.Success = false;
                    response.Message = ex.Message; 
            }

            return response;
        }

        public async Task<MessageHelper<ApplicationUser>> GetById(string id)
        {
            var response = new MessageHelper<ApplicationUser>();

            try
            {
                 var user = await _context.ApplicationUsers.Include(u=>u.UserRoles)
                    .ThenInclude(u=>u.Role)
                    .Where(u=> u.Id == id)
                    .FirstOrDefaultAsync();

                response.obj = user;
                response.Sucess = true;
                response.Message = null;
            }
            catch (Exception ex)
            {
                response.Sucess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<MessageHelper<List<ApplicationRole>>> GetRoles()
        {
            var response = new MessageHelper<List<ApplicationRole>>();

            try {
                var roles = await _context.Roles.ToListAsync();

                response.obj = roles;
                response.Sucess = true;
                response.Message = null;

            } catch (Exception ex) {
                response.Sucess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
