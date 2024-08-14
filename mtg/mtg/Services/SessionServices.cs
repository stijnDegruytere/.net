namespace mtg.SessionServices
{
    public class SessionServices
    {
        private readonly List<User> _users = [];

        public SessionServices()
        {
            // You can initialize some default users here for testing
            _users.Add(new User { Username = "user1", Password = "password1" });
            _users.Add(new User { Username = "user2", Password = "password2" });
        }

        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                return true;
            }

            return false;
        }

        public bool Register(string username, string password)
        {
            if (!_users.Any(u => u.Username == username))
            {
                _users.Add(new User { Username = username, Password = password });

                return true;
            }

            return false;
        }
    }

    public class User
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}