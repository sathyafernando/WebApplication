using ApplicationTestApi;
using System;
using System.Globalization;
using Xunit;

namespace ApplicationTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void CanBeActiveShist()
        {
            var record = new RecordService();

            var shift = new Shift
            {
                Start = DateTime.Parse("7:04:00 PM"),
                End = DateTime.Parse("11:04:00 PM"),
                CurrentTime = DateTime.ParseExact("Mon 16 Jun 8:30 PM 2008", "ddd dd MMM h:mm tt yyyy", CultureInfo.InvariantCulture)

            };

            var status = record.IsActiveShift(shift);

            Assert.True(status == true);
        }
    }
}
