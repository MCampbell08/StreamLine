using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamLine.Models;
using StreamLineLib;
using StreamLineDataModel;

namespace StreamLineLib.Services
{
    public class EFProfileService : IProfileService
    {
        StreamLineDataEntities context = new StreamLineDataEntities();

        public void AddProfileInfo(RegisterViewModel register)
        {
            ProfileInformation dbprofiles = new ProfileInformation
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                Username = register.Username,
                UserId = register.UserId,
                Password = register.Password,
                InstagramConnect = register.InstagramConnect,
                TwitterConnect = register.TwitterConnect
            };

            context.ProfileInformations.Add(dbprofiles);
            context.SaveChanges();
            
        }

        public void DeleteProfileInfo(int id)
        {
            var profile = new ProfileInformation()
            {
                UserId = id
            };
            context.ProfileInformations.Attach(profile);
            context.ProfileInformations.Remove(profile);
            context.SaveChanges();
        }

        public IList<RegisterViewModel> GetAllProfileInfo()
        {
            var profile = context.ProfileInformations.Select(x => x).ToList().Select(

                dp =>
           new RegisterViewModel
           {
               UserId = dp.UserId,
               FirstName = dp.FirstName,
               LastName = dp.LastName,
               Email = dp.Email,
               Username = dp.Username,
               Password  = dp.Password,
               InstagramConnect = dp.InstagramConnect,
               TwitterConnect  = dp.TwitterConnect


           }
               );

            return  profile.ToList();
        }

        public RegisterViewModel GetProfilebyId(int id)
        {
            return GetAllProfileInfo().Single(x => x.UserId == id);
        }
    }
}
