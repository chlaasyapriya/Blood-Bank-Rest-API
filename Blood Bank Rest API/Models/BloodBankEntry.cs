namespace Blood_Bank_Rest_API.Models
{
    public class BloodBankEntry
    {
        public int Id { get; set; }
        public string DonorName { get; set; }
        public int Age { get; set; }
        public string BloodType { get; set; }
        public string ContactInfo { get; set; }
        public int Quantity { get; set; }
        public DateOnly CollectionDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public string Status { get; set; }
    }
}
