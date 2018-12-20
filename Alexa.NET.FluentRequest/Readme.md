## Altering Values
```csharp
var fluent = new FluentSkillRequest();
fluent.Session.WithUserId("customUserValue");
```

## Creating a new LaunchRequest

```csharp
var fluent = new FluentSkillRequest().LaunchRequest();
var skillRequest = fluent.And.SkillRequest;
```

## Creating a new SessionEndedRequest

```csharp
var fluent = new FluentSkillRequest().SessionEndedRequest();
var skillRequest = fluent.And.SkillRequest;
```

## Creating a new IntentRequest

```csharp
var fluent = new FluentSkillRequest().IntentRequest(BuiltInIntent.Help)
var skillRequest = fluent.And.SkillRequest;
```

## Adding a slot to an intent

```csharp
var fluent = new FluentSkillRequest().IntentRequest("myintent")
fluent.AddSlot("key","value")
var skillRequest = fluent.And.SkillRequest;
```