using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] Animator animator;
    
    
    bool isPaused = false;
    // Start is called before the first frame update

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadNextScene()
    {        
        StartCoroutine (NextScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator NextScene(int index)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(index);
    }

    public void PauseGame()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;                       
        }

        else
        {
            Time.timeScale = 0;
            isPaused = true;            
        }
    }
}
