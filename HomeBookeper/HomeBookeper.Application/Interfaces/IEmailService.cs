﻿using HomeBookeper.Application.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookeper.Application.Interfaces
{
	public interface IEmailService
	{
		Task SendAsync(EmailRequest request);
	}
}
