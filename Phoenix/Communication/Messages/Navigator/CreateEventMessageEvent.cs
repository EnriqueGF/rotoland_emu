using System;
using System.Collections.Generic;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class CreateEventMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true) && @class.Event == null && @class.State == 0)
			{
				int int_ = Event.PopWiredInt32();
				string text = Phoenix.FilterString(Event.PopFixedString());
				string string_ = Phoenix.FilterString(Event.PopFixedString());
				int num = Event.PopWiredInt32();
				if (text.Length >= 1)
				{
					@class.Event = new RoomEvent(@class.Id, text, string_, int_, null);
					@class.Event.Tags = new List<string>();
					for (int i = 0; i < num; i++)
					{
						@class.Event.Tags.Add(Phoenix.FilterString(Event.PopFixedString()));
					}
					@class.SendMessage(@class.Event.Serialize(Session), null);
				}
			}
		}
	}
}
