using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Handshake
{
	internal sealed class DisconnectMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
		}
	}
}
