using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangedScene(string scene)
    {
        //UnityEngine.SceneManagement.Scene scene1 = SceneManager.CreateScene(scene);
        //SceneManager.SetActiveScene(scene1);
        //SceneManager.LoadSceneAsync(scene, SceneManager.activeSceneChanged);
        //string scene_curr = SceneManager.GetActiveScene().name;
        //PlayerPrefs.SetString("save", scene_curr);
        //PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(scene);
        
    }

    public void RestartScene()
    {
        //UnityEngine.SceneManagement.Scene scene1 = SceneManager.CreateScene(scene);
        //SceneManager.SetActiveScene(scene1);
        //SceneManager.LoadSceneAsync(scene, SceneManager.activeSceneChanged);
        //string scene_curr = SceneManager.GetActiveScene().name;
        //PlayerPrefs.SetString("save", scene_curr);
        //PlayerPrefs.Save();
        if (Time.timeScale != 0)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
        

    }

    public void DeleteScene(string scene)
    {
        //string saved_scene = PlayerPrefs.GetString("save");
        //SceneManager.LoadSceneAsync(scene);

        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
        //SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    public void ShowCanvas(string _canvasName)
    {
        GameObject canvasObj = gameObject.transform.root.Find(_canvasName).gameObject;

        if (canvasObj != null)
            canvasObj.SetActive(true);
    }

    public void HideCanvas(string _canvasName)
    {
        GameObject canvasObj = GameObject.Find(_canvasName);

        if (canvasObj != null)
            canvasObj.SetActive(false);
    }

    public void QuitGame() { Application.Quit(); }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    static public void Pause()
    {
        Time.timeScale = 0;
    }
}
