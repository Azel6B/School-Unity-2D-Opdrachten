using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void RestartGame()
    { 
     print("Game Restarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
