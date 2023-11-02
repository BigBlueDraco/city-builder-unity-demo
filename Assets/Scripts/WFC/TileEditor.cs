using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


enum  TypeOfSymmetry
{
	Full,
	Horizontal,
	Vertical,
}
[CustomEditor(typeof(TileWithConnectorsData))]
[CanEditMultipleObjects]
public class TileEditor : Editor
{
	private bool isSymmetricalBtn;
	SerializedProperty isSymmetrical;

	private TypeOfSymmetry typeOfSymmetry;
	SerializedProperty typeOfSymmetryProp;
	SerializedProperty up;
	SerializedProperty right;
	SerializedProperty down;
	SerializedProperty left;
	SerializedProperty test;

	
   	void OnEnable()
	{
		up = serializedObject.FindProperty("_up");
		right = serializedObject.FindProperty("_right");
		down = serializedObject.FindProperty("_down");
		left = serializedObject.FindProperty("_left");
		typeOfSymmetryProp = serializedObject.FindProperty("_typeOfSymmetry");
		isSymmetrical = serializedObject.FindProperty("_isSymmetrical");

	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		isSymmetricalBtn = isSymmetrical.boolValue;
   		isSymmetricalBtn = EditorGUILayout.Toggle("Is symmetrical", isSymmetricalBtn);
		isSymmetrical.boolValue = isSymmetricalBtn;

		
		if(isSymmetricalBtn)
			{

			int enumValue = typeOfSymmetryProp.intValue; 
			typeOfSymmetry = (TypeOfSymmetry)EditorGUILayout.EnumPopup("Type of symmetry", (TypeOfSymmetry)enumValue);
			typeOfSymmetryProp.intValue = (int)typeOfSymmetry;

				if(typeOfSymmetry == TypeOfSymmetry.Full)
				{								
					EditorGUILayout.PropertyField(up, new GUIContent("All"));

					
					if(GUILayout.Button("Confirm"))
					{
						SerializedPropertyExtension.SetValue<string[]>(down, SerializedPropertyExtension.GetValue<string[]>(up).ToArray());
						SerializedPropertyExtension.SetValue<string[]>(right, SerializedPropertyExtension.GetValue<string[]>(up).ToArray());
						SerializedPropertyExtension.SetValue<string[]>(left, SerializedPropertyExtension.GetValue<string[]>(up).ToArray());
					}

				}
				if(typeOfSymmetry == TypeOfSymmetry.Vertical)
				{
					EditorGUILayout.PropertyField(up, new GUIContent("Vertical"));
					EditorGUILayout.PropertyField(right, new GUIContent("Right"));
					EditorGUILayout.PropertyField(left, new GUIContent("Left"));
					if(GUILayout.Button("Confirm"))
					{
						SerializedPropertyExtension.SetValue<string[]>(down, SerializedPropertyExtension.GetValue<string[]>(up));
					}
				}
				if(typeOfSymmetry == TypeOfSymmetry.Horizontal)
				{
					EditorGUILayout.PropertyField(right, new GUIContent("Horizontal"));
					EditorGUILayout.PropertyField(down, new GUIContent("Down"));
					EditorGUILayout.PropertyField(up, new GUIContent("Up"));
					if (GUILayout.Button("Confirm"))
					{ 
						SerializedPropertyExtension.SetValue<string[]>(left, SerializedPropertyExtension.GetValue<string[]>(right));
					
					}
				}
			
		}
		else
		{
			EditorGUILayout.PropertyField(up, new GUIContent("Up"));
			EditorGUILayout.PropertyField(right, new GUIContent("Right"));
			EditorGUILayout.PropertyField(down, new GUIContent("Down"));
			EditorGUILayout.PropertyField(left, new GUIContent("Left"));
		}
		serializedObject.ApplyModifiedProperties();
						
		// EditorGUILayout.PropertyField(up);
		// serializedObject.ApplyModifiedProperties();
		// var tile = (Tile)target;
		// GUILayout.TextField("");
		// if(GUILayout.Button("Log"))
		// {
		// 	Debug.Log(tile);
		// }
	}
}
