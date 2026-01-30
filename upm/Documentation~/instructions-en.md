# Usage Instructions

`DerivedType` allows serialization of System.Type for the Unity inspector. It appears as a dropdown list in the Unity inspector. The list contains types derived from the specified base type; the current object's type is excluded from the list.

``` csharp
class MyComponent : MonoBehaviour
{
  [SerializeField] DerivedType _type = new(typeof(MonoBehaviour));
}
```
