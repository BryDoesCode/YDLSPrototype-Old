﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class InkTest : MonoBehaviour
{
    public TextAsset inkAsset;
    public Button button;
    public TextMeshProUGUI storyText;
    public GameObject buttonPanel;
    public StatController StatController;
    public SFXController SFXController;
    public BackgroundController BackgroundController;

    public AudioClip buttonClick;
    private AudioSource audioSource;

    private Story story;

    public TMP_FontAsset userFont;
    public int userFontSize;

    public TextMeshProUGUI timeLabel;
    public TextMeshProUGUI locationLabel;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        story = new Story(inkAsset.text);
        
        // ------------------ Observeable Variables
        story.ObserveVariable("energy", (string varName, object newValue) => {
            StatController.UpdateEnergyStat((int)newValue);
        });

        story.ObserveVariable("health", (string varName, object newValue) => {
            StatController.UpdateHealthStat((int)newValue);
        });

        story.ObserveVariable("wellness", (string varName, object newValue) => {
            StatController.UpdateWellnessStat((int)newValue);
        });
                     
        story.BindExternalFunction("EndGame", () => EndGame());
        
        userFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as TMP_FontAsset;
        storyText.fontSize = userFontSize;
        storyText.font = userFont;

        Refresh();           
        
    }

    void Refresh()
    {
        ClearUI();        

        storyText.text = GetNextStoryBlock();

        if (story.currentTags.Count > 0)
        {

            foreach (string tag in story.currentTags)
            {
                Debug.Log("Current Tags: " + tag);
                EvaluateTag(tag);
            }
        }

        if (story.currentChoices.Count > 0)
        {
                                  

            foreach (Choice choice in story.currentChoices)
            {
                
                Button choiceButton = Instantiate(button) as Button;
                
                choiceButton.transform.SetParent(buttonPanel.transform, false);

                TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                choiceText.fontSize = userFontSize;
                choiceText.font = userFont;
                //Debug.Log("Choice font: " + choiceText.font + "--- User font: " + userFont);
                
                choiceText.text = choice.text.Replace("\\n", "\n");

                //Debug.Log("Choice font: " + choiceText.font + "--- User font: " + userFont);

                choiceButton.onClick.AddListener(delegate { OnClickChoiceButton(choice); });
            }

        }
        
    }

    void OnClickChoiceButton(Choice choice)
    {
        audioSource.PlayOneShot(buttonClick);
        story.ChooseChoiceIndex(choice.index);
        Refresh();
    }

    void ClearUI()
    {
        int childCount = buttonPanel.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(buttonPanel.transform.GetChild(i).gameObject);
        }
    }

    string GetNextStoryBlock()
    {
        string text = "";

        if (story.canContinue)
        {
            text = story.ContinueMaximally();
        }

        return text;
    }

    void EvaluateTag(string tag)
    {
        if (tag.Contains("SFX")) {
            SFXController.SFXPlayer(tag);
        }
        
        if(tag.Contains("time"))
        {
            timeLabel.text = "<b>" + tag.Substring(6) + "</b>";
        }

        if (tag.Contains("location"))
        {
            locationLabel.text = "<b>" + tag.Substring(10) + "</b>";
        }

        if(tag.Contains("background"))
        {
            BackgroundController.ChangeBackgroundImage(tag.Substring(12));
        }

    }

    void EndGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }


}
