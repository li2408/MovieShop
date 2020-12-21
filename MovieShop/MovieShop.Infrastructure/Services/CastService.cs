using System;
using System.Threading.Tasks;
using AutoMapper;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using OpenQA.Selenium;

namespace MovieShop.Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
           
        }
        public async Task<CastDetailsResponseModel> GetCastDetailsWithMovies(int id)
        {
            throw new NotImplementedException();
            /*
            throw new NotImplementedException();

            var cast = await _castRepository.GetByIdAsync(id);
            if (cast == null)
            {
                throw new NotFoundException("Cast");
            }

            var response = _mapper.Map<CastDetailsResponseModel>(cast);
            return response;
            */
        }
    }
}
