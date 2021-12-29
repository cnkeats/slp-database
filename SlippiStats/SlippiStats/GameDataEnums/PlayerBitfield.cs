using System;

namespace SlippiStats.GameDataEnums
{
    [Flags]
    public enum PlayerBitfield
    {
        STAMINA_MODE = 0x01,
        SILENT_CHARACTER = 0x02,
        LOW_GRAVITY = 0x04,
        INVISIBLE = 0x08,
        BLACK_STOCK_ICON = 0x10,
        METAL = 0x20,
        START_ON_WARP_IN_PLATFORM = 0x40,
        RUMBLE_ENABLED = 0x80
    }
}