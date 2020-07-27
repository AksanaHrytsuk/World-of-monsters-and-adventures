
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [Header("Elements")]
    public Text contentText; 
    public QustSteps activeStep; // степ, выводящийся на экран в данный момент
    private QuestManager questsProgress;
    
    void ResetGame(QustSteps step)
    {
        contentText.text = step.dialogueBox;
        
    }
    
    void Start()
    {
        ResetGame(activeStep);
    }

    // Update is called once per frame
    void Update()
    {
        // if ()
        // {
        //     activeStep = activeStep.questSteps[0];
        // }
        //
        // if ()
        // {
        //     
        // }
    }
}
