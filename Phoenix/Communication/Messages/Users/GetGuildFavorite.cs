using System;
using System.Data;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Users
{
	internal sealed class GetGuildFavorite : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			int num = Event.PopWiredInt32();
			if (num > 0 && (Session != null && Session.GetHabbo() != null))
			{
				Session.GetHabbo().int_0 = num;
				using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
				{
					@class.ExecuteQuery(string.Concat(new object[]
					{
						"UPDATE user_stats SET groupid = ",
						num,
						" WHERE Id = ",
						Session.GetHabbo().Id,
						" LIMIT 1;"
					}));
				}
				DataTable dataTable_ = Session.GetHabbo().dataTable_0;
				if (dataTable_ != null)
				{
					ServerMessage Message = new ServerMessage(915u);
					Message.AppendInt32(dataTable_.Rows.Count);
					foreach (DataRow dataRow in dataTable_.Rows)
					{
						GroupsManager class2 = Groups.smethod_2((int)dataRow["groupid"]);
						Message.AppendInt32(class2.int_0);
						Message.AppendStringWithBreak(class2.string_0);
						Message.AppendStringWithBreak(class2.string_2);
						if (Session.GetHabbo().int_0 == class2.int_0)
						{
							Message.AppendBoolean(true);
						}
						else
						{
							Message.AppendBoolean(false);
						}
					}
					Session.SendMessage(Message);
					if (Session.GetHabbo().Boolean_0)
					{
						Room class14_ = Session.GetHabbo().Class14_0;
						RoomUser class3 = class14_.method_53(Session.GetHabbo().Id);
						ServerMessage Message2 = new ServerMessage(28u);
						Message2.AppendInt32(1);
						class3.method_14(Message2);
						class14_.SendMessage(Message2, null);
						GroupsManager class4 = Groups.smethod_2(Session.GetHabbo().int_0);
						if (!class14_.list_17.Contains(class4))
						{
							class14_.list_17.Add(class4);
							ServerMessage Message3 = new ServerMessage(309u);
							Message3.AppendInt32(class14_.list_17.Count);
							foreach (GroupsManager class2 in class14_.list_17)
							{
								Message3.AppendInt32(class2.int_0);
								Message3.AppendStringWithBreak(class2.string_2);
							}
							class14_.SendMessage(Message3, null);
						}
						else
						{
							foreach (GroupsManager current in class14_.list_17)
							{
								if (current == class4 && current.string_2 != class4.string_2)
								{
									ServerMessage Message3 = new ServerMessage(309u);
									Message3.AppendInt32(class14_.list_17.Count);
									foreach (GroupsManager class2 in class14_.list_17)
									{
										Message3.AppendInt32(class2.int_0);
										Message3.AppendStringWithBreak(class2.string_2);
									}
									class14_.SendMessage(Message3, null);
								}
							}
						}
					}
				}
			}
		}
	}
}
