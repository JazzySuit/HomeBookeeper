﻿using HomeBookeper.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace HomeBookeper.Infrastructure.Shared.Services
{
	public class DateTimeService : IDateTimeService
	{
		public DateTime NowUtc => DateTime.UtcNow;
	}
}
