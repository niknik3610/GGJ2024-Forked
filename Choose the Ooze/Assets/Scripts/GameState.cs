using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private State _activeState;
    private bool _cameraSettled = true;
    [SerializeField] private Transform[] _stateCameraPositions;
    [SerializeField] private Camera _camera;

    private float _cameraSpeed = 10;
    
    
    public enum State
    {
        Workshop,
        Cutting,
        Grinding,
        Cooking,
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
        if (!_cameraSettled)
        {
            Vector3 target = _stateCameraPositions[(int) _activeState].position;
            Vector3 position = _camera.transform.position;
            _camera.transform.position = Vector3.Lerp(position, target, _cameraSpeed * Time.deltaTime);
            _cameraSettled = _camera.transform.position.Equals(target);
            return;
        }
    }

    private void perform_transition(State state)
    {
        _activeState = state;
        _cameraSettled = false;
    }

    public void attemptTransition(State nextState)
    {
        if (!_cameraSettled) return;

        Ingredient heldItem = FindObjectOfType<MouseFollower>().ingredientBeingCarried;

        switch (_activeState)
        {
            case State.Workshop:
                perform_transition(nextState);
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
                    case State.Cooking:
                        break;
                    case State.Selling:
                        break;
                }
                break;
            case State.Grinding:
                break;
            case State.Cooking:
                break;
            case State.Selling:
                break;
        }
    }
}
