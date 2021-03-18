using System;
using System.Net.Sockets;
using Phoenix.Core;
using Phoenix.Util;
namespace Phoenix.Net
{
	internal sealed class AntiDDosSystem
	{
		private static string[] string_0;
		private static string string_1;
		internal static void smethod_0(int int_0)
		{
			AntiDDosSystem.string_0 = new string[int_0];
		}
		internal static bool smethod_1(Socket socket_0)
		{
			string text = socket_0.RemoteEndPoint.ToString().Split(new char[]
			{
				':'
			})[0];
			if (text == AntiDDosSystem.string_1)
			{
				return false;
			}
			else
			{
				if (AntiDDosSystem.smethod_2(text) > 10 && text != "127.0.0.1" && !LicenseTools.bool_0)
				{
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine(text + " was banned by Anti-DDoS system.");
					Console.ForegroundColor = ConsoleColor.White;
                    Logging.LogDDoS(text + " - " + DateTime.Now.ToString());
					AntiDDosSystem.string_1 = text;
					return false;
				}
				else
				{
					int num = AntiDDosSystem.smethod_4();
					if (num < 0)
					{
						return false;
					}
					else
					{
						AntiDDosSystem.string_0[num] = text;
						return true;
					}
				}
			}
		}
		private static int smethod_2(string string_2)
		{
			int num = 0;
			for (int i = 0; i < AntiDDosSystem.string_0.Length; i++)
			{
				if (AntiDDosSystem.string_0[i] == string_2)
				{
					num++;
				}
			}
			return num;
		}
		internal static void smethod_3(string string_2)
		{
			for (int i = 0; i < AntiDDosSystem.string_0.Length; i++)
			{
				if (AntiDDosSystem.string_0[i] == string_2)
				{
					AntiDDosSystem.string_0[i] = null;
					return;
				}
			}
		}
		private static int smethod_4()
		{
			for (int i = 0; i < AntiDDosSystem.string_0.Length; i++)
			{
				if (AntiDDosSystem.string_0[i] == null)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
