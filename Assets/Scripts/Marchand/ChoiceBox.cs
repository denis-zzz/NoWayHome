using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChoiceBox : MonoBehaviour
{
    public ChoiceText choice_text;
    bool choice_made = false;
    private List<ChoiceText> choice_list = new List<ChoiceText>();
    int currentChoice;

    public IEnumerator showChoices(List<string> choices, Action<int> onChoice)
    {
        choice_made = false;
        currentChoice = 0;
        gameObject.SetActive(true);
        choice_text.gameObject.SetActive(true);

        choice_list = new List<ChoiceText>();
        foreach (var choice in choices)
        {
            var choiceText = Instantiate(choice_text, transform);
            choiceText.TextField.text = choice;
            choice_list.Add(choiceText);
        }

        yield return new WaitUntil(() => choice_made == true);

        onChoice?.Invoke(currentChoice);

        gameObject.SetActive(false);
        choice_text.gameObject.SetActive(false);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            currentChoice--;
        else if (Input.GetKeyDown(KeyCode.S))
            currentChoice++;

        currentChoice = Mathf.Clamp(currentChoice, 0,
        choice_list.Count - 1);

        for (int i = 0; i < choice_list.Count; i++)
        {
            choice_list[i].SetSelected(i == currentChoice);
        }

        if (Input.GetKeyDown(KeyCode.Return))
            choice_made = true;
    }
}
