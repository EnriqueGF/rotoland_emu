using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Util;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Marketplace
{
	internal sealed class GetMarketplaceConfigurationMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = now - Phoenix.ServerStarted;
			ServerMessage Message = new ServerMessage(612u);
			Message.AppendBoolean(true);
			Message.AppendInt32(LicenseTools.int_1);
			Message.AppendInt32(1);
			Message.AppendInt32(5);
			Message.AppendInt32(1);
			Message.AppendInt32(LicenseTools.int_0);
			Message.AppendInt32(48);
			Message.AppendInt32(timeSpan.Days);
			Session.SendMessage(Message);
		}
	}
}
