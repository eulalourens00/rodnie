namespace Rodnie.API.DTO.Responses
{
    public class PinResponse
    {
        public Guid id { get; set; }
        public Guid owner_user_id {  get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        //public DateTime created_at { get; set; }
        //public DateTime updated_at { get; set; }
    }
}
