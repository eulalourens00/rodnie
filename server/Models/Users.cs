namespace Rodnie.API.Models {
    public class Users {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public bool is_verified { get; set; }
        public bool is_restricted { get; set; }
        public bool is_admin { get; set; }
    }
}
