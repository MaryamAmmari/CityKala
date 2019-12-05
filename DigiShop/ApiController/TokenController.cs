using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiShop.Models.DataModels;
using DigiShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace DigiShop.ApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private UserManager<AppUser> _userManager;
        public TokenController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("نام کاربری یا رمز عبور معتبر نیست");
            var user = await _userManager.FindByNameAsync(model.UserName);

            if(user == null)
                return BadRequest("نام کاربری یا رمز عبور معتبر نیست");

            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if(!checkPassword)
                return BadRequest("نام کاربری یا رمز عبور معتبر نیست");

            /********************CreateToken********************/

            var claims = await _userManager.GetClaimsAsync(user);
            var roles = string.Join(',', await _userManager.GetRolesAsync(user));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim("UserName", user.UserName));
            claims.Add(new Claim("Roles", roles));

            var key = new SymmetricSecurityKey(Encoding.Unicode.GetBytes("My Password"));

            var token = new JwtSecurityToken(
                issuer: "Http://CityKala.ir",
                audience: "Http://CityKala.ir",
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return Json(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = token.ValidTo,
            });

        }
    }
}