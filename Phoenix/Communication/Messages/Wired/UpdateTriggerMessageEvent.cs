using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Items;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Wired
{
	internal sealed class UpdateTriggerMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			RoomItem class2 = @class.method_28(Event.PopWiredUInt());
			if (@class != null && class2 != null)
			{
				string text = class2.GetBaseItem().InteractionType.ToLower();
				if (text != null)
				{
					if (!(text == "wf_trg_onsay"))
					{
						if (!(text == "wf_trg_enterroom"))
						{
							if (!(text == "wf_trg_timer"))
							{
								if (!(text == "wf_trg_attime"))
								{
									if (text == "wf_trg_atscore")
									{
										Event.PopWiredBoolean();
										string text2 = Event.ToString().Substring(Event.Length - (Event.RemainingLength - 2));
										string[] array = text2.Split(new char[]
										{
											'@'
										});
										class2.string_3 = array[0];
										class2.string_2 = Convert.ToString(Event.PopWiredInt32());
									}
								}
								else
								{
									Event.PopWiredBoolean();
									string text2 = Event.ToString().Substring(Event.Length - (Event.RemainingLength - 2));
									string[] array = text2.Split(new char[]
									{
										'@'
									});
									class2.string_3 = array[0];
									class2.string_2 = Convert.ToString(Convert.ToString((double)Event.PopWiredInt32() * 0.5));
								}
							}
							else
							{
								Event.PopWiredBoolean();
								string text2 = Event.ToString().Substring(Event.Length - (Event.RemainingLength - 2));
								string[] array = text2.Split(new char[]
								{
									'@'
								});
								class2.string_3 = array[0];
								class2.string_2 = Convert.ToString(Convert.ToString((double)Event.PopWiredInt32() * 0.5));
							}
						}
						else
						{
							Event.PopWiredBoolean();
							string text3 = Event.PopFixedString();
							class2.string_2 = text3;
						}
					}
					else
					{
						Event.PopWiredBoolean();
						bool value = Event.PopWiredBoolean();
						string text3 = Event.PopFixedString();
						text3 = Phoenix.DoFilter(text3, false, true);
						if (text3.Length > 100)
						{
							text3 = text3.Substring(0, 100);
						}
						class2.string_2 = text3;
						class2.string_3 = Convert.ToString(value);
					}
				}
				class2.UpdateState(true, false);
			}
		}
	}
}
