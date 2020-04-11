using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(MapController))]
public class MapEditor : Editor
{
    [SerializeField] Color debugColor = Color.red;
    MapController mapClass;
    public override void OnInspectorGUI()
    {
        mapClass = target as MapController;
        mapClass.mapData = (Map)EditorGUILayout.ObjectField("Mapdata", mapClass.mapData, typeof(Map), false);
       
        mapClass.EnemyDataObject = (Enemies)EditorGUILayout.ObjectField("Enemy data", mapClass.EnemyDataObject, typeof(Enemies), false);
        mapClass.TimeBetweenEnemies = EditorGUILayout.FloatField(mapClass.TimeBetweenEnemies);
        if (GUILayout.Button("Build Map"))
        {

            if (mapClass == null) return;
            mapClass.CreateMap();
        }
        if (GUILayout.Button("Reset Map"))
        {

            if (mapClass == null) return;
            mapClass.ResetMap();
        }
        if (mapClass.Path != null)
        {
            GUILayout.Label("Path Color", EditorStyles.boldLabel);
            debugColor = EditorGUILayout.ColorField(debugColor);
            for (int i = 0; i < mapClass.Path.Count; i++)
            {
                if (i != mapClass.Path.Count - 1)
                {
                    Vector3 startPoint = new Vector3(mapClass.Path[i].x * 2, 1, mapClass.Path[i].y * 2);
                    Vector3 endPoint = new Vector3(mapClass.Path[i + 1].x * 2, 1, mapClass.Path[i + 1].y * 2);
                    Debug.DrawLine(mapClass.transform.TransformPoint(startPoint), mapClass.transform.TransformPoint(endPoint), debugColor);
                }
            }
        }
    }

}
#endif