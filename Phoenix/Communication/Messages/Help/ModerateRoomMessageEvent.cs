using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class ModerateRoomMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_supporttool"))
			{
				uint uint_ = Event.PopWiredUInt();
				bool flag = Event.PopWiredBoolean();
				bool flag2 = Event.PopWiredBoolean();
				bool flag3 = Event.PopWiredBoolean();
				string text = "";
				if (flag)
				{
					text += "Apply Doorbell";
				}
				if (flag2)
				{
					text += " Change Name";
				}
				if (flag3)
				{
					text += " Kick Users";
				}
				Phoenix.GetGame().GetClientManager().method_31(Session, "ModTool - Room Action", text);
				Phoenix.GetGame().GetModerationTool().method_12(Session, uint_, flag3, flag, flag2);
			}
		}
	}
}
