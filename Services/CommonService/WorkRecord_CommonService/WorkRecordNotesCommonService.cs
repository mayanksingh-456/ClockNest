
using ClockNest.Helpers;
using ClockNest.Models.Employee_Model;
using ClockNest.Models.WorkRecordNotes_Model;
using ClockNest.Services.Tags_Service;
using ClockNest.Services.WorkRecordNotes_Service;
using ClockNest.ViewModels.Parameter_List;
using ClockNest.ViewModels.WorkRecordNotesViewModel;

namespace ClockNest.Services.CommonService.WorkRecord_CommonService
{
    public class WorkRecordNotesCommonService
    {
        private readonly WorkRecordNotesService _workRecordNotesService;
        private readonly TagsService _tagService;

        public WorkRecordNotesCommonService(WorkRecordNotesService workRecordNotesService, TagsService tagService)
        {
            _workRecordNotesService = workRecordNotesService;
            _tagService = tagService;
        }

        public async Task<List<Employee>> GetEmployeesAsync(int companyId, int tagId, int selectedEmployeeId)
        {
            var parameterList = new ParameterList
            {
                CompanyId = companyId,
                TagId = tagId,
                EmployeeId = selectedEmployeeId
            };
            return await _workRecordNotesService.GetEmployeeByTag2Async(parameterList);
        }

        public async Task<CreateEditWorkRecordNotesViewModel?> EditWorkRecordModel( WorkRecordNoteDetail editWorkRecordNote, int companyId, int employeeShiftId, int userId, int tagId, string? startDate, int selectedEmployeeId)
        {
            if (editWorkRecordNote == null) return null;

            DateTime date = string.IsNullOrEmpty(startDate) ? Helper.GetDateTimeNow(): DateTime.Parse(startDate);

            var workRecordAllDetails = await _workRecordNotesService.GetWorkRecordAllDetailsAsync(editWorkRecordNote.EmployeeId, employeeShiftId, editWorkRecordNote.ShiftDate);

            var hideCosts = await _workRecordNotesService.HideCosts(userId);

            return new CreateEditWorkRecordNotesViewModel
            {
                Id = editWorkRecordNote.Id,
                EmployeeId = editWorkRecordNote.EmployeeId,
                ShiftDate = editWorkRecordNote.ShiftDate,
                Notes = editWorkRecordNote.Notes,
                WorkDetails = workRecordAllDetails
            };
        }
    }
}
