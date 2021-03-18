using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class CancelEventMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true) && @class.Event != null)
			{
				@class.Event = null;
				ServerMessage Message = new ServerMessage(370u);
				Message.AppendStringWithBreak("-1");
				@class.SendMessage(Message, null);
			}
		}
	}
}
