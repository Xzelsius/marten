﻿using System;
using System.Collections.Generic;
using System.Linq;
using Baseline;
using Marten.Events.Aggregation;

namespace Marten.Events.Projections
{
    /// <summary>
    /// This type of grouper potentially sorts one event into multiple aggregates
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TEvent"></typeparam>
    internal class MultiStreamGrouper<TId, TEvent>: IGrouper<TId>
    {
        private readonly Func<TEvent, IReadOnlyList<TId>> _func;

        public MultiStreamGrouper(Func<TEvent, IReadOnlyList<TId>> expression)
        {
            // TODO -- it's possible we'll use the expression later to write metadata into the events table
            // to support the async daemon, but I'm doing it the easy way for now
            _func = expression;
        }

        public void Apply(IEnumerable<IEvent> events, ITenantSliceGroup<TId> grouping)
        {
            grouping.AddEvents(_func, events);
        }
    }
}
