namespace ApiWebFood.Interface
{
    public interface ISignInService
    {
        Task<bool> SignIn(string username, string password);
        Task SignOut();
        bool IsSignedIn();
    }
}
