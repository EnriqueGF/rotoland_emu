using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Inventory.Badges
{
	internal sealed class GetBadgesEvent : Interface
	{
        public void Handle(GameClient Session, ClientMessage Event)
        {
            Session.SendMessage(Session.GetHabbo().method_22().method_7());
        }
	}
}
