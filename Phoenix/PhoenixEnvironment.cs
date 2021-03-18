using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Phoenix.Core;
using Phoenix.Storage;
namespace Phoenix
{
	internal sealed class PhoenixEnvironment
	{
		private static Dictionary<string, string> dictionary_0;
		public PhoenixEnvironment()
		{
			PhoenixEnvironment.dictionary_0 = new Dictionary<string, string>();
		}
		public static void smethod_0(DatabaseClient class6_0)
		{
            Logging.smethod_0("Loading external texts...");
			PhoenixEnvironment.smethod_2();
			DataTable dataTable = class6_0.ReadDataTable("SELECT identifier, display_text FROM texts ORDER BY identifier ASC;");
			if (dataTable != null)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					PhoenixEnvironment.dictionary_0.Add(dataRow["identifier"].ToString(), dataRow["display_text"].ToString());
				}
			}
			Logging.WriteLine("completed!");
		}
		public static string smethod_1(string string_0)
		{
			string result;
			if (PhoenixEnvironment.dictionary_0 != null && PhoenixEnvironment.dictionary_0.Count > 0)
			{
				result = PhoenixEnvironment.dictionary_0[string_0];
			}
			else
			{
				result = string_0;
			}
			return result;
		}
		public static void smethod_2()
		{
			PhoenixEnvironment.dictionary_0.Clear();
		}
	}
}