using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Util;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class CreateFlatMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().list_6.Count <= LicenseTools.Int32_4)
			{
				string string_ = Phoenix.FilterString(Event.PopFixedString());
				string string_2 = Event.PopFixedString();
				Event.PopFixedString();
                RoomData @class = Phoenix.GetGame().GetRoomManager().method_20(Session, string_, string_2);
				if (@class != null)
				{
					ServerMessage Message = new ServerMessage(59u);
					Message.AppendUInt(@class.Id);
					Message.AppendStringWithBreak(@class.Name);
					Session.SendMessage(Message);
				}
			}
		}
	}
}
