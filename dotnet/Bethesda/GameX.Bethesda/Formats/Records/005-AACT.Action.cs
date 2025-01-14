﻿using System.IO;

namespace GameX.Bethesda.Formats.Records
{
    public class AACTRecord : Record
    {
        public override string ToString() => $"AACT: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public CREFField CNAME; // RGB color

        public override bool CreateField(BinaryReader r, BethesdaFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "EDID": EDID = r.ReadSTRV(dataSize); return true;
                case "CNAME": CNAME = r.ReadS2<CREFField>(size: dataSize); return true;
                default: return false;
            }
        }
    }
}