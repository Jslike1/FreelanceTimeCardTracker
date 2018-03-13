using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceTimeCardTracker.Models;

namespace FreelanceTimeCardTracker.DAL
{
    public interface ITimeCardDAL
    {
        List<TimeCard> GetAllTimeCards(string username);

        TimeCard GetTimeCard(string username);

        void PunchIn(TimeCard timeCard);

        void PunchOut(TimeCard timeCard);
    }
}
