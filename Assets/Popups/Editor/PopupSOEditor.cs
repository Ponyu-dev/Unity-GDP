using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Popups.Editor
{
    [CustomEditor(typeof(PopupSO))]
    public class PopupSOEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var popupSo = (PopupSO)target;
            
            EditorGUILayout.LabelField("Select Presenter Type", EditorStyles.boldLabel);
            
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(PopupPresenter)) && !type.IsAbstract)
                .ToArray();

            var typeNames = types.Select(t => t.FullName).ToArray();
            var selectedIndex = Mathf.Max(0, Array.IndexOf(typeNames, popupSo.presenterType.Type?.FullName));
            
            selectedIndex = EditorGUILayout.Popup("Presenter Type", selectedIndex, typeNames);

            if (selectedIndex >= 0 && selectedIndex < types.Length)
            {
                popupSo.presenterType.Type = types[selectedIndex];
            }
            
            if (GUI.changed)
            {
                EditorUtility.SetDirty(popupSo);
            }
        }
    }
}