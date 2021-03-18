using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Messenger
{
	internal sealed class GetBuddyRequestsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
		}
	}
}
