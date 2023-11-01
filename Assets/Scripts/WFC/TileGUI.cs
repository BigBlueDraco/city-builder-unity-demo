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
[CustomEditor(typeof(Tile))]
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
		up = serializedObject.FindProperty("up");
		right = serializedObject.FindProperty("right");
		down = serializedObject.FindProperty("down");
		left = serializedObject.FindProperty("left");
		typeOfSymmetryProp = serializedObject.FindProperty("typeOfSymmetry");
		isSymmetrical = serializedObject.FindProperty("isSymmetrical");

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
						SerializedPropertyExtension.SetValue<Tile[]>(down, SerializedPropertyExtension.GetValue<Tile[]>(up).ToArray());
						SerializedPropertyExtension.SetValue<Tile[]>(right, SerializedPropertyExtension.GetValue<Tile[]>(up).ToArray());
						SerializedPropertyExtension.SetValue<Tile[]>(left, SerializedPropertyExtension.GetValue<Tile[]>(up).ToArray());
					}

				}
				if(typeOfSymmetry == TypeOfSymmetry.Vertical)
				{
					EditorGUILayout.PropertyField(up, new GUIContent("Vertical"));
					EditorGUILayout.PropertyField(right, new GUIContent("Right"));
					EditorGUILayout.PropertyField(left, new GUIContent("Left"));
					if(GUILayout.Button("Confirm"))
					{
						SerializedPropertyExtension.SetValue<Tile[]>(down, SerializedPropertyExtension.GetValue<Tile[]>(up));
					}
				}
				if(typeOfSymmetry == TypeOfSymmetry.Horizontal)
				{
					EditorGUILayout.PropertyField(right, new GUIContent("Horizontal"));
					EditorGUILayout.PropertyField(down, new GUIContent("Down"));
					EditorGUILayout.PropertyField(up, new GUIContent("Up"));
					if (GUILayout.Button("Confirm"))
					{ 
						SerializedPropertyExtension.SetValue<Tile[]>(left, SerializedPropertyExtension.GetValue<Tile[]>(right));
					
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
