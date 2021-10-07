using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBookeper.Application.Interfaces
{
	public interface IAuthenticatedUserService
	{
		string UserId { get; }
	}
}
