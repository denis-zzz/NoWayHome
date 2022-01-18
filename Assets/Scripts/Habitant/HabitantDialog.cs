using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabitantDialog : Interactible
{
    public SignalSender dialog_signal;
    public Dialog dialog;
    public SignalSender dialog_end_signal;
    public SignalSender interagit;
    private bool inDialog = false;
    public int currentLine = 0;
    public GameObject dialogBox;
    public Text dialogText;

    public void startDialog()
    {
        dialogBox.SetActive(true);
        dialogText.gameObject.SetActive(true);
        dialog_signal.raise();
        inDialog = true;

        dialogText.text = dialog.lines[currentLine];
    }

    public void endDialog()
    {
        dialogBox.SetActive(false);
        dialogText.gameObject.SetActive(false);
        dialog_end_signal.raise();
        inDialog = false;
        currentLine = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && playerInRange)
        {
            if (!inDialog)
            {
                startDialog();
                interagit.raise();
            }
            else
            {
                currentLine++;
                if (currentLine < dialog.lines.Count)
                {
                    dialogText.text = dialog.lines[currentLine];
                }
                else
                {
                    endDialog();
                }
            }
        }
    }
}
