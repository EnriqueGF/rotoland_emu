using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Inventory.Purse
{
	internal sealed class GetCreditsInfoEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Session.GetHabbo().method_13(false);
			Session.GetHabbo().method_15(false);
		}
	}
}
