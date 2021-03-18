using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Sound
{
	internal sealed class GetNowPlayingMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			ServerMessage Message = new ServerMessage(327u);
			Message.AppendInt32(3);
			Message.AppendInt32(6);
			Message.AppendInt32(3);
			Message.AppendInt32(0);
			if (Session.GetHabbo().Class14_0 != null)
			{
				Message.AppendInt32(Session.GetHabbo().Class14_0.int_13);
			}
			else
			{
				Message.AppendInt32(0);
			}
			Session.SendMessage(Message);
		}
	}
}
