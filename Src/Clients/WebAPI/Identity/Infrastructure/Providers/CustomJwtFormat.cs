using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Shop.WebApi.Identity.Infrastructure.Providers
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private static readonly byte[] Secret =
            TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);

        private readonly string _issuer;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var properties = data.Properties;

            var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Secret), SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(_issuer, "Any", data.Identity.Claims,
                properties.IssuedUtc.Value.UtcDateTime, properties.ExpiresUtc.Value.UtcDateTime, signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}