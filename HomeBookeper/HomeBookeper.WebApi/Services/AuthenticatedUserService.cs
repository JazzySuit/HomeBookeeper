using HomeBookeper.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeBookeper.WebApi.Services
{
	public class AuthenticatedUserService : IAuthenticatedUserService
	{
		public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
		{
			UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
		}

		public string UserId { get; }
	}
}
