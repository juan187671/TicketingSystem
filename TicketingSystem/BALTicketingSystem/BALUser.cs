using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using TicketingSystem.DALTicketingSystem;
using TicketingSystem.Models;

namespace TicketingSystem.BALTicketingSystem
{
    public class BALUser
    {
        internal List<TicketingSystem.Models.User> GetUsersList()
        {
            return new DALUser().GetUsersList();
        }

        internal User getUser(string pEmail)
        {
            return new DALUser().getUsers(pEmail);
        }

        internal bool Create(User model)
        {
            model.password = new BALUtils().GetMD5(model.password);
            return new DALUser().Create(model);
        }

        internal bool UserExist(string pEmail)
        {
            throw new NotImplementedException();
        }

        internal bool Delete(string pEmail)
        {
            return new DALUser().Delete(pEmail);
        }

        internal bool Edit(User pUser)
        {
            return new DALUser().Edit(pUser);
        }

        internal List<User> GetUsersForDropList(User pUser)
        {
            return new DALTicket().GetUsersForDropList(pUser);
        }

        internal User UserLogin(string pEmail, string pPassword)
        {
            string password = new BALUtils().GetMD5(pPassword);
            return new DALTicket().UserLogin(pEmail, password);
        }
    }
}