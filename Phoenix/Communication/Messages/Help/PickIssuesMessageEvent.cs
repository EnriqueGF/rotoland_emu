using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class PickIssuesMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_supporttool"))
			{
				Event.PopWiredInt32();
				uint uint_ = Event.PopWiredUInt();
				Phoenix.GetGame().GetModerationTool().method_6(Session, uint_);
			}
		}
	}
}
