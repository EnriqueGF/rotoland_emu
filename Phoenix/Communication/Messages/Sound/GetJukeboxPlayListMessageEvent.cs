using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Sound
{
	internal sealed class GetJukeboxPlayListMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			ServerMessage Message = new ServerMessage(334u);
			Message.AppendInt32(20);
			Message.AppendInt32(16);
			for (int i = 1; i <= 16; i++)
			{
				Message.AppendInt32(i);
				Message.AppendInt32(i);
			}
			Session.SendMessage(Message);
		}
	}
}
