namespace Moona {
    using Moona.Assets;
    using UnityEditor;
#if UNITY_EDITOR
    using UnityEngine;
#endif
    [System.Serializable]
    public class Volume {
        public float value = 0f;
        public float randomRange = 0f;

#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(Volume))]
        public class VolumeDrawer : PropertyDrawer {
            private bool foldOut;
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
                // Using BeginProperty / EndProperty on the parent property means that
                // prefab override logic works on the entire property.
                EditorGUI.BeginProperty(position, label, property);
                {
                    // Draw label
                    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                    // Don't make child fields be indented
                    var indent = EditorGUI.indentLevel;
                    EditorGUI.indentLevel = 0;
                    // Calculate rects
                    var valRect = new Rect(position.x, position.y, 30, position.height);
                    var dbRect = new Rect(valRect.x + 35, position.y, 30, position.height);
                    var foldRect = new Rect(dbRect.x + 35 + 10, position.y, 20, position.height);
                    var rndRect = new Rect(foldRect.x + 25, position.y, position.width - (35 + 45 + 25), position.height);

                    // Draw fields - passs GUIContent.none to each so they are drawn without labels
                    EditorGUI.PropertyField(valRect, property.FindPropertyRelative("value"), GUIContent.none);
                    GUI.enabled = false;
                    EditorGUI.FloatField(dbRect, property.FindPropertyRelative("value").floatValue);
                    GUI.enabled = true;
                    foldOut = EditorGUI.Foldout(foldRect, foldOut, GUIContent.none);
                    if (foldOut) {
                        EditorGUI.PropertyField(rndRect, property.FindPropertyRelative("randomRange"), GUIContent.none);
                    }
                    // Set indent back to what it was
                    EditorGUI.indentLevel = indent;
                }
                EditorGUI.EndProperty();
            }
        }
#endif
    }
    [System.Serializable]
    public class Pitch {
        public float value = 0f;
        public float randomRange = 0f;

#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(Pitch))]
        public class PitchDrawer : PropertyDrawer {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
                // Using BeginProperty / EndProperty on the parent property means that
                // prefab override logic works on the entire property.
                EditorGUI.BeginProperty(position, label, property);
                {
                    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                    // Don't make child fields be indented
                    var indent = EditorGUI.indentLevel;
                    EditorGUI.indentLevel = 0;
                    // Calculate rects
                    var valRect = new Rect(position.x, position.y, position.width / 2f - 2.5f, position.height);
                    var rndRect = new Rect(valRect.x + 5f + valRect.width, position.y, valRect.width, position.height);

                    // Draw fields - passs GUIContent.none to each so they are drawn without labels
                    EditorGUI.PropertyField(valRect, property.FindPropertyRelative("value"), GUIContent.none);
                    EditorGUI.PropertyField(rndRect, property.FindPropertyRelative("randomRange"), GUIContent.none);
                    // Set indent back to what it was
                    EditorGUI.indentLevel = indent;
                }
                EditorGUI.EndProperty();
            }
        }
#endif
    }
}