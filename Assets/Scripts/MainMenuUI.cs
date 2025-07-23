using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    
    public MainMenuUI Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnLevelButtonClick(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
        this.gameObject.SetActive(false);
    }

    public void OnQuitButtonClick() {
        # if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        # else
        Application.Quit();
        # endif
    }
}
