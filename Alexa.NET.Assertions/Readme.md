# Alexa.NET.Assertions

Utility library to make tests easier to write for your Alexa skills
` using Alexa.NET.Assertions `

## Making sure you're asking, not telling

```csharp
        [Fact]
        public async Task LaunchAlwaysAsksIfTheyreReady()
        {
            SkillResponse response = ...
            response.Asks();
        }
```

## Checking the exact output type
```csharp
        [Fact]
        public async Task LaunchReturnsSsml()
        {
            SkillResponse response = ...
            response.Tells<SsmlOutputSpeech>();
        }
```

## Checking the speech is correct
```csharp
        [Fact]
        public async Task LaunchReturnsSsml()
        {
            SkillResponse response = ...
            response.Tells<PlainTextOutputSpeech>(output => output.Text.Contains("hi there"));
        }
```

## Checking you're sending a standard card with the right text

```csharp
        [Fact]
        public async Task IntentReturnsCard()
        {
            SkillResponse response = ...
            response.HasCard<StandardCard>(card => card.Title == "card title");
        }
```

## Response has a reprompt

```csharp
        [Fact]
        public async Task IntentWithReprompt()
        {
            SkillResponse response = ...
            response.HasReprompt<PlainTextOutputSpeech>(pt => pt.Text.Contains("hello?"));
        }
```

## Response has a directive

```csharp
        [Fact]
        public async Task IntentWithDirectives()
        {
            SkillResponse response = ...
            response.HasDirective<HintDirective>();
        }
```

## Response has a specific directive

```csharp
[Fact]
        public async Task IntentWithDirectives()
        {
            SkillResponse response = ...
            var directive = response.HasDirective<HintDirective>(h => h.Hint?.Text == "test");
        }
```