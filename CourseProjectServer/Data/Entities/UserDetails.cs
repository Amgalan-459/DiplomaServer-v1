namespace CourseProjectServer.Data.Entities {
    public class UserDetails {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserDetails (string email, string password) {
            Email = email;
            Password = password;
        }
    }
}
