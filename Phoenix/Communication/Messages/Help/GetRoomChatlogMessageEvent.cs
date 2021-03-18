using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class GetRoomChatlogMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_chatlogs"))
			{
				Event.PopWiredInt32();
				uint uint_ = Event.PopWiredUInt();
				if (Phoenix.GetGame().GetRoomManager().GetRoom(uint_) != null)
				{
					Session.SendMessage(Phoenix.GetGame().GetModerationTool().method_22(uint_));
				}
			}
		}
	}
}
