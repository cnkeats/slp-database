
using SlippiStats.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SlippiStats.GameDataEnums
{
    // These are incorrect
    public enum GameMode
    {
        _TIME = 0,
        _STOCK = 1,
        _COIN = 2,
        _BONUS = 3,

        [Display(Name="Stock")]
        STOCK = 8
    }
}