
using SlippiStats.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SlippiStats.GameDataEnums
{
    public enum Character
    {
        [Display(Name = "Captain Falcon")]
        [TierPlacement(5)]
        [StockIcon("/images/stock_icons/CaptainFalconHeadSSBM.png")]
        CAPTAIN_FALCON = 0,

        [Display(Name = "Donkey Kong")]
        [TierPlacement(13)]
        [StockIcon("/images/stock_icons/DonkeyKongHeadSSBM.png")]
        DONKEY_KONG = 1,

        [Display(Name = "Fox")]
        [TierPlacement(0)]
        [StockIcon("/images/stock_icons/FoxHeadSSBM.png")]
        FOX = 2,

        [Display(Name = "Mr. Game & Watch")]
        [TierPlacement(19)]
        [StockIcon("/images/stock_icons/MrGame&WatchHeadSSBM.png")]
        MR_GAME_AND_WATCH = 3,

        [Display(Name = "Kirby")]
        [TierPlacement(25)]
        [StockIcon("/images/stock_icons/KirbyHeadSSBM.png")]
        KIRBY = 4,

        [Display(Name = "Bowser")]
        [TierPlacement(24)]
        [StockIcon("/images/stock_icons/BowserHeadSSBM.png")]
        BOWSER = 5,

        [Display(Name = "Link")]
        [TierPlacement(16)]
        [StockIcon("/images/stock_icons/LinkHeadSSBM.png")]
        LINK = 6,

        [Display(Name = "Luigi")]
        [TierPlacement(10)]
        [StockIcon("/images/stock_icons/LuigiHeadSSBM.png")]
        LUIGI = 7,

        [Display(Name = "Mario")]
        [TierPlacement(14)]
        [StockIcon("/images/stock_icons/MarioHeadSSBM.png")]
        MARIO = 8,

        [Display(Name = "Marth")]
        [TierPlacement(1)]
        [StockIcon("/images/stock_icons/MarthHeadSSBM.png")]
        MARTH = 9,

        [Display(Name = "Mewtwo")]
        [TierPlacement(17)]
        [StockIcon("/images/stock_icons/MewtwoHeadSSBM.png")]
        MEWTWO = 10,

        [Display(Name = "Ness")]
        [TierPlacement(21)]
        [StockIcon("/images/stock_icons/NessHeadSSBM.png")]
        NESS = 11,

        [Display(Name = "Peach")]
        [TierPlacement(6)]
        [StockIcon("/images/stock_icons/PeachHeadSSBM.png")]
        PEACH = 12,

        [Display(Name = "Pikachu")]
        [TierPlacement(7)]
        [StockIcon("/images/stock_icons/PikachuHeadSSBM.png")]
        PIKACHU = 13,

        [Display(Name = "Ice Climbers")]
        [TierPlacement(8)]
        [StockIcon("/images/stock_icons/IceClimbersHeadSSBM.png")]
        ICE_CLIMBERS = 14,

        [Display(Name = "Jigglypuff")]
        [TierPlacement(4)]
        [StockIcon("/images/stock_icons/JigglypuffHeadSSBM.png")]
        JIGGLYPUFF = 15,

        [Display(Name = "Samus")]
        [TierPlacement(9)]
        [StockIcon("/images/stock_icons/SamusHeadSSBM.png")]
        SAMUS = 16,

        [Display(Name = "Yoshi")]
        [TierPlacement(11)]
        [StockIcon("/images/stock_icons/YoshiHeadSSBM.png")]
        YOSHI = 17,

        [Display(Name = "Zelda")]
        [TierPlacement(20)]
        [StockIcon("/images/stock_icons/ZeldaHeadSSBM.png")]
        ZELDA = 18,

        [Display(Name = "Sheik")]
        [TierPlacement(3)]
        [StockIcon("/images/stock_icons/SheikHeadSSBM.png")]
        SHEIK = 19,

        [Display(Name = "Falco")]
        [TierPlacement(2)]
        [StockIcon("/images/stock_icons/FalcoHeadSSBM.png")]
        FALCO = 20,

        [Display(Name = "Young Link")]
        [TierPlacement(15)]
        [StockIcon("/images/stock_icons/YoungLinkHeadSSBM.png")]
        YOUNG_LINK = 21,

        [Display(Name = "Dr. Mario")]
        [TierPlacement(12)]
        [StockIcon("/images/stock_icons/DrMarioHeadSSBM.png")]
        DR_MARIO = 22,

        [Display(Name = "Roy")]
        [TierPlacement(18)]
        [StockIcon("/images/stock_icons/RoyHeadSSBM.png")]
        ROY = 23,

        [Display(Name = "Pichu")]
        [TierPlacement(23)]
        [StockIcon("/images/stock_icons/PichuHeadSSBM.png")]
        PICHU = 24,

        [Display(Name = "Ganondorf")]
        [TierPlacement(13)]
        [StockIcon("/images/stock_icons/GanondorfHeadSSBM.png")]
        GANONDORF = 25,

        [Display(Name = "Master Hand")]
        [TournamentLegal(false)]
        [StockIcon("/images/stock_icons/MasterHandHeadSSBM.png")]
        MASTER_HAND = 26,

        [Display(Name = "Male Wireframe")]
        [TournamentLegal(false)]
        [StockIcon("/images/stock_icons/FightingWireFramesHeadSSBM.png")]
        WIREFRAME_MALE = 27,

        [Display(Name = "Female Wireframe")]
        [TournamentLegal(false)]
        [StockIcon("/images/stock_icons/FightingWireFramesHeadSSBM.png")]
        WIREFRAME_FEMALE = 28,

        [Display(Name = "Giga Bowser")]
        [TournamentLegal(false)]
        [StockIcon("/images/stock_icons/GigaBowserHeadSSBM.png")]
        GIGA_BOWSER = 29,

        [Display(Name = "Crazy Hand")]
        [TournamentLegal(false)]
        [StockIcon("/images/stock_icons/CrazyHandHeadSSBM.png")]
        CRAZY_HAND = 30,

        [Display(Name = "Sandbag")]
        [TournamentLegal(false)]
        [StockIcon("/images/stock_icons/SandbagHeadSSBM.png")]
        SANDBAG = 31,

        [Display(Name = "Popo")]
        [TournamentLegal(false)]
        [StockIcon("/images/stock_icons/IceClimbersHeadSSBM.png")]
        POPO = 32
    }
}