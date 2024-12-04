using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BiggerOrSmallerSettings : MonoBehaviour
{
    private VisualElement _biggerOrSmallerGame;
    private VisualElement _biggerOrSmallerSettings;

    private IntegerField _numberOfQuestionsField;
    private IntegerField _smallestNumberField;
    private IntegerField _largestNumberField;

    private Toggle _autoNextQuestionToggle;
    private Toggle _useTimerToggle;

    private Label _maxTimePerQuestionLabel;
    private IntegerField _maxTimePerQuestionField;

    private Button _startGameButton;

    private BiggerOrSmaller _gameScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameScript = GetComponent<BiggerOrSmaller>();

        var root = GetComponent<UIDocument>().rootVisualElement;

        _biggerOrSmallerGame = root.Q<VisualElement>("BiggerOrSmallerGame");
        _biggerOrSmallerSettings = root.Q<VisualElement>("BiggerOrSmallerSettings");
        _numberOfQuestionsField = root.Q<IntegerField>("NoOfQsIntField");
        _smallestNumberField = root.Q<IntegerField>("MinNumberIntField");
        _largestNumberField = root.Q<IntegerField>("MaxNumberIntField");
        _autoNextQuestionToggle = root.Q<Toggle>("AutoNextQToggle");
        _useTimerToggle = root.Q<Toggle>("UseTimerToggle");
        _maxTimePerQuestionLabel = root.Q<Label>("MaxTimePerQLabel");
        _maxTimePerQuestionField = root.Q<IntegerField>("MaxTimePerQIntField");
        _startGameButton = root.Q<Button>("StartBiggerSmallerButton");

        _useTimerToggle.RegisterCallback<ChangeEvent<bool>>(UseTimerToogleChanged);
        _startGameButton.RegisterCallback<ClickEvent>(StartGameButtonPressed);
    }

    private void UseTimerToogleChanged(ChangeEvent<bool> evt)
    {
        _maxTimePerQuestionLabel.SetEnabled(evt.newValue);
        _maxTimePerQuestionField.SetEnabled(evt.newValue);
    }

    public void StartGameButtonPressed(ClickEvent evt)
    {
        _biggerOrSmallerSettings.style.display = DisplayStyle.None;
        _biggerOrSmallerGame.style.display = DisplayStyle.Flex;

        _gameScript.InitialiseGame(int.Parse(_numberOfQuestionsField.text), int.Parse(_smallestNumberField.text), int.Parse(_largestNumberField.text), true);
    }
}
