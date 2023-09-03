using EndOfDayBalances.Contracts;
using System.Text.Json.Serialization;

namespace EndOfDayBalances.Data.Entities
{

    public class Transaction : AmountCreditDebit
    {
        public string TransactionId { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionStatus Status { get; set; }
        public TransactionCode TransactionCode { get; set; }
        public object ProprietaryTransactionCode { get; set; }
        public DateTime BookingDate { get; set; }
        public MerchantDetails MerchantDetails { get; set; }
        public EnrichedData EnrichedData { get; set; }
    }
}
