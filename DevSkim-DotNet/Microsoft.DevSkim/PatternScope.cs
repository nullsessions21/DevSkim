﻿// Copyright (C) Microsoft. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System;

namespace Microsoft.DevSkim
{
    [JsonConverter(typeof(PatternScopeConverter))]
    public enum PatternScope
    {
        All,
        Code,
        Comment,
        Html
    }

    /// <summary>
    /// Json converter for Pattern Type
    /// </summary>
    class PatternScopeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is PatternScope svr)
            {
                string svrstr = svr.ToString().ToLower();

                writer.WriteValue(svrstr);
                writer.WriteValue(svr.ToString().ToLower());
            }
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.Value is string enumString)
            {
                enumString = enumString.Replace("-", "");
                return Enum.Parse(typeof(PatternScope), enumString, true);
            }
            // TODO: Should there be a separate enum value for finding a null here?
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
