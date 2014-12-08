using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour {

    static int CurrentDifficulty = 0;

    static int HealthIncrease = 10;
    static float SpawnReduction = 0.1f;

	// Update is called once per frame
	void Update () 
    {
        if (ScoreManager.score % 200 == 0)
        {
            UpdateDifficulty();
        }   
	}

    void UpdateDifficulty()
    {
        CurrentDifficulty = ScoreManager.score / 200;
    }

    public static int BuffHealth()
    {
        return CurrentDifficulty * HealthIncrease;
    }

    public static float ReduceSpawn()
    {
        return CurrentDifficulty * SpawnReduction;
    }
}
