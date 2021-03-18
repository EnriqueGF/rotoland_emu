using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class ModMessageMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_supporttool"))
			{
				uint num = Event.PopWiredUInt();
				string text = Event.PopFixedString();
				string string_ = string.Concat(new object[]
				{
					"User: ",
					num,
					", Message: ",
					text
				});
				Phoenix.GetGame().GetClientManager().method_31(Session, "ModTool - Alert User", string_);
				Phoenix.GetGame().GetModerationTool().method_16(Session, num, text, false);
			}
		}
	}
}
