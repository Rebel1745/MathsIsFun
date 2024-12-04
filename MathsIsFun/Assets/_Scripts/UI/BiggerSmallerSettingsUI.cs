using UnityEngine;
using TMPro;

public class BiggerSmallerSettingsUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _numberOfQuestionsInput;
    [SerializeField] private TMP_InputField _minimumNumberInput;
    [SerializeField] private TMP_InputField _maximumNumberInput;
    [SerializeField] private GameObject _biggerSmallerGameUI;

    public void StartBiggerSmallerGameButtonPressed()
    {
        _biggerSmallerGameUI.SetActive(true);
        //_biggerSmallerGameUI.GetComponentInChildren<BiggerOrSmaller>().InitialiseGame(int.Parse(_numberOfQuestionsInput.text), int.Parse(_minimumNumberInput.text), int.Parse(_maximumNumberInput.text));
        gameObject.SetActive(false);
    }
}
