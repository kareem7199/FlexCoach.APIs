﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoach.Core.Entities
{
	public enum Gender
	{
		[EnumMember(Value = "Male")]
		MALE,
		[EnumMember(Value = "Female")]
		FEMALE
	}
}
