using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    private bool questNotStarted;
    private bool questInProgress;
    private bool questIsComplited;
    
    // Start is called before the first frame update
    void Start()
    {
        questNotStarted = false;
        questInProgress = false;
        questIsComplited = false;
    }
}
