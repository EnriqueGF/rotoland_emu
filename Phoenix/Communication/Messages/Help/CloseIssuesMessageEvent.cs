using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class CloseIssuesMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_supporttool"))
			{
				int int_ = Event.PopWiredInt32();
				Event.PopWiredInt32();
				uint uint_ = Event.PopWiredUInt();
				Phoenix.GetGame().GetModerationTool().method_8(Session, uint_, int_);
			}
		}
	}
}
