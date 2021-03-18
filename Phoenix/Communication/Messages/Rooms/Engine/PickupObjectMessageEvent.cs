using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Items;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Engine
{
	internal sealed class PickupObjectMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Event.PopWiredInt32();
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true))
			{
				RoomItem class2 = @class.method_28(Event.PopWiredUInt());
				if (class2 != null)
				{
					string text = class2.GetBaseItem().InteractionType.ToLower();
					if (text == null || !(text == "postit"))
					{
						@class.method_29(Session, class2.uint_0, false, true);
						Session.GetHabbo().method_23().method_11(class2.uint_0, class2.uint_2, class2.ExtraData, false);
						Session.GetHabbo().method_23().method_9(true);
						if (Session.GetHabbo().CurrentQuestId == 10u)
						{
							Phoenix.GetGame().GetQuestManager().method_1(10u, Session);
						}
					}
				}
			}
		}
	}
}
