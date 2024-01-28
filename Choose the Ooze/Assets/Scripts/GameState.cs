using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private State _activeState;
    private bool _transitionInProgress = false;
    [SerializeField] private Transform[] _stateCameraPositions;
    [SerializeField] private Camera _camera;
    public CircleWipe circleWipe;

    public int currentScore = 0;
    
    public enum State
    {
        Workshop,
        Cutting,
        Grinding,
        Selling,
    }

    static GameState instance;
    public static GameState getInstance() { return instance; }

    void Start()
    {
        instance = this;
        if (_stateCameraPositions.Length != Enum.GetNames(typeof(State)).Length)
        {
            _stateCameraPositions = new Transform[Enum.GetNames(typeof(State)).Length];
            for (int i = 0; i < _stateCameraPositions.Length; i++)
            {
                _stateCameraPositions[i] = _stateCameraPositions[0];
            }
            throw new Exception("Not enough camera positions");
        }
        _activeState = State.Workshop;
    }

    private void Update()
    {
        if (_transitionInProgress)
        {
            Vector3 target = _stateCameraPositions[(int) _activeState].position;
            target.z = -10;
            _camera.transform.position = target;
            _transitionInProgress = false;
        }
    }

    private IEnumerator perform_transition(State state)
    {
        circleWipe.Wipe();
        yield return new WaitForSeconds(circleWipe.transitionTime);
        switch (_activeState)
        {
            case State.Workshop:
                switch (state)
                {
                    case State.Workshop:
                        break;
                    case State.Cutting:
                        FindObjectOfType<CuttingBoardMinigame>().EnterMiniGame();
                        break;
                    case State.Grinding:
                        FindObjectOfType<GrindingMinigame>().StartMinigame();
                        break;
                    case State.Selling:
                        break;
                }
                break;
            case State.Cutting:
                
                switch (state)
                {
                    case State.Workshop:
                        FindObjectOfType<CuttingBoardMinigame>().ExitMiniGame();
                        break;
                    case State.Cutting:
                        break;
                    case State.Grinding:
                        break;
                    case State.Selling:
                        break;
                }
                break;
            case State.Grinding:
                switch (state)
                {
                    case State.Workshop:
                        FindObjectOfType<GrindingMinigame>().EndMinigame();
                        break;
                    case State.Cutting:
                        break;
                    case State.Grinding:
                        break;
                    case State.Selling:
                        break;
                }
                break;
            case State.Selling:
                break;
        }
        _activeState = state;
        _transitionInProgress = true;
    }

    public void attemptTransition(State nextState)
    {
        if (_transitionInProgress) return;

        Ingredient heldItem = FindObjectOfType<MouseFollower>().ingredientBeingCarried;

        StartCoroutine(perform_transition(nextState));
    }
}
