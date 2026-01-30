// =============================================================================
// Sample: DerivedType
// =============================================================================
// In the inspector the field is shown as a dropdown with types derived from
// the specified base type. At runtime use .Type to get the selected System.Type.
// =============================================================================

using Moroshka.DerivedTypes;
using UnityEngine;

public sealed class DerivedTypeSample : MonoBehaviour
{
	[SerializeField] DerivedType _strategyType = new(typeof(Strategy));

	private void Start()
	{
		if (_strategyType.Type != null) Debug.Log($"Selected type: {_strategyType.Type.Name}", this);
		else Debug.Log("No type selected (None).", this);
	}
}

// Custom base and derived types — in the inspector only these derived types
// appear in the dropdown (base type = Strategy).

public abstract class Strategy { }

public sealed class StrategyA : Strategy { }

public sealed class StrategyB : Strategy { }

public sealed class StrategyC : Strategy { }