using System;

namespace InterPlanetaryPing;

public struct GlobalConsts
{
    public static string CONFIG_FILE_DBG = Path.Combine(Environment.CurrentDirectory, "config.toml");
    public static string CONFIG_FILE = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "InterPlanetaryPing",
        "config.toml");

}

