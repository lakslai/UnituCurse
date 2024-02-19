using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Data : MonoBehaviour
{
    public TMP_InputField coinInputField;
    public TMP_InputField nameInputField;
    public TextMeshProUGUI personNameText;
    public TextMeshProUGUI coinText;

    public string path;
    public Person person;

    private void Start()
    {
        Load();
        UpdatePersonInfoText();

        coinInputField.onValueChanged.AddListener(OnCoinValueChanged);
        nameInputField.onValueChanged.AddListener(OnNameValueChanged);
    }

    private void UpdatePersonInfoText()
    {
        if (person != null)
        {
            if (personNameText != null)
            {
                personNameText.text = $"Name: {person.personName}";
            }

            if (coinText != null)
            {
                coinText.text = $"Coin: {person.coin}";
            }
        }
    }

    private void OnCoinValueChanged(string newValue)
    {
        if (!string.IsNullOrEmpty(newValue)) // Проверка на пустое значение
        {
            if (int.TryParse(newValue, out int newCoinValue))
            {
                // Обновление данных о coin
                person.coin = newCoinValue;
                // Обновление отображаемой информации
                UpdatePersonInfoText();
            }
            else
            {
                // Обработка случая, если введено некорректное значение
                Debug.LogWarning("Invalid coin value entered.");
            }
        }
    }

    private void OnNameValueChanged(string newValue)
    {
        if (!string.IsNullOrEmpty(newValue)) // Проверка на пустое значение
        {
            // Обновление данных о personName
            person.personName = newValue;
            // Обновление отображаемой информации
            UpdatePersonInfoText();
        }
    }

    public async void Save()
    {
        person.personName = nameInputField.text;
        await SaveAndLoad.Save(path, person);
    }

    public async void Load()
    {
        person = await SaveAndLoad.Load<Person>(path);

        if (person != null)
        {
            coinInputField.text = person.coin.ToString();
            nameInputField.text = person.personName;
            UpdatePersonInfoText();
        }
    }
}