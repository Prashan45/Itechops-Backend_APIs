using Org.BouncyCastle.Crypto.Generators;
using Task_API.Dbcontext;
using Task_API.Models;

namespace Task_API.AccessLayer
{
    public class Savedata
    {
        private readonly Applicationdbcontext _context;

        public Savedata(Applicationdbcontext context)
        {
            _context = context;
        }

        public void SaveInformation(Signup_Model data)
        {
            _context.Signup_tbl.Add(data);
            _context.SaveChanges();
        }
        public Signup_Model AuthenticateUser(Login_model loginModel)
        {
            var user = _context.Signup_tbl.FirstOrDefault(x => x.Email == loginModel.Email && x.Password == loginModel.Password);

            if (user == null)
                return null; 

            var userModel = new Signup_Model
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };

            return userModel;
        }
    }
}
