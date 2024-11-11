using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class BiggerOrSmaller : MonoBehaviour
{
    [SerializeField] private TMP_Text _firstNumberText;
    [SerializeField] private TMP_Text _secondNumberText;
    [SerializeField] private TMP_Text _biggerOrSmallerText;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Button _biggerButton;
    [SerializeField] private Button _smallerButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private int _minNumber = 1;
    [SerializeField] private int _maxNumber = 10;
    [SerializeField] private float _resultDelay = 0.25f;
    [SerializeField] private float _resetDelay = 0.25f;
    [SerializeField] private string _defaultBiggerOrSmallerText = "is _______ than";
    [SerializeField] private bool _autoRestart = false;
    [SerializeField] private Color _correctColor = Color.green;
    [SerializeField] private Color _incorrectColor = Color.red;

    private int _firstNumber;
    private int _secondNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseGame();
    }

    private void InitialiseGame()
    {
        if (_minNumber == _maxNumber)
        {
            Debug.LogError("Minimum and maximum numbers cannot be the same");
            return;
        }

        _resultText.text = "";
        _firstNumber = Random.Range(_minNumber, _maxNumber);
        _secondNumber = Random.Range(_minNumber, _maxNumber);
        _biggerOrSmallerText.text = _defaultBiggerOrSmallerText;

        // ensure that the numbers are not the same
        while (_firstNumber == _secondNumber)
        {
            _secondNumber = Random.Range(_firstNumber, _maxNumber);
        }

        _firstNumberText.text = _firstNumber.ToString();
        _secondNumberText.text = _secondNumber.ToString();

        EnableDisableButtons(true);

        _restartButton.gameObject.SetActive(false);
    }

    public void OnBiggerButtonPress()
    {
        _biggerOrSmallerText.text = "is BIGGER than";
        EnableDisableButtons(false);
        StartCoroutine(CheckIfCorrect("b"));
    }

    public void OnSmallerButtonPress()
    {
        _biggerOrSmallerText.text = "is SMALLER than";
        EnableDisableButtons(false);
        StartCoroutine(CheckIfCorrect("s"));
    }

    public void OnRestartButtonPress()
    {
        InitialiseGame();
    }

    private void EnableDisableButtons(bool enable)
    {
        _biggerButton.enabled = enable;
        _smallerButton.enabled = enable;
    }

    IEnumerator CheckIfCorrect(string answer)
    {
        yield return new WaitForSeconds(_resultDelay);

        if ((answer == "b" && _firstNumber > _secondNumber) || (answer == "s" && _firstNumber < _secondNumber))
        {
            _resultText.color = _correctColor;
            _resultText.text = "!! CORRECT !!";
        }
        else
        {
            _resultText.color = _incorrectColor;
            _resultText.text = "!! WRONG !!";
        }

        yield return new WaitForSeconds(_resetDelay);

        if (_autoRestart) InitialiseGame();
        else _restartButton.gameObject.SetActive(true);
    }
}
