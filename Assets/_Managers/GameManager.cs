using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float fps;
    public Text fpscounter;

    private void Awake()
    {
        //Application.targetFrameRate = 20;
        Application.targetFrameRate = -1;
    }
    private void Update()
    {
        fps = (1f / Time.unscaledDeltaTime);

    }
    private void FixedUpdate()
    {
        fpscounter.text = " FPS" + fps.ToString("F1");
        
    }
}
