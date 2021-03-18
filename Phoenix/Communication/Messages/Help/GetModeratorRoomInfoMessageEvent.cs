using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class GetModeratorRoomInfoMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_supporttool"))
			{
				uint uint_ = Event.PopWiredUInt();
                RoomData class27_ = Phoenix.GetGame().GetRoomManager().method_11(uint_);
				Session.SendMessage(Phoenix.GetGame().GetModerationTool().method_14(class27_));
			}
		}
	}
}
