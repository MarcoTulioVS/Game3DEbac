using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
[CustomEditor(typeof(FSMCharacterAnimation))]
public class StateMachineAnimationEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FSMCharacterAnimation fsm = (FSMCharacterAnimation)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State machine animation");

        //

        if (fsm.animationStateMachine == null) return;

        if(fsm.animationStateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: ", fsm.animationStateMachine.CurrentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available states");

        if (showFoldout)
        {
            if(fsm.animationStateMachine.dictionaryState != null)
            {
                var keys = fsm.animationStateMachine.dictionaryState.Keys.ToArray();
                var values = fsm.animationStateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i],values[i]));
                }
            }
        }

    }
}
