using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_planner.Business;

namespace Tour_planner.Data_Access
{
    public interface IQuery
    {
        public Task<Tourlist> GetTours();

    }
}
