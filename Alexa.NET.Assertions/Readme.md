# Alexa.NET.Assertions

Utility library to make tests easier to write for your Alexa skills
These examples also use [Alexa.Net.FluentRequest](https://nuget.org/packages/Alexa.NET.FluentRequest)

## Making sure you're asking, not telling

```csharp
        [Fact]
        public async Task LaunchAlwaysAsksIfTheyreReady()
        {
            var fluent = new Alexa.NET.FluentSkillRequest().LaunchRequest().And;
            var handler = new Launch();
            var response = await handler.Handle(new AlexaRequestInformation(fluent.SkillRequest, null));
            AlexaAssertions.Ask(response);
        }
```

## Checking the exact output
```csharp
        [Fact]
        public async Task LaunchReturnsSsml()
        {
            var fluent = new Alexa.NET.FluentSkillRequest().LaunchRequest().And;
            var handler = new Launch();
            var response = await handler.Handle(new AlexaRequestInformation(fluent.SkillRequest, null));
            AlexaAssertions.AskPlainText(response,"are you ready for this?");
        }
```

## Checking you're sending a standard card

```csharp
        [Fact]
        public async Task IntentReturnsCard()
        {
            var fluent = new Alexa.NET.FluentSkillRequest().IntentRequest("SendCard").And;
            var handler = new CardIntent();
            var response = await handler.Handle(new AlexaRequestInformation(fluent.SkillRequest, null));
            var card = AlexaAssertions.StandardCard()
            Assert.Equal("card title", card.Title);
        }
```