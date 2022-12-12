using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCompletion : SingletonBase<MapCompletion>
{
    public const string filename = "completion.dat";

    public static void ResetSavedData()
    {
        FileHandler.Reset(filename);
    }

    [Serializable]
    private class EpisodeScore
    {
        public Episode episode;
        public int score;
    }
        

    public static void SaveEpisodeResult(int levelScore)
    {
        if (Instance)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }
        else
        {
            Debug.Log($"Episode complete with score {levelScore}");
        }
    }  
    

    [SerializeField] private EpisodeScore[] completionData;    

    private new void Awake()
    {
        base.Awake();
        Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);
    }
    

    public bool TryIndex(int id, out Episode episode, out int score)
    {
        if(id >= 0 && id < completionData.Length)
        {
            episode = completionData[id].episode;
            score = completionData[id].score;
            return true;
        }
        else
        {
            episode = null;
            score = 0;
            return false;
        }
    }

    public int GetEpisodeScore(Episode m_episode)
    {
        foreach(var data in completionData)
        {
            if (data.episode == m_episode) return data.score;
        }

        return 0;
    }

    private void SaveResult(Episode currentEpisode, int result)
    {
        foreach (var item in completionData)
        {
            if(item.episode == currentEpisode)
            {
                if (result > item.score)
                {
                    item.score = result;
                    Saver<EpisodeScore[]>.Save(filename, completionData);
                }                
            }
        }
    }
}    

