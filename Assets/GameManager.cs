using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //these lists have all the dialogue for each phase of questions

    public List<string> phaseOneDialogue;
    public List<string> phaseTwoDialogue;
    public List<string> phaseThreeDialogue;
    public List<string> phaseFourDialogue;

    //holds the phase we're currently going through
    // List<string> currentDialogue;

    List<string> currentDialogue;

    //tracks the current phase and the line we're on in that phase
    // int phaseIndex = 0;
    //int dialogueIndex = 0;

    int phaseIndex = 0;
    int dialogueIndex = 0;

    //game object for all buttons
    //public GameObject choiceOne;
    // public GameObject choiceTwo;
    //public GameObject nextButton;

    public GameObject choiceOne;
    public GameObject choiceTwo;
    public GameObject nextButton;

    //text component that is showing the dialogue
    //public TMP_Text dialogueBox;

    public TMP_Text dialogueBox;

    //"score" for getting out of pound
    int escape = 0;

    //text for results of the quiz
    public string escaped;
    public string notEscaped;

    // Start is called before the first frame update
    void Start()
    {
        //turn off the choice buttons
        //  choiceOne.SetActive(false);
        // choiceTwo.SetActive(false);

        choiceOne.SetActive(false);
        choiceTwo.SetActive(false);


        //start the dialogue
        currentDialogue = phaseOneDialogue;
        dialogueBox.text = currentDialogue[dialogueIndex];
    }

    void SetDialogueText()
    {
        //if we haven't gotten our results yet
        if (phaseIndex < 4)
        {
            //set the dialogue component to show the line we're on
            dialogueBox.text = currentDialogue[dialogueIndex];
        }
    }

    public void AdvanceDialog()
    {
        //if we haven't gotten our results yet
        if (phaseIndex < 4)
        {
            //go to the next line
            dialogueIndex++;
            SetDialogueText();
            //if we're on the last line of dialogue
            if (dialogueIndex == currentDialogue.Count - 1)
            {
                //show the choices
                SetupChoices();
            }
        }
        //if we've seen our results
        else
        {
            //go to the last scene
            SceneManager.LoadScene("Main");
        }
    }

    void SetupChoices()
    {
        //turn off the next button and turn on the choice buttons
        nextButton.SetActive(false);
        choiceOne.SetActive(true);
        choiceTwo.SetActive(true);
    }

    public void NoEscapeChoice()
    {
        //if we press "no", just go to the next phase of questions
        GoToNextPhase();
    }

    public void EscapeChoice()
    {
        //if we press "yes", increase escape chance and go to next phase
        escape++;
        GoToNextPhase();
    }

    void GoToNextPhase()
    {
        //turn on the next button and turn off the choice buttons
        nextButton.SetActive(true);
        choiceOne.SetActive(false);
        choiceTwo.SetActive(false);
        //reset the dialogue line counter
        dialogueIndex = 0;
        //depending on the phase
        //run an animation and determine what the next phase is
        switch (phaseIndex)
        {
            case 0:
                currentDialogue = phaseTwoDialogue;
                phaseIndex = 1;
                break;
            case 1:
                currentDialogue = phaseThreeDialogue;
                phaseIndex = 2;
                break;
            case 2:
                currentDialogue = phaseFourDialogue;
                phaseIndex = 3;
                break;
            case 3:
                phaseIndex = 4;
                GiveResults();
                break;
        }
        SetDialogueText();
    }

    

    void GiveResults()
    {
        //if the clown score is higher than 2, then u r a clown
        if (escape > 2)
        {
            dialogueBox.text = escaped;
        }
        else
        {
            dialogueBox.text = notEscaped;
        }
    }

}
