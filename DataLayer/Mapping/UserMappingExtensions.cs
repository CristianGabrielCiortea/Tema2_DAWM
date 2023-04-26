using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mapping
{
    public static class UserMappingExtensions
    {
        public static List<UserDto> ToUserDtos(this List<User> users)
        {
            var results = users.Select(u => u.ToUserDto()).ToList();

            return results;
        }

        public static UserDto ToUserDto(this User user)
        {
            if (user == null) return null;

            var result = new UserDto();
            result.Id = user.Id;
            result.FullName = user.FirstName + " " + user.LastName;
            result.Email = user.Email;
            result.Role = user.Role;

            return result;
        }
    }
}