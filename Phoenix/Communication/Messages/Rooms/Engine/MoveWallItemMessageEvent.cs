using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Items;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Engine
{
	internal sealed class MoveWallItemMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_26(Session))
			{
				RoomItem class2 = @class.method_28(Event.PopWiredUInt());
				if (class2 != null)
				{
					string string_ = Event.PopFixedString();
					@class.method_82(Session, class2, false, string_);
				}
			}
		}
	}
}
