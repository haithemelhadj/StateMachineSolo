using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text fpscounter;

    private void Awake()
    {
        //Application.targetFrameRate = 60;
        Application.targetFrameRate = -1;
    }
    private void Update()
    {
        fpscounter.text = " FPS" + (1f / Time.deltaTime).ToString("F1");
    }
}
