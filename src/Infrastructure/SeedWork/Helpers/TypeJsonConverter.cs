// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;


namespace CocktailsApp.Infrastructure.SeedWork
{
    internal sealed class TypeJsonConverter : JsonConverter<Type>
    {
        public override Type Read(ref Utf8JsonReader r, Type _, JsonSerializerOptions __) =>
            Type.GetType(r.GetString()!, throwOnError: true)!;

        public override void Write(Utf8JsonWriter w, Type value, JsonSerializerOptions _) =>
            w.WriteStringValue($"{value.FullName}, {value.Assembly.GetName().Name}");
    }
}
