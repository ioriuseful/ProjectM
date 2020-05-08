using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spritecustom : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 position = new Vector2(0, 0);
    public Vector2 size = new Vector2(1, 1);
    [CustomEditor(typeof(Spritecustom))]
    public class TextureTrimEditor : Editor
    {

        SerializedProperty textureProp;
        SerializedProperty positionProp;
        SerializedProperty sizeProp;

        
        private void OnEnable()
        {
            textureProp = serializedObject.FindProperty("texture");
            positionProp = serializedObject.FindProperty("position");
            sizeProp = serializedObject.FindProperty("size");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // 50x50 の四角形を描画
            GUILayout.Box(GUIContent.none, GUILayout.Width(50), GUILayout.Height(50));

            // テクスチャを取得
            Texture2D tex = textureProp.objectReferenceValue as Texture2D;

            if (tex != null)
            {
                // テクスチャのどの範囲を切り取るかをRectで指定
                Rect rect = new Rect(positionProp.vector2Value, sizeProp.vector2Value);

                // さっきの四角形の位置に切り取ったテクスチャを表示
                GUI.DrawTextureWithTexCoords(GUILayoutUtility.GetLastRect(), tex, rect);
            }

            // フィールドの入力欄を表示
            EditorGUILayout.PropertyField(textureProp);
            EditorGUILayout.PropertyField(positionProp);
            EditorGUILayout.PropertyField(sizeProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
