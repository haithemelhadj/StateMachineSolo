using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float fps;
    public Text fpscounter;

    private float timer = 0f;
    private void Awake()
    {
        //Application.targetFrameRate = 20;
        Application.targetFrameRate = -1;
    }
    private void Update()
    {
        fps = (1f / Time.unscaledDeltaTime);
        UpdateFpsText(0.5f);
    }

    private void UpdateFpsText(float time)
    {
        // Accumulate time
        timer += Time.deltaTime;

        // If enough time has passed, update the text
        if (timer >= time)
        {
            fpscounter.text = " FPS" + fps.ToString("F1");//update Fps text
            timer = 0f; // Reset timer
        }
    }


}
