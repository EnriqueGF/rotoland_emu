using System;
using System.Data;
using Phoenix.HabboHotel.Users.UserDataManagement;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Users;
namespace Phoenix.HabboHotel.Users.Authenticator
{
	internal sealed class Authenticator
	{
		internal static Habbo smethod_0(string string_0, GameClient Session, UserDataFactory class12_0, UserDataFactory class12_1)
		{
			return Authenticator.smethod_1(class12_0.DataRow_0, string_0, Session, class12_1);
		}
		private static Habbo smethod_1(DataRow habboData, string SSOTicket, GameClient Session, UserDataFactory class12_0)
		{
			uint Id = (uint)habboData["Id"];
			string Username = (string)habboData["username"];
			string Name = (string)habboData["real_name"];
			uint Rank = (uint)habboData["rank"];
			string Motto = (string)habboData["motto"];
			string ip_last = (string)habboData["ip_last"];
			string look = (string)habboData["look"];
			string gender = (string)habboData["gender"];
			int credits = (int)habboData["credits"];
			int pixels = (int)habboData["activity_points"];
			double activity_points_lastupdate = (double)habboData["activity_points_lastupdate"];
			return new Habbo(Id, Username, Name, SSOTicket, Rank, Motto, look, gender, credits, pixels, activity_points_lastupdate, Phoenix.smethod_3(habboData["is_muted"].ToString()), (uint)habboData["home_room"], (int)habboData["newbie_status"], Phoenix.smethod_3(habboData["block_newfriends"].ToString()), Phoenix.smethod_3(habboData["hide_inroom"].ToString()), Phoenix.smethod_3(habboData["hide_online"].ToString()), Phoenix.smethod_3(habboData["vip"].ToString()), (int)habboData["volume"], (int)habboData["vip_points"], Phoenix.smethod_3(habboData["accept_trading"].ToString()), ip_last, Session, class12_0);
		}
		internal static Habbo smethod_2(string string_0)
		{
			UserDataFactory @class = new UserDataFactory(string_0, false);
			return Authenticator.smethod_1(@class.DataRow_0, "", null, @class);
		}
	}
}
