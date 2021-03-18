using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Catalog
{
	internal sealed class GetClubOffersMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
		}
	}
}
