using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Items;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Wired
{
	internal sealed class UpdateConditionMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			try
			{
				Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
				uint uint_ = Event.PopWiredUInt();
				RoomItem class2 = @class.method_28(uint_);
				string text = class2.GetBaseItem().InteractionType.ToLower();
				if (text != null && (text == "wf_cnd_trggrer_on_frn" || text == "wf_cnd_furnis_hv_avtrs" || text == "wf_cnd_has_furni_on"))
				{
					Event.PopWiredBoolean();
					Event.PopFixedString();
					class2.string_2 = Event.ToString().Substring(Event.Length - (Event.RemainingLength - 2));
					class2.string_2 = class2.string_2.Substring(0, class2.string_2.Length - 1);
					Event.ResetPointer();
					class2 = @class.method_28(Event.PopWiredUInt());
					Event.PopWiredBoolean();
					Event.PopFixedString();
					int num = Event.PopWiredInt32();
					class2.string_3 = "";
					for (int i = 0; i < num; i++)
					{
						class2.string_3 = class2.string_3 + "," + Convert.ToString(Event.PopWiredUInt());
					}
					if (class2.string_3.Length > 0)
					{
						class2.string_3 = class2.string_3.Substring(1);
					}
				}
			}
			catch
			{
			}
		}
	}
}
