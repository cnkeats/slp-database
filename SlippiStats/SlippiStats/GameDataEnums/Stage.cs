
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
        [Display(Name = "Dummy")]
        DUMMY = 0,
        [Display(Name = "Test")]
        TEST = 1,
        [Display(Name = "Fountain of Dreams")]
        FOUNTAIN_OF_DREAMS = 2,
        [Display(Name = "Pokémon Stadium")]
        POKEMON_STADIUM = 3,
        [Display(Name = "Princess Peach's Castle")]
        PRINCESS_PEACHS_CASTLE = 4,
        [Display(Name = "Kongo Jungle")]
        KONGO_JUNGLE = 5,
        [Display(Name = "Brinstar")]
        BRINSTAR = 6,
        [Display(Name = "Corneria")]
        CORNERIA = 7,
        [Display(Name = "Yoshi's Story")]
        YOSHIS_STORY = 8,
        [Display(Name = "Onett")]
        ONETT = 9,
        [Display(Name = "Mute City")]
        MUTE_CITY = 10,
        [Display(Name = "Rainbow Cruise")]
        RAINBOW_CRUISE = 11,
        [Display(Name = "Jungle Japes")]
        JUNGLE_JAPES = 12,
        [Display(Name = "Green Bay")]
        GREEN_BAY = 13,
        [Display(Name = "Hyrule Temple")]
        HYRULE_TEMPLE = 14,
        [Display(Name = "Brinstar Depths")]
        BRINSTAR_DEPTHS = 15,
        [Display(Name = "Yoshi's Island")]
        YOSHIS_ISLAND = 16,
        [Display(Name = "Green Greens")]
        GREEN_GREENS = 17,
        [Display(Name = "Fourside")]
        FOURSIDE = 18,
        [Display(Name = "Mushroom Kingdom I")]
        MUSHROOM_KINGDOM_I = 19,
        [Display(Name = "Mushroom Kingdom II")]
        MUSHROOM_KINGDOM_II = 20,
        //[Display(Name = "Akaneia")]
        //AKANEIA = 21, // Deleted stage
        [Display(Name = "Venom")]
        VENOM = 22,
        [Display(Name = "Poké Floats")]
        POKE_FLOATS = 23,
        [Display(Name = "Big Blue")]
        BIG_BLUE = 24,
        [Display(Name = "Icile Mountain")]
        ICICLE_MOUNTAIN = 25,
        [Display(Name = "Icetop")]
        ICETOP = 26,
        [Display(Name = "Flat Zone")]
        FLAT_ZONE = 27,
        [Display(Name = "Dream Land 64")]
        DREAM_LAND_N64 = 28,
        [Display(Name = "Yoshi's Island 64")]
        YOSHIS_ISLAND_N64 = 29,
        [Display(Name = "Kongo Jungle 64")]
        KONGO_JUNGLE_N64 = 30,
        [Display(Name = "Battlefield")]
        BATTLEFIELD = 31,
        [Display(Name = "Final Destination")]
        FINAL_DESTINATION = 32
    }
}