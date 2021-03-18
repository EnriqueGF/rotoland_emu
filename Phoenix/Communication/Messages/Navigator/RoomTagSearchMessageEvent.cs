using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class RoomTagSearchMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Event.PopWiredInt32();
			Session.SendMessage(Phoenix.GetGame().GetNavigator().method_10(Event.PopFixedString()));
		}
	}
}
