using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class GetModeratorUserInfoMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_supporttool"))
			{
				uint uint_ = Event.PopWiredUInt();
				if (Phoenix.GetGame().GetClientManager().GetNameById(uint_) != "Unknown User")
				{
					Session.SendMessage(Phoenix.GetGame().GetModerationTool().method_18(uint_));
				}
				else
				{
					Session.SendNotif("Could not load user info, invalid user.");
				}
			}
		}
	}
}
