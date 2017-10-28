using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class ErrorMsg
    {
        public bool error { get; }
        public string msg { get; }

        public ErrorMsg()
        {

        }

        public ErrorMsg(bool pError, string pMsg)
        {
            this.error = pError;
            this.msg = pMsg;
        }
    }
}