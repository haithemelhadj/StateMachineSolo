using UnityEngine;

public class InputsHandler : MonoBehaviour
{
    //public Dictionary<string, KeyCode> keyBinds = new Dictionary<string, KeyCode>();


    #region get Key Presses
    public void GetKeyPresses(KeyCode key)
    {
        GetKey(key);
        GetKeyDown(key);
        GetKeyUp(key);
    }
    public bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }
    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }
    public bool GetKeyUp(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }
    #endregion

    #region Get Axis Input

    public float GetAxisInputs(string axisName)
    {
        return Input.GetAxis(axisName);
    }
    public float GetRawAxisInputs(string axisName)
    {
        return Input.GetAxisRaw(axisName);
    }
    #endregion

}
