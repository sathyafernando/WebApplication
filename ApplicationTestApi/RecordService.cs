using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationTestApi
{
    public class RecordService
    {
        public bool IsActiveShift(Shift shift)
        {
            if (shift.CurrentTime >= shift.Start || shift.CurrentTime <= shift.End)
            {
                return true;
            }

            return false;
        }

    }

    public class Shift
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime CurrentTime { get; set; }
        public bool IsShift { get; set; }
    }
}
