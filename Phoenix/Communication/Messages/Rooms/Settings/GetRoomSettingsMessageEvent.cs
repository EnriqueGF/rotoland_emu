using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Settings
{
	internal sealed class GetRoomSettingsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true))
			{
				ServerMessage Message = new ServerMessage(465u);
				Message.AppendUInt(@class.Id);
				Message.AppendStringWithBreak(@class.Name);
				Message.AppendStringWithBreak(@class.Description);
				Message.AppendInt32(@class.State);
				Message.AppendInt32(@class.Category);
				Message.AppendInt32(@class.UsersMax);
				Message.AppendInt32(100);
				Message.AppendInt32(@class.Tags.Count);
				foreach (string current in @class.Tags)
				{
					Message.AppendStringWithBreak(current);
				}
				Message.AppendInt32(@class.list_1.Count);
				foreach (uint current2 in @class.list_1)
				{
					Message.AppendUInt(current2);
					Message.AppendStringWithBreak(Phoenix.GetGame().GetClientManager().GetNameById(current2));
				}
				Message.AppendInt32(@class.list_1.Count);
				Message.AppendBoolean(@class.AllowPet);
				Message.AppendBoolean(@class.AllowPetsEating);
				Message.AppendBoolean(@class.AllowWalkthrough);
				Message.AppendBoolean(@class.Hidewall);
				Message.AppendInt32(@class.Wallthick);
				Message.AppendInt32(@class.Floorthick);
				Session.SendMessage(Message);
			}
		}
	}
}
