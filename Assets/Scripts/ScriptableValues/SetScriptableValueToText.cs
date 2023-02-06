#pragma warning disable 0649
using NSScriptableValues;
using UnityEngine;
using UnityEngine.UI;

namespace NSScriptableValues
{
    public class SetScriptableValueToText : MonoBehaviour
    {
        [SerializeField] private ScriptableValueInt quantityScriptableValue;

        [SerializeField] private Text textQuantity;

        [Header("Complementary text")] [SerializeField]
        private bool setTextBeforeQuantity;

        [SerializeField] private string textComplementary;
        
        private void OnEnable()
        {
            quantityScriptableValue.OnValueChanged.AddListener(SetValueToText);
            quantityScriptableValue.Value = 1000;
        }

        private void OnDisable()
        {
            quantityScriptableValue.OnValueChanged.RemoveListener(SetValueToText);
        }

        private void SetValueToText()
        {
            if (setTextBeforeQuantity)
                textQuantity.text = quantityScriptableValue + textComplementary;
            else
                textQuantity.text = textComplementary + quantityScriptableValue;
        }
    }
}