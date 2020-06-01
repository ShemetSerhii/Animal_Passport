using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.WebApi.Models;

namespace AnimalPassport.WebApi.Auth
{
    public interface IAuthService
    {
        Task<UserModel> AuthenticateAsync(AuthModel model);

        UserModel Authenticate(UserModel model);
    }
}