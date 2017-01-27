namespace Acme.Widgets
    type Lever = PlasticLever | WoodenLever

namespace Acme.Suppliers
    type LeverSupplier = {name : string; leverKind : Acme.Widgets.Lever}