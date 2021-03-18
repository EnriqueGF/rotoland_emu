using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Support;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class GetFaqCategoryMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			uint uint_ = Event.PopWiredUInt();
			HelpCategory @class = Phoenix.GetGame().GetHelpTool().method_1(uint_);
			if (@class != null)
			{
				Session.SendMessage(Phoenix.GetGame().GetHelpTool().method_11(@class));
			}
		}
	}
}
