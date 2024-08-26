using UnityEngine;

public class HelperMethods : Singleton<HelperMethods>
{
    [SerializeField] private GameStateHandler gameStateHandler => GameStateHandler.Instance;

    #region CURSOR
    private bool isToggled;
    public void ToggleCursor()
    {
        if (!isToggled)
        {
            isToggled = !isToggled;
            gameStateHandler.ChangeCursorState(CursorState.Locked);
            gameStateHandler.ChangeCursorVisibility(CursorVisibility.Invisible);
        }
        else
        {
            isToggled = !isToggled;
            gameStateHandler.ChangeCursorState(CursorState.Unlocked);
            gameStateHandler.ChangeCursorVisibility(CursorVisibility.Visible);
        }
    }
    #endregion
}