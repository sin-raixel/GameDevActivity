using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript instace;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject death;
    private bool isActive = false;
    private void Start()
    {
        instace = this;
        pause.SetActive(isActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void OnResume()
    {
        TogglePause();
        Time.timeScale = 1;
    }

    public void OnRestart()
    {
        TogglePause();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit()
#endif
    }

    public void ShowDeathUI()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        death.SetActive(true);
    }

    public void OnRespawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void TogglePause()
    {
        isActive = !isActive;
        pause.SetActive(isActive);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = isActive;
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
