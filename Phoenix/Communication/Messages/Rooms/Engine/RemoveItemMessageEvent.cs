using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Items;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Engine
{
	internal sealed class RemoveItemMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true))
			{
				RoomItem class2 = @class.method_28(Event.PopWiredUInt());
				if (class2 != null && !(class2.GetBaseItem().InteractionType.ToLower() != "postit"))
				{
					@class.method_29(Session, class2.uint_0, true, true);
				}
			}
		}
	}
}
