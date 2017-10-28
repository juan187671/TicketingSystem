using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.DALTicketingSystem
{
    public class DALUser
    {
        internal List<TicketingSystem.Models.User> GetUsersList()
        {
            List<TicketingSystem.Models.User> retorno = new List<Models.User>();
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    retorno = entidades.TUSER.Select(u => new Models.User
                    {
                        email = u.email,
                        name = u.name,
                        password = u.password,
                        enable = u.enable                        
                    }).ToList();
                }
            }
            catch
            {
                retorno = new List<Models.User>();
            }
            return retorno;
        }

        internal bool Edit(User pUser)
        {
            bool retorno = false;
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    TUSER user = entidades.TUSER.Where(t => t.email.ToUpper().Equals(pUser.email.ToUpper())).FirstOrDefault();
                    if (user != null)
                    {
                        user.enable = pUser.enable;
                        entidades.SaveChanges();
                        retorno = true;
                    }
                }
            }
            catch
            {
                retorno = false;
            }
            return retorno;
        }

        internal bool Delete(string pEmail)
        {
            bool retorno = false;
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    TUSER user = entidades.TUSER.Where(t => t.email.ToUpper().Equals(pEmail.ToUpper())).FirstOrDefault();
                    if (user != null)
                    {
                        entidades.TUSER.Remove(user);
                        entidades.SaveChanges();
                        retorno = true;
                    }
                }
            }
            catch
            {
                retorno = false;
            }
            return retorno;
        }

        internal bool Create(User model)
        {
            bool retorno = false;
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    TUSER user = new TUSER()
                    {
                        email = model.email.ToUpper(),
                        name = model.name.ToUpper(),
                        password = model.password,
                        enable = model.enable
                    };
                    entidades.TUSER.Add(user);
                    entidades.SaveChanges();
                    retorno = true;                    
                }
            }
            catch
            {
                retorno = false;
            }
            return retorno;
        }

        internal User getUsers(string pEmail)
        {
            User retorno = null;
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    retorno = entidades.TUSER.Where(t => t.email.ToUpper().Equals(pEmail.ToUpper())).Select(t => new User
                    {
                        name = t.name,
                        email = t.email,
                        password = t.password,
                        enable = t.enable
                    }).FirstOrDefault();
                }
            }
            catch
            {
                retorno = null;
            }
            return retorno;
        }
    }
}