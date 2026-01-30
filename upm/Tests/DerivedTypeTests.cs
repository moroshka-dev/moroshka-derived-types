using System;
using System.Reflection;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Moroshka.DerivedTypes.Test
{
	internal sealed class DerivedTypeTests
	{
		[Test]
		public void Constructor_WithBaseType_BaseTypeIsSet()
		{
			var baseType = typeof(string);
			var derivedType = new DerivedType(baseType);

			Assert.That(derivedType.BaseType, Is.EqualTo(baseType));
		}

		[Test]
		public void Constructor_Type_IsNull()
		{
			var derivedType = new DerivedType(typeof(object));

			Assert.That(derivedType.Type, Is.Null);
		}

		[Test]
		public void OnAfterDeserialize_WithValidTypeName_TypeReturnsResolvedType()
		{
			var derivedType = new DerivedType(typeof(object));
			SetTypeName(ref derivedType, typeof(string).AssemblyQualifiedName);
			derivedType.OnAfterDeserialize();

			Assert.That(derivedType.Type, Is.EqualTo(typeof(string)));
		}

		[Test]
		public void OnAfterDeserialize_WithEmptyTypeName_TypeIsNull()
		{
			var derivedType = new DerivedType(typeof(object));
			SetTypeName(ref derivedType, string.Empty);
			derivedType.OnAfterDeserialize();

			Assert.That(derivedType.Type, Is.Null);
		}

		[Test]
		public void OnAfterDeserialize_WithNullTypeName_TypeIsNull()
		{
			var derivedType = new DerivedType(typeof(object));
			SetTypeName(ref derivedType, null);
			derivedType.OnAfterDeserialize();

			Assert.That(derivedType.Type, Is.Null);
		}

		[Test]
		public void OnBeforeSerialize_WithTypeSet_SetsTypeName()
		{
			var derivedType = new DerivedType(typeof(object));
			SetType(ref derivedType, typeof(int));
			derivedType.OnBeforeSerialize();

			Assert.That(GetTypeName(derivedType), Is.EqualTo(typeof(int).AssemblyQualifiedName));
		}

		[Test]
		public void OnBeforeSerialize_WithNullType_DoesNotOverwriteTypeName()
		{
			var derivedType = new DerivedType(typeof(object));
			var expectedName = typeof(string).AssemblyQualifiedName;
			SetTypeName(ref derivedType, expectedName);
			SetType(ref derivedType, null);
			derivedType.OnBeforeSerialize();

			Assert.That(GetTypeName(derivedType), Is.EqualTo(expectedName));
		}

		[Test]
		public void RoundTrip_SerializeThenDeserialize_PreservesType()
		{
			var derivedType = new DerivedType(typeof(object));
			SetType(ref derivedType, typeof(ArgumentException));
			derivedType.OnBeforeSerialize();
			SetType(ref derivedType, null);
			derivedType.OnAfterDeserialize();

			Assert.That(derivedType.Type, Is.EqualTo(typeof(ArgumentException)));
		}

		[Test]
		public void Type_AfterOnAfterDeserialize_ReturnsCachedTypeOnSubsequentCalls()
		{
			var derivedType = new DerivedType(typeof(object));
			SetTypeName(ref derivedType, typeof(int).AssemblyQualifiedName);
			derivedType.OnAfterDeserialize();

			Assert.That(derivedType.Type, Is.EqualTo(typeof(int)));
			SetTypeName(ref derivedType, typeof(string).AssemblyQualifiedName);
			Assert.That(derivedType.Type, Is.EqualTo(typeof(int)), "Type should remain cached from first resolution.");
		}

		private static void SetTypeName(ref DerivedType derivedType, string value)
		{
			object box = derivedType;
			var field = typeof(DerivedType).GetField("_typeName", BindingFlags.NonPublic | BindingFlags.Instance);
			field?.SetValue(box, value);
			derivedType = (DerivedType)box;
		}

		private static string GetTypeName(DerivedType derivedType)
		{
			var field = typeof(DerivedType).GetField("_typeName", BindingFlags.NonPublic | BindingFlags.Instance);
			return (string)field?.GetValue(derivedType);
		}

		private static void SetType(ref DerivedType derivedType, Type value)
		{
			object box = derivedType;
			var field = typeof(DerivedType).GetField("_type", BindingFlags.NonPublic | BindingFlags.Instance);
			field?.SetValue(box, value);
			derivedType = (DerivedType)box;
		}
	}
}
