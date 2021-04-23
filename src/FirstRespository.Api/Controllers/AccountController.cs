using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FirstRespository.Api.Dtos.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FirstRespository.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public AccountController(
            UserManager<IdentityUser<Guid>> userManager,
            SignInManager<IdentityUser<Guid>> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }
        
        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] AccountSignUpDto accountSignUpDto)
        {
            var user = new IdentityUser<Guid>(accountSignUpDto.UserName);
            var identityResult = await _userManager.CreateAsync(user, accountSignUpDto.Password);

            user = await _userManager.FindByNameAsync(accountSignUpDto.UserName);

            identityResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
            identityResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.UserName));
        
            if(identityResult.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(identityResult.Errors);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] AccountSignInDto accountSignInDto)
        {
            var user = await _userManager.FindByNameAsync(accountSignInDto.UserName);
            var claimList = await _userManager.GetClaimsAsync(user);

            await _signInManager.PasswordSignInAsync(user, accountSignInDto.Password, false, false);

            var secret = Encoding.UTF8.GetBytes("This is my very very secret key and we should keep it secret...");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(10),
                claims: claimList,
                signingCredentials: new SigningCredentials(symmetricSecurityKey , SecurityAlgorithms.HmacSha256Signature));

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var accountAccessTokenDto = new AccountAccessTokenDto()
            {
                AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken)
            };

            return Ok(accountAccessTokenDto);
        }
    }
}
