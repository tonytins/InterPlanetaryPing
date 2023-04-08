using System;

namespace InterPlanetaryPing
{
    public class Config
    {
        [Option('c', "cid")] public string CID { get; set; }
        [Option('d', "delay")] public int Delay { get; set; }

    }
}