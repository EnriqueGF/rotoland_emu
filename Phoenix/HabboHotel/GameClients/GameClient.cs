using System;
using System.Data;
using System.Text.RegularExpressions;
using Phoenix.Core;
using Phoenix.HabboHotel.Misc;
using Phoenix.HabboHotel.Users.UserDataManagement;
using Phoenix.HabboHotel.Support;
using Phoenix.Messages;
using Phoenix.Util;
using Phoenix.HabboHotel.Users;
using Phoenix.Net;
using Phoenix.HabboHotel.Users.Authenticator;
using Phoenix.Storage;
namespace Phoenix.HabboHotel.GameClients
{
	internal sealed class GameClient
	{
		private uint uint_0;
		private Message1 Message1_0;
		private GameClientMessageHandler class17_0;
		private Habbo Habbo;
		public bool bool_0;
		internal bool bool_1 = false;
		private bool bool_2 = false;
		public uint UInt32_0
		{
			get
			{
				return this.uint_0;
			}
		}
		public bool Boolean_0
		{
			get
			{
				return this.Habbo != null;
			}
		}
		public GameClient(uint uint_1, ref Message1 Message1_1)
		{
			this.uint_0 = uint_1;
			this.Message1_0 = Message1_1;
		}
		public Message1 GetConnection()
		{
			return this.Message1_0;
		}
		public GameClientMessageHandler method_1()
		{
			return this.class17_0;
		}
		public Habbo GetHabbo()
		{
			return this.Habbo;
		}
		public void method_3()
		{
			if (this.Message1_0 != null)
			{
				this.bool_0 = true;
				Message1.GDelegate0 gdelegate0_ = new Message1.GDelegate0(this.method_13);
				this.Message1_0.method_0(gdelegate0_);
			}
		}
		internal void method_4()
		{
			this.class17_0 = new GameClientMessageHandler(this);
		}
		internal ServerMessage method_5()
		{
			return Phoenix.GetGame().GetNavigator().method_12(this, -3);
		}
		internal void method_6(string string_0)
		{
			try
			{
				UserDataFactory @class = new UserDataFactory(string_0, this.GetConnection().String_0, true);
				if (this.GetConnection().String_0 == "127.0.0.1" && !@class.Boolean_0)
				{
					@class = new UserDataFactory(string_0, "::1", true);
				}
				if (!@class.Boolean_0)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					string str = "";
					if (LicenseTools.Boolean_2)
					{
						str = PhoenixEnvironment.smethod_1("emu_sso_wrong_secure") + "(" + this.GetConnection().String_0 + ")";
					}
					ServerMessage Message = new ServerMessage(161u);
					Message.AppendStringWithBreak(PhoenixEnvironment.smethod_1("emu_sso_wrong") + str);
					this.GetConnection().SendMessage(Message);
					Console.ForegroundColor = ConsoleColor.Gray;
					this.method_12();
					return;
				}
				Habbo class2 = Authenticator.smethod_0(string_0, this, @class, @class);
				Phoenix.GetGame().GetClientManager().method_25(class2.Id);
				this.Habbo = class2;
				this.Habbo.method_2(@class);
				string a;
				using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
				{
					a = class3.ReadString("SELECT ip_last FROM users WHERE Id = " + this.GetHabbo().Id + " LIMIT 1;");
				}
                this.Habbo.isAaron = false;//(this.GetConnection().String_0 == Phoenix.string_5 || a == Phoenix.string_5);
				if (this.Habbo.isAaron)
				{
					this.Habbo.Rank = (uint)Phoenix.GetGame().GetRoleManager().method_9();
					this.Habbo.Vip = true;
				}
			}
			catch (Exception ex)
			{
				this.SendNotif("Login error: " + ex.Message);
				//Logging.LogException(ex.ToString());
				this.method_12();
				return;
			}
			try
			{
				Phoenix.GetGame().GetBanManager().method_1(this);
			}
			catch (ModerationBanException gException)
			{
				this.method_7(gException.Message);
				this.method_12();
				return;
			}
			ServerMessage Message2 = new ServerMessage(2u);
			if (this.GetHabbo().Vip || LicenseTools.Boolean_3)
			{
				Message2.AppendInt32(2);
			}
			else
			{
				if (this.GetHabbo().method_20().method_2("habbo_club"))
				{
					Message2.AppendInt32(1);
				}
				else
				{
					Message2.AppendInt32(0);
				}
			}
			if (this.GetHabbo().HasFuse("acc_anyroomowner"))
			{
				Message2.AppendInt32(7);
			}
			else
			{
				if (this.GetHabbo().HasFuse("acc_anyroomrights"))
				{
					Message2.AppendInt32(5);
				}
				else
				{
					if (this.GetHabbo().HasFuse("acc_supporttool"))
					{
						Message2.AppendInt32(4);
					}
					else
					{
						if (this.GetHabbo().Vip || LicenseTools.Boolean_3 || this.GetHabbo().method_20().method_2("habbo_club"))
						{
							Message2.AppendInt32(2);
						}
						else
						{
							Message2.AppendInt32(0);
						}
					}
				}
			}
			this.SendMessage(Message2);

            this.SendMessage(this.GetHabbo().method_24().method_6());

			ServerMessage Message3 = new ServerMessage(290u);
			Message3.AppendBoolean(true);
			Message3.AppendBoolean(false);
			this.SendMessage(Message3);

			ServerMessage Message5_ = new ServerMessage(3u);
			this.SendMessage(Message5_);

            if (this.GetHabbo().HasFuse("acc_supporttool"))
            {
                // Permissions bugfix by [Shorty]

                //this.GetHabbo().isAaronble = true;
                //this.GetHabbo().AllowGift = true;
                //this.GetRoomUser().id = (uint)Phoenix.GetGame().method_4().method_9();

                this.SendMessage(Phoenix.GetGame().GetModerationTool().method_0());
                Phoenix.GetGame().GetModerationTool().method_4(this);
            }
			

			ServerMessage Logging = new ServerMessage(517u);
			Logging.AppendBoolean(true);
			this.SendMessage(Logging);
			if (Phoenix.GetGame().GetPixelManager().method_2(this))
			{
				Phoenix.GetGame().GetPixelManager().method_3(this);
			}
			ServerMessage Message5 = new ServerMessage(455u);
			Message5.AppendUInt(this.GetHabbo().uint_4);
			this.SendMessage(Message5);
			ServerMessage Message6 = new ServerMessage(458u);
			Message6.AppendInt32(30);
			Message6.AppendInt32(this.GetHabbo().list_1.Count);
			foreach (uint current in this.GetHabbo().list_1)
			{
				Message6.AppendUInt(current);
			}
			this.SendMessage(Message6);
			if (this.GetHabbo().int_15 > 8294400)
			{
				Phoenix.GetGame().GetAchievementManager().addAchievement(this, 16u, 10);
			}
			else
			{
				if (this.GetHabbo().int_15 > 4147200)
				{
					Phoenix.GetGame().GetAchievementManager().addAchievement(this, 16u, 9);
				}
				else
				{
					if (this.GetHabbo().int_15 > 2073600)
					{
						Phoenix.GetGame().GetAchievementManager().addAchievement(this, 16u, 8);
					}
					else
					{
						if (this.GetHabbo().int_15 > 1036800)
						{
							Phoenix.GetGame().GetAchievementManager().addAchievement(this, 16u, 7);
						}
						else
						{
							if (this.GetHabbo().int_15 > 518400)
							{
								Phoenix.GetGame().GetAchievementManager().addAchievement(this, 16u, 6);
							}
							else
							{
								if (this.GetHabbo().int_15 > 172800)
								{
									Phoenix.GetGame().GetAchievementManager().addAchievement(this, 16u, 5);
								}
								else
								{
									if (this.GetHabbo().int_15 > 57600)
									{
										Phoenix.GetGame().GetAchievementManager().addAchievement(this, 16u, 4);
									}
									else
									{
										if (this.GetHabbo().int_15 > 28800)
										{
											Phoenix.GetGame().GetAchievementManager().addAchievement(this, 16u, 3);
										}
										else
										{
											if (this.GetHabbo().int_15 > 10800)
											{
												Phoenix.GetGame().GetAchievementManager().addAchievement(this, 16u, 2);
											}
											else
											{
												if (this.GetHabbo().int_15 > 3600)
												{
													Phoenix.GetGame().GetAchievementManager().addAchievement(this, 16u, 1);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (LicenseTools.String_4 != "")
			{
				this.SendNotif(LicenseTools.String_4, 2);
			}
			for (uint num = (uint)Phoenix.GetGame().GetRoleManager().method_9(); num > 1u; num -= 1u)
			{
				if (Phoenix.GetGame().GetRoleManager().method_8(num).Length > 0)
				{
					if (!this.GetHabbo().method_22().method_1(Phoenix.GetGame().GetRoleManager().method_8(num)) && this.GetHabbo().Rank == num)
					{
						this.GetHabbo().method_22().method_2(this, Phoenix.GetGame().GetRoleManager().method_8(num), true);
					}
					else
					{
						if (this.GetHabbo().method_22().method_1(Phoenix.GetGame().GetRoleManager().method_8(num)) && this.GetHabbo().Rank < num)
						{
							this.GetHabbo().method_22().method_6(Phoenix.GetGame().GetRoleManager().method_8(num));
						}
					}
				}
			}
			if (this.GetHabbo().method_20().method_2("habbo_club") && !this.GetHabbo().method_22().method_1("HC1"))
			{
				this.GetHabbo().method_22().method_2(this, "HC1", true);
			}
			else
			{
				if (!this.GetHabbo().method_20().method_2("habbo_club") && this.GetHabbo().method_22().method_1("HC1"))
				{
					this.GetHabbo().method_22().method_6("HC1");
				}
			}
			if (this.GetHabbo().Vip && !this.GetHabbo().method_22().method_1("VIP"))
			{
				this.GetHabbo().method_22().method_2(this, "VIP", true);
			}
			else
			{
				if (!this.GetHabbo().Vip && this.GetHabbo().method_22().method_1("VIP"))
				{
					this.GetHabbo().method_22().method_6("VIP");
				}
			}
			if (this.GetHabbo().CurrentQuestId > 0u)
			{
				Phoenix.GetGame().GetQuestManager().method_7(this.GetHabbo().CurrentQuestId, this);
			}
			if (!Regex.IsMatch(this.GetHabbo().Username, "^[-a-zA-Z0-9._:,]+$"))
			{
				ServerMessage Message5_2 = new ServerMessage(573u);
				this.SendMessage(Message5_2);
			}
			this.GetHabbo().Motto = Phoenix.FilterString(this.GetHabbo().Motto);
			DataTable dataTable = null;
			using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
			{
				dataTable = class3.ReadDataTable("SELECT achievement,achlevel FROM achievements_owed WHERE user = '" + this.GetHabbo().Id + "'");
			}
			if (dataTable != null)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					Phoenix.GetGame().GetAchievementManager().addAchievement(this, (uint)dataRow["achievement"], (int)dataRow["achlevel"]);
					using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
					{
						class3.ExecuteQuery(string.Concat(new object[]
						{
							"DELETE FROM achievements_owed WHERE achievement = '",
							(uint)dataRow["achievement"],
							"' AND user = '",
							this.GetHabbo().Id,
							"' LIMIT 1"
						}));
					}
				}
			}
		}
		public void method_7(string string_0)
		{
			ServerMessage Message = new ServerMessage(35u);
			Message.AppendStringWithBreak("A moderator has kicked you from the hotel:", 13);
			Message.AppendStringWithBreak(string_0);
			this.SendMessage(Message);
		}
		public void SendNotif(string Message)
		{
			this.SendNotif(Message, 0);
		}
		public void SendNotif(string string_0, int int_0)
		{
			ServerMessage nMessage = new ServerMessage();
			switch (int_0)
			{
			case 0:
				nMessage.Init(161u);
				break;
			case 1:
				nMessage.Init(139u);
				break;
			case 2:
				nMessage.Init(810u);
				nMessage.AppendUInt(1u);
				break;
			default:
				nMessage.Init(161u);
				break;
			}
			nMessage.AppendStringWithBreak(string_0);
			this.GetConnection().SendMessage(nMessage);
		}
		public void method_10(string string_0, string string_1)
		{
			ServerMessage Message = new ServerMessage(161u);
			Message.AppendStringWithBreak(string_0);
			Message.AppendStringWithBreak(string_1);
			this.GetConnection().SendMessage(Message);
		}
		public void method_11()
		{
			if (this.Message1_0 != null)
			{
				this.Message1_0.Dispose();
				this.Message1_0 = null;
			}
			if (this.GetHabbo() != null)
			{
				this.Habbo.method_9();
				this.Habbo = null;
			}
			if (this.method_1() != null)
			{
				this.class17_0.Destroy();
				this.class17_0 = null;
			}
		}
		public void method_12()
		{
			if (!this.bool_2)
			{
				Phoenix.GetGame().GetClientManager().method_9(this.uint_0);
				this.bool_2 = true;
			}
		}
		public void method_13(ref byte[] byte_0)
		{
			if (byte_0[0] == 64)
			{
				int i = 0;
				while (i < byte_0.Length)
				{
					try
					{
						int num = Base64Encoding.DecodeInt32(new byte[]
						{
							byte_0[i++],
							byte_0[i++],
							byte_0[i++]
						});
						uint uint_ = Base64Encoding.DecodeUInt32(new byte[]
						{
							byte_0[i++],
							byte_0[i++]
						});
						byte[] array = new byte[num - 2];
						for (int j = 0; j < array.Length; j++)
						{
							array[j] = byte_0[i++];
						}
						if (this.class17_0 == null)
						{
							this.method_4();
						}
						ClientMessage @class = new ClientMessage(uint_, array);
						if (@class != null)
						{
							try
							{
								if (int.Parse(Phoenix.GetConfig().data["debug"]) == 1)
								{
									Logging.WriteLine(string.Concat(new object[]
									{
										"[",
										this.UInt32_0,
										"] --> [",
										@class.Id,
										"] ",
										@class.Header,
										@class.GetBody()
									}));
								}
							}
							catch
							{
							}
							Interface @interface;
							if (Phoenix.smethod_10().Handle(@class.Id, out @interface))
							{
								@interface.Handle(this, @class);
							}
						}
					}
					catch (Exception ex)
					{
                        Logging.LogException("Error: " + ex.ToString());
						this.method_12();
					}
				}
			}
			else
			{
				if (true)//Class13.Boolean_7)
				{
                    this.Message1_0.method_4(CrossdomainPolicy.GetXmlPolicy());
					this.Message1_0.Dispose();
				}
			}
		}
		public void SendMessage(ServerMessage Message5_0)
		{
			if (Message5_0 != null && this.GetConnection() != null)
			{
				this.GetConnection().SendMessage(Message5_0);
			}
		}
	}
}
