# Moroshka.DerivedTypes AI TRAINING DATA

## PACKAGE

com.moroshka.derived-types|Unity 6000.2|MIT|namespace:Moroshka.DerivedTypes(Runtime),Moroshka.DerivedTypes.Editor(Editor)

## PURPOSE

Serialize System.Type in Unity inspector. Field renders as PopupField dropdown. List=TypeCache.GetTypesDerivedFrom(BaseType). Excludes declaring type when declaringType.IsSubclassOf(baseType). Storage=AssemblyQualifiedName in _typeName.

## DerivedType STRUCT (Runtime)

[Serializable] struct DerivedType : ISerializationCallbackReceiver

- ctor(Type baseType): BaseType=baseType; _typeName=null; _type=null
- BaseType { get; } Type readonly set in ctor
- Type { get; }: if _type!=null return _type; if !string.IsNullOrWhiteSpace(_typeName) _type=Type.GetType(_typeName); return _type
- _typeName [SerializeField] string private
- _type Type private not serialized
- OnBeforeSerialize(): if _type!=null _typeName=_type.AssemblyQualifiedName
- OnAfterDeserialize(): _type=!string.IsNullOrEmpty(_typeName)?Type.GetType(_typeName):null

## USAGE

[SerializeField] DerivedType _field = new(typeof(BaseClass)); at runtime: _field.Type → System.Type or null if None

## DerivedTypeDrawer (Editor)

[CustomPropertyDrawer(typeof(DerivedType))] PropertyDrawer. CreatePropertyGUI→PopupField displayName=property.displayName options=["None"]+derivedTypeNames. GetSerializableType via fieldInfo.GetValue. FillNames: TypeCache.GetTypesDerivedFrom(baseType); if declaringType.IsSubclassOf(baseType) exclude declaringType; store AssemblyQualifiedName??FullName??Name. OnValueChanged→_typeNameProperty.stringValue=qualifiedNames[index]; ApplyModifiedProperties. Index lookup: qualifiedNames.IndexOf(value) else displayNames.IndexOf(value) for legacy FullName/Name.

## ASSEMBLIES

Moroshka.DerivedTypes: no deps, autoReferenced
Moroshka.DerivedTypes.Editor: ref Moroshka.DerivedTypes, includePlatforms Editor

## RULES

- Constructor requires baseType; BaseType immutable
- Type lazy-resolved via Type.GetType; cached after OnAfterDeserialize or first get
- None=empty string in _typeName; Type returns null
- Drawer excludes declaring type from dropdown when declaring type extends base type
- Do not set _type/_typeName directly; use Unity serialization flow
- For runtime instantiation: Activator.CreateInstance(derivedType.Type) when Type!=null

## EXAMPLE

public abstract class Strategy{}
public class StrategyA:Strategy{}
[SerializeField] DerivedType _strategyType=new(typeof(Strategy)); _strategyType.Type?.Name
