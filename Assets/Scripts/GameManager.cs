using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject gameHud;
    public GameObject gameMenu;
    public GameObject playerPlaceholder;

    private GameObject currentPlayer;

    public Text playerLifesText;

    private int lifes = 10;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() { 
        
    }

    public void AddLife(int value) {
        lifes += value;

        playerLifesText.text = "Vidas: " + lifes;

        if (lifes == 0) {
            SceneManager.LoadScene("Level1");
        } else {
            Destroy(currentPlayer);
            currentPlayer = Instantiate(playerPrefab, playerPlaceholder.transform.position, Quaternion.identity);
        }
    }

    public void NewGame() {
        gameMenu.SetActive(false);
        gameHud.SetActive(true);

        currentPlayer = Instantiate(playerPrefab, playerPlaceholder.transform.position, Quaternion.identity);

    }

    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void PassLvl2() {
        SceneManager.LoadScene("Level2");
    }

}
