using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : Singleton<GameStateHandler>
{
    [SerializeField] private CursorState _cursorState;
    [SerializeField] private CursorVisibility _cursorVisibility;

    public CursorState CurrentCursorState => _cursorState;
    public CursorVisibility CurrentCursorVisibility => _cursorVisibility;

    public void ChangeCursorState(CursorState i_CursorState)
    {
        ChangeState(ref _cursorState, i_CursorState, cursorState =>
        {
            switch (cursorState)
            {
                case CursorState.Unlocked:
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case CursorState.Locked:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
            }
        });
    }

    public void ChangeCursorVisibility(CursorVisibility i_CursorVisibility)
    {
        ChangeState(ref _cursorVisibility, i_CursorVisibility, cursorVisibility =>
        {
            switch (cursorVisibility)
            {
                case CursorVisibility.Visible:
                    Cursor.visible = true;
                    break;
                case CursorVisibility.Invisible:
                    Cursor.visible = false;
                    break;
            }
        });
    }

    private void ChangeState<T>(ref T currentState, T newState, Action<T> onStateChange = null) where T : struct, Enum
    {
        if (IsInvalidState(newState, currentState))
        {
            currentState = default;
            Debug.LogError($"State: {newState} is not valid.");
            return;
        }

        if (IsDuplicate(newState, currentState)) return;

        currentState = newState;
        onStateChange?.Invoke(currentState);
        Debug.LogWarning($"{typeof(T).Name} State changed to: {currentState}");
    }

    #region INVALID_STATE_HANDLER
    private bool IsInvalidState<T>(T newState, T currentState) where T : struct, Enum
    {
        if (!Enum.IsDefined(typeof(T), newState))
        {
            Debug.LogError($"Invalid state provided: {newState}");
            return true;
        }
        return false;
    }
    #endregion

    #region DUPLICATION_STATE_HANDLER
    private bool IsDuplicate<T>(T newState, T currentState) where T : struct, Enum
    {
        return EqualityComparer<T>.Default.Equals(newState, currentState);
    }
    #endregion
}


public enum CursorState
{
    Locked,
    Unlocked
}

public enum CursorVisibility
{
    Visible,
    Invisible
}