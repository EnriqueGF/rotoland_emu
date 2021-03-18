using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Handshake
{
	internal sealed class UniqueIDMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
		}
	}
}
