namespace ApiWebFood.Data
{
    public class UserToken
    {
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string TokenString { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpries { get; set; }
    }
}
