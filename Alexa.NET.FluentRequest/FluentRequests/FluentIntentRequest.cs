using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;

namespace Alexa.NET.FluentRequests
{
    public class FluentIntentRequest:NET.FluentRequests.FluentRequest
    {
        public FluentIntentRequest(FluentSkillRequest skillRequest, string name) : base(skillRequest)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override Request.Type.Request Request => new IntentRequest
        {
            DialogState = DialogState,
            RequestId = GetRequestId(),
            Locale = GetLocale(),
            Timestamp = GetTimestamp(),
            Intent = new Intent
            {
                Name = Name,
                ConfirmationStatus = ConfirmationStatus,
                Slots = Slots
            }
        };

        public FluentIntentRequest WithConfirmationStatus(string confirmationStatus)
        {
            ConfirmationStatus = confirmationStatus;
            return this;
        }

        public FluentIntentRequest WithDialogState(string dialogState)
        {
            DialogState = dialogState;
            return this;
        }

        internal string ConfirmationStatus { get; set; }
        internal string DialogState { get; set; }

        internal Dictionary<string,Slot> Slots { get; set; } = new Dictionary<string, Slot>();

        public FluentIntentRequest AddSlot(string key, string value)
        {
            Slots.Add(key,new Slot{Name = key,Value=value});
            return this;
        }

        public FluentIntentRequest AddSlot(Slot slot)
        {
            Slots.Add(slot.Name,slot);
            return this;
        }
    }
}
