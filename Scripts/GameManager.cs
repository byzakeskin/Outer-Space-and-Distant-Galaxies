using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject player;
    public GameObject spawner;
    public GameObject gameover;
    public GameObject scoreText;
    

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        Gameover,
    }

    GameManagerState state;

    public void Start()
    {
        state = GameManagerState.Opening;
    }

    public void UpdateGameManagerState()
    {
        switch (state)
        {
            case GameManagerState.Opening:
                playButton.SetActive(true);
                gameover.SetActive(false);
                break;

            case GameManagerState.Gameplay:
                scoreText.GetComponent<UIManager>().Score = 0;
                playButton.SetActive(false);
                spawner.GetComponent<SpawnManager>().Init();
                player.GetComponent<Player>().Init();
                spawner.GetComponent<SpawnManager>().ScheduledEnemySpawner();
                break;

            case GameManagerState.Gameover:
                spawner.GetComponent <SpawnManager>().UnscheduleEnemySpawner();
                Invoke("ChangeToOpeningState", 3f);
                gameover.SetActive(true);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        this.state = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        state = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

}
