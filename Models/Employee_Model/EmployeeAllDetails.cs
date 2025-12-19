using System.Diagnostics;

namespace ClockNest.Models.Employee_Model
{
    public class EmployeeAllDetails
    {
      
            public EmployeeAllDetails()
            {
                Employee = new Employee();
                PersonTypes = new List<PersonType>();
                Titles = new List<Title>();
                Rosters = new List<Roster>();
                Activities = new List<Activity>();
                //CostCentres = new List<CostCentre>();
                //TerminalGroups = new List<TerminalGroupDetails>();
                //GTTerminalGroups = new List<GTTerminalGroupDetails>();
                //AccessGroups = new List<AccessGroupDetails>();
                //ShapeCompanies = new List<ShapeCompany>();
                //SSPRules = new List<SSPRule>();
                //EmploymentTypes = new List<EmploymentType>();
                //FlexitimeRules = new List<FlexitimeRule>();
                //AccrualRules = new List<AccrualRule>();
                //Employees = new List<EmployeeName>();
                //Tags = new List<Tag>();
                //Processes = new List<Process>();
                //AbsencePeriod = new AbsencePeriod();
                //EmployeePhoto = new EmployeePhoto();
                //EmployeeTagDetails = new List<EmployeeTagDetails>();
                //MailMergeTemplateDocuments = new List<CompanyDocument>();
                //NationalityTypes = new List<NationalityType>();
                //EthnicTypes = new List<EthnicType>();
                //ReligionTypes = new List<ReligionType>();
            }

            public Employee Employee { get; set; }
            public List<PersonType> PersonTypes { get; set; }
            public List<Roster> Rosters { get; set; }
            public List<Title> Titles { get; set; }
            public List<Activity> Activities { get; set; }
            //public List<CostCentre> CostCentres { get; set; }
            //public List<TerminalGroupDetails> TerminalGroups { get; set; }
            //public List<GTTerminalGroupDetails> GTTerminalGroups { get; set; }
            //public List<AccessGroupDetails> AccessGroups { get; set; }
            //public List<ShapeCompany> ShapeCompanies { get; set; }
            //public List<SSPRule> SSPRules { get; set; }
            //public List<EmploymentType> EmploymentTypes { get; set; }
            //public List<FlexitimeRule> FlexitimeRules { get; set; }
            //public List<AccrualRule> AccrualRules { get; set; }
            //public List<EmployeeName> Employees { get; set; }
            //public List<Tag> Tags { get; set; }
            //public List<Process> Processes { get; set; }
            //public AbsencePeriod AbsencePeriod { get; set; }
            //public EmployeePhoto EmployeePhoto { get; set; }
            //public List<EmployeeTagDetails> EmployeeTagDetails { get; set; }
            //public List<CompanyDocument> MailMergeTemplateDocuments { get; set; }
            //public List<NationalityType> NationalityTypes { get; set; }
            //public List<EthnicType> EthnicTypes { get; set; }
            //public List<ReligionType> ReligionTypes { get; set; }
            //public List<Folder> Folders { get; set; }
        }
}
