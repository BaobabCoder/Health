﻿// <copyright file="NoOpHealthCheckRegistry.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Metrics.Health.Internal.NoOp
{
    public sealed class NoOpHealthCheckRegistry : IHealthCheckRegistry
    {
        /// <inheritdoc />
        public IReadOnlyDictionary<string, HealthCheck> Checks { get; } = new Dictionary<string, HealthCheck>();

        /// <inheritdoc />
        public void AddChecks(IEnumerable<HealthCheck> checks) { }

        /// <inheritdoc />
        public void AddCheck(string name, Func<ValueTask<HealthCheckResult>> check) { }

        /// <inheritdoc />
        public void AddCheck(string name, Func<CancellationToken, ValueTask<HealthCheckResult>> check) { }
    }
}
