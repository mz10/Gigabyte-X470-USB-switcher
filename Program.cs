using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Gigabyte.EasyTune.Common;
using Gigabyte.USBDACUP.PowerManagement;

namespace GBUSBSwitcher {
    class Program {
		public static void Main(string[] args) {
			if(args.Length == 0) {
				Console.WriteLine("The program must run as administrator");
				Console.WriteLine("Use: GBUSBSwitcher.exe argument");
				Console.WriteLine("Example:\nGBUSBSwitcher.exe 50\n");

				Console.WriteLine("Argument can be:");
				Console.WriteLine("0 (Voltage 0 V)");
				Console.WriteLine("50 (Voltage 5 V)");
				Console.WriteLine("51 (Voltage 5.1 V)");
				Console.WriteLine("52 (Voltage 5.2 V)");
				Console.WriteLine("53 (Voltage 5.3 V)");
				Console.WriteLine("switch (switch 0/5V)");

				Console.ReadLine();
			}
			else if(args.Length >= 1) {
				new USB(args[0]);
            }
		}
	}

	public static class Voltage {
		public static uint V0   = 2147473647;
		public static uint V5   = 2147483647;
		public static uint V5_1 = 2147493647;
		public static uint V5_2 = 2147503647;
		public static uint V5_3 = 2147513647;
	}

	class USB {
		static USBPowerManagement usbVoltageManager;
		static List<TuningConfigData> usbTuningDatas;
		static List<SaveTunningConfigData> usbSaveTunningDatas;
		static TuningConfigDataConverter tcdConvertor;

		public USB(string type) {
			try {
				usbVoltageManager = new USBPowerManagement();
				usbTuningDatas = new List<TuningConfigData>();
				usbSaveTunningDatas = new List<SaveTunningConfigData>();
				tcdConvertor = new TuningConfigDataConverter();
				usbVoltageManager.Read(ref usbTuningDatas);
			}
			catch (Exception e) {
				Console.WriteLine("Failed to load DLLs!:\n" + e.Message);
			}

			SetVoltage(type);
		}

		void SetVoltage(string type) {
			switch (type) {
				case "0":  Update(Voltage.V0); break;
				case "50": Update(Voltage.V5); break;
				case "51": Update(Voltage.V5_1); break;
				case "52": Update(Voltage.V5_2); break;
				case "53": Update(Voltage.V5_3); break;
				case "switch": type = UpdateSwitch(); break;
				default: Console.WriteLine("Bad voltage value!: " + type); break;
			}

			var tw = new StreamWriter(@"lastVoltage.txt");
			tw.Write(type);
			tw.Close();
		}

		string UpdateSwitch() {
			string file = @"lastVoltage.txt";
			string type = File.Exists(file) ? File.ReadLines(file).First() : "0";

			switch(type) {
				case "0":	
					type = "50";
					Console.WriteLine("Switch on!");
					Update(Voltage.V5); 
					break;
				case "50":	
					type = "0";
					Console.WriteLine("Switch off!");
					Update(Voltage.V0);  
					break;
				default: Console.WriteLine("Bad type voltage in file!"); type = "0"; break;
			}

			return type;
		}

		void Update(uint voltage = 0) {
			try {
				foreach (TuningConfigData dt in usbTuningDatas) {
					dt.ActiveValue = dt.CurrentValue;
					//GetCurrentValue(true, usbTuningDatas, out num);
					dt.CurrentValue = voltage;
				}

				tcdConvertor.ToSaveTunningConfigData(ref usbTuningDatas, ref usbSaveTunningDatas);
				usbVoltageManager.Write(usbSaveTunningDatas);

			}
			catch (Exception e) {
				Console.WriteLine("Voltage cannot be written", e.Message);
			}
		}
	}
}