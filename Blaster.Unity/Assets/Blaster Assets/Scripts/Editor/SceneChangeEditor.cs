//using BlueOrb.Scripts.AI.PlayMaker.Scene;
//using BlueOrb.Scripts.Editor.Extensions;
//using HutongGames.PlayMakerEditor;
//using BlueOrb.Controller.Scene;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEditor;
//using UnityEngine;

//namespace BlueOrb.Scripts.Editor
//{
//    [CustomActionEditor(typeof(SceneChange))]
//    public class SceneChangeEditor : CustomActionEditor
//    {
//        public override void OnEnable()
//        {
//            // Do any expensive initialization stuff here.
//            // This is called when the editor is created.
//        }

//        public override bool OnGUI()
//        {
//            // If you need to reference the action directly:
//            var action = target as SceneChange;

//            // You can draw the full default inspector.

//            GUILayout.Label("Default Inspector:", EditorStyles.boldLabel);

//            var isDirty = DrawDefaultInspector();

//            var SpawnPoints = new List<KeyValuePair<string, string>>();
//            SpawnPoints.Add(new KeyValuePair<string, string>("-1", "Select Spawn Point..."));

//            var agentSceneConfig = action.SceneConfig.Value as SceneConfig;

//            if (agentSceneConfig != null)
//            {
//                // Get list of spawn points
//                foreach (var spawnPoint in agentSceneConfig.SpawnPoints)
//                {
//                    SpawnPoints.Add(new KeyValuePair<string, string>(spawnPoint.UniqueId, spawnPoint.Name));
//                }
//            }

//            var oldSpawnpointId = action.SpawnPointUniqueId.Value;
//            action.SpawnPointUniqueId.Value = ControlExtensions.Popup("Spawnpoint", oldSpawnpointId,
//                SpawnPoints);

//            isDirty = isDirty || oldSpawnpointId != action.SpawnPointUniqueId.Value;

//            // Or draw individual controls

//            //GUILayout.Label("Field Controls:", EditorStyles.boldLabel);

//            //EditField("logLevel");
//            //EditField("floatVariable");

//            //// Or add your own controls using any GUILayout method

//            //GUILayout.Label("Your Controls:", EditorStyles.boldLabel);

//            //if (GUILayout.Button("Press Me"))
//            //{
//            //    EditorUtility.DisplayDialog("My Dialog", "Hello", "OK");
//            //    isDirty = true; // e.g., if you change action data
//            //}

//            // OnGUI should return true if you change action data!

//            return isDirty || GUI.changed;
//        }
//    }
//}
