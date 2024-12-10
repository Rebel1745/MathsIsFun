using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BiggerOrSmallerSettings : MonoBehaviour, IGameSettings
{
    private VisualElement _biggerOrSmallerGame;
    private VisualElement _biggerOrSmallerSettings;

    private IntegerField _numberOfQuestionsField;
    private IntegerField _smallestNumberField;
    private IntegerField _largestNumberField;
    private IntegerField _maxTimeToAnswerField;

    private Toggle _autoNextQuestionToggle;
    private Toggle _useTimerToggle;
    private Toggle _unlimitedTimeToggle;

    private VisualElement _unlimitedTimePerQuestion;
    private VisualElement _maxTimePerQuestion;

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
        _unlimitedTimePerQuestion = root.Q<VisualElement>("UnlimitedTimePerQLayout");
        _unlimitedTimeToggle = root.Q<Toggle>("UnlimitedTimeToggle");
        _maxTimePerQuestion = root.Q<VisualElement>("MaxTimePerQLayout");
        _startGameButton = root.Q<Button>("StartBiggerSmallerButton");
        _maxTimeToAnswerField = root.Q<IntegerField>("MaxTimePerQIntField");

        _useTimerToggle.RegisterCallback<ChangeEvent<bool>>(UseTimerToogleChanged);
        _unlimitedTimeToggle.RegisterCallback<ChangeEvent<bool>>(UnlimitedTimeToogleChanged);
        _startGameButton.RegisterCallback<ClickEvent>(StartGameButtonPressed);
    }

    private void UseTimerToogleChanged(ChangeEvent<bool> evt)
    {
        _unlimitedTimePerQuestion.SetEnabled(evt.newValue);
    }

    private void UnlimitedTimeToogleChanged(ChangeEvent<bool> evt)
    {
        _maxTimePerQuestion.SetEnabled(!evt.newValue);
        _maxTimeToAnswerField.isReadOnly = evt.newValue;
    }

    public void StartGameButtonPressed(ClickEvent evt)
    {
        GameSettings gs = new()
        {
            GameName = "Bigger or Smaller",
            GameVE = _biggerOrSmallerGame,
            GameSettingsVE = _biggerOrSmallerSettings,
            GameSettingsInterface = this,
            NumberOfQuestions = _numberOfQuestionsField.value,
            SmallestNumber = _smallestNumberField.value,
            LargestNumber = _largestNumberField.value,
            AutoNextQuestion = _autoNextQuestionToggle.value,
            UseTimer = _useTimerToggle.value,
            UnlimitedTime = _unlimitedTimeToggle.value,
            MaxTimeToAnswer = _maxTimeToAnswerField.value
        };

        _biggerOrSmallerSettings.style.display = DisplayStyle.None;
        _biggerOrSmallerGame.style.display = DisplayStyle.Flex;

        _gameScript.InitialiseGame(gs);
    }

    public void ShowGameSettings(GameSettings gs)
    {
        _biggerOrSmallerSettings.style.display = DisplayStyle.Flex;
        _numberOfQuestionsField.value = gs.NumberOfQuestions;
        _smallestNumberField.value = gs.SmallestNumber;
        _largestNumberField.value = gs.LargestNumber;
        _autoNextQuestionToggle.value = gs.AutoNextQuestion;
        _useTimerToggle.value = gs.UseTimer;
        _unlimitedTimeToggle.value = gs.UnlimitedTime;
        _maxTimeToAnswerField.value = gs.MaxTimeToAnswer;
    }
}
