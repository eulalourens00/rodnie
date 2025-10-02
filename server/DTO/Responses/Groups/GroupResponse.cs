namespace Rodnie.API.DTO.Responses.Groups {
    public class GroupResponse {
        public Guid id { get; set; }
        public string name { get; set; }
        public Guid owner_user_id { get; set; }
    }
}
