using Project1.Entities;
using Project1;

public class Link
{
    public Direction PreviousDirection { get; private set; } = Direction.Down; // Default facing down

    private ILinkState _currentState;

    public void ChangeState(ILinkState newState)
    {
        // Update PreviousDirection based on movement state
        if (newState is LinkMoveUpState) PreviousDirection = Direction.Up;
        if (newState is LinkMoveDownState) PreviousDirection = Direction.Down;
        if (newState is LinkMoveLeftState) PreviousDirection = Direction.Left;
        if (newState is LinkMoveRightState) PreviousDirection = Direction.Right;

        // Change the current state
        _currentState = newState;
        _currentState.Enter(this);
    }
}
