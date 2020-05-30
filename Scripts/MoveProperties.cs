﻿using System.Collections.Generic;

public class MoveProperties
{
    public Coordinate Coordinate { get; }
    public MoveType MoveType { get; set; }

    public MoveProperties(Coordinate coordinate, MoveType moveType)
    {
        Coordinate = coordinate;
        MoveType = moveType;
    }
    
    public MoveProperties(Coordinate coordinate)
    {
        Coordinate = coordinate;
    }

    public static MoveProperties GetUnavailableMoveProperties()
    {
        return new MoveProperties(new Coordinate(0, 0), MoveType.Unavailable);
    }

    public static void AddToMoveset(List<MoveProperties> moveset, MoveProperties move)
    {
        if (move.MoveType != MoveType.Unavailable)
        {
            moveset.Add(move);
        }
    }
}