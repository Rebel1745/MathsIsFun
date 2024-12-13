using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ProgressBar : MonoBehaviour
{
    public static ProgressBar instance;

    private VisualElement _progressBarLayout;
    private SliderInt _progressSlider;
    private Label _questionNumberText;
    private Label _questionResultText;
    private Button _nextQuestionButton;

    private int _numberOfQuestions;
    private bool _autoNextQuestion;

    // Timers
    private VisualElement _timersLayout;
    private bool _useTimer;
    private Label _totalTimeLabel;
    private float _totalTimeElapsed;
    private bool _updateTotalTimeTimer;
    private bool _useTimeToAnswerTimer;
    private float _maxTimePerQuestion;
    private VisualElement _timeToAnswerLayout;
    private Label _timeToAnswerLabel;
    private float _remainingTimeToAnswer;
    private bool _updateTimeToAnswerTimer;

    public event Action OnMaxTimeAllowedToAnswerSurpassed;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _progressBarLayout = root.Q<VisualElement>("ProgressBarContainer");
        _progressSlider = root.Q<SliderInt>("ProgressBar");
        _questionNumberText = root.Q<Label>("QuestionNumberText");
        _questionResultText = root.Q<Label>("QuestionResultText");
        _nextQuestionButton = root.Q<Button>("NextQuestionButton");
        _timersLayout = root.Q<VisualElement>("TimersLayout");
        _totalTimeLabel = root.Q<Label>("TotalTimeText");
        _timeToAnswerLayout = root.Q<VisualElement>("TimeToAnswerLayout");
        _timeToAnswerLabel = root.Q<Label>("TimeToAnswerText");
    }

    private void Update()
    {
        if (!_useTimer) return;

        UpdateTimers();
    }

    void UpdateTimers()
    {
        if (_updateTotalTimeTimer)
        {
            _totalTimeElapsed += Time.deltaTime;
            _totalTimeLabel.text = _totalTimeElapsed.ToString();
        }

        if (_updateTimeToAnswerTimer)
        {
            _remainingTimeToAnswer -= Time.deltaTime;

            if (_remainingTimeToAnswer <= 0.0f)
            {
                _remainingTimeToAnswer = 0.0f;
                // we have taken longer than allowed, fire off the MaxTimeAllowedToAnswerSurpassed event
                OnMaxTimeAllowedToAnswerSurpassed?.Invoke();
            }

            _timeToAnswerLabel.text = _remainingTimeToAnswer.ToString();
        }
    }

    public void PauseTotalTimeTimer(bool pause)
    {
        _updateTotalTimeTimer = !pause;
    }

    public void PauseTimeToAnswerTimer(bool pause)
    {
        _updateTimeToAnswerTimer = !pause;
    }

    public void RestartTimeToAnswerTimer()
    {
        _remainingTimeToAnswer = _maxTimePerQuestion;
        PauseTimeToAnswerTimer(false);
    }

    public float GetTotalTimeTaken()
    {
        return _totalTimeElapsed;
    }

    public void SetupProgressBar(GameSettings gs)
    {
        _numberOfQuestions = gs.NumberOfQuestions;
        _progressSlider.highValue = _numberOfQuestions;
        _autoNextQuestion = gs.AutoNextQuestion;
        _useTimer = gs.UseTimer;
        _useTimeToAnswerTimer = !gs.UnlimitedTime;
        _maxTimePerQuestion = (float)gs.MaxTimeToAnswer;

        if (gs.UseTimer)
        {
            _updateTotalTimeTimer = true;
            _timersLayout.style.display = DisplayStyle.Flex;

            if (_useTimeToAnswerTimer)
            {
                _updateTimeToAnswerTimer = true;
                _timeToAnswerLayout.style.display = DisplayStyle.Flex;
            }
            else
            {
                _timeToAnswerLayout.style.display = DisplayStyle.None;
            }
        }
        else
        {
            _timersLayout.style.display = DisplayStyle.None;
        }

        ShowProgressBar();
    }

    public void ShowProgressBar()
    {
        _progressBarLayout.style.display = DisplayStyle.Flex;
    }

    public void HideProgressBar()
    {
        _progressBarLayout.style.display = DisplayStyle.None;
    }

    public void NextQuestion(int currentQuestion)
    {
        if (!_autoNextQuestion) _nextQuestionButton.style.display = DisplayStyle.None;
        _questionResultText.style.display = DisplayStyle.None;
        _questionNumberText.text = currentQuestion + "/" + _numberOfQuestions;
        _progressSlider.value = currentQuestion;
        _remainingTimeToAnswer = _maxTimePerQuestion;
    }

    public void CorrectAnswer()
    {
        _questionResultText.style.display = DisplayStyle.Flex;
        _questionResultText.text = "! CORRECT !";
        if (!_autoNextQuestion) _nextQuestionButton.style.display = DisplayStyle.Flex;
    }

    public void IncorrectAnswer(bool isTimeout)
    {
        _questionResultText.style.display = DisplayStyle.Flex;
        _questionResultText.text = isTimeout ? "! TIMEOUT !" : "! INCORRECT !";
        if (!_autoNextQuestion) _nextQuestionButton.style.display = DisplayStyle.Flex;
    }
}
