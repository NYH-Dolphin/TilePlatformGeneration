using Editor.TileSystem;
using UnityEditor;
using UnityEngine;

namespace Editor.TilemapEditor
{
    [CustomEditor(typeof(TilemapGenerator))]
    public class TilemapGeneratorEditor : UnityEditor.Editor
    {
        private TilemapGenerator _generator;

        private void OnEnable()
        {
            _generator = (TilemapGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            using (new EditorGUILayout.HorizontalScope())
            {
                
                if (GUILayout.Button("Update Tileset"))
                {
                    _generator.OnUpdateTileset();
                }

                if (GUILayout.Button("Update Tilemap Array"))
                {
                    _generator.OnUpdateTilemap();
                }
            }
            
            using (new EditorGUILayout.HorizontalScope())
            {
                
                if (GUILayout.Button("Clean Tilemap"))
                {
                    _generator.OnCleanTilemap();
                }
                
                if (GUILayout.Button("Random 5X5 Tilemap"))
                {
                    _generator.OnRandomTilemap5x5();
                }
            }
            
            
            
            
        }
    }
}