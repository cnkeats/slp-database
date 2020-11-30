using System.ComponentModel.DataAnnotations;

namespace SlippiStats.GameDataEnums
{
    // These are incorrect
    public enum GameMode
    {
        _TIME = 0,
        _STOCK = 1,
        _COIN = 2,
        _BONUS = 3,

        [Display(Name = "Stock")]
        STOCK = 8
    }
}