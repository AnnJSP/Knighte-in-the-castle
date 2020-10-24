using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FillController : MonoBehaviour
{
    #region Buttoms

    [SerializeField] private Image _filledImage;
    [SerializeField] private Slider _slider;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }

    }

    public void PowerOfAudio()
    {
        AudioListener.volume = _slider.value;
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Level_1");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PauseGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPaused = true;
#else
        Time.timeScale = 0;
#endif
    }

    public void PlayGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPaused = false;
#else
        Time.timeScale = 1;
#endif
    }

    #endregion
}
