using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.BusinessLogic.Utils;
using AnimalPassport.DataAccess.Interfaces;
using AnimalPassport.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AnimalPassport.BusinessLogic.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public UserManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = unitOfWork.GetRepository<User>();
            _roleRepository = unitOfWork.GetRepository<Role>();
        }

        public async Task<UserModel> GetAsync(string login, string password)
        {
            var users = (await _userRepository.GetAsync(
                u => u.Email == login, 
                includeProperties: source => source.Include(s => s.Role))).ToList();

            if (!users.Any(u => CryptoProvider.VerifyHashedPassword(u.Password, password)))
            {
                throw new Exception("Incorrect email or password");
            }

            return _mapper.Map<UserModel>(users.SingleOrDefault(u => CryptoProvider.VerifyHashedPassword(u.Password, password)));
        }

        public async Task<IEnumerable<UserInfo>> GetPetOwners()
        {
            var users = await _userRepository.GetAsync(u => u.Role.Name == "Власник домашньої тварини",
                includeProperties: source => source.Include(u => u.Role));

            return _mapper.Map<List<UserInfo>>(users);
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return _mapper.Map<List<RoleDto>>(roles);
        }

        public async Task<UserModel> RegisterAsync(RegisterModel model)
        {
            var user = _mapper.Map<User>(model);

            user.RoleId = new Guid(model.Role);

            var id = _userRepository.Create(user);
           
            await _unitOfWork.SaveChangesAsync();

            var u = await _userRepository.GetAsync(id, source => source.Include(u => u.Role));

            return _mapper.Map<UserModel>(u);
        }
    }
}