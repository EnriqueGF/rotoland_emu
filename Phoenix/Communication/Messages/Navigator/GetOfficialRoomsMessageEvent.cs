using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class GetOfficialRoomsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Session.SendMessage(Phoenix.GetGame().GetNavigator().method_5());
		}
	}
}
