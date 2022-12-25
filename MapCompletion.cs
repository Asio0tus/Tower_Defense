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
        //public Episode episode;
        public string episodeName;
        public int score;
    }
        

    public static void SaveEpisodeResult(int levelScore)
    {
        if (Instance)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode.EpisodeName, levelScore);
        }
        else
        {
            Debug.Log($"Episode complete with score {levelScore}");
        }
    }  
    

    [SerializeField] private EpisodeScore[] completionData;

    private int totalScore;
    public int TotalScore => totalScore;

    private new void Awake()
    {
        base.Awake();
        Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);

        Episode[] episodes = FindObjectsOfType<Episode>();

        for(int i = 0; i < episodes.Length; i++)
        {
            completionData[i].episodeName = episodes[i].EpisodeName;
        }

        //print("score awake " + completionData[0].score);

        foreach (var episodeScore in completionData)
        {
            totalScore += episodeScore.score;
        }
    }
    

    public bool TryIndex(int id, out string episodeName, out int score)
    {
        if(id >= 0 && id < completionData.Length)
        {
            episodeName = completionData[id].episodeName;
            score = completionData[id].score;
            return true;
        }
        else
        {
            episodeName = null;
            score = 0;
            return false;
        }
    }

    public int GetEpisodeScore(Episode m_episode)
    {
        /*foreach (var data in completionData)
        {
            if (data.episode == m_episode) return data.score;
        }*/
                
        int score = 0;

        print("1level=  " + m_episode);

        for (int i = 0; i < completionData.Length; i++)
        {
            print(completionData[i].episodeName + " == " + completionData[i].score);
            
            if (completionData[i].episodeName == m_episode.EpisodeName) // ÓÑËÎÂÈÅ ÍÅ ÂÛÏÎËÍßÅÒÑß ÁÅÇ ÏÅÐÅÇÀÏÈÑÈ ÑÝÉÂ ÔÀÉËÀ
            {
                score = completionData[i].score;
                //print("score " + score);
            }
        }        

        return score;        
    }

    private void SaveResult(string currentEpisodeName, int result)
    {
        print(currentEpisodeName + "   " + result);

        foreach (var item in completionData)
        {           
            if(item.episodeName == currentEpisodeName)
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

