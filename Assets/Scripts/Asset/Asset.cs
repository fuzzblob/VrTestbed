namespace Moona.Assets {
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    public enum PlayMode {
        OneShot = 0,
        Looping = 1,
        Retrigger = 2,
        //Scatter = 
    }

    [CreateAssetMenu()]
    public class Asset : ScriptableObject {
        public PlayMode Mode = PlayMode.OneShot;
        public Volume volume = null;
        public Pitch pitch = null;
        
        // MixerAssignment
        // spatial
        // rtpc
        // advanced
        // music
        // items
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Asset))]
    [CanEditMultipleObjects]
    public class AssetInspector : Editor {
        private Asset asset;

        public override void OnInspectorGUI() {
            if (targets.Length > 1) {
                EditorGUILayout.HelpBox("MultiObject editing not implemented.", MessageType.Error);
            }
            asset = target as Asset;

            foreach (var o in targets) {

            }

            base.OnInspectorGUI();
        }
        private void OnSceneGUI() {
            // TODO: render attenuation spheres of in-scene asset references
        }
    }
#endif
}