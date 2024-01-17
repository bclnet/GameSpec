﻿//#define Arkane
//#define Bethesda
//#define Bioware
//#define Black
//#define Blizzard
//#define Capcom
//#define Cig
#define Cryptic
//#define Crytek
//#define Cyanide
//#define Epic
//#define Frictional
//#define Frontier
//#define Id
//#define IW
//#define Monolith
//#define Origin
//#define Red
//#define Ubisoft
//#define Unity
//#define Valve
//#define WbB

using System.Collections.Generic;

namespace GameSpec
{
    /// <summary>
    /// Default Options for Applications.
    /// </summary>
    public class DefaultOptions
    {
        public string Family { get; set; }
        public string Game { get; set; }
        public string Edition { get; set; }
        public string ForcePath { get; set; }
        public bool ForceOpen { get; set; }
    }

    public partial class FamilyManager
    {
#if Arkane
        static string[] FamilyKeys = new[] { "Arkane", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:1",
            Family = "Arkane",
            Game = "AF", // Arx Fatalis [open, read, texture:GL]
            //Game = "DOM", // Dark Messiah of Might and Magic [open, read]
            //Game = "D", // Dishonored [unreal]
            //Game = "D2", // Dishonored 2 [open, read]
            //Game = "P", // Prey [open, read]
            //Game = "D:DOTO", // Dishonored: Death of the Outsider
            //Game = "W:YB", // Wolfenstein: Youngblood
            //Game = "W:CP", // Wolfenstein: Cyberpilot
            //Game = "DL", // Deathloop
            //Missing: Game = "RF", // Redfall (future)
        };
#elif Bethesda
        static string[] FamilyKeys = new[] { "Bethesda", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Bethesda",
            Game = "Morrowind", // The Elder Scrolls III: Morrowind
            //Game = "Oblivion", // The Elder Scrolls IV: Oblivion
            //Game = "Fallout3", // Fallout 3
            //Game = "FalloutNV", // Fallout New Vegas
            //Game = "Skyrim", // The Elder Scrolls V: Skyrim
            //Game = "Fallout4", // Fallout 4
            //Game = "SkyrimSE", // The Elder Scrolls V: Skyrim – Special Edition
            //Game = "Fallout:S", // Fallout Shelter
            //Game = "Fallout4VR", // Fallout 4 VR
            //Game = "SkyrimVR", // The Elder Scrolls V: Skyrim VR
            //Game = "Fallout76", // Fallout 76
            //Game = "Starfield", // Starfield (future)
        };
#elif Bioware
        static string[] FamilyKeys = new[] { "Bioware", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Bioware",
            //Game = "SWTOR", // Star Wars: The Old Republic
            //Game = "NWN", // Neverwinter Nights
            //Game = "NWN2", // Neverwinter Nights 2
            //Game = "KotOR", // Star Wars: Knights of the Old Republic
        };
#elif Black
        static string[] FamilyKeys = new[] { "Black", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Black",
            //Game = "Fallout", // Fallout
            //Game = "Fallout2", // Fallout 2
        };
#elif Blizzard
        static string[] FamilyKeys = new[] { "Blizzard", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Blizzard",
            //Game = "SC", // StarCraft
            //Game = "D2R", // Diablo II: Resurrected
            //Missing: Game = "W3", // Warcraft III: Reign of Chaos
            //Game = "WOW", // World of Warcraft
            //Missing: Game = "WOWC", // World of Warcraft: Classic
            //Game = "SC2", // StarCraft II: Wings of Liberty
            //Game = "D3", // Diablo III
            //Game = "HS", // Hearthstone
            //Game = "HOTS", // Heroes of the Storm
            //Game = "DI", // Diablo Immortal
            //Game = "OW2", // Overwatch 2
            //Missing: Game = "D4", // Diablo IV
        };
#elif Capcom
        static string[] FamilyKeys = new[] { "Capcom", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Capcom",
            Game = "CAS", // [Kpka] Capcom Arcade Stadium
            //Game = "Fighting:C", // [] Capcom Fighting Collection
            //Game = "GNG:R", // Ghosts 'n Goblins Resurrection
            //Game = "MM:LC", // Mega Man Legacy Collection
            //Game = "MM:LC2", // Mega Man Legacy Collection 2
            //Game = "MM:XD", // Mega Man X DiVE [Unity]
            //Game = "MMZX:LC", // Mega Man Zero/ZX Legacy Collection
            //Game = "MHR", // Monster Hunter Rise
            //Game = "MH:S2", // Monster Hunter Stories 2: Wings of Ruin

            //Game = "PWAA:T", // Phoenix Wright: Ace Attorney Trilogy
            //Game = "RDR2", // Red Dead Redemption 2
            //Game = "RER", // Resident Evil Resistance
            //Game = "RE:RV", // Resident Evil Re:Verse

            //Game = "Disney:AC", // The Disney Afternoon Collection
            //Game = "TGAA:C", // The Great Ace Attorney Chronicles
            //Game = "USF4", // Ultra Street Fighter IV

            //Game = "BionicCommando", // Bionic Commando (2009 video game)
            //Game = "BionicCommando:R", // Bionic Commando Rearmed
            //Game = "Arcade:S", // Capcom Arcade 2nd Stadium
            //Game = "BEU:B", // Capcom Beat 'Em Up Bundle
            //Game = "DV", // Dark Void
            //Game = "DV:Z", // Dark Void Zero
            //Game = "DR", // Dead Rising
            //Game = "DR2", // Dead Rising 2
            //Game = "DR2:OtR", // Dead Rising 2: Off the Record
            //Game = "DR3", // Dead Rising 3
            //Game = "DR4", // Dead Rising 4
            //Game = "DMC3:S", // XX
            //Game = "DMC4:S", // XX
            //Game = "DMC5", // XX
            //Game = "DMC:HD", // XX
            //Game = "DMC:DMC", // XX
            //Game = "Dragon", // XX
            //Game = "DT:R", // XX
        };
#elif Cig
        static string[] FamilyKeys = new[] { "Cig", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            //ForcePath = "app:DataForge",
            //ForcePath = "app:StarWords",
            //ForcePath = "app:Subsumption",

            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Cig",
            Game = "StarCitizen", // Star Citizen
        };
#elif Cryptic
        static string[] FamilyKeys = new[] { "Cryptic", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:0",
            Family = "Cryptic",
            Game = "CO", // Champions Online [open, read]
            //Game = "STO", // Star Trek Online [open, read]
            //Game = "NVW", // Neverwinter [open, read]
        };
#elif Crytek
        static string[] FamilyKeys = new[] { "Crytek", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Crytek",
            //Game = "ArcheAge", // ArcheAge
            //Game = "Hunt", // Hunt: Showdown
            //Game = "MWO", // MechWarrior Online
            //Game = "Warface", // Warface
            //Game = "Wolcen", // Wolcen: Lords of Mayhem
            //Game = "Crysis", // Crysis Remastered
            //Game = "Ryse", // Ryse: Son of Rome
            //Game = "Robinson", // Robinson: The Journey
            //Game = "Snow", // SNOW - The Ultimate Edition
        };
#elif Cyanide
        static string[] FamilyKeys = new[] { "Cyanide", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Cyanide",
            //Game = "Council", // Council
            //Game = "Werewolf:TA", // Werewolf: The Apocalypse - Earthblood
        };
#elif Epic
        static string[] FamilyKeys = new[] { "Epic", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Epic",
            Game = "UE1", // Unreal
            //Game = "BioShock", // BioShock
            //Game = "BioShockR", // BioShock Remastered
            //Game = "BioShock2", // BioShock 2
            //Game = "BioShock2R", // BioShock 2 Remastered
            //Game = "BioShock:Inf", // BioShock Infinite
        };
#elif Frictional
        static string[] FamilyKeys = new[] { "Frictional", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Frictional",
            //Game = "P:O", // Penumbra: Overture
            //Game = "P:BP", // Penumbra: Black Plague
            //Game = "P:R", // Penumbra: Requiem
            //Game = "A:TDD", // Amnesia: The Dark Descent
            //Game = "A:AMFP", // Amnesia: A Machine for Pigs
            //Game = "SOMA", // SOMA
            //Game = "A:R", // Amnesia: Rebirth
        };
#elif Frontier
        static string[] FamilyKeys = new[] { "Frontier", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Frontier",
            Game = "ED"
        };
#elif Id
        static string[] FamilyKeys = new[] { "Id", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Id",
            Game = "Q", // Quake
            //Game = "Q2", // Quake II
            //Game = "Q3:A", // Quake III Arena
            //Game = "D3", // Doom 3
            //Game = "Q:L", // Quake Live
            //Game = "R", // Rage
            //Game = "D", // Doom
            //Game = "D:VFR", // Doom VFR
            //Game = "R2", // Rage 2
            //Game = "D:E", // Doom Eternal
            //Game = "Q:C", // Quake Champions
        };
#elif IW
        static string[] FamilyKeys = new[] { "IW", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "IW",
            //Game = "COD2", // Call of Duty 2 - IWD 
            //Game = "COD3", // Call of Duty 3 - XBOX only
            //Game = "COD4", // Call of Duty 4: Modern Warfare - IWD, FF
            //Game = "COD:WaW", // Call of Duty: World at War - IWD, FF
            //Game = "MW2", // Call of Duty: Modern Warfare 2
            //Game = "COD:BO", // Call of Duty: Black Ops - IWD, FF
            //Game = "MW3", // Call of Duty: Call of Duty: Modern Warfare 3
            //Game = "COD:BO2", // Call of Duty: Black Ops 2 - FF
            //Game = "COD:AW", // Call of Duty: Advanced Warfare
            //Game = "COD:BO3", // Call of Duty: Black Ops III - XPAC,FF
            //Game = "MW3", // Call of Duty: Modern Warfare 3
            //Game = "WWII", // Call of Duty: WWII

            Game = "BO4", // Call of Duty Black Ops 4
            //Game = "BOCW", // Call of Duty Black Ops Cold War
            //Game = "Vanguard", // Call of Duty Vanguard
        };
#elif Monolith
        static string[] FamilyKeys = new[] { "Monolith", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Monolith",
            //Game = "FEAR", // F.E.A.R.
            //Game = "FEAR:EP", // F.E.A.R.: Extraction Point
            //Game = "FEAR:PM", // F.E.A.R.: Perseus Mandate
            //Game = "FEAR2", // F.E.A.R. 2: Project Origin
            //Game = "FEAR3", // F.E.A.R. 3
        };
#elif Origin
        static string[] FamilyKeys = new[] { "Origin", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Origin",
            //Game = "UO", // Ultima Online
            //Game = "UltimaIX", // Ultima IX
        };
#elif Red
        static string[] FamilyKeys = new[] { "Red", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Red",
            //Game = "Witcher", // The Witcher Enhanced Edition
            //Game = "Witcher2", // The Witcher 2
            //Game = "Witcher3", // The Witcher 3: Wild Hunt
            //Game = "CP77", // Cyberpunk 2077
            //Game = "Witcher4", // The Witcher 4 Polaris (future)
        };
#elif Ubisoft
        static string[] FamilyKeys = new[] { "Ubisoft", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Ubisoft",
            //Game = "XX", // xx
        };
#elif Unity
        static string[] FamilyKeys = new[] { "Unity", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Unity",
            //Game = "AmongUs", // Among Us
            //Game = "Cities", // Cities: Skylines
            //Game = "Tabletop", // Tabletop Simulator
            //Game = "UBoat", // Destroyer: The U-Boat Hunter
            //Game = "7D2D", // 7 Days to Die
        };
#elif Valve
        static string[] FamilyKeys = new[] { "Valve", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "Valve",
            Game = "HL", // Half-Life [open, read, texture:GL]
            //Game = "TF", // Team Fortress Classic [open, read, texture:GL]
            //Game = "CS", // Counter-Strike [open, read]
            //Game = "Ricochet", // Ricochet [open, read]
            //Game = "HL:BS", // Half-Life: Blue Shift [open, read]
            //Game = "DOD", // Day of Defeat [open, read]
            //Game = "CS:CZ", // Counter-Strike: Condition Zero [open, read]
            //Game = "HL:Src", // Half-Life: Source [open, read]
            //Game = "CS:Src", // Counter-Strike: Source [open, read]
            //Game = "HL2", // Half-Life 2 [open, read]
            //Game = "HL2:DM", // Half-Life 2: Deathmatch [open, read]
            //Game = "HL:DM:Src", // Half-Life Deathmatch: Source [open, read]
            //Game = "HL2:E1", // Half-Life 2: Episode One [open, read]
            //Game = "Portal", // Portal [open, read]
            //Game = "HL2:E2", // Half-Life 2: Episode Two [open]
            //Game = "TF2", // Team Fortress 2 [open, read]
            //Game = "L4D", // Left 4 Dead [open, read]
            //Game = "L4D2", // Left 4 Dead 2 [open, read]
            //Game = "DOD:Src", // Day of Defeat: Source [open, read]
            //Game = "Portal2", // Portal 2 [open, read]
            //Game = "CS:GO", // Counter-Strike: Global Offensive [open, read]
            //Game = "D2", // Dota 2 [open, read, texture:GL, model:GL]
            //Game = "TheLab:RR", // The Lab: Robot Repair [open, read, texture:GL, model:GL]
            //Game = "TheLab:SS", // The Lab: Secret Shop [!unity]
            //Game = "TheLab:TL", // The Lab: The Lab [!unity]
            //Game = "HL:Alyx", // Half-Life: Alyx [open, read, texture:GL, model:GL]
        };
#elif WbB
        static string[] FamilyKeys = new[] { "WbB", "Unknown" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
            ForceOpen = true,
            ForcePath = "sample:*",
            Family = "WbB",
            Game = "AC", // Asheron's Call [open, read, texture:GL]
        };
#else
        static string[] FamilyKeys = new[] { "Arkane", "Bethesda", "Bioware", "Black", "Blizzard", "Capcom", "Cig", "Cryptic", "Crytek", "Cyanide", "Epic", "Frictional", "Frontier", "Id", "IW", "Monolith", "Origin", "Red", "Ubisoft", "Unity", "Unknown", "Valve", "WbB" };

        public static DefaultOptions AppDefaultOptions = new DefaultOptions
        {
        };
#endif
    }
}
