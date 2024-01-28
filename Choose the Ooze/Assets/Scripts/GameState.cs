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

    public Animator transition;
    public float transitionTime = 1f;
    
    
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
        transition.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionTime);
        _activeState = state;
        _transitionInProgress = true;
    }

    public void attemptTransition(State nextState)
    {
        if (_transitionInProgress) return;

        Ingredient heldItem = FindObjectOfType<MouseFollower>().ingredientBeingCarried;

        StartCoroutine(perform_transition(nextState));
        switch (_activeState)
        {
            case State.Workshop:
                break;
            case State.Cutting:
                switch (nextState)
                {
                    case State.Workshop:
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
                break;
            case State.Selling:
                break;
        }
    }
}
