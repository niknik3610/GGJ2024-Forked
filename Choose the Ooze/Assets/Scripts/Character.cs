using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public ClientType type {get; private set;} = ClientType.None;
    private MouseFollower _mouseFollower;

    private Sprite _sadSprite;
    private Sprite _happySprite;
    private SpriteRenderer _renderer;

    public float waitBeforeLeavingTime = 4f;
    void Awake()
    {
        _mouseFollower = FindObjectOfType<MouseFollower>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetClientType(ClientType Type)
    {
        type = Type;
        switch (type)
        {
            case ClientType.None:
                break;
            case ClientType.Fish:
                _sadSprite = Resources.Load<Sprite>("Characters/SadFisherman");
                _happySprite = Resources.Load<Sprite>("Characters/HappyFisherman");
                _renderer.sprite = _sadSprite;
                break;
            case ClientType.Witch:
                _sadSprite = Resources.Load<Sprite>("Characters/WitchSad");
                _happySprite = Resources.Load<Sprite>("Characters/WitchHappy");
                _renderer.sprite = _sadSprite;
                break;
            case ClientType.Viking:
                break;
            case ClientType.Cultist:
                _sadSprite = Resources.Load<Sprite>("Characters/CultistSad");
                _happySprite = Resources.Load<Sprite>("Characters/CultistHappy");
                _renderer.sprite = _sadSprite;
                break;
            case ClientType.Scientist:
                _sadSprite = Resources.Load<Sprite>("Characters/ScientistSad");
                _happySprite = Resources.Load<Sprite>("Characters/ScientistHappy");
                _renderer.sprite = _sadSprite;
                break;
            case ClientType.Princess:
                _sadSprite = Resources.Load<Sprite>("Characters/PrincessSad");
                _happySprite = Resources.Load<Sprite>("Characters/PrincessHappy");
                _renderer.sprite = _sadSprite;
                break;
            case ClientType.Milkman:
                break;
            case ClientType.Child:
                break;
        }
    }

    void Update()
    {
        Mouse mouse = Mouse.current;
        
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            var raycastHits = Physics.RaycastAll(ray);
            foreach(var rhit in raycastHits)
            {
                if(rhit.collider.gameObject == gameObject)
                {
                    if(_mouseFollower.potionBeingCarried != null)
                    {
                        GivePotion(_mouseFollower.potionBeingCarried);
                    }
                }
            }
        }
    }
    public void GivePotion(Potion potion)
    {
        GameState.getInstance().currentScore += (int)potion.value;
        float happinessLevelFloat;
        Happiness level;
        if(potion.perfectValue == 0)
        {
            happinessLevelFloat = 0;
        }
        else
        {
            happinessLevelFloat = potion.value / potion.perfectValue;
        }
        if(happinessLevelFloat < 0.001f)
        {
            level = Happiness.Terrible;
        }
        else if(happinessLevelFloat < 0.33f)
        {
            level = Happiness.Poor;
        }
        else if(happinessLevelFloat < 0.66f)
        {
            level = Happiness.Normal;
        }
        else if(happinessLevelFloat < 0.99f)
        {
            level = Happiness.Great;
        }
        else
        {
            level = Happiness.Perfect;
        }
        StartCoroutine(leave(level));

    }

    public IEnumerator leave(Happiness level)
    {
        CustomerManager cm = FindObjectOfType<CustomerManager>();
        DialogueManager dm = FindObjectOfType<DialogueManager>();
        bool textSet = false;
        for(int i = 0; i < dm.dialogues.acceptanceDialogues.Length; i++)
        {
            if(dm.dialogues.acceptanceDialogues[i].happinessLevel == level)
            {
                int rand = Random.Range(0, dm.dialogues.acceptanceDialogues[i].dialogueLines.Length);
                string text = dm.dialogues.acceptanceDialogues[i].dialogueLines[rand];
                cm.SetDialogueTextField(text);
                textSet = true;
                if((int)level > (int)Happiness.Poor)
                {
                    _renderer.sprite = _happySprite;
                }
                break;
            }
        }
        if(!textSet)
        {
            cm.SetDialogueTextField("Thanks!");
        }
        Destroy(_mouseFollower.potionBeingCarried.gameObject);
        cm.OKButton.interactable = false;
        yield return new WaitForSeconds(waitBeforeLeavingTime);
        _mouseFollower.potionBeingCarried = null;
        GameState.getInstance().circleWipe.Wipe();
        yield return new WaitForSeconds(GameState.getInstance().circleWipe.transitionTime);
        cm.OKButton.interactable = true;
        cm.CreateRandomCustomer();
        Destroy(gameObject);
    }

}
