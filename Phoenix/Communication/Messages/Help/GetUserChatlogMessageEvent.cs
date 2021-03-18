using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class GetUserChatlogMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_chatlogs"))
			{
				Session.SendMessage(Phoenix.GetGame().GetModerationTool().method_20(Event.PopWiredUInt()));
			}
		}
	}
}
