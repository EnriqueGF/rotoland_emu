using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Inventory.Furni
{
	internal sealed class GetPetInventoryEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().method_23() != null)
			{
				Session.SendMessage(Session.GetHabbo().method_23().method_15());
			}
		}
	}
}
