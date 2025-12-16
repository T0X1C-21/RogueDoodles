#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveSpawner))]
public class WaveSpawnerEditor : Editor {

    EnemyType enemyType;

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        WaveSpawner waveSpawner = (WaveSpawner) target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("----- EDITOR TOOLS -----", EditorStyles.largeLabel);

        enemyType = (EnemyType) EditorGUILayout.EnumPopup("Enemy Type", enemyType);

        if(GUILayout.Button("Spawn Enemy")) {
            waveSpawner.Editor_SpawnEnemy(enemyType);
        }
    }

}

#endif