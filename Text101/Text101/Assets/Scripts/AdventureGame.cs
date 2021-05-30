using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
    [SerializeField]
    private Text textComponent;

    [SerializeField]
    private State startingState;

    private State state;
    // Start is called before the first frame update
    void Start()
    {
        state = startingState;
        textComponent.text = state.GetStateStory();
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    private void ManageState()
    {
        var nextStates = state.GetNextState();
        for (int i = 0; i < nextStates.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1+i))
            {
                state = nextStates[i];
            }
        }
  
        textComponent.text = state.GetStateStory();
    }
}
