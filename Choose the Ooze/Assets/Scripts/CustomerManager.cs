using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public ClientRequestGenerator requestGenerator;
    public Character currentCharacter = null;
    public GameObject characterPrefab;
    public TextMeshProUGUI dialogueTextField;
    public Transform characterPosition;
    public Cauldron cauldron;
    public Button OKButton;
    void Start()
    {
        requestGenerator = new ClientRequestGenerator();
    }
    void Update()
    {
        if(currentCharacter == null)
        {
            CreateRandomCustomer();
        }
    }

    public void CreateRandomCustomer()
    {
        GameObject characterObject = Instantiate(characterPrefab, characterPosition.position, Quaternion.identity, characterPosition);
        characterObject.GetComponent<Character>().SetClientType(ClientType.Fish);
        currentCharacter = characterObject.GetComponent<Character>();
        ClientRequest request = requestGenerator.generateRequest(currentCharacter.type); 
        SetDialogueTextField(FindObjectOfType<DialogueManager>().generateCustomerDialogue(currentCharacter.type, request));
        cauldron.SetExpectedIngredients(request);
    }

    public void SetDialogueTextField(String text)
    {
        dialogueTextField.text = text;
    }
}
