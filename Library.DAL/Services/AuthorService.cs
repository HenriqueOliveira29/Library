using Supermarket.Data.Interfaces.Repository;
using Supermarket.Data.Interfaces.Services;
using Supermarket.Data.Models.Authors;
using Supermarket.Data.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        public async Task<MessageHelper> Create(CreateAuthorDTO createAuthor)
        {
            MessageHelper result = new MessageHelper();
            try
            {
                CreateAuthorDTOValidator validator = new CreateAuthorDTOValidator();
                var responseValidate = await validator.ValidateAsync(createAuthor);
                if (responseValidate == null || responseValidate.IsValid == false) {
                    if (responseValidate == null) {
                        result.Message = "Erro ao validar a informacao";
                        return result;
                    }
                    result.Message = responseValidate.Errors.FirstOrDefault()!.ErrorMessage;
                    return result;
                }
                var AlreadyHaveAuthor = _authorRepository.GetByName(createAuthor.Name);
                if (AlreadyHaveAuthor != null) {
                    result.Message = "Ja exiiste este autor";
                    return result;
                }

                var author = await _authorRepository.Create(createAuthor.ToEntity());

                if (author == null) {
                    result.Sucess = false;
                    result.Message = "Nao foi possivel criar o autor";
                    return result;
                }
                result.Sucess = true;
                result.Message = "Autor criado com sucesso";
            }
            catch (Exception ex) {
                result.Sucess = false;
                result.Message = ex.Message;
                return result;
            }
            return result;

        }

        public async Task<MessageHelper> Delete(int id)
        {
            MessageHelper result = new MessageHelper();
            try {
                var haveAuthor = _authorRepository.GetById(id).Result;

                if (haveAuthor == null) {
                    result.Sucess = false;
                    result.Message = "Este Autor nao existe";
                    return result;
                }

                var deletebook = await _authorRepository.Delete(haveAuthor);

                if (deletebook == false) {
                    result.Sucess = false;
                    result.Message = "Nao foi possivel eliminar o Autor";
                    return result;
                }

                result.Sucess= true;
                result.Message = "Autor eliminado com sucesso";
            } catch (Exception ex) {
                result.Sucess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<PaginateList<ListAuthorDTO>> GetAll(SearchDTO search)
        {
            PaginateList<ListAuthorDTO> result = new PaginateList<ListAuthorDTO>();
            try
            {
                if (search.PageSize > 100)
                {
                    search.PageSize = 100;
                }

                if (search.PageSize <= 0)
                {
                    search.PageSize = 1;
                }

                if (search.CurrentPage <= 0)
                {
                    search.CurrentPage = 1;
                }

                var responseRepository = await _authorRepository.GetAll(search.SearchBy, search.OrderBy, search.CurrentPage, search.PageSize);
                result.Items = responseRepository.Items.Select(t => new ListAuthorDTO(t)).ToList();
                result.Success = true;
                result.TotalRecords = responseRepository.TotalRecords;
                result.CurrentPage = responseRepository.CurrentPage;
                result.PageSize = responseRepository.PageSize;


                return result;

            }
            catch (Exception ex)
            {
                result.Success = false;
                return result;
            }
        }

        public async Task<MessageHelper<List<ListAuthorDTO>>> GetAuthors()
        {
            MessageHelper<List<ListAuthorDTO>> result = new MessageHelper<List<ListAuthorDTO>>();
            try
            {   
                var authors = await _authorRepository.GetAuthors();
                if (authors == null) {
                    result.Message = "Erro";
                    result.Sucess = false;
                    return result;
                }
                result.obj = authors.Select(t => new ListAuthorDTO(t)).ToList();
                result.Sucess = true;

            }
            catch (Exception ex) {
                   result.Message = ex.Message;
                   result.Sucess = false;
                   
                   return result;
            }
            return result;
        }

        public async Task<MessageHelper<AuthorDTO>> GetById(int authorid)
        {
            MessageHelper<AuthorDTO> result = new MessageHelper<AuthorDTO>();
            try
            {
                var author = await _authorRepository.GetById(authorid);

                if (author == null)
                {
                    result.Sucess = false;
                    result.Message = "Nao foi possivel encontrar este autor";
                    return result;
                }
                result.obj = new AuthorDTO(author);
                result.Sucess = true;
            }
            catch (Exception ex) {
                result.Sucess= false;
                result.Message = ex.Message;
                return result;
            }
            return result;

            
        }

        public async Task<MessageHelper<AuthorDTO>> Update(EditAuthorDTO editAuthor)
        {
            MessageHelper<AuthorDTO> result = new();
            try
            {
                EditAuthorDTOValidator validator = new EditAuthorDTOValidator();
                var responseValidate = validator.Validate(editAuthor);
                if (responseValidate.IsValid == false || responseValidate == null) {

                    if (responseValidate == null)
                    {
                        result.Sucess = false;
                        result.Message = "Erro ao validar a informacao";
                        return result;
                    }

                    result.Message = responseValidate.Errors.FirstOrDefault()!.ErrorMessage;
                    return result;
                }

                var author = await _authorRepository.GetById(editAuthor.AuthorId);
                if (author == null) {
                    result.Sucess = false;
                    result.Message = "Nao existe este Autor";
                    return result;
                }
                author.Name = editAuthor.Name;
                author.BirthDate = editAuthor.BirthDate;
                author.DeadDate = editAuthor.DeadDate;

                author = await _authorRepository.Update(author);

                result.Sucess = true;
                result.obj = new AuthorDTO(author);
                
            }
            catch (Exception ex) {
                result.Sucess = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
