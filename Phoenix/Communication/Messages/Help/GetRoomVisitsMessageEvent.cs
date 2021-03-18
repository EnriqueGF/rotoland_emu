using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class GetRoomVisitsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_supporttool"))
			{
				uint uint_ = Event.PopWiredUInt();
				Session.SendMessage(Phoenix.GetGame().GetModerationTool().method_19(uint_));
			}
		}
	}
}
