using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Phoenix.HabboHotel.Users.UserDataManagement;
using Phoenix.HabboHotel.Users.Subscriptions;
using Phoenix.HabboHotel.Users.Inventory;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Util;
using Phoenix.Messages;
using Phoenix.HabboHotel.Users.Messenger;
using Phoenix.HabboHotel.Users.Badges;
using Phoenix.Storage;
namespace Phoenix.HabboHotel.Users
{
	internal sealed class Habbo
	{
		public uint Id;
		public string Username;
		public string RealName;
        public bool isAaron;
		public bool isVisible;
		public bool bool_2;
		public string SSO;
		public string LastIp;
		public uint Rank;
		public string Motto;
		public string Figure;
		public string Gender;
		public int int_0;
		public DataTable dataTable_0;
		public List<int> list_0;
		public int int_1;
		public int Credits;
		public int ActivityPoints;
		public double LastActivityPointsUpdate;
		public bool bool_3;
		public int int_4;
		internal bool bool_4 = false;
		public uint uint_2;
		public bool bool_5;
		public bool bool_6;
		public uint CurrentRoomId;
		public uint uint_4;
		public bool bool_7;
		public uint uint_5;
		public List<uint> list_1;
		public List<uint> list_2;
		public List<string> list_3;
		public Dictionary<uint, int> dictionary_0;
		public List<uint> list_4;
		private SubscriptionManager class53_0;
		private HabboMessenger class105_0;
		private BadgeComponent class56_0;
		private InventoryComponent class38_0;
		private AvatarEffectsInventoryComponent class50_0;
		private GameClient Session;
		public List<uint> CompletedQuests;
		public uint CurrentQuestId;
		public int CurrentQuestProgress;
		public int int_6;
		public int int_7;
		public int int_8;
		public int int_9;
		public uint uint_7;
		public int NewbieStatus;
		public bool bool_8;
		public bool bool_9;
		public bool bool_10;
		public bool BlockNewFriends;
		public bool HideInRom;
		public bool HideOnline;
		public bool Vip;
		public int Volume;
		public int VipPoints;
		public int AchievementScore;
		public int RoomVisits;
		public int int_15;
		public int int_16;
		public int Respect;
		public int RespectGiven;
		public int GiftsGiven;
		public int GiftsReceived;
		public int int_21;
		public int int_22;
		private UserDataFactory UserDataFactory;
		internal List<RoomData> list_6;
		public int int_23;
		public DateTime dateTime_0;
		public bool bool_15;
		public int int_24;
		private bool bool_16 = false;
		public bool Boolean_0
		{
			get
			{
				return this.CurrentRoomId >= 1u;
			}
		}
		public Room Class14_0
		{
			get
			{
				if (this.CurrentRoomId <= 0u)
				{
					return null;
				}
				else
				{
					return Phoenix.GetGame().GetRoomManager().GetRoom(this.CurrentRoomId);
				}
			}
		}
		internal UserDataFactory Class12_0
		{
			get
			{
				return this.UserDataFactory;
			}
		}
		internal string String_0
		{
			get
			{
				this.bool_16 = true;
				int num = (int)Phoenix.GetUnixTimestamp() - this.int_16;
				string text = string.Concat(new object[]
				{
					"UPDATE users SET last_online = UNIX_TIMESTAMP(), online = '0', activity_points_lastupdate = '",
					this.LastActivityPointsUpdate,
					"' WHERE Id = '",
					this.Id,
					"' LIMIT 1; "
				});
				object obj = text;
				return string.Concat(new object[]
				{
					obj,
					"UPDATE user_stats SET RoomVisits = '",
					this.RoomVisits,
					"', OnlineTime = OnlineTime + ",
					num,
					", Respect = '",
					this.Respect,
					"', RespectGiven = '",
					this.RespectGiven,
					"', GiftsGiven = '",
					this.GiftsGiven,
					"', GiftsReceived = '",
					this.GiftsReceived,
					"' WHERE Id = '",
					this.Id,
					"' LIMIT 1; "
				});
			}
		}
		public Habbo(uint UserId, string Username, string Name, string SSO, uint Rank, string Motto, string Look, string Gender, int Credits, int Pixels, double Activity_Points_LastUpdate, bool Muted, uint HomeRoom, int NewbieStatus, bool BlockNewFriends, bool HideInRoom, bool HideOnline, bool Vip, int Volume, int Points, bool AcceptTrading, string LastIp, GameClient Session, UserDataFactory userDataFactory)
		{
			if (Session != null)
			{
				Phoenix.GetGame().GetClientManager().method_0(UserId, Username, Session);
			}
			this.Id = UserId;
			this.Username = Username;
			this.RealName = Name;
			this.isAaron = false;
            this.isVisible = true;
			this.SSO = SSO;
			this.Rank = Rank;
			this.Motto = Motto;
			this.Figure = Phoenix.FilterString(Look.ToLower());
			this.Gender = Gender.ToLower();
			this.Credits = Credits;
			this.VipPoints = Points;
			this.ActivityPoints = Pixels;
			this.LastActivityPointsUpdate = Activity_Points_LastUpdate;
			this.bool_2 = AcceptTrading;
			this.bool_3 = Muted;
			this.uint_2 = 0u;
			this.bool_5 = false;
			this.bool_6 = false;
			this.CurrentRoomId = 0u;
			this.uint_4 = HomeRoom;
			this.list_1 = new List<uint>();
			this.list_2 = new List<uint>();
			this.list_3 = new List<string>();
			this.dictionary_0 = new Dictionary<uint, int>();
			this.list_4 = new List<uint>();
			this.NewbieStatus = NewbieStatus;
			this.bool_10 = false;
			this.BlockNewFriends = BlockNewFriends;
			this.HideInRom = HideInRoom;
			this.HideOnline = HideOnline;
			this.Vip = Vip;
			this.Volume = Volume;
			this.int_1 = 0;
			this.int_24 = 1;
			this.LastIp = LastIp;
			this.bool_7 = false;
			this.uint_5 = 0u;
			this.Session = Session;
			this.UserDataFactory = userDataFactory;
			this.list_6 = new List<RoomData>();
			this.list_0 = new List<int>();
			DataRow dataRow = null;
			using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
			{
				@class.AddParamWithValue("user_id", UserId);
				dataRow = @class.ReadDataRow("SELECT * FROM user_stats WHERE Id = @user_id LIMIT 1");
				if (dataRow == null)
				{
					@class.ExecuteQuery("INSERT INTO user_stats (Id) VALUES ('" + UserId + "')");
					dataRow = @class.ReadDataRow("SELECT * FROM user_stats WHERE Id = @user_id LIMIT 1");
				}
				this.dataTable_0 = @class.ReadDataTable("SELECT * FROM group_memberships WHERE userid = @user_id");
				IEnumerator enumerator;
				if (this.dataTable_0 != null)
				{
					enumerator = this.dataTable_0.Rows.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							DataRow dataRow2 = (DataRow)enumerator.Current;
							GroupsManager class2 = Groups.smethod_2((int)dataRow2["groupid"]);
							if (class2 == null)
							{
								DataTable dataTable = @class.ReadDataTable("SELECT * FROM groups WHERE Id = " + (int)dataRow2["groupid"] + " LIMIT 1;");
								IEnumerator enumerator2 = dataTable.Rows.GetEnumerator();
								try
								{
									while (enumerator2.MoveNext())
									{
										DataRow dataRow3 = (DataRow)enumerator2.Current;
										if (!Groups.GroupsManager.ContainsKey((int)dataRow3["Id"]))
										{
											Groups.GroupsManager.Add((int)dataRow3["Id"], new GroupsManager((int)dataRow3["Id"], dataRow3, @class));
										}
									}
									continue;
								}
								finally
								{
									IDisposable disposable = enumerator2 as IDisposable;
									if (disposable != null)
									{
										disposable.Dispose();
									}
								}
							}
							if (!class2.list_0.Contains((int)UserId))
							{
								class2.method_0((int)UserId);
							}
						}
					}
					finally
					{
						IDisposable disposable = enumerator as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					int num = (int)dataRow["groupid"];
					GroupsManager class3 = Groups.smethod_2(num);
					if (class3 != null)
					{
						this.int_0 = num;
					}
					else
					{
						this.int_0 = 0;
					}
				}
				else
				{
					this.int_0 = 0;
				}
				DataTable dataTable2 = @class.ReadDataTable("SELECT groupid FROM group_requests WHERE userid = '" + UserId + "';");
				enumerator = dataTable2.Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DataRow dataRow2 = (DataRow)enumerator.Current;
						this.list_0.Add((int)dataRow2["groupid"]);
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			this.RoomVisits = (int)dataRow["RoomVisits"];
			this.int_16 = (int)Phoenix.GetUnixTimestamp();
			this.int_15 = (int)dataRow["OnlineTime"];
			this.Respect = (int)dataRow["Respect"];
			this.RespectGiven = (int)dataRow["RespectGiven"];
			this.GiftsGiven = (int)dataRow["GiftsGiven"];
			this.GiftsReceived = (int)dataRow["GiftsReceived"];
			this.int_21 = (int)dataRow["DailyRespectPoints"];
			this.int_22 = (int)dataRow["DailyPetRespectPoints"];
			this.AchievementScore = (int)dataRow["AchievementScore"];
			this.CompletedQuests = new List<uint>();
			this.uint_7 = 0u;
			this.CurrentQuestId = (uint)dataRow["quest_id"];
			this.CurrentQuestProgress = (int)dataRow["quest_progress"];
			this.int_6 = (int)dataRow["lev_builder"];
			this.int_8 = (int)dataRow["lev_identity"];
			this.int_7 = (int)dataRow["lev_social"];
			this.int_9 = (int)dataRow["lev_explore"];
			if (Session != null)
			{
				this.class53_0 = new SubscriptionManager(UserId, userDataFactory);
				this.class56_0 = new BadgeComponent(UserId, userDataFactory);
				this.class38_0 = new InventoryComponent(UserId, Session, userDataFactory);
				this.class50_0 = new AvatarEffectsInventoryComponent(UserId, Session, userDataFactory);
				this.bool_8 = false;
				this.bool_9 = false;
				foreach (DataRow dataRow3 in userDataFactory.DataTable_10.Rows)
				{
					this.list_6.Add(Phoenix.GetGame().GetRoomManager().method_17((uint)dataRow3["Id"], dataRow3));
				}
			}
		}
		public void method_0(DatabaseClient class6_0)
		{
			this.dataTable_0 = class6_0.ReadDataTable("SELECT * FROM group_memberships WHERE userid = " + this.Id);
			if (this.dataTable_0 != null)
			{
				foreach (DataRow dataRow in this.dataTable_0.Rows)
				{
					GroupsManager @class = Groups.smethod_2((int)dataRow["groupid"]);
					if (@class == null)
					{
						DataTable dataTable = class6_0.ReadDataTable("SELECT * FROM groups WHERE Id = " + (int)dataRow["groupid"] + " LIMIT 1;");
						IEnumerator enumerator2 = dataTable.Rows.GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								DataRow dataRow2 = (DataRow)enumerator2.Current;
								if (!Groups.GroupsManager.ContainsKey((int)dataRow2["Id"]))
								{
									Groups.GroupsManager.Add((int)dataRow2["Id"], new GroupsManager((int)dataRow2["Id"], dataRow2, class6_0));
								}
							}
							continue;
						}
						finally
						{
							IDisposable disposable = enumerator2 as IDisposable;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
					}
					if (!@class.list_0.Contains((int)this.Id))
					{
						@class.method_0((int)this.Id);
					}
				}
				int num = class6_0.ReadInt32("SELECT groupid FROM user_stats WHERE Id = " + this.Id + " LIMIT 1");
				GroupsManager class2 = Groups.smethod_2(num);
				if (class2 != null)
				{
					this.int_0 = num;
				}
				else
				{
					this.int_0 = 0;
				}
			}
			else
			{
				this.int_0 = 0;
			}
		}
		internal void method_1(DatabaseClient class6_0)
		{
			this.list_6.Clear();
			class6_0.AddParamWithValue("name", this.Username);
			DataTable dataTable = class6_0.ReadDataTable("SELECT * FROM rooms WHERE owner = @name ORDER BY Id ASC");
			foreach (DataRow dataRow in dataTable.Rows)
			{
				this.list_6.Add(Phoenix.GetGame().GetRoomManager().method_17((uint)dataRow["Id"], dataRow));
			}
		}
		public void method_2(UserDataFactory class12_1)
		{
			this.method_8(class12_1);
			this.method_5(class12_1);
			this.method_6(class12_1);
			this.method_7(class12_1);
			this.method_25();
		}
		public bool HasFuse(string string_7)
		{
			if (Phoenix.GetGame().GetRoleManager().method_3(this.Id))
			{
				return Phoenix.GetGame().GetRoleManager().method_4(this.Id, string_7);
			}
			else
			{
				return Phoenix.GetGame().GetRoleManager().method_1(this.Rank, string_7);
			}
		}
		public int method_4()
		{
			if (this.isAaron)
			{
				return 0;
			}
			else
			{
				return Phoenix.GetGame().GetRoleManager().method_2(this.Rank);
			}
		}
		public void method_5(UserDataFactory class12_1)
		{
			this.list_1.Clear();
			DataTable dataTable_ = class12_1.DataTable_1;
			foreach (DataRow dataRow in dataTable_.Rows)
			{
				this.list_1.Add((uint)dataRow["room_id"]);
			}
		}
		public void method_6(UserDataFactory class12_1)
		{
			DataTable dataTable_ = class12_1.DataTable_2;
			foreach (DataRow dataRow in dataTable_.Rows)
			{
				this.list_2.Add((uint)dataRow["ignore_id"]);
			}
		}
		public void method_7(UserDataFactory class12_1)
		{
			this.list_3.Clear();
			DataTable dataTable_ = class12_1.DataTable_3;
			foreach (DataRow dataRow in dataTable_.Rows)
			{
				this.list_3.Add((string)dataRow["tag"]);
			}
			if (this.list_3.Count >= 5 && this.method_19() != null)
			{
				Phoenix.GetGame().GetAchievementManager().addAchievement(this.method_19(), 7u, 1);
			}
		}
		public void method_8(UserDataFactory class12_1)
		{
			DataTable dataTable = class12_1.DataTable_0;
			if (dataTable != null)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					this.dictionary_0.Add((uint)dataRow["achievement_id"], (int)dataRow["achievement_level"]);
				}
			}
		}
		public void method_9()
		{
			if (!this.bool_9)
			{
				this.bool_9 = true;
				Phoenix.GetGame().GetClientManager().method_1(this.Id, this.Username);
				if (!this.bool_16)
				{
					this.bool_16 = true;
					using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
					{
						@class.ExecuteQuery(string.Concat(new object[]
						{
							"UPDATE users SET last_online = UNIX_TIMESTAMP(), users.online = '0', activity_points = '",
							this.ActivityPoints,
							"', activity_points_lastupdate = '",
							this.LastActivityPointsUpdate,
							"', credits = '",
							this.Credits,
							"' WHERE Id = '",
							this.Id,
							"' LIMIT 1;"
						}));
						int num = (int)Phoenix.GetUnixTimestamp() - this.int_16;
						@class.ExecuteQuery(string.Concat(new object[]
						{
							"UPDATE user_stats SET RoomVisits = '",
							this.RoomVisits,
							"', OnlineTime = OnlineTime + ",
							num,
							", Respect = '",
							this.Respect,
							"', RespectGiven = '",
							this.RespectGiven,
							"', GiftsGiven = '",
							this.GiftsGiven,
							"', GiftsReceived = '",
							this.GiftsReceived,
							"' WHERE Id = '",
							this.Id,
							"' LIMIT 1; "
						}));
					}
				}
				if (this.Boolean_0 && this.Class14_0 != null)
				{
					this.Class14_0.method_47(this.Session, false, false);
				}
				if (this.class105_0 != null)
				{
					this.class105_0.bool_0 = true;
					this.class105_0.method_5(true);
					this.class105_0 = null;
				}
				if (this.class53_0 != null)
				{
					this.class53_0.method_0();
					this.class53_0 = null;
				}
				this.class38_0.method_18();
			}
		}
		internal void method_10(uint RoomId)
		{
			if (LicenseTools.Boolean_6)
			{
				using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
				{
					@class.ExecuteQuery(string.Concat(new object[]
					{
						"INSERT INTO user_roomvisits (user_id,room_id,entry_timestamp,exit_timestamp,hour,minute) VALUES ('",
						this.Id,
						"','",
						RoomId,
						"',UNIX_TIMESTAMP(),'0','",
						DateTime.Now.Hour,
						"','",
						DateTime.Now.Minute,
						"')"
					}));
				}
			}
			this.CurrentRoomId = RoomId;
			if (this.Class14_0.Owner != this.Username && this.CurrentQuestId == 15u)
			{
				Phoenix.GetGame().GetQuestManager().method_1(15u, this.method_19());
			}
			this.class105_0.method_5(false);
		}
		public void method_11()
		{
			try
			{
				if (LicenseTools.Boolean_6)
				{
					using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
					{
						@class.ExecuteQuery(string.Concat(new object[]
						{
							"UPDATE user_roomvisits SET exit_timestamp = UNIX_TIMESTAMP() WHERE room_id = '",
							this.CurrentRoomId,
							"' AND user_id = '",
							this.Id,
							"' ORDER BY entry_timestamp DESC LIMIT 1"
						}));
					}
				}
			}
			catch
			{
			}
			this.CurrentRoomId = 0u;
			if (this.class105_0 != null)
			{
				this.class105_0.method_5(false);
			}
		}
		public void method_12()
		{
			if (this.GetMessenger() == null)
			{
				this.class105_0 = new HabboMessenger(this.Id);
				this.class105_0.method_0(this.UserDataFactory);
				this.class105_0.method_1(this.UserDataFactory);
				GameClient @class = this.method_19();
				if (@class != null)
				{
					@class.SendMessage(this.class105_0.method_21());
					@class.SendMessage(this.class105_0.method_23());
					this.class105_0.method_5(true);
				}
			}
		}
		public void method_13(bool bool_17)
		{
			ServerMessage Message = new ServerMessage(6u);
			Message.AppendStringWithBreak(this.Credits + ".0");
			this.Session.SendMessage(Message);
			if (bool_17)
			{
				using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
				{
					@class.ExecuteQuery(string.Concat(new object[]
					{
						"UPDATE users SET credits = '",
						this.Credits,
						"' WHERE Id = '",
						this.Id,
						"' LIMIT 1;"
					}));
				}
			}
		}
		public void method_14(bool bool_17, bool bool_18)
		{
			if (bool_17)
			{
				using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
				{
					this.VipPoints = @class.ReadInt32("SELECT vip_points FROM users WHERE Id = '" + this.Id + "' LIMIT 1;");
				}
			}
			if (bool_18)
			{
				using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
				{
					@class.ExecuteQuery(string.Concat(new object[]
					{
						"UPDATE users SET vip_points = '",
						this.VipPoints,
						"' WHERE Id = '",
						this.Id,
						"' LIMIT 1;"
					}));
				}
			}
			this.method_16(0);
		}
		public void method_15(bool bool_17)
		{
			this.method_16(0);
			if (bool_17)
			{
				using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
				{
					@class.ExecuteQuery(string.Concat(new object[]
					{
						"UPDATE users SET activity_points = '",
						this.ActivityPoints,
						"' WHERE Id = '",
						this.Id,
						"' LIMIT 1;"
					}));
				}
			}
		}
		public void method_16(int int_25)
		{
			ServerMessage Message = new ServerMessage(438u);
			Message.AppendInt32(this.ActivityPoints);
			Message.AppendInt32(int_25);
			Message.AppendInt32(0);
			ServerMessage Message2 = new ServerMessage(438u);
			Message2.AppendInt32(this.VipPoints);
			Message2.AppendInt32(0);
			Message2.AppendInt32(1);
			ServerMessage Message3 = new ServerMessage(438u);
			Message3.AppendInt32(this.VipPoints);
			Message3.AppendInt32(0);
			Message3.AppendInt32(2);
			ServerMessage Message4 = new ServerMessage(438u);
			Message4.AppendInt32(this.VipPoints);
			Message4.AppendInt32(0);
			Message4.AppendInt32(3);
			ServerMessage Message5 = new ServerMessage(438u);
			Message5.AppendInt32(this.VipPoints);
			Message5.AppendInt32(0);
			Message5.AppendInt32(4);
			this.Session.SendMessage(Message);
			this.Session.SendMessage(Message2);
			this.Session.SendMessage(Message3);
			this.Session.SendMessage(Message4);
			this.Session.SendMessage(Message5);
		}
		public void method_17()
		{
			if (!this.bool_3)
			{
				this.method_19().SendNotif("You have been muted by a moderator.");
				this.bool_3 = true;
			}
		}
		public void method_18()
		{
			if (this.bool_3)
			{
				this.bool_3 = false;
			}
		}
		private GameClient method_19()
		{
			return Phoenix.GetGame().GetClientManager().method_2(this.Id);
		}
		public SubscriptionManager method_20()
		{
			return this.class53_0;
		}
		public HabboMessenger GetMessenger()
		{
			return this.class105_0;
		}
		public BadgeComponent method_22()
		{
			return this.class56_0;
		}
		public InventoryComponent method_23()
		{
			return this.class38_0;
		}
		public AvatarEffectsInventoryComponent method_24()
		{
			return this.class50_0;
		}
		public void method_25()
		{
			this.CompletedQuests.Clear();
			DataTable dataTable = null;
			using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
			{
				dataTable = @class.ReadDataTable("SELECT quest_id FROM user_quests WHERE user_id = '" + this.Id + "'");
			}
			if (dataTable != null)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					this.CompletedQuests.Add((uint)dataRow["quest_Id"]);
				}
			}
		}
		public void method_26(bool bool_17, GameClient class16_1)
		{
			ServerMessage Message = new ServerMessage(266u);
			Message.AppendInt32(-1);
			Message.AppendStringWithBreak(class16_1.GetHabbo().Figure);
			Message.AppendStringWithBreak(class16_1.GetHabbo().Gender.ToLower());
			Message.AppendStringWithBreak(class16_1.GetHabbo().Motto);
			Message.AppendInt32(class16_1.GetHabbo().AchievementScore);
			Message.AppendStringWithBreak("");
			class16_1.SendMessage(Message);
			if (class16_1.GetHabbo().Boolean_0)
			{
				Room class14_ = class16_1.GetHabbo().Class14_0;
				if (class14_ != null)
				{
					RoomUser @class = class14_.method_53(class16_1.GetHabbo().Id);
					if (@class != null)
					{
						if (bool_17)
						{
							DataRow dataRow = null;
							using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
							{
								class2.AddParamWithValue("userid", class16_1.GetHabbo().Id);
								dataRow = class2.ReadDataRow("SELECT * FROM users WHERE Id = @userid LIMIT 1");
							}
							class16_1.GetHabbo().Motto = Phoenix.FilterString((string)dataRow["motto"]);
							class16_1.GetHabbo().Figure = Phoenix.FilterString((string)dataRow["look"]);
						}
						ServerMessage Message2 = new ServerMessage(266u);
						Message2.AppendInt32(@class.VirtualId);
						Message2.AppendStringWithBreak(class16_1.GetHabbo().Figure);
						Message2.AppendStringWithBreak(class16_1.GetHabbo().Gender.ToLower());
						Message2.AppendStringWithBreak(class16_1.GetHabbo().Motto);
						Message2.AppendInt32(class16_1.GetHabbo().AchievementScore);
						Message2.AppendStringWithBreak("");
						class14_.SendMessage(Message2, null);
					}
				}
			}
		}
		public void method_27()
		{
			DataRow dataRow;
			using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
			{
				dataRow = @class.ReadDataRow("SELECT vip FROM users WHERE Id = '" + this.Id + "' LIMIT 1;");
			}
			this.Vip = Phoenix.smethod_3(dataRow["vip"].ToString());
			ServerMessage Message = new ServerMessage(2u);
			if (this.Vip || LicenseTools.Boolean_3)
			{
				Message.AppendInt32(2);
			}
			else
			{
				if (this.method_20().method_2("habbo_club"))
				{
					Message.AppendInt32(1);
				}
				else
				{
					Message.AppendInt32(0);
				}
			}
			if (this.HasFuse("acc_anyroomowner"))
			{
				Message.AppendInt32(7);
			}
			else
			{
				if (this.HasFuse("acc_anyroomrights"))
				{
					Message.AppendInt32(5);
				}
				else
				{
					if (this.HasFuse("acc_supporttool"))
					{
						Message.AppendInt32(4);
					}
					else
					{
						if (this.Vip || LicenseTools.Boolean_3 || this.method_20().method_2("habbo_club"))
						{
							Message.AppendInt32(2);
						}
						else
						{
							Message.AppendInt32(0);
						}
					}
				}
			}
			this.method_19().SendMessage(Message);
		}
		public void method_28(string string_7)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(this.CurrentRoomId);
			if (@class != null)
			{
				RoomUser class2 = @class.method_53(this.Id);
				ServerMessage Message = new ServerMessage(25u);
				Message.AppendInt32(class2.VirtualId);
				Message.AppendStringWithBreak(string_7);
				Message.AppendBoolean(false);
				this.method_19().SendMessage(Message);
			}
		}
	}
}
