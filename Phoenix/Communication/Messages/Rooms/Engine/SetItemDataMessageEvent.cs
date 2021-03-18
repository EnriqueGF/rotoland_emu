using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Items;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Engine
{
	internal sealed class SetItemDataMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null)
			{
				RoomItem class2 = @class.method_28(Event.PopWiredUInt());
				if (class2 != null && !(class2.GetBaseItem().InteractionType.ToLower() != "postit"))
				{
					string text = Event.PopFixedString();
					string text2 = text.Split(new char[]
					{
						' '
					})[0];
					string str = Phoenix.DoFilter(text.Substring(text2.Length + 1), true, true);
					if (@class.method_26(Session) || text.StartsWith(class2.ExtraData))
					{
						string text3 = text2;
						if (text3 != null && (text3 == "FFFF33" || text3 == "FF9CFF" || text3 == "9CCEFF" || text3 == "9CFF9C"))
						{
							class2.ExtraData = text2 + " " + str;
							class2.UpdateState(true, true);
						}
					}
				}
			}
		}
	}
}
