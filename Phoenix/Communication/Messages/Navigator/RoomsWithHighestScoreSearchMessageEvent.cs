using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class RoomsWithHighestScoreSearchMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Session.GetConnection().SendData(Phoenix.GetGame().GetNavigator().SerializeNavigator(Session, -2));
		}
	}
}
