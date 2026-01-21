using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethods : MonoBehaviour
{
    public static string conditionText;

    public TMP_Text winOrLoseText;

    public void Start()
    {
        winOrLoseText.text = conditionText;
        PlayerMovement.Instance.UnlockCursor();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads current scene
        Debug.Log("Clicked Restart");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(0);
    }


    public void QuitGame()
    {
        Application.Quit(); // Quits the game
        Debug.Log("Clicked Quit game");
    }
}
