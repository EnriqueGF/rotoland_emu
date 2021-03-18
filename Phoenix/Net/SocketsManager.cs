using System;
using System.Net.Sockets;
using Phoenix.Core;
namespace Phoenix.Net
{
	internal sealed class SocketsManager
	{
		private Message1[] Message1_0;
		private Class114 class114_0;
		public int Int32_0
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.Message1_0.Length; i++)
				{
					if (this.Message1_0[i] != null)
					{
						num++;
					}
				}
				return num;
			}
		}
		public SocketsManager(string string_0, int int_0, int int_1)
		{
			this.Message1_0 = new Message1[int_1];
			this.class114_0 = new Class114(string_0, int_0, this);
		}
		public void method_0()
		{
			for (int i = 0; i < this.Message1_0.Length; i++)
			{
				if (this.Message1_0[i] != null)
				{
					this.Message1_0[i].Dispose();
				}
			}
			this.Message1_0 = null;
			this.class114_0 = null;
		}
		public bool method_1(uint uint_0)
		{
			return this.Message1_0[(int)((UIntPtr)uint_0)] != null;
		}
		public Message1 method_2(uint uint_0)
		{
			return this.Message1_0[(int)((UIntPtr)uint_0)];
		}
		public Class114 method_3()
		{
			return this.class114_0;
		}
		internal int method_4()
		{
			return Array.IndexOf<Message1>(this.Message1_0, null);
		}
		internal void method_5(SocketInformation socketInformation_0, int int_0)
		{
			Message1 Message = new Message1(Convert.ToUInt32(int_0), socketInformation_0);
			this.Message1_0[int_0] = Message;
			Phoenix.GetGame().GetClientManager().method_8((uint)int_0, ref Message);
			if (Phoenix.GetConfig().data["emu.messages.connections"] == "1")
			{
				Logging.WriteLine(string.Concat(new object[]
				{
					">> Connection [",
					int_0,
					"] from [",
					Message.String_0,
					"]"
				}));
			}
		}
		internal void method_6(uint uint_0)
		{
			this.Message1_0[(int)((UIntPtr)uint_0)] = null;
		}
		internal void method_7()
		{
			this.class114_0.method_5();
			for (int i = 0; i < this.Message1_0.Length; i++)
			{
				if (this.Message1_0[i] != null)
				{
					this.Message1_0[i].Dispose();
				}
			}
		}
	}
}