
using SlippiStats.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SlippiStats.GameDataEnums
{
    public enum Character
    {
        [Display(Name = "Captain Falcon")]
        CAPTAIN_FALCON = 0,
        [Display(Name = "Donkey Kong")]
        DONKEY_KONG = 1,
        [Display(Name = "Fox")]
        FOX = 2,
        [Display(Name = "Mr. Game & Watch")]
        MR_GAME_AND_WATCH = 3,
        [Display(Name = "Kirby")]
        KIRBY = 4,
        [Display(Name = "Bowser")]
        BOWSER = 5,
        [Display(Name = "Link")]
        LINK = 6,
        [Display(Name = "Luigi")]
        LUIGI = 7,
        [Display(Name = "Mario")]
        MARIO = 8,
        [Display(Name = "Marth")]
        MARTH = 9,
        [Display(Name = "Mewtwo")]
        MEWTWO = 10,
        [Display(Name = "Ness")]
        NESS = 11,
        [Display(Name = "Peach")]
        PEACH = 12,
        [Display(Name = "Pikachu")]
        PIKACHU = 13,
        [Display(Name = "Ice Climbers")]
        ICE_CLIMBERS = 14,
        [Display(Name = "Jigglypuff")]
        JIGGLYPUFF = 15,
        [Display(Name = "Samus")]
        SAMUS = 16,
        [Display(Name = "Yoshi")]
        YOSHI = 17,
        [Display(Name = "Zelda")]
        ZELDA = 18,
        [Display(Name = "Sheik")]
        SHEIK = 19,
        [Display(Name = "Falco")]
        FALCO = 20,
        [Display(Name = "Young Link")]
        YOUNG_LINK = 21,
        [Display(Name = "Dr. Mario")]
        DR_MARIO = 22,
        [Display(Name = "Roy")]
        ROY = 23,
        [Display(Name = "Pichu")]
        PICHU = 24,
        [Display(Name = "Ganondorf")]
        GANONDORF = 25,
        [Display(Name = "Master Hand")]
        MASTER_HAND = 26,
        [Display(Name = "Male Wireframe")]
        WIREFRAME_MALE = 27,
        [Display(Name = "Female Wireframe")]
        WIREFRAME_FEMALE = 28,
        [Display(Name = "Giga Bowser")]
        GIGA_BOWSER = 29,
        [Display(Name = "Crazy Hand")]
        CRAZY_HAND = 30,
        [Display(Name = "Sandbag")]
        SANDBAG = 31,
        [Display(Name = "Popo")]
        POPO = 32
    }
}