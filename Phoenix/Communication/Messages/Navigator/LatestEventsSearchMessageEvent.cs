using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class LatestEventsSearchMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			int int_ = int.Parse(Event.PopFixedString());
			Session.SendMessage(Phoenix.GetGame().GetNavigator().method_8(Session, int_));
		}
	}
}
