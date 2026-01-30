# Инструкции по использованию

`DerivedType` позволяет сериализовать System.Type для Unity инспектора. В инспекторе Unity отображается как выпадающий список. Список содержит перечисление типов, унаследованных от заданного базового типа; из списка исключается тип текущего объекта.

``` csharp
class MyComponent : MonoBehaviour
{
  [SerializeField] DerivedType _type = new(typeof(MonoBehaviour));
}
```
