using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DataAccessLayer;

namespace WebApplication1.BusinessLayer
{
    public class BusinessSettings
    {
        public static void SetBusiness()
        {
            DatabaseSettings.SetDatabase();
        }
    }
}
