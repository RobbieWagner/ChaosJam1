using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{

    [Header("General Dialogue Info")]
    [SerializeField] private TextMeshProUGUI currentText;

    private Story currentStory;
    public IEnumerator dialogueCoroutine {get; private set;}

    private string currentSentenceText = "";
    private bool sentenceTyping = false;
    private bool skipSentenceTyping = false;

    [SerializeField] private Image textBox;
    [SerializeField] private Image continueIcon;
    private bool canContinue;
    public bool CanContinue
    {
        get { return canContinue; }
        set
        {
            if(value == canContinue) return;
            canContinue = value;
        }
    }

    public static DialogueManager Instance {get; private set;}

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            Instance = this; 
        } 

        CanContinue = false;
    }

    public void EnterDialogueMode(Story story)
    {
        StartCoroutine(EnterDialogueModeCo(story));
    }

    public IEnumerator EnterDialogueModeCo(Story story)
    {
        if(dialogueCoroutine == null)
        {
            dialogueCoroutine = RunDialogue(story);
            yield return StartCoroutine(dialogueCoroutine);
        }

        StopCoroutine(EnterDialogueModeCo(story));
    }

    #region core mechanics
    public IEnumerator RunDialogue(Story story)
    {
        textBox.enabled = true;
        continueIcon.enabled = false;
        yield return null;

        currentStory = story;

        yield return StartCoroutine(ReadNextSentence());

        while(dialogueCoroutine != null)
        {
            yield return null;
        }

        StopCoroutine(RunDialogue(story));
    }

    private void OnNextDialogueLine(InputValue value)
    {
        if(CanContinue)
        {
            StartCoroutine(ReadNextSentence());
        }
        else if(sentenceTyping)
        {
            skipSentenceTyping = true;
        }
    }

    public IEnumerator ReadNextSentence()
    {
        CanContinue = false;
        continueIcon.enabled = false;

        yield return null;
        if(currentStory.canContinue)
        {
            currentText.text = "";
            sentenceTyping = true;

            currentSentenceText = ConfigureSentence(currentStory.Continue());

            char nextChar;
            for(int i = 0; i < currentSentenceText.Length; i++)
            {
                if(skipSentenceTyping) break;
                nextChar = currentSentenceText[i];
                if(nextChar == '<')
                {
                    while(nextChar != '>' && i < currentSentenceText.Length)
                    {
                        currentText.text += nextChar;
                        i++;
                        nextChar = currentSentenceText[i];
                    } 
                }

                currentText.text += nextChar;
                GameSounds.Instance.PlayDialogueSound();
                yield return new WaitForSeconds(.05f);
            }

            currentText.text = currentSentenceText;
            sentenceTyping = false;

            yield return new WaitForSeconds(0.125f);
            continueIcon.enabled = true;
            yield return new WaitForSeconds(1.125f);
            StartCoroutine(ReadNextSentence());
        }
        else
        {
            yield return StartCoroutine(EndDialogue());
        }

        StopCoroutine(ReadNextSentence());
    }

    public IEnumerator EndDialogue()
    {
        yield return null;
        //Debug.Log("Dialogue ended");
        currentText.text = "";
        dialogueCoroutine = null;
        currentStory = null;
        textBox.enabled = false;
        StopCoroutine(EndDialogue());
    }
    #endregion

    #region Text and Text Field Configuration

    private string ConfigureSentence(string input)
    {
        string configuredText = input;

        List<char> allowList = new List<char>() {' ', '-', '\'', ',', '.'};
        string name = SaveDataManager.LoadString("name", "Lux");

        bool nameAllowed = true;
        foreach(char c in name)
        {
            if(!Char.IsLetterOrDigit(c) && !allowList.Contains(c))
            {
                nameAllowed = false;
                break;
            }
        }

        if(!nameAllowed)
        {
            name = "Lux";
            SaveDataManager.SaveString("name", "Lux");
        } 

        configuredText = configuredText.Replace("^NAME^", name);

        return configuredText;
    }
    #endregion
}