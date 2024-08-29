using UnityEngine;
using UnityEngine.InputSystem;

public class MiscInputHandlers : MonoBehaviour
{
    public InputActionReference CursorRef;

    [SerializeField] private HelperMethods helperMethods => HelperMethods.Instance;

    #region ENABLE
    private void OnEnable()
    {
        CursorRef.action.Enable();
    }
    #endregion

    #region DISABLE
    private void OnDisable()
    {
        CursorRef.action.Disable();
    }
    #endregion

    private void Start()
    {
        
    }

    #region INPUT_CALLBACK
    public void OnCursor(InputValue i_Value)
    {
        if (i_Value.isPressed)
        {
            Debug.Log("Enabled");
            helperMethods.ToggleCursor();
        }
    }
    #endregion
}