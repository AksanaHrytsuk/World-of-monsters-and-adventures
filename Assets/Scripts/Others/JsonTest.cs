using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    [SerializeField] private TextAsset configFile;
    // Start is called before the first frame update
    void Start()
    {
        MyClass myClass = new MyClass();
        myClass.level = 1;
    }
}
