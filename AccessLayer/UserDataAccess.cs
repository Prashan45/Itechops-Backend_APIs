using System;
using System.Collections.Generic;
using System.Linq;
using Task_API.Dbcontext;
using Task_API.Models;

namespace Task_API.AccessLayer
{
    public class UserDataAccess
    {
        private readonly Applicationdbcontext _context;

        public UserDataAccess(Applicationdbcontext context)
        {
            _context = context;
        }

        public List<Signup_Model> GetAllUsers()
        {
            return _context.Signup_tbl.Select(u => new Signup_Model
            {
                Id = u.Id,
                Name = u.Name,
                Contact = u.Contact,
                Email = u.Email,
                Role = u.Role,
                ProfilePicture = u.ProfilePicture,
            }).ToList();
        }

        public Signup_Model GetUserById(int id)
        {
            return _context.Signup_tbl.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(Signup_Model newUser)
        {
            _context.Signup_tbl.Add(newUser);
            _context.SaveChanges();
        }

        public void UpdateUser(Signup_Model updatedUser)
        {
            _context.Signup_tbl.Update(updatedUser);
            _context.SaveChanges();
        }
    }
}
