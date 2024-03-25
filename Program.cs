
// Usage: Construct IniFileReader while passing in a .ini file (full path can be used)
IniFileReader ifr = new IniFileReader("test.ini");
// Usage: Example of retrieving a specific Section & value using GetValue()
Console.WriteLine($"Read specific value: [Configuration] - DefaultVolume: {ifr.GetValue("Configuration","Volume")}");
// Usage: Referencing a section or value that doesn't exist will simply not read the value (no crash)
Console.WriteLine($"Read specific value: [FakeSection] - FakeValue: {ifr.GetValue("FakeSection","FakeValue")}{Environment.NewLine}");
// Usage: Display all sections and values
// Note: test.ini contains samples of duplicates and bad values to show how reader is "fail-safe"
ifr.DisplayAllSections();