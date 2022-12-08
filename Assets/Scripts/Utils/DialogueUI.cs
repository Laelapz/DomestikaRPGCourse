using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float textSpeed;

    [Header("Dependencies")]
    [SerializeField] private GameObject ui;

    [SerializeField] private GameObject leftCharacter;
    [SerializeField] private GameObject rightCharacter;

    [SerializeField] private TextMeshProUGUI leftCharacterName;
    [SerializeField] private Image leftCharacterPortrait;
    [SerializeField] private TextMeshProUGUI rightCharacterName;
    [SerializeField] private Image rightCharacterPortrait;
    [SerializeField] private TextMeshProUGUI dialogueBox;

    private string _currenteSentence;


    public void StartConversation(
        string leftCharacterName,
        Sprite leftCharacterPortrait,
        string rightCharacterName,
        Sprite rightCharacterPortrait)
    {

        this.CleanUI();

        this.leftCharacterName.text = leftCharacterName;
        this.leftCharacterPortrait.sprite = leftCharacterPortrait;

        this.rightCharacterName.text = rightCharacterName;
        this.rightCharacterPortrait.sprite = rightCharacterPortrait;

        this.dialogueBox.text = "";

        ToggleLeftCharacter(false);
        ToggleRightCharacter(false);
    }

    public void DisplaySentence(string characterName, string sentenceText)
    {
        if(characterName == leftCharacterName.text)
        {
            ToggleLeftCharacter(true);
            ToggleRightCharacter(false);
        }
        else
        {
            ToggleLeftCharacter(false);
            ToggleRightCharacter(true);
        }

        this._currenteSentence = sentenceText;
        StartCoroutine(TypeCurrentSentence());
    }

    private IEnumerator TypeCurrentSentence()
    {
        this.dialogueBox.text = "";

        foreach(char letter in this._currenteSentence.ToCharArray())
        {
            this.dialogueBox.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        this.dialogueBox.text = this._currenteSentence;
        this._currenteSentence = null;

    }

    public bool IsSentenceInProcess()
    {
        return this._currenteSentence != null;
    }

    public void FinishDisplayingSentence()
    {
        StopAllCoroutines();
        this.dialogueBox.text = this._currenteSentence;
        this._currenteSentence = null;
    }

    private void CleanUI()
    {
        this.leftCharacterName.text = null;
        this.leftCharacterPortrait.sprite = null;

        this.rightCharacterName.text = null;
        this.rightCharacterPortrait.sprite = null;

        this.dialogueBox.text = "";
    }

    private void ToggleLeftCharacter(bool status)
    {
        this.leftCharacter.SetActive(status);
    }

    private void ToggleRightCharacter(bool status)
    {
        this.rightCharacter.SetActive(status);
    }

    public void EndConversation()
    {
        this.CleanUI();
    }
    
}