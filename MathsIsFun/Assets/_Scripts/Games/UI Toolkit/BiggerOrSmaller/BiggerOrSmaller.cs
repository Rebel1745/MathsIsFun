using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class BiggerOrSmaller : MonoBehaviour
{
    private VisualElement _biggerOrSmallerGame;
    private Label _firstNumberText;
    private Label _secondNumberText;
    private Label _biggerOrSmallerText;
    private Label _resultText;
    private Button _biggerButton;
    private Button _smallerButton;
    private int _minNumber;
    private int _maxNumber;
    private bool _autoNextQuestion;
    private float _resultDelay = 0.25f;
    private float _nextQuestionDelay = 0.25f;
    private string _defaultBiggerOrSmallerText = "is _______ than";

    private int _numberOfQuestions;
    private int _currentQuestionNumber;
    private int _firstNumber;
    private int _secondNumber;
    private int _numberOfQuestionsCorrect;

    private void Start()
    {
        GetUIElements();
    }

    void GetUIElements()
    {
        //Grab the topmost visual element in the UI Document
        var root = GetComponent<UIDocument>().rootVisualElement;

        _biggerOrSmallerGame = root.Q<VisualElement>("BiggerOrSmallerGame");

        _firstNumberText = root.Q<Label>("Number1");
        _secondNumberText = root.Q<Label>("Number2");
        _biggerOrSmallerText = root.Q<Label>("IsBiggerOrSmaller");
        //_resultText = root.Q<Label>("Result");

        _biggerButton = root.Q<Button>("BiggerButton");
        _smallerButton = root.Q<Button>("SmallerButton");

        _biggerButton.RegisterCallback<ClickEvent>(OnBiggerButtonPress);
        _smallerButton.RegisterCallback<ClickEvent>(OnSmallerButtonPress);
    }

    public void InitialiseGame(int numberOfQuestions, int minNumber, int maxNumber, bool autoNextQuestion)
    {
        _numberOfQuestions = numberOfQuestions;
        _currentQuestionNumber = 0;
        _numberOfQuestionsCorrect = 0;
        _minNumber = minNumber;
        _maxNumber = maxNumber + 1; // set this to +1 as it will now be included in the random range (which is max number exclusive)
        _autoNextQuestion = autoNextQuestion;

        if (_minNumber == _maxNumber)
        {
            Debug.LogError("Minimum and maximum numbers cannot be the same");
            return;
        }
        if (_minNumber > _maxNumber)
        {
            Debug.LogError("Minimum number cannot be bigger than the maximum number");
            return;
        }

        NextQuestion();
    }

    private void NextQuestion()
    {
        _currentQuestionNumber++;
        //_resultText.text = "";
        //_resultText.RemoveFromClassList("ResultTextCorrect");
        //_resultText.RemoveFromClassList("ResultTextIncorrect");
        _firstNumber = Random.Range(_minNumber, _maxNumber);
        _secondNumber = Random.Range(_minNumber, _maxNumber);
        _biggerOrSmallerText.text = _defaultBiggerOrSmallerText;

        int currentNumberChangeAttempt = 0;

        // ensure that the numbers are not the same
        while (_firstNumber == _secondNumber)
        {
            _secondNumber = Random.Range(_firstNumber, _maxNumber);

            if (currentNumberChangeAttempt > 10)
            {
                Debug.Log("Too many attempts made to set the numbers, defaulting to the largest/smallest");
                _firstNumber = _minNumber;
                _secondNumber = _maxNumber;
            }

            currentNumberChangeAttempt++;
        }

        _firstNumberText.text = _firstNumber.ToString();
        _secondNumberText.text = _secondNumber.ToString();

        EnableDisableButtons(true);
    }

    public void OnBiggerButtonPress(ClickEvent evt)
    {
        _biggerOrSmallerText.text = "is BIGGER than";
        EnableDisableButtons(false);
        StartCoroutine(CheckIfCorrect("b"));
    }

    public void OnSmallerButtonPress(ClickEvent evt)
    {
        _biggerOrSmallerText.text = "is SMALLER than";
        EnableDisableButtons(false);
        StartCoroutine(CheckIfCorrect("s"));
    }

    public void OnNextQuestionButtonPress()
    {
        NextQuestion();
    }

    private void EnableDisableButtons(bool enable)
    {
        _biggerButton.SetEnabled(enable);
        _smallerButton.SetEnabled(enable);
    }

    IEnumerator CheckIfCorrect(string answer)
    {
        yield return new WaitForSeconds(_resultDelay);

        if ((answer == "b" && _firstNumber > _secondNumber) || (answer == "s" && _firstNumber < _secondNumber))
        {
            //_resultText.AddToClassList("ResultTextCorrect");
            //_resultText.text = "!! CORRECT !!";
            _numberOfQuestionsCorrect++;
        }
        else
        {
            //_resultText.AddToClassList("ResultTextIncorrect");
            //_resultText.text = "!! WRONG !!";
        }

        yield return new WaitForSeconds(_nextQuestionDelay);

        if (_currentQuestionNumber == _numberOfQuestions)
        {
            /*_resultUI.SetActive(true);
            _resultUI.GetComponent<ResultsScreenUI>().ShowResults(_numberOfQuestions, _numberOfQuestionsCorrect);
            gameObject.SetActive(false);*/
            DisplayResult();
        }
        else if (_autoNextQuestion) NextQuestion();
        //else _nextQuestionButton.gameObject.SetActive(true);
    }

    void DisplayResult()
    {
        _biggerOrSmallerGame.style.display = DisplayStyle.None;
        ResultsScreen.instance.ShowResults(_numberOfQuestions, _numberOfQuestionsCorrect);
    }
}