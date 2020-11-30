namespace SlippiStats.Models
{
    // https://github.com/project-slippi/slippi-wiki/blob/master/SPEC.md#game-end-method
    public enum GameEndMethod
    {
        UNRESOLVED = 0,
        RESOLVED = 3,
        TIME = 1,
        GAME = 2,
        NO_CONTEST = 7
    }

    public enum LRAS
    {
        NONE = -1,
        PLAYER1 = 0,
        PLAYER2 = 1,
        PLAYER3 = 2,
        PLAYER4 = 3
    }

    public class SlpGameEnd
    {
        public GameEndMethod GameEndMethod { get; set; }
        public LRAS LRASInitiatorIndex { get; set; }
    }
}