using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Phoenix.Core;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Support;
using Phoenix.HabboHotel.Achievements;
using Phoenix.Net;
using Phoenix.Util;
using Phoenix.Messages;
using Phoenix.Storage;
namespace Phoenix.HabboHotel.GameClients
{
	internal sealed class GameClientManager
	{
		private Task task_0;
		private GameClient[] Session;
		private Hashtable hashtable_0;
		private Hashtable hashtable_1;
		private Timer timer_0;
		private List<Message1> list_0;
		public int ClientCount
		{
			get
			{
				int result;
				if (this.Session == null)
				{
					result = 0;
				}
				else
				{
					int num = 0;
					for (int i = 0; i < this.Session.Length; i++)
					{
						if (this.Session[i] != null && this.Session[i].GetHabbo() != null && !string.IsNullOrEmpty(this.Session[i].GetHabbo().Username))
						{
							num++;
						}
					}
					num++;
					result = num;
				}
				return result;
			}
		}
		public GameClientManager(int int_0)
		{
			this.hashtable_0 = new Hashtable();
			this.hashtable_1 = new Hashtable();
			this.Session = new GameClient[int_0];
			this.list_0 = new List<Message1>();
			this.timer_0 = new Timer(new TimerCallback(this.method_4), null, 500, 500);
		}
		public void method_0(uint uint_0, string string_0, GameClient class16_1)
		{
			this.hashtable_0[uint_0] = class16_1;
			this.hashtable_1[string_0.ToLower()] = class16_1;
		}
		public void method_1(uint uint_0, string string_0)
		{
			this.hashtable_0[uint_0] = null;
			this.hashtable_1[string_0.ToLower()] = null;
		}
		public GameClient method_2(uint uint_0)
		{
			GameClient result;
			if (this.Session == null || this.hashtable_0 == null)
			{
				result = null;
			}
			else
			{
				if (this.hashtable_0.ContainsKey(uint_0))
				{
					result = (GameClient)this.hashtable_0[uint_0];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}
		public GameClient GetClientByHabbo(string string_0)
		{
			GameClient result;
			if (this.Session == null || this.hashtable_1 == null)
			{
				result = null;
			}
			else
			{
				if (this.hashtable_1.ContainsKey(string_0.ToLower()))
				{
					result = (GameClient)this.hashtable_1[string_0.ToLower()];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}
		private void method_4(object object_0)
		{
			try
			{
				List<Message1> list = this.list_0;
				this.list_0 = new List<Message1>();
				if (list != null)
				{
					foreach (Message1 current in list)
					{
						if (current != null)
						{
							current.method_1();
						}
					}
				}
			}
			catch (Exception ex)
			{
                Logging.LogThreadException(ex.ToString(), "Disconnector task");
			}
		}
		internal void method_5(Message1 Message1_0)
		{
			if (!this.list_0.Contains(Message1_0))
			{
				this.list_0.Add(Message1_0);
			}
		}
		public void method_6()
		{
		}
		public GameClient method_7(uint uint_0)
		{
			GameClient result;
			try
			{
				result = this.Session[(int)((UIntPtr)uint_0)];
			}
			catch
			{
				result = null;
			}
			return result;
		}
		internal void method_8(uint uint_0, ref Message1 Message1_0)
		{
			this.Session[(int)((UIntPtr)uint_0)] = new GameClient(uint_0, ref Message1_0);
			this.Session[(int)((UIntPtr)uint_0)].method_3();
		}
		public void method_9(uint uint_0)
		{
			GameClient @class = this.method_7(uint_0);
			if (@class != null)
			{
				Phoenix.smethod_14().method_6(uint_0);
				@class.method_11();
				this.Session[(int)((UIntPtr)uint_0)] = null;
			}
		}
		public void method_10()
		{
			if (this.task_0 == null)
			{
				this.task_0 = new Task(new Action(this.method_12));
				this.task_0.Start();
			}
		}
		public void method_11()
		{
			if (this.task_0 != null)
			{
				this.task_0 = null;
			}
		}
		private void method_12()
		{
			int num = int.Parse(Phoenix.GetConfig().data["client.ping.interval"]);
			if (num <= 100)
			{
				throw new ArgumentException("Invalid configuration value for ping interval! Must be above 100 miliseconds.");
			}
			while (true)
			{
				try
				{
					ServerMessage Message = new ServerMessage(50u);
					List<GameClient> list = new List<GameClient>();
					List<GameClient> list2 = new List<GameClient>();
					for (int i = 0; i < this.Session.Length; i++)
					{
						GameClient @class = this.Session[i];
						if (@class != null)
						{
							if (@class.bool_0)
							{
								@class.bool_0 = false;
								list2.Add(@class);
							}
							else
							{
								list.Add(@class);
							}
						}
					}
					foreach (GameClient @class in list)
					{
						try
						{
							@class.method_12();
						}
						catch
						{
						}
					}
					byte[] byte_ = Message.GetBytes();
					foreach (GameClient @class in list2)
					{
						try
						{
							@class.GetConnection().SendData(byte_);
						}
						catch
						{
						}
					}
				}
				catch (Exception ex)
				{
                    Logging.LogThreadException(ex.ToString(), "Connection checker task");
				}
				Thread.Sleep(num);
			}
		}
		internal void method_13()
		{
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null)
				{
					try
					{
						@class.SendMessage(AchievementManager.smethod_1(@class));
					}
					catch
					{
					}
				}
			}
		}
		internal void method_14(ServerMessage Message5_0)
		{
			byte[] byte_ = Message5_0.GetBytes();
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null)
				{
					try
					{
						@class.GetConnection().SendData(byte_);
					}
					catch
					{
					}
				}
			}
		}
		internal void method_15(ServerMessage Message5_0, ServerMessage Message5_1)
		{
			byte[] byte_ = Message5_0.GetBytes();
			byte[] byte_2 = Message5_1.GetBytes();
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null)
				{
					try
					{
						if (@class.GetHabbo().Boolean_0)
						{
							@class.GetConnection().SendData(byte_);
						}
						else
						{
							@class.GetConnection().SendData(byte_2);
						}
					}
					catch
					{
					}
				}
			}
		}
		internal void method_16(ServerMessage Message5_0, ServerMessage Message5_1)
		{
			byte[] byte_ = Message5_0.GetBytes();
			byte[] byte_2 = Message5_1.GetBytes();
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null)
				{
					try
					{
						if (@class.GetHabbo().HasFuse("receive_sa"))
						{
							if (@class.GetHabbo().Boolean_0)
							{
								@class.GetConnection().SendData(byte_);
							}
							else
							{
								@class.GetConnection().SendData(byte_2);
							}
						}
					}
					catch
					{
					}
				}
			}
		}
		internal void method_17(GameClient class16_1, ServerMessage Message5_0)
		{
			byte[] byte_ = Message5_0.GetBytes();
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null && @class != class16_1)
				{
					try
					{
						if (@class.GetHabbo().HasFuse("receive_sa"))
						{
							@class.GetConnection().SendData(byte_);
						}
					}
					catch
					{
					}
				}
			}
		}
		internal void method_18(int int_0)
		{
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null && @class.GetHabbo() != null)
				{
					try
					{
						@class.GetHabbo().Credits += int_0;
						@class.GetHabbo().method_13(true);
						@class.SendNotif("You just received " + int_0 + " credits from staff!");
					}
					catch
					{
					}
				}
			}
		}
		internal void method_19(int int_0, bool bool_0)
		{
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null && @class.GetHabbo() != null)
				{
					try
					{
						@class.GetHabbo().ActivityPoints += int_0;
						@class.GetHabbo().method_15(bool_0);
						@class.SendNotif("You just received " + int_0 + " pixels from staff!");
					}
					catch
					{
					}
				}
			}
		}
		internal void method_20(int int_0, bool bool_0)
		{
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null && @class.GetHabbo() != null)
				{
					try
					{
						@class.GetHabbo().VipPoints += int_0;
						@class.GetHabbo().method_14(false, bool_0);
						@class.SendNotif("You just received " + int_0 + " points from staff!");
					}
					catch
					{
					}
				}
			}
		}
		internal void method_21(string string_0)
		{
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null && @class.GetHabbo() != null)
				{
					try
					{
						@class.GetHabbo().method_22().method_2(@class, string_0, true);
						@class.SendNotif("You just received a badge from hotel staff!");
					}
					catch
					{
					}
				}
			}
		}
		public void method_22(ServerMessage Message5_0, string string_0)
		{
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null)
				{
					try
					{
						if (string_0.Length <= 0 || (@class.GetHabbo() != null && @class.GetHabbo().HasFuse(string_0)))
						{
							@class.SendMessage(Message5_0);
						}
					}
					catch
					{
					}
				}
			}
		}
		public void method_23()
		{
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null && (@class.GetHabbo() != null && @class.GetHabbo().method_24() != null))
				{
					@class.GetHabbo().method_24().method_7();
				}
			}
		}
		internal void CloseAll()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
			{
				for (int i = 0; i < this.Session.Length; i++)
				{
					GameClient class2 = this.Session[i];
					if (class2 != null && class2.GetHabbo() != null)
					{
						try
						{
							class2.GetHabbo().method_23().method_19(@class, true);
							stringBuilder.Append(class2.GetHabbo().String_0);
							flag = true;
						}
						catch
						{
						}
					}
				}
				if (flag)
				{
					try
					{
						@class.ExecuteQuery(stringBuilder.ToString());
					}
					catch (Exception ex)
					{
						Logging.smethod_8(ex.ToString());
					}
				}
			}
			Console.WriteLine("Done saving users inventory!");
			Console.WriteLine("Closing server connections...");
			try
			{
				for (int i = 0; i < this.Session.Length; i++)
				{
					GameClient class2 = this.Session[i];
					if (class2 != null && class2.GetConnection() != null)
					{
						try
						{
							class2.GetConnection().Dispose();
						}
						catch
						{
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logging.smethod_8(ex.ToString());
			}
			Array.Clear(this.Session, 0, this.Session.Length);
			Console.WriteLine("Connections closed!");
		}
		public void method_25(uint uint_0)
		{
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null && @class.GetHabbo() != null && @class.GetHabbo().Id == uint_0)
				{
					@class.method_12();
				}
			}
		}
		public string GetNameById(uint uint_0)
		{
			GameClient @class = this.method_2(uint_0);
			string result;
			if (@class != null)
			{
				result = @class.GetHabbo().Username;
			}
			else
			{
				DataRow dataRow = null;
				using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
				{
					dataRow = class2.ReadDataRow("SELECT username FROM users WHERE Id = '" + uint_0 + "' LIMIT 1");
				}
				if (dataRow == null)
				{
					result = "Unknown User";
				}
				else
				{
					result = (string)dataRow[0];
				}
			}
			return result;
		}
		public uint method_27(string string_0)
		{
			GameClient @class = this.GetClientByHabbo(string_0);
			uint result;
			if (@class != null)
			{
				result = @class.GetHabbo().Id;
			}
			else
			{
				DataRow dataRow = null;
				using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
				{
					dataRow = class2.ReadDataRow("SELECT Id FROM users WHERE username = '" + string_0 + "' LIMIT 1");
				}
				if (dataRow == null)
				{
					result = 0u;
				}
				else
				{
					result = (uint)dataRow[0];
				}
			}
			return result;
		}
		public void method_28()
		{
			Dictionary<GameClient, ModerationBanException> dictionary = new Dictionary<GameClient, ModerationBanException>();
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null)
				{
					try
					{
						Phoenix.GetGame().GetBanManager().method_1(@class);
					}
					catch (ModerationBanException value)
					{
						dictionary.Add(@class, value);
					}
				}
			}
			foreach (KeyValuePair<GameClient, ModerationBanException> current in dictionary)
			{
				current.Key.method_7(current.Value.Message);
				current.Key.method_12();
			}
		}
		public void method_29()
		{
			try
			{
				if (this.Session != null)
				{
					if (Phoenix.string_5 == "127.0.0.1")
					{
						LicenseTools.bool_15 = true;
					}
					for (int i = 0; i < this.Session.Length; i++)
					{
						GameClient @class = this.Session[i];
						if (@class != null && (@class.GetHabbo() != null && Phoenix.GetGame().GetPixelManager().method_2(@class)))
						{
							Phoenix.GetGame().GetPixelManager().method_3(@class);
						}
					}
				}
			}
			catch (Exception ex)
			{
                Logging.LogThreadException(ex.ToString(), "GCMExt.CheckPixelUpdates task");
			}
		}
		internal List<ServerMessage> method_30()
		{
			List<ServerMessage> list = new List<ServerMessage>();
			int num = 0;
			ServerMessage Message = new ServerMessage();
			Message.Init(161u);
			Message.AppendStringWithBreak("Users online:\r");
			for (int i = 0; i < this.Session.Length; i++)
			{
				GameClient @class = this.Session[i];
				if (@class != null && @class.GetHabbo() != null)
				{
					if (num > 20)
					{
						list.Add(Message);
						num = 0;
						Message = new ServerMessage();
						Message.Init(161u);
					}
					num++;
					Message.AppendStringWithBreak(string.Concat(new object[]
					{
						@class.GetHabbo().Username,
						" {",
						@class.GetHabbo().Rank,
						"}\r"
					}));
				}
			}
			list.Add(Message);
			return list;
		}
		internal void method_31(GameClient class16_1, string string_0, string string_1)
		{
            if (LicenseTools.Boolean_5)
			{
				using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
				{
					@class.AddParamWithValue("extra_data", string_1);
					@class.ExecuteQuery(string.Concat(new object[]
					{
						"INSERT INTO cmdlogs (user_id,user_name,command,extra_data,timestamp) VALUES ('",
						class16_1.GetHabbo().Id,
						"','",
						class16_1.GetHabbo().Username,
						"','",
						string_0,
						"', @extra_data, UNIX_TIMESTAMP())"
					}));
				}
			}
		}
	}
}
