using System;
using FfsScheduleParser.Domain;

namespace FfsScheduleParser.Services
{
    public interface IWorkingShiftService
    {
        WorkingShift CreateShiftWithConfigParams(DateTime startDate);
    }
}
