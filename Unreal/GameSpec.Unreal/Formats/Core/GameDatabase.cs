﻿using static GameSpec.Unreal.Formats.Core.GameDatabase;
using static GameSpec.Unreal.Formats.Core.Game;
using static GameSpec.Unreal.Formats.Core.VERUE4;
using System;

namespace GameSpec.Unreal.Formats.Core
{
    public enum Game
    {
        UNKNOWN = 0,           // should be 0
        //
        UE1 = 0x0100000,
        Undying,
        //
        UE2 = 0x0200000,
        UT2,
        Pariah,
        SplinterCell,
        SplinterCellConv,
        Lineage2,
        Exteel,
        Ragnarok2,
        RepCommando,
        Loco,
        BattleTerr,
        UC1,               // note: not UE2X
        XIII,
        Vanguard,
        AA2,
        EOS,
        //
        VENGEANCE = 0x0210000, // variant of UE2
        Tribes3,
        Swat4,             // not autodetected, overlaps with Tribes3
        Bioshock,
        //
        LEAD = 0x0220000,
        //
        UE2X = 0x0400000,
        UC2,
        //
        UE3 = 0x0800000,
        EndWar,
        MassEffect,
        MassEffect2,
        MassEffect3,
        MassEffectLE,
        R6Vegas2,
        MirrorEdge,
        TLR,
        Huxley,
        Turok,
        Fury,
        XMen,
        MagnaCarta,
        ArmyOf2,
        CrimeCraft,
        X50Cent,
        AVA,
        Frontlines,
        Batman,
        Batman2,
        Batman3,
        Batman4,
        Borderlands,
        AA3,
        DarkVoid,
        Legendary,
        Tera,
        BladeNSoul,
        APB,
        AlphaProtocol,
        Transformers,
        MortalOnline,
        Enslaved,
        MOHA,
        MOH2010,
        Berkanix,
        DOH,
        DCUniverse,
        Bulletstorm,
        Undertow,
        Singularity,
        Tron,
        Hunted,
        DND,
        ShadowsDamned,
        Argonauts,
        SpecialForce2,
        GunLegend,
        TaoYuan,
        Tribes4,
        Dishonored,
        Hawken,
        Fable,
        DmC,
        PLA,
        AliensCM,
        GoWJ,
        GoWU,
        Bioshock3,
        RememberMe,
        MarvelHeroes,
        LostPlanet3,
        XcomB,
        Xcom2,
        Thief4,
        Murdered,
        SOV,
        VEC,
        Dust514,
        Guilty,
        Alice,
        DunDef,
        Gigantic,
        MetroConflict,
        Smite,
        DevilsThird,
        RocketLeague,
        GRAV,
        //
        MIDWAY3 = 0x0810000,   // variant of UE3
        A51,
        Wheelman,
        MK,
        Strangle,
        TNA,
        //
        UE4_BASE = 0x1000000,
        // bytes: 01.00.0N.NX : 01=UE4, 00=masked by ENGINE, NN=UE4 subversion, X=game (4 bits, 0=base engine)
        // Add custom UE4 game engines here
        Ark = UE4_BASE + 5 << 4 + 1, // 4.5
        FableLegends = UE4_BASE + 6 << 4 + 1, // 4.6
        HIT = UE4_BASE + 8 << 4 + 1, // 4.8
        SeaOfThieves = UE4_BASE + 10 << 4 + 1, // 4.10
        Gears4 = UE4_BASE + 11 << 4 + 1, // 4.11
        DaysGone = UE4_BASE + 11 << 4 + 2, // 4.11
        Lawbreakers = UE4_BASE + 13 << 4 + 1, // 4.13
        StateOfDecay2 = UE4_BASE + 13 << 4 + 2,
        Tekken7 = UE4_BASE + 14 << 4 + 2, // 4.14
        NGB = UE4_BASE + 16 << 4 + 1, // 4.16
        UT4 = UE4_BASE + 16 << 4 + 2, // 4.16
        LIS2 = UE4_BASE + 17 << 4 + 1, // 4.17
        KH3 = UE4_BASE + 17 << 4 + 2, // 17..18 (16 - crash anim, 19 - new SkelMesh format, not matching)
        AscOne = UE4_BASE + 18 << 4 + 1, // 4.18
        Paragon = UE4_BASE + 19 << 4 + 1, // 4.19
        Borderlands3 = UE4_BASE + 20 << 4 + 1, // 4.20
        Jedi = UE4_BASE + 21 << 4 + 1, // 4.21
        UE4_25_Plus = UE4_BASE + 25 << 4 + 1, // 4.25
        Dauntless = UE4_BASE + 26 << 4 + 1, // 4.26
    }

    public enum Platform
    {
        UNKNOWN = 0,
        PC,
        XBOX360,
        PS3,
        PS4,
        SWITCH,
        IOS,
        ANDROID,
        COUNT,
    }

    static class GameDatabase
    {
        public const int LATEST_UE4_VERSION = 27; // UE4.XX
        public static Game GAME_UE4(int x) => UE4_BASE + (x << 4);
        public static int GAME_UE4_GET_MINOR(Game x) => (x - UE4_BASE) >> 4;	// reverse operation for GAME_UE4(x)
        public const int GAME_ENGINE = 0xFFF0000; // mask for game engine

        // Note: there's no conflicts between UE3 and UE4 flags - some obsoleve UE3 flags are marked as unused in UE4,
        // and some UE4 flags are missing in UE3 just because bitmask is not fully used there.
        // UE3 flags
        public const uint PKG_Cooked = 0x00000008;       // UE3
        public const uint PKG_StoreCompressed = 0x02000000;      // UE3, deprecated in UE4.16

        // UE4 flags
        public const uint PKG_UnversionedProperties = 0x00002000;        // UE4.25+
        public const uint PKG_FilterEditorOnly = 0x80000000;		// UE4

        readonly static (string, string, Game)[] GListOfGames = {
		#region Unreal engine 1
			("Unreal engine 1", "ue1", UE1),
            ("Unreal 1", null, UE1),
            ("Unreal Tournament 1 (UT99)", null, UE1),
            ("The Wheel of Time", null, UE1),
            ("DeusEx", null, UE1),
            ("Rune", null, UE1),
            ("Undying", "undying", Undying),
		#endregion
		#region Unreal Engine 2
			("Unreal engine 2", "ue2", UE2),
            ("Unreal Tournament 2003,2004", "ut2", UT2),
            ("Unreal Championship", "uc1", UC1),
            ("Splinter Cell 1-4", "scell", SplinterCell),
            ("Splinter Cell: Conviction", "scconv", SplinterCellConv),
            ("Lineage 2", "l2", Lineage2),
            ("Land of Chaos Online (LOCO)", "loco", Loco),
            ("Battle Territory Online", "bterr", BattleTerr),
            ("Star Wars: Republic Commando", "swrc", RepCommando),
            ("XIII", "xiii", XIII),
		#endregion
		#region Unreal Engine 2.5
			("UE2Runtime", null, UE2),
            ("Tribes: Vengeance", "t3", Tribes3),
            ("SWAT 4", "swat4", Swat4),
            ("Bioshock, Bioshock 2", "bio", Bioshock),
            ("Ragnarok Online 2", "rag2", Ragnarok2),
            ("Exteel", "extl", Exteel),
            ("America's Army 2", "aa2", AA2),
            ("Vanguard: Saga of Heroes", "vang", Vanguard),
            ("Echo of Soul", "eos", EOS),
            ("Killing Floor", null, UE2),
		#endregion
		#region Unreal Engine 2X
			("Unreal Championship 2: The Liandri Conflict", "uc2", UC2),
        #endregion
        #region Unreal engine 3
		    ("Unreal engine 3", "ue3", UE3),
            ("Unreal Tournament 3", null, UE3),
            ("Gears of War", null, UE3),
#if _XBOX360_
		    ("Gears of War 2", null, UE3),
		    ("Gears of War 3", null, UE3),
		    ("Gears of War: Judgment", "gowj", GoWJ),
#endif
		    ("Gears of War: Ultimate", "gowu", GoWU),
#if _IPHONE_
		    ("Infinity Blade", null, UE3),
#endif
		    ("Bulletstorm", "bs", Bulletstorm),
            ("EndWar", "endwar", EndWar),
            ("Rainbow 6: Vegas 2", "r6v2", R6Vegas2),
            ("Mass Effect",   "mass",  MassEffect ),
            ("Mass Effect 2", "mass2", MassEffect2),
            ("Mass Effect 3", "mass3", MassEffect3),
            ("Mass Effect Legendary Edition", "massl", MassEffectLE),
            ("BlackSite: Area 51", "a51", A51),
            ("Mortal Kombat vs. DC Universe", "mk", MK),
            ("Mortal Kombat", "mk", MK),
            ("Injustice: Gods Among Us", "mk", MK),
            ("Mortal Kombat X", "mk", MK),
            ("Turok", "turok", Turok),
            ("Fury", "fury", Fury),
            ("TNA iMPACT!", "tna", TNA),
            ("WWE All Stars", "tna", TNA),
            ("Stranglehold", "strang", Strangle),
            ("Army of Two", "ao2", ArmyOf2),
            ("Destroy All Humans", "doh", DOH),
            ("Huxley", "huxley", Huxley),
            ("The Last Remnant", "tlr", TLR),
            ("Mirror's Edge", "medge", MirrorEdge),
            ("X-Men Origins: Wolverine", "xmen", XMen),
            ("Magna Carta 2", "mcarta", MagnaCarta),
            ("Batman: Arkham Asylum",  "batman",  Batman),
            ("Batman: Arkham City",    "batman2", Batman2),
            ("Batman: Arkham Origins", "batman3", Batman3),
            ("Batman: Arkham Knight",  "batman4", Batman4),
            ("Crime Craft", "crime", CrimeCraft),
            ("AVA Online", "ava", AVA),
            ("Frontlines: Fuel of War", "frontl", Frontlines),
            ("Homefront",               "frontl", Frontlines),
            ("50 Cent: Blood on the Sand", "50cent", X50Cent),
            ("Borderlands",   "border", Borderlands),
            ("Borderlands 2", "border", Borderlands),
            ("Brothers in Arms: Hell's Highway", "border", Borderlands),
            ("Aliens: Colonial Marines", "acm", AliensCM),
            ("Dark Void", "darkv", DarkVoid),
            ("Legendary: Pandora's Box", "leg", Legendary),
            ("TERA: The Exiled Realm of Arborea", "tera", Tera),
            ("Blade & Soul", "bns", BladeNSoul),
            ("Alpha Protocol", "alpha", AlphaProtocol),
            ("All Points Bulletin", "apb", APB),
            ("The Bourne Conspiracy",           "trans", Transformers),
            ("Transformers: War for Cybertron", "trans", Transformers),
            ("Transformers: Dark of the Moon",  "trans", Transformers),
            ("Transformers: Fall of Cybertron", "trans", Transformers),
            ("America's Army 3", "aa3", AA3),
            ("Mortal Online", "mo", MortalOnline),
            ("Enslaved: Odyssey to the West", "ens", Enslaved),
            ("Medal of Honor: Airborne", "moha", MOHA),
            ("Medal of Honor 2010", "moh2010", MOH2010),
            ("Alice: Madness Returns", "alice", Alice),
            ("Berkanix", "berk", Berkanix),
            ("Undertow", "undertow", Undertow),
            ("Singularity", "sing", Singularity),
            ("Nurien", null, UE3),
            ("Hunted: The Demon's Forge", "hunt", Hunted),
            ("Dungeons & Dragons: Daggerdale", "dnd", DND),
            ("Shadows of the Damned", "shad", ShadowsDamned),
            ("Rise of the Argonauts", "argo", Argonauts),
            ("Thor: God of Thunder",  "argo", Argonauts),
            ("Gunslayer Legend", "gunsl", GunLegend),
            ("Special Force 2", "sf2", SpecialForce2),
            ("Tribes: Ascend", "t4", Tribes4),
            ("Dishonored", "dis", Dishonored),
            ("Fable: The Journey", "fable", Fable),
            ("Fable Anniversary",  "fable", Fable),
            ("DmC: Devil May Cry", "dmc", DmC),
            ("Hawken", null, UE3),
            ("Passion Leads Army", "pla", PLA),
            ("Tao Yuan", "taoyuan", TaoYuan),
            ("Bioshock Infinite", "bio3", Bioshock3),
            ("Remember Me", "rem", RememberMe),
            ("Life is Strange", "rem", RememberMe),
            ("Marvel Heroes", "mh", MarvelHeroes),
            ("Lost Planet 3", "lp3", LostPlanet3),
            ("Yaiba: Ninja Gaiden Z", "lp3", LostPlanet3),
            ("The Bureau: XCOM Declassified", "xcom", XcomB),
            ("XCOM 2", "xcom2", Xcom2),
            ("Thief", "thief4", Thief4),
            ("Murdered: Soul Suspect", "murd", Murdered),
            ("Seal of Vajra", "sov", SOV),
            ("The Vanishing of Ethan Carter", "vec", VEC),
            ("Dust 514", "dust514", Dust514),
            ("Guilty Gear Xrd", "guilty", Guilty),
            ("Dungeon Defenders", "dundef", DunDef),
            ("Gigantic", "gigantic", Gigantic),
            ("Metro Conflict", "metroconf", MetroConflict),
            ("SMITE", "smite", Smite),
            ("Devil's Third", "dev3rd", DevilsThird),
            ("Rocket League", "rocketleague", RocketLeague),
            ("GRAV", "grav", GRAV),
		#endregion
		#region Unreal engine 4
		    // Dummy tag for all UE4 versions
            ($"Unreal engine 4.0-4.{LATEST_UE4_VERSION}", $"ue4.[0-{LATEST_UE4_VERSION}]", GAME_UE4(LATEST_UE4_VERSION+1)), // some invalid version number, but not zero
            ("Unreal engine 4.25 Plus", "ue4.25+", UE4_25_Plus),
             // Add custom UE4 versions here
            ("Gears of War 4", "gears4", Gears4),
            ("Days Gone", "daysgone", DaysGone),
            ("Ark: Survival Evolved", "ark", Ark),
            ("Tekken 7", "tekken7", Tekken7),
            ("Lawbreakers", "lawbr", Lawbreakers),
            ("State of Decay 2", "sod2", StateOfDecay2),
            ("Dauntless", "dauntless", Dauntless),
            ("Paragon", "paragon", Paragon),
            ("Unreal Tournament 4", "ut4", UT4),
            ("Heroes of Incredible Tales", "hit", HIT),
            ("New Gundam Breaker", "ngb", NGB),
            ("Life is Strange 2", "lis2", LIS2),
            ("Ascendant One", "asc1", AscOne),
            ("Borderlands 3", "border3", Borderlands3),
            ("Kingdom Hearts 3", "kh3", KH3),
            ("Star Wars Jedi: Fallen Order", "jedi", Jedi),
            ("Fable Legends", "fablel", FableLegends),
            ("Sea of Thieves", "sot", SeaOfThieves),
        #endregion
        };
    }

    partial class UPackage
    {
        const int PACKAGE_V2 = 100;
        const int PACKAGE_V3 = 180;

        public void DetectGame()
        {
            if (GForcePlatform != Platform.UNKNOWN) Platform = GForcePlatform;
            if (GForceGame != UNKNOWN) { SetGame(GForceGame); return; }

            // check if already detected game requires some additional logic
            if (Game == Lineage2)
            {
                if (ArLicenseeVer >= 1000) SetGame(Exteel);   // lineage LicenseeVer < 1000, exteel >= 1000
                return;
            }
            if (Game == UE4_BASE)
            {
                // Detection for UE4 games
                if (ArVer == 415 && ArLicenseeVer == 17) SetGame(FableLegends); // UE4 game
                CheckGameCollision();
                return;
            }
            // skip autodetection when Ar.Game is explicitly set by SerializePackageFileSummary, when code detects custom package tag
            if (Game != UNKNOWN) return;
            #region UE2 games
            // Digital Extremes games
            if (((ArVer >= 117 && ArVer <= 119) && (ArLicenseeVer >= 25 && ArLicenseeVer <= 27)) ||
                  (ArVer == 120 && (ArLicenseeVer == 27 || ArLicenseeVer == 28)) ||
                 ((ArVer >= 121 && ArVer <= 128) && ArLicenseeVer == 29)) SetGame(UT2);
            if (ArVer == 119 && ArLicenseeVer == 0x9127) SetGame(Pariah);
            if (ArVer == 119 && (ArLicenseeVer == 28 || ArLicenseeVer == 30)) SetGame(UC1);
            if (ArVer == 151 && (ArLicenseeVer == 0 || ArLicenseeVer == 1)) SetGame(UC2);
            if ((ArVer >= 131 && ArVer <= 134) && ArLicenseeVer == 29) SetGame(Loco);

            if ((ArVer == 100 && (ArLicenseeVer >= 9 && ArLicenseeVer <= 17)) ||        // Splinter Cell 1
                 (ArVer == 102 && (ArLicenseeVer >= 29 && ArLicenseeVer <= 28))) SetGame(SplinterCell);       // Splinter Cell 2

            if (ArLicenseeVer == 1 && ((ArVer >= 133 && ArVer <= 148) || (ArVer >= 154 && ArVer <= 159))) SetGame(RepCommando);

            if (((ArVer == 129 || ArVer == 130) && (ArLicenseeVer >= 0x17 && ArLicenseeVer <= 0x1B)) ||
                 ((ArVer == 123) && (ArLicenseeVer >= 3 && ArLicenseeVer <= 0xF)) ||
                 ((ArVer == 126) && (ArLicenseeVer >= 0x12 && ArLicenseeVer <= 0x17))) SetGame(Tribes3);

            if ((ArVer == 141 && (ArLicenseeVer == 56 || ArLicenseeVer == 57)) || //?? Bioshock and Bioshock 2
                 (ArVer == 142 && ArLicenseeVer == 56) ||                   // Bioshock Remastered
                 (ArVer == 143 && ArLicenseeVer == 59)) SetGame(Bioshock); // Bioshock 2 multiplayer, Bioshock 2 Remastered
            #endregion
            #region UE3 games
            // most UE3 games has single version for all packages
            // here is a list of such games, sorted by version
            if (ArVer == 241 && ArLicenseeVer == 71) SetGame(R6Vegas2);
            //if (ArVer == 329 && ArLicenseeVer == 0)		SetGame(EndWar);	// LicenseeVer == 0
            if (ArVer == 375 && ArLicenseeVer == 25) SetGame(Strangle);   //!! has extra tag
            if (ArVer == 377 && ArLicenseeVer == 25) SetGame(A51);        //!! has extra tag
            if (ArVer == 390 && ArLicenseeVer == 32) SetGame(Wheelman);   //!! has extra tag
            if (ArVer == 407 && (ArLicenseeVer == 26 || ArLicenseeVer == 36)) SetGame(Fury);
            if (ArVer == 421 && ArLicenseeVer == 11) SetGame(MOHA);
            //if (ArVer == 435 && ArLicenseeVer == 0)		SetGame(Undertow);	// LicenseeVer==0!
            if (ArVer == 446 && ArLicenseeVer == 25) SetGame(MagnaCarta);
            if (ArVer == 451 && (ArLicenseeVer >= 52 || ArLicenseeVer <= 53)) SetGame(AVA);
            if (ArVer == 455 && ArLicenseeVer == 90) SetGame(DOH);
            if (ArVer == 507 && ArLicenseeVer == 11) SetGame(TLR);
            if (ArVer == 536 && ArLicenseeVer == 43) SetGame(MirrorEdge);
            if (ArVer == 538 && ArLicenseeVer == 73) SetGame(X50Cent);
            if (ArVer == 539 && (ArLicenseeVer == 43 || ArLicenseeVer == 47)) SetGame(Argonauts); // Rise of the Argonauts, Thor: God of Thunder
            if (ArVer == 539 && ArLicenseeVer == 91) SetGame(AlphaProtocol);
            if (ArVer == 547 && (ArLicenseeVer == 31 || ArLicenseeVer == 32)) SetGame(APB);
            if (ArVer == 567 && ArLicenseeVer == 39) SetGame(Legendary);
            //	if (ArVer == 568 && ArLicenseeVer == 0)		SetGame(AA3);	//!! LicenseeVer == 0 ! bad!
            if (ArVer == 568 && ArLicenseeVer == 101) SetGame(XMen);
            if (ArVer == 576 && ArLicenseeVer == 5) SetGame(CrimeCraft);
            if (ArVer == 576 && ArLicenseeVer == 21) SetGame(Batman);
            if (ArVer == 576 && (ArLicenseeVer == 61 || ArLicenseeVer == 66)) SetGame(DarkVoid); // demo and release
            if (ArVer == 581 && ArLicenseeVer == 58) SetGame(MOH2010);
            if (ArVer == 584 && ArLicenseeVer == 126) SetGame(Singularity);
            if (ArVer == 648 && ArLicenseeVer == 3) SetGame(Tron);
            if (ArVer == 648 && ArLicenseeVer == 6405) SetGame(DCUniverse);
            if (ArVer == 673 && ArLicenseeVer == 2) SetGame(Enslaved);
            if (ArVer == 678 && ArLicenseeVer == 32771) SetGame(MortalOnline);
            if (ArVer == 690 && ArLicenseeVer == 0) SetGame(Alice); // only this game has LicenseeVer==0 here!
            if (ArVer == 706 && ArLicenseeVer == 28) SetGame(ShadowsDamned);
            if (ArVer == 708 && ArLicenseeVer == 35) SetGame(Dust514);
            if (ArVer == 721 && ArLicenseeVer == 148) SetGame(Thief4);
            if (ArVer == 727 && ArLicenseeVer == 75) SetGame(Bioshock3);
            if (ArVer == 742 && ArLicenseeVer == 29) SetGame(Bulletstorm);
            if (ArVer == 787 && ArLicenseeVer == 47) SetGame(AliensCM);
            if (ArVer == 801 && ArLicenseeVer == 30) SetGame(Dishonored);
            if (ArVer == 805 && ArLicenseeVer == 2) SetGame(Tribes4);
            if (ArVer == 805 && ArLicenseeVer == 101) SetGame(Batman2);
            if ((ArVer == 806 || ArVer == 807) && (ArLicenseeVer == 103 || ArLicenseeVer == 137 || ArLicenseeVer == 138)) SetGame(Batman3);
            if (ArVer == 863 && ArLicenseeVer == 32995) SetGame(Batman4);
            if (ArVer == 845 && ArLicenseeVer == 4) SetGame(DmC);
            if (ArVer == 845 && (ArLicenseeVer >= 101 && ArLicenseeVer <= 107)) SetGame(Xcom2);
            if (ArVer == 849 && ArLicenseeVer == 32795) SetGame(XcomB);
            if ((ArVer == 850 || ArVer == 860) && (ArLicenseeVer == 1017 || ArLicenseeVer == 26985)) SetGame(Fable); // 850 = Fable: The Journey, 860 = Fable Anniversary
            if (ArVer == 860 && ArLicenseeVer == 93) SetGame(Murdered);
            if (ArVer == 860 && (ArLicenseeVer == 97 || ArLicenseeVer == 98)) SetGame(LostPlanet3); // 97 = Lost Planet 3, 98 = Yaiba: Ninja Gaiden Z
            if (ArVer == 868 && ArLicenseeVer == 2) SetGame(Guilty);
            if (ArVer == 868 && (ArLicenseeVer >= 18 && ArLicenseeVer <= 22)) SetGame(RocketLeague);
            if (ArVer == 904 && (ArLicenseeVer == 9 || ArLicenseeVer == 14)) SetGame(SpecialForce2);

            // UE3 games with the various versions of files
            if ((ArVer == 374 && ArLicenseeVer == 16) ||
                (ArVer == 375 && ArLicenseeVer == 19) ||
                (ArVer == 392 && ArLicenseeVer == 23) ||
                (ArVer == 393 && (ArLicenseeVer >= 27 && ArLicenseeVer <= 61))) SetGame(Turok);
            if ((ArVer == 380 && ArLicenseeVer == 35) ||        // TNA Impact
                (ArVer == 398 && ArLicenseeVer == 37)) SetGame(TNA); // WWE All Stars //!! has extra tag
            if ((ArVer == 391 && ArLicenseeVer == 92) ||        // XBox 360 version
                (ArVer == 491 && ArLicenseeVer == 1008)) SetGame(MassEffect);     // PC version
            if (ArVer == 512 && ArLicenseeVer == 130) SetGame(MassEffect2);
            if (ArVer == 684 && (ArLicenseeVer == 185 || ArLicenseeVer == 194)) SetGame(MassEffect3); // 185 = demo, 194 = release
            if ((ArVer == 684 && ArLicenseeVer == 171) ||       // ME1 LE
                (ArVer == 684 && ArLicenseeVer == 168) ||       // ME2 LE
                (ArVer == 685 && ArLicenseeVer == 205)) { SetGame(MassEffectLE); GForceGame = MassEffectLE; }        // ME3 LE - // for making non-standard compression flags known
            if ((ArVer == 402 && ArLicenseeVer == 30) ||        //!! has extra tag; MK vs DC
                (ArVer == 472 && ArLicenseeVer == 46) ||       // Mortal Kombat
                (ArVer == 573 && ArLicenseeVer == 49) ||       // Injustice: God Among Us
                (ArVer == 677 && ArLicenseeVer == 157)) SetGame(MK); // Mortal Kombat X
            if ((ArVer == 402 && (ArLicenseeVer == 0 || ArLicenseeVer == 10)) ||    //!! has extra tag
                 (ArVer == 491 && (ArLicenseeVer >= 13 && ArLicenseeVer <= 16)) ||
                 (ArVer == 496 && (ArLicenseeVer >= 16 && ArLicenseeVer <= 23))) SetGame(Huxley);
            if ((ArVer == 433 && ArLicenseeVer == 52) ||        // Frontlines: Fuel of War
                 (ArVer == 576 && ArLicenseeVer == 100)) SetGame(Frontlines); // Homefront
            if ((ArVer == 445 && ArLicenseeVer == 79) ||        // Army of Two
                 (ArVer == 482 && ArLicenseeVer == 222) ||      // Army of Two: the 40th Day
                 (ArVer == 483 && ArLicenseeVer == 4317)) SetGame(ArmyOf2);      // ...
            if ((ArVer == 511 && ArLicenseeVer == 39) ||        // The Bourne Conspiracy
                 (ArVer == 511 && ArLicenseeVer == 145) ||      // Transformers: War for Cybertron (PC version)
                 (ArVer == 511 && ArLicenseeVer == 144) ||      // Transformers: War for Cybertron (PS3 and XBox 360 version)
                 (ArVer == 537 && ArLicenseeVer == 174) ||      // Transformers: Dark of the Moon
                 (ArVer == 846 && ArLicenseeVer == 181)) SetGame(Transformers);       // Transformers: Fall of Cybertron
            if ((ArVer == 512 && ArLicenseeVer == 35) ||        // Brothers in Arms: Hell's Highway
                 (ArVer == 584 && (ArLicenseeVer == 57 || ArLicenseeVer == 58)) || // Borderlands: release and update
                 (ArVer == 832 && ArLicenseeVer == 46)) SetGame(Borderlands); // Borderlands 2
            if ((ArVer == 568 && (ArLicenseeVer >= 9 && ArLicenseeVer <= 10)) ||
                (ArVer == 610 && (ArLicenseeVer >= 13 && ArLicenseeVer <= 14))) SetGame(Tera);
            if ((ArVer == 832 || ArVer == 893) && ArLicenseeVer == 21) SetGame(RememberMe); // Remember Me (832) or Life Is Strange (893)
            if (ArVer == 835 && ArLicenseeVer == 56) { SetGame(GoWU); GForceGame = GoWU; } // Gears of War: Ultimate
            if (ArVer == 867 && ArLicenseeVer == 9) SetGame(Gigantic);
            #endregion
            CheckGameCollision();
            if (Game == UNKNOWN)
            {
                // generic or unknown engine
                if (ArVer < PACKAGE_V2) SetGame(UE1);
                else if (ArVer < PACKAGE_V3) SetGame(UE2);
                else SetGame(UE3);
                // UE4 has version numbering from zero, plus is has "unversioned" packages, so GAME_UE4_BASE is set by
                // FPackageFileSummary serializer explicitly.
            }
        }

        const int OVERRIDE_ME1_LVER = 90;           // real version is 1008, which is greater than LicenseeVersion of Mass Effect 2 and 3
        const int OVERRIDE_TRANSFORMERS3 = 566;         // real version is 846
        const int OVERRIDE_DUNDEF_VER = 685;            // smaller than 686 (for FStaticLODModel)
        const int OVERRIDE_SF2_VER = 700;
        const int OVERRIDE_SF2_VER2 = 710;
        const int OVERRIDE_LIS_VER = 832;			// >= 832 || < 858 (for UMaterial), < 841 (for USkeletalMesh)

        static readonly (Game g, int v)[] ueVersions =
        {
	        // This game uses mostly UE4.13 structures, but has 4.14 package file format. So, game enum
	        // is defined as GAME_UE4(13), but we're defining package version 4.14.
	        (Lawbreakers, (int)X14),
            (DaysGone, 499),					// 500 = VER_UE4_INNER_ARRAY_TAG_INFO, but it's not here
	        (EndWar, 224),
            (Tera, 568),
            (Hunted, 708),						// real version is 709, which is incorrect
	        (DND, 673),						// real version is 674
	        (GoWJ, 828),						// real version is 846
	        (GoWU, 614),					// real version is 835, version is clamped by FStaticMeshUVItem3
        };

        static readonly int[] ue4Versions =
        {
            (int)X0, (int)X1, (int)X2, (int)X3, (int)X4,
            (int)X5, (int)X6, (int)X7, (int)X8, (int)X9,
            (int)X10, (int)X11, (int)X12, (int)X13, (int)X14,
            (int)X15, (int)X16, (int)X17, (int)X18, (int)X19,
            (int)X20, (int)X21, (int)X22, (int)X23, (int)X24,
            (int)X25, (int)X26, (int)X27,
	        // NEW_ENGINE_VERSION
        };

        public void OverrideVersion()
        {
            if (GForcePackageVersion != 0) { ArVer = GForcePackageVersion; return; }

            // Remember current versions for logging
            var OldVer = ArVer;
            var OldLVer = ArLicenseeVer;

            // Simple overrides, when game has exact package version
            for (var i = 0; i < ueVersions.Length; i++)
                if (ueVersions[i].g == Game) { ArVer = ueVersions[i].v; goto end_override; }

            if (Game >= GAME_UE4(0) && Game < GAME_UE4(LATEST_UE4_VERSION + 1) && ArVer == 0)
            {
                // Special path for UE4, when engine version is specified and packages are unversioned.
                // Override version only if package is unversioned. Mixed versioned and unversioned packages could
                // appear in UE4 game when it has editor support (like UT4).
                ArVer = ue4Versions[GAME_UE4_GET_MINOR(Game)];
                return;
            }
            else if (Game == UE4_BASE && ArVer != 0)
            {
                // Path for UE4 when packages are versioned: detect engine version by ArVer.
                // Versioned packages provides FCustomVersion info, however some objects like UStaticMesh doesn't
                // use versioning, we're relying of GAME_UE4(x) there.
                for (var i = ue4Versions.Length - 1; i >= 0; i--)
                    if (ArVer >= ue4Versions[i]) { SetGame(GAME_UE4(i)); break; }
                return;
            }

            // Convert game tag to ArVer
            if (Game == MassEffect) ArLicenseeVer = OVERRIDE_ME1_LVER;
            if (Game == Transformers && ArLicenseeVer >= 181) ArVer = OVERRIDE_TRANSFORMERS3; // Transformers: Fall of Cybertron
            if (Game == SpecialForce2)
            {
                // engine for this game is upgraded without changing ArVer, they have ArVer set too high and changind ArLicenseeVer only
                if (ArLicenseeVer >= 14) ArVer = OVERRIDE_SF2_VER2;
                else if (ArLicenseeVer == 9) ArVer = OVERRIDE_SF2_VER;
            }
            if (Game == RememberMe)
            {
                if (ArVer > 832) ArVer = OVERRIDE_LIS_VER; // 832 = Remember Me, higher - Life is Strange
            }
            if (Game == DunDef)
            {
                if (ArVer >= 686) ArVer = OVERRIDE_DUNDEF_VER;
            }

        end_override:
            if ((ArVer != OldVer || ArLicenseeVer != OldLVer) && Game < UE4_BASE)
                Console.WriteLine($"Overridden version {OldVer}/{OldLVer} -> {ArVer}/{ArLicenseeVer}");
        }
    }
}