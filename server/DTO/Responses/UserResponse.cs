namespace Rodnie.API.DTO.Responses {
    public class UserResponse {
        public string token { get; set; }
        public Guid id { get; set; }
        public string username { get; set; }
        public string phone { get; set; }
        public bool is_verified { get; set; }
    }
}
