using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace Moroshka.DerivedTypes.Editor
{
	/// <summary>
	/// Property drawer for <see cref="DerivedType"/> in the Unity inspector.
	/// Renders a dropdown with types derived from the field's base type.
	/// </summary>
	[CustomPropertyDrawer(typeof(DerivedType))]
	public sealed class DerivedTypeDrawer : PropertyDrawer
	{
		private List<string> _displayNames;
		private List<string> _qualifiedNames;
		private SerializedProperty _typeNameProperty;

		/// <inheritdoc />
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var container = new VisualElement();
			if (_displayNames == null) FillNames(property);
			_typeNameProperty = property.FindPropertyRelative("_typeName");
			var index = GetIndex(_typeNameProperty.stringValue);
			var popup = new PopupField<string>(property.displayName, _displayNames, index);
			popup.RegisterValueChangedCallback(OnValueChanged);
			container.Add(popup);
			return container;
		}

		/// <summary>
		/// Fills the list of display names and assembly-qualified names for the dropdown.
		/// </summary>
		private void FillNames(SerializedProperty property)
		{
			var serializableType = GetSerializableType(property);
			var baseType = serializableType.BaseType;
			var derivedTypes = TypeCache.GetTypesDerivedFrom(baseType).ToList();
			var declaringType = property.serializedObject.targetObject.GetType();
			if (declaringType.IsSubclassOf(baseType))
				derivedTypes = derivedTypes.Where(t => t != declaringType).ToList();

			_displayNames = new List<string> { "None" };
			_qualifiedNames = new List<string> { string.Empty };
			foreach (var type in derivedTypes)
			{
				_displayNames.Add(type.Name);
				_qualifiedNames.Add(type.AssemblyQualifiedName ?? type.FullName ?? type.Name);
			}
		}

		/// <summary>
		/// Gets the <see cref="DerivedType"/> value from the serialized property's target object.
		/// </summary>
		private DerivedType GetSerializableType(SerializedProperty property)
		{
			object targetObject = property.serializedObject.targetObject;
			return (DerivedType)fieldInfo.GetValue(targetObject);
		}

		/// <summary>
		/// Gets the index for the serialized type name (AssemblyQualifiedName or legacy FullName).
		/// </summary>
		private int GetIndex(string value)
		{
			if (string.IsNullOrEmpty(value)) return 0;
			var index = _qualifiedNames?.IndexOf(value) ?? -1;
			if (index >= 0) return index;
			// Legacy: value might be FullName or type name only
			index = _displayNames?.IndexOf(value) ?? -1;
			if (index >= 0) return index;
			return 0;
		}

		/// <summary>
		/// Handles popup value change: writes AssemblyQualifiedName to the serialized property.
		/// </summary>
		private void OnValueChanged(ChangeEvent<string> evt)
		{
			if (_typeNameProperty == null) return;
			var index = _displayNames?.IndexOf(evt.newValue) ?? 0;
			if (index < 0) index = 0;
			_typeNameProperty.stringValue = index > 0 && index < _qualifiedNames.Count
				? _qualifiedNames[index]
				: string.Empty;
			_typeNameProperty.serializedObject.ApplyModifiedProperties();
		}
	}
}