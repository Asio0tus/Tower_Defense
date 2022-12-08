using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLevel : MonoBehaviour
{
    private Episode m_episode;
    [SerializeField] Image[] starsImages;
    [SerializeField] Sprite starBlue;
    //[SerializeField] private Text text;

    public void LoadLevel()
    {
        LevelSequenceController.Instance.StartEpisode(m_episode);
    }

    public void SetLevelData(Episode episode, int score)
    {
        m_episode = episode;

        for(int i = 0; i < score; i++)
        {
            starsImages[i].sprite = starBlue;
        }
        //text.text = $"{score} / 3";
    }
}
