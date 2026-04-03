# CsPublisherSubscriber

https://JimFawcett.github.io/CsPublisherSubscriber.html

Demonstrates the publish subscribe pattern using C#

## Structure

Single file `PublisherSubscriber/DemoPubSub.cs` with four types in the `PublisherSubscriber` namespace:

- **`Publisher`** — holds an `EventHandler` delegate (`eventDel`) and a nested `Eva : EventArgs` class that carries a `string msg` payload. Its `doEvents()` method fires the event 10 times with a 200 ms delay between each invocation.
- **`Subscriber`** — registers its `notified()` handler with the publisher's `eventDel` in its constructor. When notified, it casts the `EventArgs` to `Publisher.Eva` to extract and print the message.
- **`Utilities`** — static helper class providing `title` and `putLine` delegates for console formatting.
- **`Program`** — entry point; creates a `Publisher` and a `Subscriber`, then calls `pub.doEvents()`.

## Operation

1. `Publisher` is constructed with a base message string.
2. `Subscriber` is constructed with a reference to the publisher, wiring its handler via `+=`.
3. `pub.doEvents()` loops 10 times, building a numbered message (e.g. `"this is demo #3"`), sleeping 200 ms, then invoking all registered handlers via `eventDel.Invoke()`.
4. Each invocation calls `Subscriber.notified()`, which prints the message to the console.

## Build

```
dotnet build PublisherSubscriber/PublisherSubscriber.csproj
```

## Run

```
dotnet run --project PublisherSubscriber/PublisherSubscriber.csproj
```

## Target Framework

net10.0
