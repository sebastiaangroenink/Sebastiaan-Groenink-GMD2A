using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStats : MonoBehaviour
{
    public PathFinding pathfinder;

    public List<float> statList = new List<float>();
    public List<GameObject> recourceList = new List<GameObject>();

    public float happiness;

    public float gameTick;


    public GameObject target;

    public string printAction;

    public bool busy;
    public string currTask;


    /// <summary>
    /// Very simple game Tick system to make a decision only every 2.5 seconds.
    /// Also sets all base player stat values to max.
    /// </summary>
    void Start()
    {

        gameTick = 2.5f;

        
        for (int i = 0; i < statList.Count; i++)
        {
            statList[i] = 100.0f;
        }
    }

    /// <summary>
    /// Decreases player stats over time and checks if there's an action to be taken.
    /// </summary>
    void Update()
    {

        gameTick -= 1 * Time.deltaTime;
        DecreaseStats();

        if (gameTick <= 0)
        {
            for (int i = 0; i < statList.Count; i++)
            {
                TakeAction(statList[i], i);
            }

            gameTick = 2.5f;
        }
    }

    /// <summary>
    /// Consume rates for each statistic.
    /// </summary>
    void DecreaseStats()
    {
        statList[0] -= 0.5f * Time.deltaTime; //energy
        statList[1] -= 0.75f * Time.deltaTime; //hygiene
        statList[2] -= 1.5f * Time.deltaTime; //hunger 
        statList[3] -= 2.5f * Time.deltaTime; //bladder
    }

    /// <summary>
    /// Checks if a stat's float value is between the given parameters to take action. If so, then rolls for a number, if it's higher than the statistic's current value, 
    /// Take actions to resupply that statistic.
    /// </summary>
    /// <param name="stat"> Input of the checked statistic.</param>
    /// <param name="statNumber">Index number of the statistic in the stat list.</param>
    /// 
    public void TakeAction(float stat, int statNumber)
    {
        if (Random.Range(0, 75) >= stat && !busy)
        {
            pathfinder.target = recourceList[statNumber].transform;
            printAction =
                statNumber == 0 ? "going to bed" :
                statNumber == 1 ? "going to shower" :
                statNumber == 2 ? "grabbing food" :
                statNumber == 3 ? "going to toilet" : "Nothing";
            print(printAction);
            currTask =
                statNumber == 0 ? "energy" :
                statNumber == 1 ? "hygiene" :
                statNumber == 2 ? "hunger" :
                statNumber == 3 ? "bladder" : "Nothing";
            busy = true;
        }
    }
    /// <summary>
    /// Once a task has been given and the player reached the destination, refill that specific statistic and allow for new rolls.
    /// </summary>
    public void TaskReward()
    {
        switch (currTask)
        {
            case ("energy"):
                statList[0] = 100.0f;
                target = recourceList[0];
                break;

            case ("hygiene"):
                statList[1] = 100.0f;
                target = recourceList[1];
                break;

            case ("hunger"):
                statList[2] = 100.0f;
                target = recourceList[2];
                break;

            case ("bladder"):
                statList[3] = 100.0f;
                target = recourceList[3];
                break;

            case ("Nothing"):
                print("No reward was given");
                break;
        }
    }
}
