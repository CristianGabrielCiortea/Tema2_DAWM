using Core.Dtos;
using DataLayer.Entities;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService
    {
        private AuthorizationService authorizationService { get; set; }
        
        private readonly UnitOfWork _unitOfWork;

        public UserService(AuthorizationService authorizationService, UnitOfWork unitOfWork)
        {
            this.authorizationService = authorizationService;
            _unitOfWork = unitOfWork;
        }

        public void Register(RegisterDto registerData)
        {
            if (registerData == null && _unitOfWork.Users.GetByEmail(registerData.Email) != null)
            {
                return;
            }

            var hashedPassword = authorizationService.HashPassword(registerData.Password);

            var user = new User
            {
                FirstName = registerData.FirstName,
                LastName = registerData.LastName,
                Email = registerData.Email,
                PasswordHash = hashedPassword,
                Role = registerData.Role,
            };

            _unitOfWork.Users.Insert(user);
            _unitOfWork.SaveChanges();
        }

        public string Validate(LoginDto loginData)
        {
            var user = _unitOfWork.Users.GetByEmail(loginData.Email);

            if (user == null)
            {
                return "Invalid user";
            }


            var passwordFine = authorizationService.VerifyHashedPassword(user.PasswordHash, loginData.Password);

            if(passwordFine)
            {
                return authorizationService.GetToken(user);
            }
            else
            {
                return "Invalid password";
            }
        }
    }
}