using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HabitantDialog : Interactible
{
    public SignalSender dialog_signal;
    public Dialog dialog;
    public ChoiceBox choice_box;
    public SignalSender dialog_end_signal;
    public SignalSender interagit;
    public bool inDialog = false;
    public GameObject dialogBox;
    public Text dialogText;

    public IEnumerator startDialog(List<string> choices = null, Action<int> onChoice = null)
    {
        yield return new WaitForEndOfFrame();
        dialogBox.SetActive(true);
        dialogText.gameObject.SetActive(true);
        dialog_signal.raise();
        inDialog = true;

        foreach (var line in dialog.lines)
        {
            dialogText.text = line;
            yield return new WaitUntil(() =>
           Input.GetKeyDown("space"));
            yield return new WaitForEndOfFrame();
        }

        if (choices != null && choices.Count > 1)
        {
            yield return choice_box.showChoices(choices, onChoice);
        }

        endDialog();
    }

    public void endDialog()
    {
        dialogBox.SetActive(false);
        dialogText.gameObject.SetActive(false);
        dialog_end_signal.raise();
        inDialog = false;
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown("space") && playerInRange)
        {
            if (!inDialog)
            {
                StartCoroutine(startDialog());
                interagit.raise();
            }
        }
    }
}
