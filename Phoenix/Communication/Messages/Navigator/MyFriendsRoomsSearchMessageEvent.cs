using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class MyFriendsRoomsSearchMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Session.SendMessage(Phoenix.GetGame().GetNavigator().method_12(Session, -4));
		}
	}
}
