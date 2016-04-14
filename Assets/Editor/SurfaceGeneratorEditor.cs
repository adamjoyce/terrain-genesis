using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SurfaceGenerator))]
public class SurfaceGeneratorEditor : Editor {

    private SurfaceGenerator generator;

    // Updates changes made in the inspector immediately.
    public override void OnInspectorGUI() {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck())
            RefreshGenerator();
    }

    // Refreshes the surface generator.
    private void RefreshGenerator() {
        if (Application.isPlaying)
            generator.Refresh();
    }

    // Ensure the target being inspeccted is the generator and allow for undo and redo refreshes.
    private void OnEnable() {
        generator = target as SurfaceGenerator;
        Undo.undoRedoPerformed += RefreshGenerator;
    }

    // Allow for undo and redo refreshes.
    private void OnDisable() {
        Undo.undoRedoPerformed -= RefreshGenerator;
    }
}
