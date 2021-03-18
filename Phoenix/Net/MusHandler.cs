using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using System.Text;
using Phoenix.HabboHotel.Misc;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Users;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Net
{
	internal sealed class MusHandler
	{
		private Socket socket_0;
		private byte[] byte_0 = new byte[1024];
		public MusHandler(Socket socket_1)
		{
			this.socket_0 = socket_1;
			try
			{
				this.socket_0.BeginReceive(this.byte_0, 0, this.byte_0.Length, SocketFlags.None, new AsyncCallback(this.method_1), this.socket_0);
			}
			catch
			{
				this.method_0();
			}
		}
		public void method_0()
		{
			try
			{
				this.socket_0.Shutdown(SocketShutdown.Both);
				this.socket_0.Close();
				this.socket_0.Dispose();
			}
			catch
			{
			}
		}
		public void method_1(IAsyncResult iasyncResult_0)
		{
			try
			{
				int count = 0;
				try
				{
					count = this.socket_0.EndReceive(iasyncResult_0);
				}
				catch
				{
					this.method_0();
					return;
				}
				string @string = Encoding.Default.GetString(this.byte_0, 0, count);
				if (@string.Length > 0)
				{
					this.method_2(@string);
				}
			}
			catch
			{
			}
			this.method_0();
		}
		public void method_2(string string_0)
		{
			string text = string_0.Split(new char[]
			{
				Convert.ToChar(1)
			})[0];
			string text2 = string_0.Split(new char[]
			{
				Convert.ToChar(1)
			})[1];
			GameClient @class = null;
			DataRow dataRow = null;
			string text3 = text.ToLower();
			if (text3 != null)
			{
				if (MusCommands.dictionary_0 == null)
				{
					MusCommands.dictionary_0 = new Dictionary<string, int>(29)
					{

						{
							"update_items",
							0
						},

						{
							"update_catalogue",
							1
						},

						{
							"update_catalog",
							2
						},

						{
							"updateusersrooms",
							3
						},

						{
							"senduser",
							4
						},

						{
							"updatevip",
							5
						},

						{
							"giftitem",
							6
						},

						{
							"giveitem",
							7
						},

						{
							"unloadroom",
							8
						},

						{
							"roomalert",
							9
						},

						{
							"updategroup",
							10
						},

						{
							"updateusersgroups",
							11
						},

						{
							"shutdown",
							12
						},

						{
							"update_filter",
							13
						},

						{
							"refresh_filter",
							14
						},

						{
							"updatecredits",
							15
						},

						{
							"updatesettings",
							16
						},

						{
							"updatepixels",
							17
						},

						{
							"updatepoints",
							18
						},

						{
							"reloadbans",
							19
						},

						{
							"update_bots",
							20
						},

						{
							"signout",
							21
						},

						{
							"exe",
							22
						},

						{
							"alert",
							23
						},

						{
							"sa",
							24
						},

						{
							"ha",
							25
						},

						{
							"hal",
							26
						},

						{
							"updatemotto",
							27
						},

						{
							"updatelook",
							28
						}
					};
				}
				int num;
				if (MusCommands.dictionary_0.TryGetValue(text3, out num))
				{
					uint num2;
					uint uint_2;
					Room class4;
					uint num3;
					string text5;
					switch (num)
					{
					case 0:
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							Phoenix.GetGame().GetItemManager().method_0(class2);
							goto IL_C70;
						}
					case 1:
					case 2:
						break;
					case 3:
					{
						Habbo class3 = Phoenix.GetGame().GetClientManager().method_2(Convert.ToUInt32(text2)).GetHabbo();
						if (class3 != null)
						{
							using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
							{
								class3.method_1(class2);
								goto IL_C70;
							}
						}
						goto IL_C70;
					}
					case 4:
						goto IL_34E;
					case 5:
					{
						Habbo class3 = Phoenix.GetGame().GetClientManager().method_2(Convert.ToUInt32(text2)).GetHabbo();
						if (class3 != null)
						{
							class3.method_27();
							goto IL_C70;
						}
						goto IL_C70;
					}
					case 6:
					case 7:
					{
						num2 = uint.Parse(text2.Split(new char[]
						{
							' '
						})[0]);
						uint uint_ = uint.Parse(text2.Split(new char[]
						{
							' '
						})[1]);
						int int_ = int.Parse(text2.Split(new char[]
						{
							' '
						})[2]);
						string string_ = text2.Substring(num2.ToString().Length + uint_.ToString().Length + int_.ToString().Length + 3);
						Phoenix.GetGame().GetCatalog().method_7(string_, num2, uint_, int_);
						goto IL_C70;
					}
					case 8:
						uint_2 = uint.Parse(text2);
						class4 = Phoenix.GetGame().GetRoomManager().GetRoom(uint_2);
						Phoenix.GetGame().GetRoomManager().method_16(class4);
						goto IL_C70;
					case 9:
						num3 = uint.Parse(text2.Split(new char[]
						{
							' '
						})[0]);
						class4 = Phoenix.GetGame().GetRoomManager().GetRoom(num3);
						if (class4 != null)
						{
							string string_2 = text2.Substring(num3.ToString().Length + 1);
							for (int i = 0; i < class4.RoomUser_0.Length; i++)
							{
								RoomUser class5 = class4.RoomUser_0[i];
								if (class5 != null)
								{
									class5.GetClient().SendNotif(string_2);
								}
							}
							goto IL_C70;
						}
						goto IL_C70;
					case 10:
					{
						int int_2 = int.Parse(text2.Split(new char[]
						{
							' '
						})[0]);
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							Groups.smethod_3(class2, int_2);
							goto IL_C70;
						}
					}
					case 11:
						goto IL_5BF;
					case 12:
						goto IL_602;
					case 13:
					case 14:
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							ChatCommandHandler.InitWords(class2);
							goto IL_C70;
						}
					case 15:
						goto IL_633;
					case 16:
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							Phoenix.GetGame().method_17(class2);
							goto IL_C70;
						}
					case 17:
						goto IL_6F7;
					case 18:
						@class = Phoenix.GetGame().GetClientManager().method_2(uint.Parse(text2));
						if (@class != null)
						{
							@class.GetHabbo().method_14(true, false);
							goto IL_C70;
						}
						goto IL_C70;
					case 19:
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							Phoenix.GetGame().GetBanManager().method_0(class2);
						}
						Phoenix.GetGame().GetClientManager().method_28();
						goto IL_C70;
					case 20:
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							Phoenix.GetGame().GetBotManager().method_0(class2);
							goto IL_C70;
						}
					case 21:
						goto IL_839;
					case 22:
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							class2.ExecuteQuery(text2);
							goto IL_C70;
						}
					case 23:
						goto IL_880;
					case 24:
					{
						ServerMessage Message = new ServerMessage(134u);
						Message.AppendUInt(0u);
						Message.AppendString("PHX: " + text2);
						Phoenix.GetGame().GetClientManager().method_16(Message, Message);
						goto IL_C70;
					}
					case 25:
					{
						ServerMessage Message2 = new ServerMessage(808u);
						Message2.AppendStringWithBreak(PhoenixEnvironment.smethod_1("mus_ha_title"));
						Message2.AppendStringWithBreak(text2);
						ServerMessage Message3 = new ServerMessage(161u);
						Message3.AppendStringWithBreak(text2);
						Phoenix.GetGame().GetClientManager().method_15(Message2, Message3);
						goto IL_C70;
					}
					case 26:
					{
						string text4 = text2.Split(new char[]
						{
							' '
						})[0];
						text5 = text2.Substring(text4.Length + 1);
						ServerMessage Message4 = new ServerMessage(161u);
						Message4.AppendStringWithBreak(string.Concat(new string[]
						{
							PhoenixEnvironment.smethod_1("mus_hal_title"),
							"\r\n",
							text5,
							"\r\n-",
							PhoenixEnvironment.smethod_1("mus_hal_tail")
						}));
						Message4.AppendStringWithBreak(text4);
						Phoenix.GetGame().GetClientManager().method_14(Message4);
						goto IL_C70;
					}
					case 27:
					case 28:
					{
						uint_2 = uint.Parse(text2);
						@class = Phoenix.GetGame().GetClientManager().method_2(uint_2);
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							dataRow = class2.ReadDataRow("SELECT look,gender,motto,mutant_penalty,block_newfriends FROM users WHERE UserId = '" + @class.GetHabbo().Id + "' LIMIT 1");
						}
						@class.GetHabbo().Figure = (string)dataRow["look"];
						@class.GetHabbo().Gender = dataRow["gender"].ToString().ToLower();
						@class.GetHabbo().Motto = Phoenix.FilterString((string)dataRow["motto"]);
						@class.GetHabbo().BlockNewFriends = Phoenix.smethod_3(dataRow["block_newfriends"].ToString());
						ServerMessage Message5 = new ServerMessage(266u);
						Message5.AppendInt32(-1);
						Message5.AppendStringWithBreak(@class.GetHabbo().Figure);
						Message5.AppendStringWithBreak(@class.GetHabbo().Gender.ToLower());
						Message5.AppendStringWithBreak(@class.GetHabbo().Motto);
						@class.SendMessage(Message5);
						if (@class.GetHabbo().Boolean_0)
						{
							class4 = Phoenix.GetGame().GetRoomManager().GetRoom(@class.GetHabbo().CurrentRoomId);
							RoomUser class6 = class4.method_53(@class.GetHabbo().Id);
							ServerMessage Message6 = new ServerMessage(266u);
							Message6.AppendInt32(class6.VirtualId);
							Message6.AppendStringWithBreak(@class.GetHabbo().Figure);
							Message6.AppendStringWithBreak(@class.GetHabbo().Gender.ToLower());
							Message6.AppendStringWithBreak(@class.GetHabbo().Motto);
							Message6.AppendInt32(@class.GetHabbo().AchievementScore);
							Message6.AppendStringWithBreak("");
							class4.SendMessage(Message6, null);
						}
						text3 = text.ToLower();
						if (text3 == null)
						{
							goto IL_C70;
						}
						if (text3 == "updatemotto")
						{
							Phoenix.GetGame().GetAchievementManager().addAchievement(@class, 5u, 1);
							goto IL_C70;
						}
						if (text3 == "updatelook")
						{
							Phoenix.GetGame().GetAchievementManager().addAchievement(@class, 1u, 1);
							goto IL_C70;
						}
						goto IL_C70;
					}
					default:
						goto IL_C70;
					}
					using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
					{
						Phoenix.GetGame().GetCatalog().method_0(class2);
					}
					Phoenix.GetGame().GetCatalog().method_1();
					Phoenix.GetGame().GetClientManager().method_14(new ServerMessage(441u));
					goto IL_C70;
					IL_34E:
					num2 = uint.Parse(text2.Split(new char[]
					{
						' '
					})[0]);
					num3 = uint.Parse(text2.Split(new char[]
					{
						' '
					})[1]);
					GameClient class7 = Phoenix.GetGame().GetClientManager().method_2(num2);
					class4 = Phoenix.GetGame().GetRoomManager().GetRoom(num3);
					if (class7 != null)
					{
						ServerMessage Message7 = new ServerMessage(286u);
						Message7.AppendBoolean(class4.Boolean_3);
						Message7.AppendUInt(num3);
						class7.SendMessage(Message7);
						goto IL_C70;
					}
					goto IL_C70;
					IL_5BF:
					uint_2 = uint.Parse(text2);
					using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
					{
						Phoenix.GetGame().GetClientManager().method_2(uint_2).GetHabbo().method_0(class2);
						goto IL_C70;
					}
					IL_602:
					Phoenix.smethod_18();
					goto IL_C70;
					IL_633:
					@class = Phoenix.GetGame().GetClientManager().method_2(uint.Parse(text2));
					if (@class != null)
					{
						int int_3 = 0;
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							int_3 = (int)class2.ReadDataRow("SELECT credits FROM users WHERE UserId = '" + @class.GetHabbo().Id + "' LIMIT 1")[0];
						}
						@class.GetHabbo().Credits = int_3;
						@class.GetHabbo().method_13(false);
						goto IL_C70;
					}
					goto IL_C70;
					IL_6F7:
					@class = Phoenix.GetGame().GetClientManager().method_2(uint.Parse(text2));
					if (@class != null)
					{
						int int_4 = 0;
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							int_4 = (int)class2.ReadDataRow("SELECT activity_points FROM users WHERE UserId = '" + @class.GetHabbo().Id + "' LIMIT 1")[0];
						}
						@class.GetHabbo().ActivityPoints = int_4;
						@class.GetHabbo().method_15(false);
						goto IL_C70;
					}
					goto IL_C70;
					IL_839:
					Phoenix.GetGame().GetClientManager().method_2(uint.Parse(text2)).method_12();
					goto IL_C70;
					IL_880:
					string text6 = text2.Split(new char[]
					{
						' '
					})[0];
					text5 = text2.Substring(text6.Length + 1);
					ServerMessage Message8 = new ServerMessage(808u);
					Message8.AppendStringWithBreak(PhoenixEnvironment.smethod_1("mus_alert_title"));
					Message8.AppendStringWithBreak(text5);
					Phoenix.GetGame().GetClientManager().method_2(uint.Parse(text6)).SendMessage(Message8);
				}
			}
			IL_C70:
			ServerMessage Message9 = new ServerMessage(1u);
			Message9.AppendString("Hello Housekeeping, Love from Phoenix Emu");
			this.socket_0.Send(Message9.GetBytes());
		}
	}
}
