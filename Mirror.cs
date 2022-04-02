using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public enum State{
        Reflecting,
        NotReflect,
    }
    public State state;

    private void Start() {
        state = State.NotReflect;
    }
    private void Update() {
        switch(state){
            case State.Reflecting:
                Debug.Log("xx");
                break;

        }
    }
}
