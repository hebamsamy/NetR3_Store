using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels;

namespace Managers
{
    public class AccountManager :MainManager<User>
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private VendorManager vendorManager;
        private IConfiguration configuration;
        public AccountManager(ProjectContext context 
            ,UserManager<User> _userManager, 
            SignInManager<User> _signmanager,
            VendorManager _vendorManager,
            IConfiguration _configuration
            ) : base(context) {
            signInManager = _signmanager;
            userManager = _userManager;
            vendorManager = _vendorManager;
            configuration = _configuration;
        }


        public async Task<IdentityResult> Register(UserRegisterViewModel viewModel )
        {
            User user = viewModel.ToModel();
            var result =  await userManager.CreateAsync( user , viewModel.Password);
            if (result != null && result.Succeeded)
            {

                result = await userManager.AddToRoleAsync(user, viewModel.Role);
                //based on role Add TO Table
                if (result != null && result.Succeeded == true && viewModel.Role == "Vendor")
                {
                    vendorManager.Add(new Vendor()
                    {
                        UserID = user.Id,
                        JoinedDate = DateTime.Now
                    });
                }
                return result;
            }
            else
            {
                //to Do Add Error
                return IdentityResult.Failed();
            }
        }

        public async Task<string> Login(UserLoginViewModel viewModel) {

            //is value in (viewModel.LoginMethod) EMAIL OR UserName
            var user = await
                userManager.FindByEmailAsync(viewModel.LoginMethod);

            if (user == null)
            {
                user = await userManager.FindByNameAsync(viewModel.LoginMethod);
                if (user == null)
                {
                    return "Failed";
                }
            }
            
            var res =  await  signInManager.PasswordSignInAsync(user, viewModel.Password,viewModel.RemeberMe,true);
            if (res.Succeeded)
            {
                 List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                var roles = await userManager.GetRolesAsync(user);
                roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));
                
                JwtSecurityToken securityToken = new JwtSecurityToken(
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Key"]))
                    , SecurityAlgorithms.HmacSha256),
                    expires : DateTime.Now.AddHours(1),
                    claims:claims
                    );
                return new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
            else
            {
                return string.Empty;
            }
            //return
            
        }
         public async void SignOut()
         {
            await signInManager.SignOutAsync();
         } 
        public async Task<IdentityResult> ChangePassword(UserChangePassword viewModel)
        {
            var User = await userManager.FindByIdAsync(viewModel.UserID);

            return await userManager.ChangePasswordAsync(User, viewModel.CurrentPassword, viewModel.NewPassword);
        }
        public async Task<string> GetResetPasswordCode(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return string.Empty;
            }
            else
            {
                return await userManager.GeneratePasswordResetTokenAsync(user);
            }
        }
        public async Task<IdentityResult> ResetPassword(UserResetPasswordViewModel viewModel )
        {
            var user = await userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                return await userManager.ResetPasswordAsync
                     (user, viewModel.Code, viewModel.NewPassword);
            }
            else {
                return IdentityResult.Failed(
                    new IdentityError()
                    {
                        Description = "Sorry In valid Operation !!!"
                    });
            }
        }
    }
}
