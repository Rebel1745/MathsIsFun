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
    }

    public void SetupProgressBar(int numberOfQuestions, bool autoNextQuestion)
    {
        _numberOfQuestions = numberOfQuestions;
        _progressSlider.highValue = _numberOfQuestions;
        _autoNextQuestion = autoNextQuestion;

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
    }

    public void CorrectAnswer()
    {
        _questionResultText.style.display = DisplayStyle.Flex;
        _questionResultText.text = "! CORRECT !";
        if (!_autoNextQuestion) _nextQuestionButton.style.display = DisplayStyle.Flex;
    }

    public void IncorrectAnswer()
    {
        _questionResultText.style.display = DisplayStyle.Flex;
        _questionResultText.text = "! INCORRECT !";
        if (!_autoNextQuestion) _nextQuestionButton.style.display = DisplayStyle.Flex;
    }
}
