using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix
{
	internal interface Interface
	{
		void Handle(GameClient Session, ClientMessage Event);
	}
}
