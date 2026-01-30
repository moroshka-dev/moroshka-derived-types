using System;
using UnityEngine;

namespace Moroshka.DerivedTypes
{
	/// <summary>
	/// A structure for type serialization in Unity.
	/// Allows saving and loading type information through Unity serialization.
	/// </summary>
	[Serializable]
	public struct DerivedType : ISerializationCallbackReceiver
	{
		[SerializeField] private string _typeName;
		private Type _type;

		/// <summary>
		/// Creates a new instance of DerivedType.
		/// </summary>
		/// <param name="baseType">The base type to serialize.</param>
		public DerivedType(Type baseType)
		{
			BaseType = baseType;
			_typeName = null;
			_type = null;
		}

		/// <summary>
		/// Gets the base type.
		/// </summary>
		public Type BaseType { get; }

		/// <summary>
		/// Gets the current type. If the type is not loaded yet, attempts to load it from the saved type name.
		/// </summary>
		public Type Type
		{
			get
			{
				if (_type != null) return _type;
				if (!string.IsNullOrWhiteSpace(_typeName)) _type = Type.GetType(_typeName);
				return _type;
			}
		}

		/// <summary>
		/// Called before serialization. Saves the full type name.
		/// </summary>
		public void OnBeforeSerialize()
		{
			if (_type != null) _typeName = _type.AssemblyQualifiedName;
		}

		/// <summary>
		/// Called after deserialization. Restores the type from the saved name.
		/// </summary>
		public void OnAfterDeserialize()
		{
			_type = !string.IsNullOrEmpty(_typeName)
				? Type.GetType(_typeName)
				: null;
		}
	}
}