using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamLine.Models;

namespace StreamLineLib.Services
{
    public class ProfileService : IProfileService
    {
        public void AddProfileInfo(RegisterViewModel register)
        {
            throw new NotImplementedException();
        }

        public void DeleteProfileInfo(int id)
        {
            throw new NotImplementedException();
        }

        public IList<RegisterViewModel> GetAllProfileInfo()
        {
            throw new NotImplementedException();
        }

        public RegisterViewModel GetProfilebyId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<RegisterViewModel> GetStaticProfileInfolList()
        {
            List<RegisterViewModel> results = new List<RegisterViewModel>();

            results.Add(
                new RegisterViewModel()
                {
                   UserId = 1,
                   FirstName = "Billy",
                   LastName = "Johnny",
                   Email = "byjohnny@gmail.com",
                   Username = "JohnoleBilly",
                   Password = "1234",
                   ConfirmPassword ="1234",
                   InstagramConnect = false,
                   TwitterConnect = false

                });

            results.Add(
               new RegisterViewModel()
               {
                   UserId = 2,
                   FirstName = "Jilly",
                   LastName = "Lohnny",
                   Email = "byLohnny@gmail.com",
                   Username = "LohnoleJilly",
                   Password = "0000",
                   ConfirmPassword = "0000",
                   InstagramConnect = false,
                   TwitterConnect = false

               });
              
            return results;
        }
    }
}
