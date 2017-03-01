﻿// Copyright (c) Allan Hardy. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using App.Metrics.Extensions.Middleware.Integration.Facts.Startup;
using App.Metrics.Extensions.Middleware.Internal;
using App.Metrics.Timer;
using FluentAssertions;
using Xunit;

namespace App.Metrics.Extensions.Middleware.Integration.Facts.Middleware.OAuth2
{
    public class PerRequestTimerMiddlewareCountsTests : IClassFixture<MetricsHostTestFixture<DefaultTestStartup>>
    {
        public PerRequestTimerMiddlewareCountsTests(MetricsHostTestFixture<DefaultTestStartup> fixture)
        {
            Client = fixture.Client;
            Context = fixture.Context;
        }

        public HttpClient Client { get; }

        public IMetrics Context { get; }

        [Fact]
        public async Task can_count_requests_per_endpoint_and_also_get_a_total_count()
        {
            await Client.GetAsync("/api/test");
            await Client.GetAsync("/api/test/error");

            Func<string, TimerValue> getTimerValue = metricName => Context.Snapshot.GetTimerValue(
                HttpRequestMetricsRegistry.ContextName,
                metricName);

            getTimerValue("Http Request Transactions|route:GET api/test").Histogram.Count.Should().Be(1);
            getTimerValue("Http Request Transactions|route:GET api/test/error").Histogram.Count.Should().Be(1);
            getTimerValue("Overall Http Request Transactions").Histogram.Count.Should().Be(2);
        }
    }
}