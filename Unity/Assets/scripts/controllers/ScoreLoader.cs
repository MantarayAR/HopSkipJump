using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ScoreLoader : MonoBehaviour
{
    private int highScore = 0;
    private int score = 0;

    // Singleton
    public static ScoreLoader control;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;

            this.Load();
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    void Destroy()
    {
        this.Save();
    }

    void OnGUI()
    {
        this.PropagateScores();
    }

    public void PropagateScores()
    {
        GameObject scoreObject = GameObject.Find("Score");

        if (scoreObject != null)
        {
            Score score = scoreObject.GetComponent<Score>();
            score.highScore = this.highScore;
            score.score = this.score;
        }
    }

    public void Add(int value = 100)
    {
        this.score += value;

        if (this.score > this.highScore)
        {
            this.highScore = this.score;
        }
    }

    public void ResetScore()
    {
        this.score = 0;
    }

    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/jumpSave.gd");
            bf.Serialize(file, this.highScore);
            file.Close();
        }
        catch (Exception)
        {
            if (Debug.isDebugBuild)
                Debug.Log("The high score could not be saved");
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void Load()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/jumpSave.gd", FileMode.Open);
            this.highScore = (int)bf.Deserialize(file);
            file.Close();
        }
        catch (Exception)
        {
            if (Debug.isDebugBuild)
                Debug.Log("The high score could not be read");
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
}
