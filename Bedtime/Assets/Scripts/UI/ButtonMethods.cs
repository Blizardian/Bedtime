using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethods : MonoBehaviour
{
    public static string conditionText;

    public TMP_Text winOrLoseText;

    public void Start()
    {
        winOrLoseText.text = conditionText; // Updates this text accordingly
        PlayerMovement.UnlockCursor(); // Unlocks the cursor (So the player can interact with the UI)
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene
        Debug.Log("Clicked Restart");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(2); // Loads the scene number that is given
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene(1); // Loads the intro
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); // Loads the scene number that is given
    }
    public void QuitGame()
    {
        Application.Quit(); // Quits the game
        Debug.Log("Clicked Quit game");
    }
}
