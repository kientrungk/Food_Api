using ApiWebFood.Interface;
using Microsoft.AspNetCore.SignalR;

namespace ApiWebFood.Controllers.Client
{
    public class SignInService : ISignInService
    {
        private bool _isSignedIn;

        public bool IsSignedIn()
        {
            return _isSignedIn;
        }

        public async Task<bool> SignIn(string username, string password)
        {
           bool IsCheck = await AuthenticateUser(username, password);
            if (IsCheck)
            {
                _isSignedIn = true;
            }
            return IsCheck;
        }

        public Task SignOut()
        {
            // Thực hiện các thao tác cần thiết để đăng xuất
            _isSignedIn = false;
            return Task.CompletedTask;
        }
        private Task<bool> AuthenticateUser(string username, string password)
        {
            // Kiểm tra thông tin đăng nhập và trả về kết quả
            // Đây chỉ là một ví dụ, bạn cần triển khai logic xác thực phù hợp với ứng dụng của mình
            bool isLoginSuccessful = (username == "admin" && password == "123456");

            return Task.FromResult(isLoginSuccessful);
        }
    }
}
