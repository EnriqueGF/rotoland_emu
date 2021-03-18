using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Handshake
{
	internal sealed class InitCryptoMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Interface @interface;
			if (Phoenix.smethod_10().Handle(1817u, out @interface))
			{
				@interface.Handle(Session, null);
			}
		}
	}
}
