using System.Collections.Generic;
using System.Linq;
using Controller;
using Model;
using Util;

namespace GameObjectScript
{
  public abstract class PieceController : ElementOnGrid
  {
    private List<MoveProperties> availableMoves;

    public Model.Piece PieceType { get; set; }
    private bool moved;
    internal bool Captured { get; set; }

    public void Init(GameController gameController)
    {
      this.gameController = gameController;
      availableMoves = new List<MoveProperties>();
    }

    void OnMouseDown()
    {
      Activate();
    }

    public abstract List<MoveProperties> GetPossibleMoveset();

    public void ClearAvailableMoves()
    {
      availableMoves.Clear();
    }

    public void SetAvailableMoves(List<MoveProperties> moveset)
    {
      availableMoves = new List<MoveProperties>(moveset);
    }

    public bool NoAvailableMoves()
    {
      return availableMoves.Count == 0;
    }

    public bool IsMoved()
    {
      return moved;
    }

    public void Move(Coordinate coordinate)
    {
      Util.Util.MovePhysically(this, coordinate);
      Coordinate = coordinate;
      moved = true;
    }

    protected List<MoveProperties> FilterInitialMoveset(List<MoveProperties> inputMoveset)
    {
      return inputMoveset.ConvertAll(moveProperties =>
          gameController.GetMoveProperties(moveProperties.Coordinate, Color))
        .Where(properties => properties.MoveType != MoveType.Unavailable).ToList();
    }

    public void Activate()
    {
      if (Captured)
      {
        gameController.TryPromotePiece(this);
      }
      else if (!IsActivated)
      {
        gameController.ResetBoardAndPieces();

        if (!gameController.IsAllowedToMove(this)) return;
        IsActivated = true;
        gameController.HighlightCells(availableMoves, Coordinate);
      }
    }

    protected List<MoveProperties> CheckDiagonalMoves()
    {
      return CheckCyclicMoves(PieceMovesetParameters.GetDiagonalMovesetParametersList(Coordinate));
    }

    protected List<MoveProperties> CheckHorizontalVerticalMoves()
    {
      return CheckCyclicMoves(PieceMovesetParameters.GetHorizontalVerticalMovesetParametersList(Coordinate));
    }

    private List<MoveProperties> CheckCyclicMoves(List<MovesetParameters> movesetParametersList)
    {
      var moveset = new List<MoveProperties>();
      movesetParametersList.ForEach(movesetParameters =>
      {
        for (var coordinate = new Coordinate(movesetParameters.InitialX, movesetParameters.InitialY);
          movesetParameters.ConditionX(coordinate.X) && movesetParameters.ConditionY(coordinate.Y);
          coordinate.X += movesetParameters.IteratorDiffX, coordinate.Y += movesetParameters.IteratorDiffY)
        {
          MoveProperties.AddToMoveset(moveset,
            gameController.GetMoveProperties(new Coordinate(coordinate), Color));
          if (!gameController.CanMoveOnto(coordinate))
          {
            break;
          }
        }
      });
      return moveset;
    }
  }
}