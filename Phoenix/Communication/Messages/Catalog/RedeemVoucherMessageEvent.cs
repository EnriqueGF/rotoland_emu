using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Catalog
{
	internal sealed class RedeemVoucherMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Phoenix.GetGame().GetCatalog().method_21().method_2(Session, Event.PopFixedString());
		}
	}
}
