using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingSystem.DALTicketingSystem
{
    public class DALUtils
    {
        internal List<string> GetListOfStatus()
        {
            List<string> retorno = new List<string>();
            try
            {
                using (var entidades = new TicketingSystemEntities())
                {
                    List<TTICKETSTATUS> status = entidades.TTICKETSTATUS.ToList();
                    foreach (TTICKETSTATUS t in status)
                    {
                        retorno.Add(t.status);
                    }
                }
            }
            catch
            {
                retorno = new List<string>();
            }
            return retorno;
        }
    }
}