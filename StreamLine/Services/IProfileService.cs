using StreamLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLineLib.Services
{
    public interface IProfileService
    {
        IList<RegisterViewModel> GetAllProfileInfo();

        RegisterViewModel GetProfilebyId(int id);

        void AddProfileInfo(RegisterViewModel register);

        void DeleteProfileInfo(int id);

        IList<RegisterViewModel> GetStaticProfileInfolList();

        IList<RegisterViewModel> GetRegisterData();






    }
}
