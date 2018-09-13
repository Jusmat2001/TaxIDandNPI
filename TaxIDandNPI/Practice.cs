namespace TaxIDandNPI
{
    public class Practice
    {
        public int PracticeId { get; set; }

        public int PracticeNpi { get; set; }

        public int PracticeTaxId { get; set; }

        public string PracticeName { get; set; }

        public string PracticeFullInfo
        {
            get
            {
                return $"Practice- {PracticeId}, NPI- {PracticeNpi}, Tax ID- {PracticeTaxId}";
            }
        }
    }

    public class Doctor
    {
        public int PracticeId { get; set; }

        public int DrNpi { get; set; }

        public string DrName { get; set; }

        public string DrFullInfo
        {
            get
            {
                return $"Dr- {DrName},   Dr NPI- {DrNpi},   Practice- {PracticeId}";
            }
        }
    }
}