using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

public class Numbers {
    public class NormalisedAttribute : PropertyAttribute { }
    public class DecibelAttribute : PropertyAttribute { }

#if UNITY_EDITOR
    // Tell the MyRangeDrawer that it is a drawer for properties with the MyRangeAttribute.
    [CustomPropertyDrawer(typeof(Numbers.DecibelAttribute))]
    public class DecibelAttributeDrawer : PropertyDrawer {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // First get the attribute since it contains the range for the slider
            var db = (Numbers.DecibelAttribute)attribute;

            // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
            if (property.propertyType == SerializedPropertyType.Float) {
                property.floatValue = Mathf.Min(0f, Mathf.Max(-80f, property.floatValue));
            }
            else if (property.propertyType == SerializedPropertyType.Integer) {
                property.intValue = Mathf.Min(0, Mathf.Max(-80, property.intValue));
            }
            else {
                EditorGUI.HelpBox(position, "Use DecibelProperty with float or int.", MessageType.Warning);
            }
        }
    }
    // Tell the MyRangeDrawer that it is a drawer for properties with the MyRangeAttribute.
    [CustomPropertyDrawer(typeof(Numbers.NormalisedAttribute))]
    public class NormalisedAttributeDrawer : PropertyDrawer {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
            if (property.propertyType == SerializedPropertyType.Float) {
                property.floatValue = Mathf.Max(0f, Mathf.Min(1f, property.floatValue));
            }
            else if (property.propertyType == SerializedPropertyType.Integer) {
                property.intValue = Mathf.Max(0, Mathf.Min(1, property.intValue));
            }
            else {
                EditorGUI.HelpBox(position, "Use DecibelProperty with float or int.", MessageType.Warning);
            }
        }
    }
#endif
}
