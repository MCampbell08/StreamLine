using System;
using System.Collections.Generic;
using System.Linq;
using StreamLine.Models;
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
                TwitterConnect = register.TwitterConnect,
                Birthdate = register.BirthDate,
                Gender = register.GenderType.ToString()


            };

            context.ProfileInformations.Attach(dbprofiles);
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


        public IList<RegisterViewModel> GetRegisterData()
        {
            var proifle = context.AspNetUsers.Select(x => x).ToList().Select(

                dd =>

                new RegisterViewModel
                {
                    Email = dd.Email,
                    Username = dd.UserName

                }
                  );

            return proifle.ToList();

        }

        public RegisterViewModel GetProfilebyId(int id)
        {
            return GetAllProfileInfo().Single(x => x.UserId == id);
        }

        public IList<RegisterViewModel> GetStaticProfileInfolList()
        {
            throw new NotImplementedException();
        }
    }
}
