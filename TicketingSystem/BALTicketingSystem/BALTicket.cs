using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.BALTicketingSystem
{
    public class BALTicket
    {
        public List<Models.Ticket> GetAssigneeTickets(Models.User pUser)
        {
            return new DALTicketingSystem.DALTicket().GetAssigneeTickets(pUser);
        }

        internal Ticket GetTicketDetails(User pUser, int pId)
        {
            return new DALTicketingSystem.DALTicket().GetTicketDetails(pUser, pId);
        }

        internal bool DeleteTicket(User pUsuario, int pId)
        {
            return new DALTicketingSystem.DALTicket().DeleteTicket(pUsuario, pId);
        }

        internal bool Create(Ticket ticket)
        {
            return new DALTicketingSystem.DALTicket().Create(ticket);
        }

        internal List<Ticket> GetAllAssigneeTickets(User pUser)
        {
            return new DALTicketingSystem.DALTicket().GetAllAssigneeTickets(pUser);
        }

        internal bool Edit(User pUsuario,int pId, string pBody, string pStatus, string pAssignee)
        {
            return new DALTicketingSystem.DALTicket().Edit(pUsuario,pId, pBody, pStatus, pAssignee);
        }
    }
}