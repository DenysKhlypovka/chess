public class MoveProperties
{
    public int LocX { get; }
    public int LocY { get; }
    public MoveType MoveType { get; set; }

    private MoveProperties(int locX, int locY, MoveType moveType)
    {
        LocX = locX;
        LocY = locY;
        MoveType = moveType;
    }

    public MoveProperties(int locX, int locY)
    {
        LocX = locX;
        LocY = locY;
    }

    public static MoveProperties GetUnavailableMoveProperties()
    {
        return new MoveProperties(0, 0, MoveType.Unavailable);
    }
}