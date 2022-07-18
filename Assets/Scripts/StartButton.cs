using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Start button", menuName = "Buttons")]
public class StartButton : ScriptableObject
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
