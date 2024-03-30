using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor.UIElements;
using UnityEditor;
#endif 

namespace DefaultNamespace
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Good))]
    public class GoodDrawerUIE : PropertyDrawer
    {
        private SerializedProperty _link;
        private SerializedProperty _type;
        private SerializedProperty _amount;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that prefab override logic work on the entire property
            EditorGUI.BeginProperty(position, label, property);

            _type = property.FindPropertyRelative("_type");
            _amount = property.FindPropertyRelative("_quantity");
            _link = property.FindPropertyRelative("_link");

            Rect foldOutBox = new Rect(position.min.x, position.min.y,
                position.size.x, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded, label);

            if (property.isExpanded)
            {
                // Calculate rects
                var amountRect = new Rect(position.x, position.y + 15, position.width, 18);
                var unitRect = new Rect(position.x, position.y + 35, position.width, 18);
                var linkRect = new Rect(position.x, position.y + 55, position.width, 18);

                // Draw fields - pass GUIContent.none to each so they are drawn without labels
                EditorGUI.PropertyField(amountRect, _type, new GUIContent("Type"));
                EditorGUI.PropertyField(unitRect, _amount, new GUIContent("Quantity"));
                switch (_type.intValue)
                {
                    case (0):
                    {
                        _link.boxedValue = AssetManager.Instance.ClayToken;
                        break;
                    }
                    case (1):
                    {
                        _link.boxedValue = AssetManager.Instance.StoneToken;
                        break;
                    }
                    case 2:
                    {
                        _link.boxedValue = AssetManager.Instance.WoodToken;
                        break;
                    }
                    case 6:
                    {
                        _link.boxedValue = AssetManager.Instance.GlassToken;
                        break;
                    }
                    case 7:
                    {
                        _link.boxedValue = AssetManager.Instance.PapyrusToken;
                        break;
                    }
                    case 5:
                    {
                        _link.boxedValue = AssetManager.Instance.ConflictToken;
                        break;
                    }
                    case 8:
                    {
                        _link.boxedValue = AssetManager.Instance.VPToken;
                        break;
                    }
                    case 3:
                    {
                        _link.boxedValue = AssetManager.Instance.GoldToken1;
                        break;
                    }
                    case 4:
                    {
                        _amount.intValue = 1;
                        break;
                    }
                }
                EditorGUI.PropertyField(linkRect, property.FindPropertyRelative("_link"), new GUIContent("Link"));
                
            }

            EditorGUI.EndProperty();
        }

        /// <summary>
        /// request more height for properties and return it
        /// </summary>
        /// <param name="property">Reference to </param>
        /// <param name="label"></param>
        /// <returns></returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int totalLine = 1;
            if (property.isExpanded)
            {
                totalLine += 3;
            }
            
            return (EditorGUIUtility.singleLineHeight * totalLine + 5);
        }
    }
#endif
}