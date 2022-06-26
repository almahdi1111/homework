using AutoMapper;
using homeworksatarday.Models.Entites;
using homeworksatarday.Models.RequestDTO;
using homeworksatarday.Models.Rresponse;
using homeworksatarday.Repositries.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;


namespace homeworksatarday.Repositries
{
    public class UserRepo : IUserRepo
    {
        private readonly MasterDbContext dbContext;
        private readonly IMapper mapper;

        public UserRepo(MasterDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public IQueryable GetAll()
        {
            return dbContext.Users.Where(user => user.IsDeleted == false).Select(user => mapper.Map<UserResponseDTO>(user));
        }
        public UserResponseDTO GetById(int id, out string Error)
        {
            Error = "";
            var CurrentUser = dbContext.Users.Where(users => users.UserId == id && users.IsDeleted == false).FirstOrDefault();
            if (CurrentUser == null)
            {
                Error = "User Not Found or Deleted";
                return null;
            }


            return mapper.Map<UserResponseDTO>(CurrentUser);
        }
        public Users Add(UserAddDTO NewUser, out string ErorrMessage)
        {
            ErorrMessage = "";
            if (dbContext.Users.Any(user => user.UserName.Equals(NewUser.UserName)))
            {
                ErorrMessage = "Dublicate name";
                return null;

            }


            var CurrentUser = mapper.Map<Users>(NewUser);
            dbContext.Users.Add(CurrentUser);
            dbContext.SaveChanges();
            return CurrentUser;

        }
        public Users Update(UserUpdateDTO NewUser, int id, out string ErorrMessage)
        {
            ErorrMessage = "";
            if (string.IsNullOrEmpty(NewUser.UserName))
            {
                ErorrMessage = "name is invalid";
                return null;
            }
            var CurrentUser = dbContext.Users.Where(user => user.UserId == id && user.IsDeleted == false).SingleOrDefault();
            if (CurrentUser == null)
            {
                ErorrMessage = "User not found or deleted";

            }
            var UpdateUser = mapper.Map(NewUser, CurrentUser);
            dbContext.SaveChanges();
            return UpdateUser;

        }
        public Users UpdatePatch(int id, JsonPatchDocument UserPatch, out string ErorrMessage)
        {
            ErorrMessage = "";
            var CurrentUser = dbContext.Users.Where(user => user.UserId == id).SingleOrDefault();
            if (CurrentUser == null)
            {
                ErorrMessage = "User is not Found";
                return null;
            }
            UserPatch.ApplyTo(CurrentUser);
            if (!(string.IsNullOrWhiteSpace(CurrentUser.UserName) || string.IsNullOrWhiteSpace(CurrentUser.Email)))
            {
                ErorrMessage = "name or Email is Invalid";
                return null;
            }
            dbContext.SaveChanges();
            return CurrentUser;
        }
        public Users Delete(int id, out string ErorrMessage)
        {
            ErorrMessage = "";
            var CurrentUser = dbContext.Users.Where(user => user.UserId == id).SingleOrDefault();
            if (CurrentUser == null)
            {
                ErorrMessage = "User is not found";
                return null;
            }

            CurrentUser.IsDeleted = true;
            dbContext.SaveChanges();
            return CurrentUser;
        }
    }
}
