USB switcher (in CMD) for Gigabyte motherboards with function DAC-UP. 
It replaces the program DAC-UP from Gigabyte and switching of USB works in CMD.

Requirements:
This is tested on X470 AORUS GAMING 7 WIFI, but it may work for others mainboards with DAC-UP.

Install "USB DAC UP 2" and copy GBUSBSwitcher.exe to c:\Program Files (x86)\GIGABYTE\USB DAC\ or copy DLLs from this folder to folder, where is GBUSBSwitcher.exe.

Run GBUSBSwitcher.exe as administrator.

Required DLLS:
Gigabyte.dll
Gigabyte.ComputerSystemHardware.dll
Gigabyte.ComputerSystemHardware.BIOS.EasyMethods.dll
Gigabyte.EasyTune.Common.dll
Gigabyte.EasyTune.EasyFunctions.dll
Gigabyte.EasyTune.PowerManagement.dll
Gigabyte.NativeFunctions.dll
Gigabyte.Resources.dll
Gigabyte.Resources.EasyTune.dll
Gigabyte.USBDACUP.dll
yccV2.dll

Use: GBUSBSwitcher.exe argument
Example:
GBUSBSwitcher.exe 50

Argument can be:
0 (Voltage 0 V)
50 (Voltage 5 V)
51 (Voltage 5.1 V)
52 (Voltage 5.2 V)
53 (Voltage 5.3 V)
switch (switch 0/5V)

Warning: try this program on your own responsibility. It hasn't been tested on other mainboards, but I don't think it should damage the board.
