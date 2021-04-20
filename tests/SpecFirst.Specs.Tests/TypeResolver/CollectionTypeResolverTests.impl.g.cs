
namespace SpecFirst.Specs.Tests
{
    using System;
    using SpecFirst.Core.TypeResolver;

    public partial class infer_type_from_collection_text
    {
        private partial (string, string) infer_type_from_collection_text_implementation(string collection)
        {
            var type = CollectionTypeResolver.Resolve(collection, out var parsedValue);
            return (TypeHelper.GetTypeString(type), TypeHelper.Serialize(parsedValue, type));
        }
    }

}