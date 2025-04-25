using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ScoreManager : MonoBehaviour
{
    
    public int wumpaCounter {  get; private set; }
    public int crashTokenCounter { get; private set; }

    public static ScoreManager scoreManager;
    public Text wumpaCounterText;
    public Text crashTokenCounterText;

    void Start()    {
        scoreManager = this;
        wumpaCounter = 0;
        crashTokenCounter = 0;

    }

    public void RaiseScoreWumpa(int i)
    {
        wumpaCounter += i;
        wumpaCounterText.text = string.Format($"{{0:D{3}}}", wumpaCounter);
    }

    public void RaiseScoreCrashToken(int i)
    {
        crashTokenCounter += i;
        crashTokenCounterText.text = "X" + crashTokenCounter.ToString();

    }
}
