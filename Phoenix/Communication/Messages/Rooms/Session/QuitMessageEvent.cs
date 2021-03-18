using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Rooms.Session
{
	internal sealed class QuitMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			try
			{
				if (Session != null && Session.GetHabbo() != null && Session.GetHabbo().Boolean_0)
				{
					Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId).method_47(Session, true, false);
				}
			}
			catch
			{
			}
		}
	}
}
