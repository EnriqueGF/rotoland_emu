using System;
using System.Collections.Generic;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class EditEventMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true) && @class.Event != null)
			{
				int int_ = Event.PopWiredInt32();
				string string_ = Phoenix.FilterString(Event.PopFixedString());
				string string_2 = Phoenix.FilterString(Event.PopFixedString());
				int num = Event.PopWiredInt32();
				@class.Event.Category = int_;
				@class.Event.Name = string_;
				@class.Event.Description = string_2;
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
