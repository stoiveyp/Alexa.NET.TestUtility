﻿using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.FluentRequests
{
    public class FluentContext
    {
        private FluentRequest Request { get; }
        internal Context Context { get; }

        internal FluentContext(FluentRequest request)
        {
            Request = request;
            Context = new Context
            {
                AudioPlayer = null,
                System = new AlexaSystem
                {
                    User = request.Session.Session.User,
                    ApiEndpoint = "https://api.amazonalexa.com",
                    Application = request.Session.Session.Application,
                    ApiAccessToken = "apiAccess" + request.NextRandom(),
                    Device = new Device
                    {
                        DeviceID = "deviceId" + request.NextRandom(),
                        SupportedInterfaces = new Dictionary<string, object>()
                    }
                }
            };
        }

        public FluentRequest And => Request;
        public FluentContext WithPlaybackState(PlaybackState state)
        {
            Context.AudioPlayer = state;
            return this;
        }

        public FluentContext WithUserId(string userId)
        {
            Context.System.User.UserId = userId;
            return this;
        }

        public FluentContext WithApiAccessToken(string apiAccessToken)
        {
            Context.System.ApiAccessToken = apiAccessToken;
            return this;
        }

        public FluentContext WithAccessToken(string accessToken)
        {
            Context.System.User.AccessToken = accessToken;
            return this;
        }

        public FluentContext WithApplicationId(string applicationId)
        {
            Context.System.Application.ApplicationId = applicationId;
            return this;
        }

        public FluentContext WithDeviceId(string deviceId)
        {
            Context.System.Device.DeviceID = deviceId;
            return this;
        }

        public FluentContext AddInterface(string supportedInterface, object value = null)
        {
            if (Context.System.Device.IsInterfaceSupported(supportedInterface))
            {
                Context.System.Device.SupportedInterfaces.Remove(supportedInterface);
            }

            Context.System.Device.SupportedInterfaces.Add(supportedInterface,value ?? new JObject());
            return this;
        }
    }
}
