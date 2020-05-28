using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;

namespace AnimalPassport.BusinessLogic.Interfaces
{
    public interface IUserManager
    {
        Task<UserModel> GetAsync(string login, string password);

        Task RegisterAsync(RegisterModel model);

        Task<IEnumerable<RoleDto>> GetRolesAsync();
    }
}