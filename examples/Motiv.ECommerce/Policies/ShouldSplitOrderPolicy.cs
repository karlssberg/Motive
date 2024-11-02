﻿using Motiv.ECommerce.Behaviors;
using Motiv.ECommerce.Models;

namespace Motiv.ECommerce.Policies;

public class ShouldSplitOrderPolicy() : Policy<FulfillmentContext, IBehavior>(
    Spec.Build(IsExpensive)
        .WhenTrue<IBehavior>(new SplitOrderBehavior())
        .WhenFalse(new DefaultBehavior())
        .Create("should split order"))
{
    private static SpecBase<FulfillmentContext, string> IsExpensive { get; } =
        Spec.From((FulfillmentContext context) => context.Order.Products.Sum(product => product.Price) > 1000)
            .Create("is expensive");
}
