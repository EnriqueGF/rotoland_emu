using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class PopularRoomsSearchMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Session.GetConnection().SendData(Phoenix.GetGame().GetNavigator().SerializeNavigator(Session, Event.PopFixedInt32()));
		}
	}
}
