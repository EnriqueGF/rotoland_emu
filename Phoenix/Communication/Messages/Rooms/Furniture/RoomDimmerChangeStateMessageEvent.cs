using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Items;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Furniture
{
	internal sealed class RoomDimmerChangeStateMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			try
			{
				Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
				if (@class != null && @class.method_27(Session, true) && @class.class67_0 != null)
				{
					RoomItem class2 = null;
					foreach (RoomItem class3 in @class.Hashtable_1.Values)
					{
						if (class3.GetBaseItem().InteractionType.ToLower() == "dimmer")
						{
							class2 = class3;
							break;
						}
					}
					if (class2 != null)
					{
						if (@class.class67_0.Enabled)
						{
							@class.class67_0.method_1();
						}
						else
						{
							@class.class67_0.method_0();
						}
						class2.ExtraData = @class.class67_0.method_7();
						class2.method_4();
					}
				}
			}
			catch
			{
			}
		}
	}
}
