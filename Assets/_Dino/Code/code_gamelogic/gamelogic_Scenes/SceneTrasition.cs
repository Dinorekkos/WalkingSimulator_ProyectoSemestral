using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrasition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
    public void Quit()
    {
        Application.Quit(); 
        
    }
}
