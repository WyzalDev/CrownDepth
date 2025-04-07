namespace CrownDepth.Stat
{
    public static class EndGame
    {
        public static EndGameType EndGameType;
    }

    public enum EndGameType
    {
        Win,
        Loose,
        LooseByOldness
    }
}