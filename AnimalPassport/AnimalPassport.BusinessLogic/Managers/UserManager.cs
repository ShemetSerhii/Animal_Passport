using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.BusinessLogic.Utils;
using AnimalPassport.DataAccess.Interfaces;
using AnimalPassport.Entities.Entities;
using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalPassport.BusinessLogic.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = unitOfWork.GetRepository<User>();
        }

        public async Task<UserModel> GetAsync(string login, string password)
        {
            var users = (await _userRepository.GetAsync(u => u.Email == login)).ToList();

            if (!users.Any(u => CryptoProvider.VerifyHashedPassword(u.Password, password)))
            {
                throw new Exception("Incorrect email or password");
            }

            return _mapper.Map<UserModel>(users.SingleOrDefault(u => CryptoProvider.VerifyHashedPassword(u.Password, password)));
        }

        public async Task RegisterAsync(RegisterModel model)
        {
            var user = _mapper.Map<User>(model);

            _userRepository.Create(user);
           
            await _unitOfWork.SaveChangesAsync();
        }
    }
}