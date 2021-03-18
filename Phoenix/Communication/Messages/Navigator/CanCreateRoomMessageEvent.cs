using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Util;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class CanCreateRoomMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			ServerMessage Message = new ServerMessage(512u);
			if (Session.GetHabbo().list_6.Count > LicenseTools.Int32_4)
			{
				Message.AppendBoolean(true);
				Message.AppendInt32(LicenseTools.Int32_4);
			}
			else
			{
				Message.AppendBoolean(false);
			}
			Session.SendMessage(Message);
		}
	}
}
