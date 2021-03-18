using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class CallForHelpMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			bool flag = false;
			if (Phoenix.GetGame().GetModerationTool().method_9(Session.GetHabbo().Id))
			{
				flag = true;
			}
			if (!flag)
			{
				string string_ = Phoenix.FilterString(Event.PopFixedString());
				Event.PopWiredInt32();
				int int_ = Event.PopWiredInt32();
				uint uint_ = Event.PopWiredUInt();
				Phoenix.GetGame().GetModerationTool().method_3(Session, int_, uint_, string_);
			}
			ServerMessage Message = new ServerMessage(321u);
			Message.AppendBoolean(flag);
			Session.SendMessage(Message);
		}
	}
}
