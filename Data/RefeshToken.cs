namespace ApiWebFood.Data
{
    public class RefeshToken
    {
        public string Token { get; set; }
        public DateTime create { get; set; }

        public DateTime Expires { get; set; }
    }
}
