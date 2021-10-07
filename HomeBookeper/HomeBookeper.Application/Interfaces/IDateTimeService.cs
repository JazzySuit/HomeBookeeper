using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBookeper.Application.Interfaces
{
	public interface IDateTimeService
	{
		DateTime NowUtc { get; }
	}
}
