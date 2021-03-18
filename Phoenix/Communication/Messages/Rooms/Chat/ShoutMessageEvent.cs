using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Chat
{
	internal sealed class ShoutMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null)
			{
				RoomUser class2 = @class.method_53(Session.GetHabbo().Id);
				if (class2 != null)
				{
					class2.method_1(Session, Phoenix.FilterString(Event.PopFixedString()), true);
				}
			}
		}
	}
}
