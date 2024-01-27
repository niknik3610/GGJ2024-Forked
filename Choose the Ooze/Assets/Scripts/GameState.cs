using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.GraphicsBuffer;

public class GameState : MonoBehaviour
{
    private State _activeState;
    private bool _cameraSettled = true;
    [SerializeField] private Vector3[] _stateCameraPositions;
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
    void Start()
    {
        if (_stateCameraPositions.Length != Enum.GetNames(typeof(State)).Length)
        {
            _stateCameraPositions = new Vector3[Enum.GetNames(typeof(State)).Length];
            for (int i = 0; i < _stateCameraPositions.Length; i++)
            {
                _stateCameraPositions[i] = _stateCameraPositions[0];
            }
            throw new Exception("Not enough camera positions");
        }
        _activeState = State.Workshop;
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (!_cameraSettled)
        {

            Vector3 target = _stateCameraPositions[(int) _activeState];
            Vector3 position = _camera.transform.position;
            _camera.transform.position = Vector3.Lerp(position, target, _cameraSpeed * Time.deltaTime);
            _cameraSettled = _camera.transform.position.Equals(target);
        }
    }

    private void perform_transition(State state)
    {
        _activeState = state;
        _cameraSettled = false;
    }

    public void attemptTransition(State nextState, MouseFollower heldItem)
    {
        if (!_cameraSettled) return;
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
