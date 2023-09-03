namespace EndOfDayBalances.Data.Entities
{
    public class Party
    {
        public string PartyId { get; set; }
        public string FullName { get; set; }
        public List<object> Addresses { get; set; }
        public object PartyType { get; set; }
        public object IsIndividual { get; set; }
        public object IsAuthorizingParty { get; set; }
        public object EmailAddress { get; set; }
        public List<object> PhoneNumbers { get; set; }
    }
}
