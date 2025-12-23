namespace ClockNest.Models.Employee_Model
{
    public class TerminalGroupDetails
    {
        public TerminalGroupDetails()
        {
            TerminalDetails = new List<TerminalGroupTerminalDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<TerminalGroupTerminalDetails> TerminalDetails { get; set; }

        public string TerminalDetailsDisplay
        {
            get
            {
                var displayText = string.Empty;
                //return String.Join(", ", TerminalDetails.FindAll(t=>t.Name).ToArray());
                TerminalDetails.ForEach(delegate (TerminalGroupTerminalDetails t)
                {
                    displayText += t.Name + " ";

                });

                return displayText;
            }

        }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
