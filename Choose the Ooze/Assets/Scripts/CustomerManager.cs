using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public ClientType[] implementedTypes;
    public ClientRequestGenerator requestGenerator;
    public Character currentCharacter = null;
    public GameObject characterPrefab;
    public TextMeshProUGUI dialogueTextField;
    public Transform characterPosition;
    public Cauldron cauldron;
    public Button OKButton;
    private ClientType lastClient = ClientType.None;
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
        characterObject.GetComponent<Character>().SetClientType(ChooseClientType());
        currentCharacter = characterObject.GetComponent<Character>();
        lastClient = currentCharacter.type;
        ClientRequest request = requestGenerator.generateRequest(currentCharacter.type); 
        SetDialogueTextField(FindObjectOfType<DialogueManager>().generateCustomerDialogue(currentCharacter.type, request));
        cauldron.SetExpectedIngredients(request);
    }

    public void SetDialogueTextField(string text)
    {
        dialogueTextField.text = text;
    }
    public ClientType ChooseClientType()
    {
        int rand = Random.Range(0, implementedTypes.Length);
        while(implementedTypes[rand] == lastClient)
        {
            rand = Random.Range(0, implementedTypes.Length);
        }
        Debug.Log(implementedTypes[rand]);
        return implementedTypes[rand];
    }
}
