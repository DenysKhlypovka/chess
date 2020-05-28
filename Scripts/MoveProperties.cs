using GameObjectScript;

public class MoveProperties
{
    public int LocX { get; }
    public int LocY { get; }
    public FigureController ControllerOfFigureAtPosition { get; }
    public MoveType MoveType { get; set; }

    public MoveProperties(int locX, int locY, FigureController controllerOfFigureAtPosition, MoveType moveType)
    {
        LocX = locX;
        LocY = locY;
        ControllerOfFigureAtPosition = controllerOfFigureAtPosition;
        MoveType = moveType;
    }

    public static MoveProperties GetUnavailableMoveProperties()
    {
        return new MoveProperties(0, 0, null, MoveType.Unavailable);
    }
}