using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Acheivments : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        IAchievement achievement1 = Social.CreateAchievement();
        achievement1.id = "TrackComplete";
        achievement1.percentCompleted = 100.0;
        Social.LoadAchievements(achievements => {
            if (achievements.Length > 0)
            {
                Debug.Log("Got " + achievements.Length + " achievement instances");
                string myAchievements = "My achievements:\n";
                foreach (IAchievement achievement in achievements)
                {
                    myAchievements += "\t" +
                        achievement.id + " " +
                        achievement.percentCompleted + " " +
                        achievement.completed + " " +
                        achievement.lastReportedDate + "\n";
                }
                Debug.Log(myAchievements);
            }
            else
                Debug.Log("No achievements returned");
        });
    }

    // Update is called once per frame
    void Update()
    {
        ShowAchievementsUI();
    }
}
