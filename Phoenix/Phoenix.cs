using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Phoenix.Core;
using Phoenix.HabboHotel;
using Phoenix.Net;
using Phoenix.Storage;
using Phoenix.Util;
using Phoenix.Communication;
using Phoenix.Messages;
namespace Phoenix
{
    internal sealed class Phoenix
    {
        public const int build = 14986;
        public const string string_0 = "localhost";
        private static PacketManager class117_0;
        private static ConfigurationData Configuration;
        private static DatabaseManager DatabaseManager;
        private static SocketsManager ConnectionManage;
        private static MusListener MusListener;
        private static Game Game;
        internal static DateTime ServerStarted;
        public string string_2 = Phoenix.smethod_1(14986.ToString());
        public string string_3 = LicenceServer + "licence" + Convert.ToChar(46).ToString() + "php" + Convert.ToChar(63).ToString();
        public static string string_4 = LicenceServer + "override" + Convert.ToChar(46).ToString() + "php";
        public static bool bool_0 = false;
        public static int int_1 = 0;
        public static int int_2 = 0;
        public static string string_5 = null;
        public static string string_6;
        public static string string_7;
        private static bool bool_1 = false;
        internal static string LicenceServer;   //Licence server (config.conf)

        public static string PrettyVersion
        {
            get
            {
                return "Phoenix v3.11.0 (Build " + build + ")";
            }
        }
        internal static Game Class3_0
        {
            get
            {
                return Phoenix.Game;
            }
            set
            {
                Phoenix.Game = value;
            }
        }
        public static string smethod_0(string string_8)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] array = Encoding.UTF8.GetBytes(string_8);
            array = mD5CryptoServiceProvider.ComputeHash(array);
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                byte b = array2[i];
                stringBuilder.Append(b.ToString("x2").ToLower());
            }
            string text = stringBuilder.ToString();
            return text.ToUpper();
        }
        public static string smethod_1(string string_8)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(string_8);
            byte[] array = new SHA1Managed().ComputeHash(bytes);
            string text = string.Empty;
            byte[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                byte b = array2[i];
                text += b.ToString("X2");
            }
            return text;
        }
        public void Initialize()
        {   
            if (!Licence.smethod_0(true))
            {
                Phoenix.ServerStarted = DateTime.Now;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("        ______  _                       _          _______             ");
                Console.WriteLine("       (_____ \\| |                     (_)        (_______)            ");
                Console.WriteLine("        _____) ) | _   ___   ____ ____  _ _   _    _____   ____  _   _ ");
                Console.WriteLine("       |  ____/| || \\ / _ \\ / _  )  _ \\| ( \\ / )  |  ___) |    \\| | | |");
                Console.WriteLine("       | |     | | | | |_| ( (/ /| | | | |) X (   | |_____| | | | |_| |");
                Console.WriteLine("       |_|     |_| |_|\\___/ \\____)_| |_|_(_/ \\_)  |_______)_|_|_|\\____|");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("                                       " + PrettyVersion);
                Console.WriteLine();
                Console.WriteLine("          Dedicated and VPS Hosting available at Otaku-Hosting.com");
                Console.WriteLine("    VPS Hosting from just £4.99 for the first month with coupon OTAKU50!");
                Console.ResetColor();

                try
                {
                    Phoenix.Configuration = new ConfigurationData("config.conf");
                    DateTime now = DateTime.Now;
                    LicenceServer = GetConfig().data["LicenceServer.URL"];  //LicenceServer URL
                    string_6 = GetConfig().data["Otaku-Studios.username"];
                    string_7 = GetConfig().data["Otaku-Studios.password"];
                    //Lookds = new Random().Next(Int32.MaxValue).ToString();
                    int num = string_6.Length * string_7.Length;

                    if (string_6 == "" || string_7 == "" || LicenseTools.Boolean_7)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Phoenix.Destroy("Invalid Licence details found #0001", false);
                    }
                    else
                    {
                        LicenseTools.String_6 = Phoenix.string_6;
                        LicenseTools.String_3 = Phoenix.string_7;
                        string text = new Random().Next(Int32.MaxValue).ToString();
                        text = Licence.smethod_1(text, this.string_3);

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string str = new Random().Next(Int32.MaxValue).ToString();//text.Substring(32, 32);
                        str = Phoenix.smethod_0(str + Phoenix.string_6);
                        str = Phoenix.smethod_0(str + "4g");
                        str = Phoenix.smethod_1(str + Phoenix.string_7);
                        string b = Phoenix.smethod_0(num.ToString());

                        DatabaseServer Message3_ = new DatabaseServer(Phoenix.GetConfig().data["db.hostname"], uint.Parse(Phoenix.GetConfig().data["db.port"]), Phoenix.GetConfig().data["db.username"], Phoenix.GetConfig().data["db.password"]);
                        text = "r4r43mfgp3kkkr3mgprekw[gktp6ijhy[h]5h76ju6j7uj7";//text.Substring(64, 96);
                        Database Message2_ = new Database(Phoenix.GetConfig().data["db.name"], uint.Parse(Phoenix.GetConfig().data["db.pool.minsize"]), uint.Parse(Phoenix.GetConfig().data["db.pool.maxsize"]));
                        Phoenix.DatabaseManager = new DatabaseManager(Message3_, Message2_);

                        try
                        {
                            using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
                            {
                                @class.ExecuteQuery("UPDATE users SET online = '0'");
                                @class.ExecuteQuery("UPDATE rooms SET users_now = '0'");
                            }
                            Phoenix.ConnectionManage.method_7();
                            Phoenix.Game.ContinueLoading();
                        }
                        catch
                        {
                        }

                        LicenseTools.String_1 = text;
                        Phoenix.Game = new Game(int.Parse(Phoenix.GetConfig().data["game.tcp.conlimit"]));
                        string text2 = LicenseTools.String_5 + Phoenix.smethod_0((LicenseTools.String_6.Length * 14986).ToString());
                        text2 += Phoenix.smethod_0((LicenseTools.String_3.Length % 14986).ToString());

                        Phoenix.class117_0 = new PacketManager();
                        Phoenix.class117_0.Handshake();
                        Phoenix.class117_0.Messenger();
                        Phoenix.class117_0.Navigator();
                        Phoenix.class117_0.RoomsAction();
                        Phoenix.class117_0.RoomsAvatar();
                        Phoenix.class117_0.RoomsChat();
                        Phoenix.class117_0.RoomsEngine();
                        Phoenix.class117_0.RoomsFurniture();
                        Phoenix.class117_0.RoomsPets();
                        Phoenix.class117_0.RoomsSession();
                        Phoenix.class117_0.RoomsSettings();
                        Phoenix.class117_0.Catalog();
                        Phoenix.class117_0.Marketplace();
                        Phoenix.class117_0.Recycler();
                        Phoenix.class117_0.Quest();
                        Phoenix.class117_0.InventoryAchievements();
                        Phoenix.class117_0.InventoryAvatarFX();
                        Phoenix.class117_0.InventoryBadges();
                        Phoenix.class117_0.InventoryFurni();
                        Phoenix.class117_0.InventoryPurse();
                        Phoenix.class117_0.InventoryTrading();
                        Phoenix.class117_0.Avatar();
                        Phoenix.class117_0.Users();
                        Phoenix.class117_0.Register();
                        Phoenix.class117_0.Help();
                        Phoenix.class117_0.Sound();
                        Phoenix.class117_0.Wired();
                    }

                    LicenseTools.int_12 = int.Parse(Phoenix.GetConfig().data["game.tcp.port"]);
                    LicenseTools.int_13 = int.Parse(Phoenix.GetConfig().data["mus.tcp.port"]);

                    Phoenix.MusListener = new MusListener(Phoenix.GetConfig().data["mus.tcp.bindip"], LicenseTools.int_13, Phoenix.GetConfig().data["mus.tcp.allowedaddr"].Split(new char[] { ';' }), 20);
                    Phoenix.ConnectionManage = new SocketsManager(LicenseTools.string_33, LicenseTools.int_12, int.Parse(Phoenix.GetConfig().data["game.tcp.conlimit"]));
                    Phoenix.ConnectionManage.method_3().method_0();
                    TimeSpan timeSpan = DateTime.Now - now;
                    Logging.WriteLine(string.Concat(new object[]
					{
						"Server -> READY! (",
						timeSpan.Seconds,
						" s, ",
						timeSpan.Milliseconds,
						" ms)"
					}));
                    Console.Beep();
                }
                catch (KeyNotFoundException)
                {
                    Logging.WriteLine("Failed to boot, key not found.");
                    Logging.WriteLine("Press any key to shut down ...");
                    Console.ReadKey(true);
                    Phoenix.smethod_16();
                }
                catch (InvalidOperationException ex)
                {
                    Logging.WriteLine("Failed to initialize PhoenixEmulator: " + ex.Message);
                    Logging.WriteLine("Press any key to shut down ...");
                    Console.ReadKey(true);
                    Phoenix.smethod_16();
                }
            }
        }
        public static int smethod_2(string string_8)
        {
            return Convert.ToInt32(string_8);
        }
        public static bool smethod_3(string string_8)
        {
            return string_8 == "1";
        }
        public static string smethod_4(bool bool_2)
        {
            if (bool_2)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        public static int smethod_5(int int_3, int int_4)
        {
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] array = new byte[4];
            rNGCryptoServiceProvider.GetBytes(array);
            int seed = BitConverter.ToInt32(array, 0);
            return new Random(seed).Next(int_3, int_4 + 1);
        }
        public static double GetUnixTimestamp()
        {
            return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }
        public static string FilterString(string str)
        {
            return DoFilter(str, false, false);
        }
        public static string DoFilter(string Input, bool bool_2, bool bool_3)
        {
            Input = Input.Replace(Convert.ToChar(1), ' ');
            Input = Input.Replace(Convert.ToChar(2), ' ');
            Input = Input.Replace(Convert.ToChar(9), ' ');
            if (!bool_2)
            {
                Input = Input.Replace(Convert.ToChar(13), ' ');
            }
            if (bool_3)
            {
                Input = Input.Replace('\'', ' ');
            }
            return Input;
        }
        public static bool smethod_9(string string_8)
        {
            if (string.IsNullOrEmpty(string_8))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < string_8.Length; i++)
                {
                    if (!char.IsLetter(string_8[i]) && !char.IsNumber(string_8[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public static PacketManager smethod_10()
        {
            return Phoenix.class117_0;
        }
        public static ConfigurationData GetConfig()
        {
            return Configuration;
        }
        public static DatabaseManager GetDatabase()
        {
            return DatabaseManager;
        }
        public static Encoding GetDefaultEncoding()
        {
            return Encoding.Default;
        }
        public static SocketsManager smethod_14()
        {
            return Phoenix.ConnectionManage;
        }
        internal static Game GetGame()
        {
            return Game;
        }
        public static void smethod_16()
        {
            Logging.WriteLine("Destroying PhoenixEmu environment...");
            if (Phoenix.GetGame() != null)
            {
                Phoenix.GetGame().ContinueLoading();
                Phoenix.Game = null;
            }
            if (Phoenix.smethod_14() != null)
            {
                Logging.WriteLine("Destroying connection manager.");
                Phoenix.smethod_14().method_3().method_2();
                Phoenix.smethod_14().method_0();
                Phoenix.ConnectionManage = null;
            }
            if (Phoenix.GetDatabase() != null)
            {
                try
                {
                    Logging.WriteLine("Destroying database manager.");
                    MySqlConnection.ClearAllPools();
                    Phoenix.DatabaseManager = null;
                }
                catch
                {
                }
            }
            Logging.WriteLine("Uninitialized successfully. Closing.");
        }
        internal static void smethod_17(string string_8)
        {
            try
            {
                ServerMessage Message = new ServerMessage(139u);
                Message.AppendStringWithBreak(string_8);
                Phoenix.GetGame().GetClientManager().method_14(Message);
            }
            catch
            {
            }
        }
        internal static void smethod_18()
        {
            Phoenix.Destroy("", true);
        }
        internal static void Destroy(string string_8, bool ExitWhenDone)
        {
            LicenseTools.bool_16 = true;
            try
            {
                Phoenix.smethod_10().Clear();
            }
            catch
            {
            }
            if (string_8 != "")
            {
                if (Phoenix.bool_1)
                {
                    return;
                }
                Console.WriteLine(string_8);
                Logging.smethod_7();
                Phoenix.smethod_17("ATTENTION:\r\nThe server is shutting down. All furniture placed in rooms/traded/bought after this message is on your own responsibillity.");
                Phoenix.bool_1 = true;
                Console.WriteLine("Server shutting down...");
                try
                {
                    Phoenix.Game.GetRoomManager().method_4();
                }
                catch
                {
                }
                try
                {
                    Phoenix.smethod_14().method_3().method_1();
                    Phoenix.GetGame().GetClientManager().CloseAll();
                }
                catch
                {

                }
                try
                {
                    Console.WriteLine("Destroying database manager.");
                    MySqlConnection.ClearAllPools();
                    Phoenix.DatabaseManager = null;
                }
                catch
                {
                }
                Console.WriteLine("System disposed, goodbye!");
            }
            else
            {
                Logging.smethod_7();
                Phoenix.bool_1 = true;
                try
                {
                    Phoenix.Game.GetRoomManager().method_4();
                }
                catch
                {
                }
                try
                {
                    Phoenix.smethod_14().method_3().method_1();
                    Phoenix.GetGame().GetClientManager().CloseAll();
                }
                catch
                {
                }
                Phoenix.ConnectionManage.method_7();
                Phoenix.Game.ContinueLoading();
                Console.WriteLine(string_8);
            }
            if (ExitWhenDone)
            {
                Environment.Exit(0);
            }
        }
        public static bool smethod_20(int int_3, int int_4)
        {
            return int_3 % int_4 == 0;
        }
        public static DateTime smethod_21(double double_0)
        {
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return result.AddSeconds(double_0).ToLocalTime();
        }
    }
}
