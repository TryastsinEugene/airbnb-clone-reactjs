﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace RealEstateAPI.Helpers
{
	public class JwtService
	{
		private string secureKey = "this is a very very secure key ever";
		public string Generate(int id)
		{
			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
			var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
			var header = new JwtHeader(credentials);

			var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.UtcNow.AddHours(24));
			var securityToken = new JwtSecurityToken(header, payload);

			return new JwtSecurityTokenHandler().WriteToken(securityToken);
		}

		public JwtSecurityToken Verify(string jwt)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secureKey);
			tokenHandler.ValidateToken(jwt, new TokenValidationParameters
			{
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuerSigningKey = true,
				ValidateIssuer = false,
				ValidateAudience = false
			}, out SecurityToken validatedToken);
			
			return validatedToken == null ? null : (JwtSecurityToken)validatedToken;
		}
	}
}
