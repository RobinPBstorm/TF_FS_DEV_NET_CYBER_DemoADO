using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoADO.Models;

namespace DemoADO
{
    public static class Mapper
	{
		public static Trainer TrainerMapper(IDataRecord record)
		{
			return new Trainer((int)record["Id"], 
				(string)record["FirstName"], 
				(string)record["LastName"],
				(record["BirthDate"] == DBNull.Value) ? null : (DateTime)record["BirthDate"], 
				(bool)record["IsActive"]);
		}
	}
}
