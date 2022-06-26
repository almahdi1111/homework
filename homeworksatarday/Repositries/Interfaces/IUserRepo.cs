using homeworksatarday.Models.Entites;
using homeworksatarday.Models.RequestDTO;
using homeworksatarday.Models.Rresponse;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;

namespace homeworksatarday.Repositries.Interfaces
{
    public interface IUserRepo
    {

        IQueryable GetAll();
        UserResponseDTO GetById(int id, out string Error);
        Users Add(UserAddDTO NewUser, out string ErorrMessage);
        Users Update(UserUpdateDTO NewUser, int id, out string ErorrMessage);
        Users UpdatePatch(int id, JsonPatchDocument UserPatch, out string ErorrMessage);
        Users Delete(int id, out string ErorrMessage);





    }
}
