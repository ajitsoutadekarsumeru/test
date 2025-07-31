# ENCollect.ApiManagement.RateLimiter

## Overview

This library provides a **simple fixed-window rate-limiting** mechanism, allowing developers to **enforce per-API, per-user request limits**. The rate limiter checks whether an incoming request has exceeded a configured limit within a specified time window, and **throws** `RateLimitExceededException` if so. This exception can then be translated into an **HTTP 429 (Too Many Requests)** status code at a higher layer.

## Key Features

- **Per-API, Per-User Limits**: Enforce different limits on different APIs, each scoped to the user making the request.
- **Fixed-Window Logic**: A straightforward implementation for counting requests within a time-based window.
- **Configuration-Driven**: Typically reads from `appsettings.json`, minimizing code changes when adjusting rate limits.
- **Extendable**: If additional checks or logic are needed (e.g., concurrency limits, advanced policies), the library can be adapted.

## Unit Tests

A **full suite of unit tests** has been created to verify the library’s core functionality:

- Ensuring **no limit** is enforced if an API is missing from config.
- Validating correct behavior when **within** the limit vs. **exceeding** the limit.
- Confirming that the usage count **resets** once the time window expires.
- Handling **concurrency** scenarios and **null/empty** inputs.

All tests are **successfully passing**. See the `RateLimiterTests` (xUnit) for further details.

## Usage in Controllers or FlexBridge

We have provided **examples** illustrating how an **ASP.NET Core Controller** (or a custom `FlexBridge`) might integrate with this rate limiter by calling:

1. A **separate method** (e.g., `RateLimitAndEnrich(...)`) that enforces the rate limit **before**
2. Calling the existing `RunService(...)` logic.

**Important**: While sample code is available, there could still be **edge cases** or **errors** in how these components integrate. We recommend **careful review** and testing in your environment to ensure everything works as expected.

## Single Responsibility Principle

To avoid cluttering the existing code, the **rate-limit check** is kept **separate** from `RunService(...)`. This aligns with the **Single Responsibility Principle (SRP)**, ensuring that:

- **`RunService(...)`** remains focused on executing the main command or service logic.
- **Rate-limiting** is handled beforehand in a distinct method, typically just before `RunService(...)` is invoked.

## Returning HTTP 429

When a request **exceeds** the configured limit, the library **throws** a `RateLimitExceededException`. According to **REST API best practices**, the recommended status code for rate-limit violations is **429 Too Many Requests**. You can configure your **ASP.NET Core middleware** or **global exception handler** to catch `RateLimitExceededException` and return a **429** with an appropriate response body.