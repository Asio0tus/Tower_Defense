using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLevel : MonoBehaviour
{
    [SerializeField] private Episode m_episode;
    [SerializeField] private Image[] starsImages;
    [SerializeField] private Sprite starBlue;
    [SerializeField] private Button levelButton;
    [SerializeField] private GameObject lockPanel;
        

    private int scoreResultLevel;
        

    public int ScoreResultLevel => scoreResultLevel;

    private bool isComplete;
    public bool IsComplete { get { return gameObject.activeSelf && isComplete; } }
        

    public void LoadLevel()
    {
        LevelSequenceController.Instance.StartEpisode(m_episode);
    }

    public void SetLevelData(Episode episode, int score)
    {
        print(episode.EpisodeName);
        
    }

    public void Initialize()
    {
        scoreResultLevel = MapCompletion.Instance.GetEpisodeScore(m_episode);
              
        
        for (int i = 0; i < scoreResultLevel; i++)
        {
            starsImages[i].sprite = starBlue;
            isComplete = true;
        }
    }

    public void SetLevelLocked()
    {
        gameObject.SetActive(true);

        lockPanel.SetActive(true);
        levelButton.interactable = false;
    }

    
}
