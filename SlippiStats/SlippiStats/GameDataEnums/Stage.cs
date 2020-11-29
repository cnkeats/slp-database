
using SlippiStats.Extensions;
using SlippiStats.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SlippiStats.GameDataEnums
{
    public enum Stage
    {
        [StageIcon("")]
        [Display(Name = "Dummy")]
        DUMMY = 0,
        [StageIcon("")]
        [Display(Name = "Test")]
        TEST = 1,
        [StageIcon("/images/stage_icons/FountainOfDreamsIconSSBM.png")]
        [Display(Name = "Fountain of Dreams")]
        FOUNTAIN_OF_DREAMS = 2,
        [StageIcon("/images/stage_icons/PokemonStadiumIconSSBm.png")]
        [Display(Name = "Pokémon Stadium")]
        POKEMON_STADIUM = 3,
        [StageIcon("/images/stage_icons/PrincessPeachsCastleIconSSBM.png")]
        [Display(Name = "Princess Peach's Castle")]
        PRINCESS_PEACHS_CASTLE = 4,
        [StageIcon("/images/stage_icons/KongoJungleIconSSBM.png")]
        [Display(Name = "Kongo Jungle")]
        KONGO_JUNGLE = 5,
        [StageIcon("/images/stage_icons/BrinstarIconSSBM.png")]
        [Display(Name = "Brinstar")]
        BRINSTAR = 6,
        [StageIcon("/images/stage_icons/CorneriaIconSSBM.png")]
        [Display(Name = "Corneria")]
        CORNERIA = 7,
        [StageIcon("/images/stage_icons/YoshisStoryIconSSBM.png")]
        [Display(Name = "Yoshi's Story")]
        YOSHIS_STORY = 8,
        [StageIcon("/images/stage_icons/OnettIconSSBM.png")]
        [Display(Name = "Onett")]
        ONETT = 9,
        [StageIcon("/images/stage_icons/MuteCityIconSSBM.png")]
        [Display(Name = "Mute City")]
        MUTE_CITY = 10,
        [StageIcon("/images/stage_icons/RainbowCruiseIconSSBM.png")]
        [Display(Name = "Rainbow Cruise")]
        RAINBOW_CRUISE = 11,
        [StageIcon("/images/stage_icons/JungleJapesIconSSBM.png")]
        [Display(Name = "Jungle Japes")]
        JUNGLE_JAPES = 12,
        [StageIcon("/images/stage_icons/GreenBayIconSSBM.png")]
        [Display(Name = "Green Bay")]
        GREEN_BAY = 13,
        [StageIcon("/images/stage_icons/TempleIconSSBM.png")]
        [Display(Name = "Hyrule Temple")]
        HYRULE_TEMPLE = 14,
        [StageIcon("/images/stage_icons/BrinstarDepthsIconSSBM.png")]
        [Display(Name = "Brinstar Depths")]
        BRINSTAR_DEPTHS = 15,
        [StageIcon("/images/stage_icons/YoshisIslandIconSSBM.png")]
        [Display(Name = "Yoshi's Island")]
        YOSHIS_ISLAND = 16,
        [StageIcon("/images/stage_icons/GreenGreensIconSSBM.png")]
        [Display(Name = "Green Greens")]
        GREEN_GREENS = 17,
        [StageIcon("/images/stage_icons/FoursideIconSSBM.png")]
        [Display(Name = "Fourside")]
        FOURSIDE = 18,
        [StageIcon("/images/stage_icons/MushroomKingdomIconSSBM.png")]
        [Display(Name = "Mushroom Kingdom I")]
        MUSHROOM_KINGDOM_I = 19,
        [StageIcon("/images/stage_icons/MushroomKingdomIIIconSSBM.png")]
        [Display(Name = "Mushroom Kingdom II")]
        MUSHROOM_KINGDOM_II = 20,
        // Deleted stage
        //[StageIcon("")]
        //[Display(Name = "Akaneia")]
        //AKANEIA = 21,
        [StageIcon("/images/stage_icons/VenomIconSSBM.png")]
        [Display(Name = "Venom")]
        VENOM = 22,
        [StageIcon("/images/stage_icons/PokeFloatsIconSSBM.png")]
        [Display(Name = "Poké Floats")]
        POKE_FLOATS = 23,
        [StageIcon("/images/stage_icons/BigBlueIconSSBM.png")]
        [Display(Name = "Big Blue")]
        BIG_BLUE = 24,
        [StageIcon("/images/stage_icons/IcicleMountainIconSSBM.png")]
        [Display(Name = "Icile Mountain")]
        ICICLE_MOUNTAIN = 25,
        // Debug-only stage
        [StageIcon("")]
        [Display(Name = "Icetop")]
        ICETOP = 26,
        [StageIcon("/images/stage_icons/FlatZoneIconSSBM.png")]
        [Display(Name = "Flat Zone")]
        FLAT_ZONE = 27,
        [StageIcon("/images/stage_icons/PastDreamLandIconSSBM.png")]
        [Display(Name = "Dream Land 64")]
        DREAM_LAND_N64 = 28,
        [StageIcon("/images/stage_icons/PastYoshisIslandIconSSBM.png")]
        [Display(Name = "Yoshi's Island 64")]
        YOSHIS_ISLAND_N64 = 29,
        [StageIcon("/images/stage_icons/PastKongoJungleIconSSBM.png")]
        [Display(Name = "Kongo Jungle 64")]
        KONGO_JUNGLE_N64 = 30,
        [StageIcon("/images/stage_icons/BattlefieldIconSSBM.png")]
        [Display(Name = "Battlefield")]
        BATTLEFIELD = 31,
        [StageIcon("/images/stage_icons/FinalDestinationIconSSBM.png")]
        [Display(Name = "Final Destination")]
        FINAL_DESTINATION = 32
    }
}