using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.DALTicketingSystem
{
    public class DALTicket
    {
        internal List<Ticket> GetAssigneeTickets(User pUser)
        {
            List<Ticket> retorno = new List<Ticket>();
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    //TUSER es el asignado.
                    List<TTICKET> aux = entidades.TTICKET.Include("TUSER").Where(t => t.TUSER.email.Equals(pUser.email) && t.status.Equals("open")).ToList();
                    foreach (TTICKET t in aux)
                    {
                        retorno.Add(new Ticket(t.id, t.title, t.body, t.status, (User)t.TUSER1, (User)t.TUSER, t.created));
                    }
                }
            }
            catch
            {
                retorno = new List<Ticket>();
            }
            return retorno;
        }

        internal List<Ticket> GetAllAssigneeTickets(User pUser)
        {
            List<Ticket> retorno = new List<Ticket>();
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    //TUSER es el asignado.
                    List<TTICKET> aux = entidades.TTICKET.Include("TUSER").Where(t => t.TUSER.email.Equals(pUser.email)).ToList();
                    foreach (TTICKET t in aux)
                    {
                        retorno.Add(new Ticket(t.id, t.title, t.body, t.status, (User)t.TUSER1, (User)t.TUSER, t.created));
                    }
                }
            }
            catch
            {
                retorno = new List<Ticket>();
            }
            return retorno;
        }

        internal User UserLogin(string pEmail, string pPassword)
        {
            User retorno = null;
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    TUSER aux = entidades.TUSER.Where(t => t.email.ToUpper().Equals(pEmail.ToUpper()) && t.password.Equals(pPassword) ).FirstOrDefault();
                    if (aux != null)
                    {
                        retorno = new User(aux.email, aux.password, aux.name, aux.enable);
                    }
                }
            }
            catch
            {

            }
            return retorno;
        }

        internal Ticket GetTicketDetails(User pUser, int pId)
        {
            Ticket retorno = null;
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    //TUSER es el asignado.
                    TTICKET aux = entidades.TTICKET.Include("TUSER").Where(t => t.id == pId && (t.autor.Equals(pUser.email) || t.assignee.Equals(pUser.email))).FirstOrDefault();
                    if (aux != null)
                    {
                        retorno = new Ticket(aux.id, aux.title, aux.body, aux.status, (User)aux.TUSER1, (User)aux.TUSER, aux.created);
                    }
                }
            }
            catch (Exception ex)
            {
                retorno = null;
            }
            return retorno;
        }

        internal List<User> GetUsersForDropList(User pUser)
        {
            List<User> retorno = new List<User>();
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    List<TUSER> usuarios = entidades.TUSER.Where(t => !t.email.ToUpper().Equals(pUser.email.ToUpper())).ToList();
                    foreach (TUSER u in usuarios)
                    {
                        retorno.Add(new User(u.email, u.password, u.name, u.enable));
                    }
                }
            }
            catch (Exception ex)
            {
                retorno = new List<User>();
            }
            return retorno;
        }

        internal bool DeleteTicket(User pUser, int pId)
        {
            bool retorno = false;
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    TTICKET aux = entidades.TTICKET.Include("TUSER").Where(t => t.id == pId && (t.autor.Equals(pUser.email) || t.assignee.Equals(pUser.email))).FirstOrDefault();
                    if (aux != null)
                    {
                        aux.status = "closed";
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

        internal bool Create(Ticket ticket)
        {
            bool retorno = false;
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    entidades.TTICKET.Add(new TTICKET
                    {
                        title = ticket.title,
                        body = ticket.body,
                        status = "open",
                        autor = ticket.author.email.ToUpper(),
                        assignee = ticket.assignee.email.ToUpper(),
                        created = DateTime.Now

                    });
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

        internal bool Edit(User pUsuario, int pId, string pBody, string pStatus, string pAssignee)
        {
            bool retorno = false;
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    TTICKET ticket = entidades.TTICKET.Where(t => t.id == pId && t.status.Equals("open") && t.assignee.ToUpper().Equals(pUsuario.email.ToUpper())).FirstOrDefault();
                    if (ticket != null)
                    {
                        ticket.body = pBody;
                        ticket.status = pStatus;
                        ticket.assignee = pAssignee;
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
    }
}