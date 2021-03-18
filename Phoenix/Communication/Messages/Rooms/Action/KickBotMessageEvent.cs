using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Action
{
	internal sealed class KickBotMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true))
			{
				RoomUser class2 = @class.method_52(Event.PopWiredInt32());
				if (class2 != null && class2.Boolean_4)
				{
					@class.method_6(class2.VirtualId, true);
				}
			}
		}
	}
}
