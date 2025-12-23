namespace ClockNest.Models.Employee_Model
{
    public class GTTerminalGroupDetails
    {
        public GTTerminalGroupDetails()
        {
            GTTerminalDetails = new List<GTTerminalGroupGTTerminalDetails>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }

        public List<GTTerminalGroupGTTerminalDetails> GTTerminalDetails { get; set; }

        public string GTTerminalDetailsDisplay
        {
            get
            {
                var displayText = string.Empty;
                //return String.Join(", ", GTTerminalDetails.FindAll(t=>t.Name).ToArray());
                GTTerminalDetails.ForEach(delegate (GTTerminalGroupGTTerminalDetails t)
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
