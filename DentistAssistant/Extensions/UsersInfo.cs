using DentistAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentistAssistant.Extensions
{
    public class UsersInfo
    {
        public static List<Users> Users { get; private set; }

        public static void SetUsers(List<Users> users)
        {
            Users = users;
        }
    }
}
