using System;
using Phoenix.Core;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Rooms.Session
{
	internal sealed class OpenConnectionMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Event.PopWiredInt32();
			uint num = Event.PopWiredUInt();
			Event.PopWiredInt32();
			if (Phoenix.GetConfig().data["emu.messages.roommgr"] == "1")
			{
				Logging.WriteLine("[RoomMgr] Requesting Public Room [ID: " + num + "]");
			}
            RoomData @class = Phoenix.GetGame().GetRoomManager().method_12(num);
			if (@class != null && !(@class.Type != "public"))
			{
				Session.method_1().method_5(num, "");
			}
		}
	}
}
