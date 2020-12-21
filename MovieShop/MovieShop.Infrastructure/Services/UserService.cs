using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _encryptionService;
        private readonly IAsyncRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly IPurchaseRepository _purchaseRepository;


        public UserService(IUserRepository repository, ICryptoService encryptionService, IAsyncRepository<UserRole> userRoleRepository, IPurchaseRepository purchaseRepository)
        {
            _userRepository = repository;
            _encryptionService = encryptionService;
            _userRoleRepository = userRoleRepository;
            _purchaseRepository = purchaseRepository;


        }
            
        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {

            //we check if the email exists in the db
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }
            var hashedPassword = _encryptionService.HashPassword(password, user.Salt);
            var isSuccess = user.HashedPassword == hashedPassword;
            var response = new UserLoginResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                
            };
            //var response = _mapper.Map<UserLoginResponseModel>(user);
            //var userRoles = roles.ToList();
            //if (userRoles.Any())
            //{
            //    response.Roles = userRoles.Select(r => r.Role.Name).ToList();
            //}
            if (isSuccess)
                return response;
            else
                return null;
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            return null;
            //throw new NotImplementedException();
            /*
          
            var purchasedMovies = await _purchaseRepository.ListAllWithIncludesAsync(
                p => p.UserId == id,
                p => p.Movie);

            PurchaseResponseModel model = new PurchaseResponseModel();
            model.UserId = id;
            model.PurchasedMovies = purchasedMovies;
            return model;
            */
        }

        public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
        {
            //step1: create an user
            //step2: add the created user to the userRepository
            //step3: send the response back to the controller. (important)

            //make sure email does not exist in the database
            //we need to send email to our User repository and see if the data exists for the email
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            //if the user exists and there's an email that matches it.
            if (dbUser != null && string.Equals(dbUser.Email, requestModel.Email, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Email Already Exits");

            //First step is to create a unique random salt.
            var salt = _encryptionService.CreateSalt();
            var hashedPassword = _encryptionService.HashPassword(requestModel.Password, salt);
            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };
            var createdUser = await _userRepository.AddAsync(user);
            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            return response;
        }

        public Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string email)
        {
            throw new NotImplementedException();
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            /*
            if (_currentUserService.UserId != purchaseRequest.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to purchase");
            if (_currentUserService.UserId != null) purchaseRequest.UserId = _currentUserService.UserId.Value;
            // See if Movie is already purchased.
            if (await IsMoviePurchased(purchaseRequest))
                throw new ConflictException("Movie already Purchased");
            // Get Movie Price from Movie Table
            var movie = await _movieService.GetMovieAsync(purchaseRequest.MovieId);
            purchaseRequest.TotalPrice = movie.Price;

            var purchase = _mapper.Map<Purchase>(purchaseRequest);
            await _purchaseRepository.AddAsync(purchase);
            */
        }
    }
}
