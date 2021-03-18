using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class MyRoomHistorySearchMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Session.SendMessage(Phoenix.GetGame().GetNavigator().method_7(Session));
		}
	}
}
