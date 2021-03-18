using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class SearchFaqsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			string text = Phoenix.FilterString(Event.PopFixedString());
			if (text.Length >= 1)
			{
				Session.SendMessage(Phoenix.GetGame().GetHelpTool().method_10(text));
			}
		}
	}
}
