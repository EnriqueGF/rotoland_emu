using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Support;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class GetFaqTextMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			uint uint_ = Event.PopWiredUInt();
			HelpTopic @class = Phoenix.GetGame().GetHelpTool().method_4(uint_);
			if (@class != null)
			{
				Session.SendMessage(Phoenix.GetGame().GetHelpTool().method_9(@class));
			}
		}
	}
}
