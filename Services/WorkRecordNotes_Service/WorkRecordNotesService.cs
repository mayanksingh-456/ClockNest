
using ClockNest.Enum;
using ClockNest.Models.Employee_Model;
using ClockNest.Models.Overtime_Model;
using ClockNest.Models.SelfService_Model;
using ClockNest.Models.Tag_Modal;
using ClockNest.Models.User_Model;
using ClockNest.Models.WorkRecordNotes_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using ClockNest.ViewModels.WorkRecordNotesViewModel;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;




namespace ClockNest.Services.WorkRecordNotes_Service
{
    public class WorkRecordNotesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserContext _userContext;
        private readonly AuthenticationStateProvider _auth;

        public WorkRecordNotesService(IHttpClientFactory httpClientFactory, UserContext userContext, AuthenticationStateProvider auth)
        {
            _httpClientFactory = httpClientFactory;
            _userContext = userContext;
            _auth = auth;
        }

        public async Task<List<WorkRecordNoteDetail>> GetWorkRecordNotesAsync(ParameterList parameterList)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordnotedetailsWithPhoto/get", parameterList).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<WorkRecordNoteDetail>>().ConfigureAwait(false);
                if (data == null)
                    throw new Exception("Work record note details not found.");
                return data;
            }
            return new List<WorkRecordNoteDetail>();

        }

        //Get company
        public async Task<Company?> GetCompanyAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/account/company/get", companyId).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Company>().ConfigureAwait(false);
                return data;
            }
            return null;
        }

        //get employee tag2
        public async Task<List<Employee>> GetEmployeeByTag2Async(ParameterList parameterList)
        {

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/setup/employees/employeesbytag2/get", parameterList);

            if (response.IsSuccessStatusCode)
            {
                var employees = await response.Content.ReadFromJsonAsync<List<Employee>>() ?? new();
                employees = employees.Where(e => e.PersonTypeId != 3).ToList();

                var responseA = await client.PostAsJsonAsync("chronicle/timeattendance/absenceperiods/get", parameterList.CompanyId);

                if (responseA.IsSuccessStatusCode)
                {
                    var absencePeriods = await responseA.Content.ReadFromJsonAsync<List<AbsencePeriod>>();
                    foreach (Employee employee in employees)
                    {
                        var ap = absencePeriods.Find(a => a.EmployeeId == employee.Id);
                        if (ap != null)
                        {
                            employee.HolidayEntitlement1 = ap.HolidayEntitlement1;
                            employee.HolidayCarried1 = ap.HolidayCarried1;
                        }
                    }
                }
                return employees;
            }
            return new List<Employee>();
        }

        //Get work record all details
        public async Task<WorkRecordAllDetails> GetWorkRecordAllDetailsAsync(int employeeId, int? employeeShiftId, DateTime date)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var parameterList = new ParameterList
            {
                EmployeeId = employeeId,
                EmployeeShiftId = employeeShiftId.GetValueOrDefault(0),
                Date = date
            };
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordalldetails/get", parameterList).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<WorkRecordAllDetails>().ConfigureAwait(false);
                if (data == null)
                    throw new Exception("Work record all details not found.");
                return data;
            }
            return null;
        }

        //hide cost
        public async Task<bool> HideCosts(int userId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/user/get", userId).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var savedUser = await response.Content.ReadFromJsonAsync<User>().ConfigureAwait(false);

                if (savedUser?.UserValueAccess.Where(x => x.ValueAccessTypeId == 1).Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        //Get Access Movement
        public async Task<List<EmployeeAccessControlSwipes>> GetEmployeeAccessControlSwipesAsync(ParameterList parameterList)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/accesscontrol/employeeaccesscontrolswipes/get", parameterList).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<EmployeeAccessControlSwipes>>().ConfigureAwait(false);
                if (data == null)
                    throw new Exception("Employee access control swipes not found.");
                return data;
            }
            return new List<EmployeeAccessControlSwipes>();
        }

        public async Task<List<Activity>> GetActivitiesAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/nonarchivedactivitiesandbreaks/get", companyId).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<List<Activity>>()
                    .ConfigureAwait(false) ?? new List<Activity>();
            }

            return new List<Activity>();
        }

        public async Task<List<CostCentre>> GetCostCentresAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/nonarchivedcostcentres/get", companyId).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<List<CostCentre>>()
                    .ConfigureAwait(false) ?? new List<CostCentre>();
            }

            return new List<CostCentre>();
        }

        //work record active
        public async Task<bool> IsWorkRecordActive(int workRecordId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/isworkrecordactive/get", workRecordId);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }

            return false;
        }

        //save ork record
        public async Task<List<WorkRecord>?> SaveWorkRecordAsync(WorkRecord workRecord)
        {
            if (workRecord == null) throw new ArgumentNullException(nameof(workRecord), "WorkRecord cannot be null");

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecord/post", workRecord);

            if (response.IsSuccessStatusCode)
            {
                var savedWorkRecords = await response.Content.ReadFromJsonAsync<List<WorkRecord>>();
                return savedWorkRecords;
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return null;
        }

        //Recalculate Overtime
        public async Task<List<Overtime>?> RecalculateOvertimeAsync(ParameterList parameterList)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var refreshResponse = await client.PostAsJsonAsync("chronicle/timeattendance/refreshovertime/post", parameterList);
            if (!refreshResponse.IsSuccessStatusCode)
            {
                var error = await refreshResponse.Content.ReadAsStringAsync();
                return null;
            }

            var overtimeResponse = await client.PostAsJsonAsync("chronicle/timeattendance/overtime/get", parameterList);
            if (!overtimeResponse.IsSuccessStatusCode)
            {
                var error = await overtimeResponse.Content.ReadAsStringAsync();
                return null;
            }

            var overtimeList = await overtimeResponse.Content.ReadFromJsonAsync<List<Overtime>>();
            return overtimeList;
        }

        //delete work record
        public async Task<bool> DeleteWorkRecordAsync(List<WorkRecord> selectedWorkRecord)
        {
            if (selectedWorkRecord == null || selectedWorkRecord.Count == 0)
                throw new ArgumentException("workRecord cannot be empty");

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordsdelete/post", selectedWorkRecord);

            if (response.IsSuccessStatusCode)
                return true;

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to delete work record: {response.StatusCode} - {errorContent}");
        }

        //Create shift
        public async Task<List<Shift>> GetWorkRecordShiftAsync(int companyId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
                var response = await client.PostAsJsonAsync("chronicle/setup/shiftpatterns/nonarchivedshifts/get", companyId);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<Shift>();
                }

                var result = await response.Content.ReadFromJsonAsync<List<Shift>>();
                return result ?? new List<Shift>();
            }
            catch
            {
                return new();
            }
        }

        //get roster shift
        public async Task<List<RosterScheduledShift>> GetRosterScheduledShiftsAsync(ParameterList parameterList)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/rosterscheduledshifts/get", parameterList);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to get roster scheduled shifts: {response.StatusCode} - {error}");
            }

            var shifts = await response.Content.ReadFromJsonAsync<List<RosterScheduledShift>>();
            return shifts ?? new List<RosterScheduledShift>();
        }

        //create default day
        public async Task<bool> CreateDefaultDayAsync(ParameterList parameters)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/createdefaultday/post", parameters);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<EmployeeShift> SetEmployeeShiftAsync(EmployeeShift employeeShift, int userId)
        {
             var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var parameterList = new ParameterList
            {
                EmployeeShift = employeeShift,
                UserId = userId
            };

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/employeeshift/post", parameterList);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EmployeeShift>();
            }
            else
            {
                throw new HttpRequestException($"Error posting employee shift: {response.ReasonPhrase}");
            }
        }

        //delete shift
        public async Task<bool> DeleteEmployeeShiftAsync(List<EmployeeShift> selectedShift, int userId)
        {
            if (selectedShift == null || selectedShift.Count == 0)
                throw new ArgumentException("workRecord cannot be empty");

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var parameterList = new ParameterList
            {
                EmployeeShifts = selectedShift,
                UserId = userId
            };
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/employeeshiftsdelete/post", parameterList);

            if (response.IsSuccessStatusCode)
                return true;

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to delete shift: {response.StatusCode} - {errorContent}");
        }

        // overtime
        public async Task<List<SelectListItem>> GetOvertimeRecordAsync(int companyId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
                var response = await client.PostAsJsonAsync("chronicle/setup/overtimerules/overtimegroups/get", companyId);

                if (response.IsSuccessStatusCode)
                {
                    var getOvertimeRecords = await response.Content.ReadFromJsonAsync<List<OvertimeGroup>>();
                    return getOvertimeRecords.Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching overtime groups: {ex.Message}");
            }
            return new List<SelectListItem>();
        }

        // overtime/ Toil
        public async Task<bool> ToilOvertimeAsync(ParameterList parameterList)
        {
            if (parameterList == null || parameterList.Overtime == null) throw new ArgumentException("ParameterList or Overtime cannot be null.", nameof(parameterList));

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/toilovertime/post", parameterList);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to toil overtime: {response.StatusCode} - {response.ReasonPhrase}\n{errorContent}");
            }
        }

        //<!-- overtime/ authorize-->
        public async Task<bool> AuthorizeOvertimeAsync(ParameterList parameterList)
        {
            if (parameterList == null || parameterList.Overtime == null) throw new ArgumentException("ParameterList or Overtime cannot be null.", nameof(parameterList));

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/authoriseovertime/post", parameterList);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to authorize overtime: {response.StatusCode} - {response.ReasonPhrase}\n{errorContent}");
            }
        }

        //<!-- overtime/ decline-->
        public async Task<bool> DeclineOvertimeAsync(ParameterList parameterList)
        {
            if (parameterList == null || parameterList.Overtime == null) throw new ArgumentException("ParameterList or Overtime cannot be null.", nameof(parameterList));

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/declineovertime/post", parameterList);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to decline overtime: {response.StatusCode} - {response.ReasonPhrase}\n{errorContent}");
            }
        }

        //<!-- overtime/ unpaid-->
        public async Task<bool> UnpaidOvertimeAsync(ParameterList parameterList)
        {
            if (parameterList == null || parameterList.Overtime == null) throw new ArgumentException("ParameterList or Overtime cannot be null.", nameof(parameterList));

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/unpaidovertime/post", parameterList);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to mark as unpaid overtime: {response.StatusCode} - {response.ReasonPhrase}\n{errorContent}");
            }
        }

        //Get absence by type
        public async Task<List<Absence>> GetAbsencesByTypeAsync(List<EnumAbsenceType> absenceTypes, int companyId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
                HttpResponseMessage response;

                if (absenceTypes.Contains(EnumAbsenceType.Holiday))
                {
                    response = await client.PostAsJsonAsync("chronicle/setup/hsa/nonarchivedholidays/get", companyId);
                }
                else if (absenceTypes.Contains(EnumAbsenceType.Sickness))
                {
                    response = await client.PostAsJsonAsync( "chronicle/setup/hsa/nonarchivedsicknesses/get", companyId);
                }
                else
                {
                    response = await client.PostAsJsonAsync("chronicle/setup/hsa/nonarchivedabsences/get", companyId);
                }

                var result = await response.Content.ReadFromJsonAsync<List<Absence>>();
                return result ?? new List<Absence>();
            }
            catch
            {
                return new List<Absence>();
            }

        }

        //public async Task<List<Absence>> GetNonArchivedHolidaysAsync(int companyId)
        //{
        //     var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
        //    var response = await client.PostAsJsonAsync("chronicle/setup/hsa/nonarchivedholidays/get", companyId);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception($"Failed to fetch non-archived holidays. Status: {response.StatusCode}");
        //    }

        //    var result = await response.Content.ReadFromJsonAsync<List<Absence>>();

        //    return result ?? new List<Absence>();
        //}
        //public async Task<List<Absence>> GetNonArchivedSicknessesAsync(int companyId)
        //{
        //     var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
        //    var response = await client.PostAsJsonAsync("chronicle/setup/hsa/nonarchivedsicknesses/get", companyId);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception($"Failed to fetch non-archived sicknesses. Status: {response.StatusCode}");
        //    }

        //    var result = await response.Content.ReadFromJsonAsync<List<Absence>>();

        //    return result ?? new List<Absence>();
        //}
        //public async Task<List<Absence>> GetNonArchivedAbsencesAsync(int companyId)
        //{
        //     var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
        //    var response = await client.PostAsJsonAsync("chronicle/setup/hsa/nonarchivedabsences/get", companyId);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception($"Failed to fetch non-archived absences. Status: {response.StatusCode}");
        //    }

        //    var result = await response.Content.ReadFromJsonAsync<List<Absence>>();

        //    return result ?? new List<Absence>();
        //}
        //public async Task<List<Absence>> GetAbsencesByTypeAsync(int absenceTypeId, int companyId)
        //{
        //    return absenceTypeId switch
        //    {
        //        (int)ClockNest.Enum.EnumAbsenceType.Holiday => await GetNonArchivedHolidaysAsync(companyId),
        //        (int)ClockNest.Enum.EnumAbsenceType.Sickness => await GetNonArchivedSicknessesAsync(companyId),
        //        _ => await GetNonArchivedAbsencesAsync(companyId),
        //    };
        //}


        public class AbsenceSaveResponse
        {
            public bool Success { get; set; }
            public string StatusCode { get; set; }
            public string Message { get; set; }
        }

        //Absence Save
        public async Task<AbsenceSaveResponse> SetAbsenteeRecordAsync(AbsenteeRecord absenteeRecord)
        {
            if (absenteeRecord == null)
                throw new ArgumentNullException(nameof(absenteeRecord), "Absentee record cannot be null.");

            try
            {
                var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
                var response = await client.PostAsJsonAsync("chronicle/timeattendance/absenteerecord/post", absenteeRecord);
                var rawContent = (await response.Content.ReadAsStringAsync())?.Trim('"', ' ', '\r', '\n');

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error response content: {rawContent}");
                    return new AbsenceSaveResponse
                    {
                        Success = false,
                        StatusCode = ((int)response.StatusCode).ToString(),
                        Message = rawContent
                    };
                }

                var knownFailureStatuses = new[]
                {
                    "EXCEEDED_TWO_ABSENCES",
                    "MULTIPLE_ABSENCE_DAYS",
                    "HOLIDAY_BLACKOUT",
                    "THRESHOLD_EXCEEDED",
                    "LOCKED",
                    "HOLIDAY_REQUEST_BEYOND_LEAVE_DATE",
                    "ENTITLEMENT_EXCEEDED",
                    "INVALID_TIMES",
                    "SSP_RULES_VIOLATED"
                };

                var isHalfdayInfo = rawContent == "HALFDAY_AVAILABLE";

                return new AbsenceSaveResponse
                {
                    Success = rawContent == "OK" || isHalfdayInfo,
                    StatusCode = ((int)response.StatusCode).ToString(),
                    Message = rawContent
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex}");
                return new AbsenceSaveResponse
                {
                    Success = false,
                    StatusCode = "500",
                    Message = ex.Message
                };
            }
        }

        //Delete Absence
        public async Task<bool> DeleteAbsenteeRecordAsync(List<AbsenteeRecord> records)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);           
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/absenteerecordsdeletebyid/post", records);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API call failed with status code {response.StatusCode}: {errorContent}");
            }
        }

        //Get Exceptional Item
        public async Task<List<ExceptionalItemType>> GetWorkRecordExceptionalItemAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/setup/employees/exceptionalitemtypes/get", companyId);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to fetch exceptional item types. Status: {response.StatusCode}, Error: {error}");
            }
            var result = await response.Content.ReadFromJsonAsync<List<ExceptionalItemType>>();
            return result ?? new List<ExceptionalItemType>();
        }

        //Save exceptional item
        public async Task<int> SetExceptionalItemAsync(ExceptionalItem exceptionalItem)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/exceptionalitem/post", exceptionalItem);
        response.EnsureSuccessStatusCode();
            var exceptionalItemId = await response.Content.ReadFromJsonAsync<int>();

            if (exceptionalItemId == 0)
            {
                throw new Exception("API returned exceptionalItemId = 0. Save failed.");
            }

            return exceptionalItemId;
        }

        //delete exceptional item
        public async Task<bool> DeleteExceptionalItemAsync(List<ExceptionalItem> exceptionalItems)
        {
            if (exceptionalItems == null || !exceptionalItems.Any())
                throw new ArgumentException("List of exceptional items to delete cannot be empty.", nameof(exceptionalItems));

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/exceptionalitemsdelete/post", exceptionalItems);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to delete exceptional items. Status: {response.StatusCode}, Error: {error}");
            }
        }

        //notes       
        public async Task<CreateEditWorkRecordNotesViewModel> GetWorkRecordNotesAsync(int employeeId, DateTime shiftDate)
        {
            if (employeeId == 0)  throw new ArgumentException("EmployeeId cannot be 0");

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var parameterList = new ParameterList
            {
                EmployeeId = employeeId,
                Date = shiftDate
            };

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordnotes/get", parameterList);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to get work record notes: {response.StatusCode}");

            var notes = await response.Content.ReadFromJsonAsync<List<WorkRecordNote>>();

            var model = new CreateEditWorkRecordNotesViewModel
            {
                Id = notes?.FirstOrDefault()?.Id ?? 0,
                EmployeeId = employeeId,
                ShiftDate = shiftDate,
                Notes = notes?.FirstOrDefault()?.Notes ?? string.Empty
            };

            return model;
        }


        //save notes
        public async Task<bool> SaveWorkRecordNotesAsync(WorkRecordNote workRecordNote)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordnotes/post", workRecordNote);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            var error = await response.Content.ReadAsStringAsync();
            return false;
        }
        
        public async Task<bool> SaveWorkRecordShowAsLateAsync(ParameterList parameterList)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
                var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordshowaslate/post", parameterList);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<bool?>();
                return result ?? false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SaveWorkRecordShowAsLateAsync failed: {ex}");
                return false;
            }
        }


        //verified toggle
        public async Task<bool> SetWorkRecordVerifiedAsync(Verified verified)
        {
            if (verified == null) throw new ArgumentException("Verified payload cannot be null.", nameof(verified));

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordverified/post", verified);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to verify work record: {response.StatusCode} - {response.ReasonPhrase}\n{errorContent}");
            }
        }

        //delete verified
        public async Task<bool> DeleteWorkRecordVerifiedAsync(Verified verified)
        {
            if (verified == null) throw new ArgumentException("Verified payload cannot be null.", nameof(verified));

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordverifieddelete/post", verified);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to delete verified work record: {response.StatusCode} - {response.ReasonPhrase}\n{errorContent}");
            }
        }

        //locked toggle
        public async Task<bool> SetWorkRecordLockedAsync(Locked locked)
        {
            if (locked == null) throw new ArgumentException("Locked payload cannot be null.", nameof(locked));

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordlocked/post", locked);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to lock work record: {response.StatusCode} - {response.ReasonPhrase}\n{errorContent}");
            }
        }
        public async Task<bool> DeleteWorkRecordLockedAsync(Locked locked)
        {
            if (locked == null) throw new ArgumentException("Locked payload cannot be null.", nameof(locked));

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordlockeddelete/post", locked);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to delete locked work record: {response.StatusCode} - {response.ReasonPhrase}\n{errorContent}");
            }
        }

        //Raise exception
        public async Task<bool> RaiseWorkRecordExceptions(List<int> exceptionIds, int userId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var parameterList = new ParameterList
            {
                Ids = exceptionIds,
                UserId = userId
            };
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/raisealerts/post", parameterList);

            return response.IsSuccessStatusCode &&
            await response.Content.ReadFromJsonAsync<bool>();
        }

        // clear exception
        public async Task<bool> ClearWorkRecordExceptions(List<int> exceptionIds, int userId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var parameterList = new ParameterList
            {
                Ids = exceptionIds,
                UserId = userId
            };
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/clearalerts/post", parameterList);

            return response.IsSuccessStatusCode &&
            await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
