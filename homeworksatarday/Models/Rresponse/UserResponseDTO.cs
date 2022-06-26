using homeworksatarday.Models.Entites;
using System;
using System.Collections.Generic;

namespace homeworksatarday.Models.Rresponse
{
    public class UserResponseDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Boolean IsDeleted { get; set; } = false;

    }
}
