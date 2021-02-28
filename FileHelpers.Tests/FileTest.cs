using System.Dynamic;
using System.IO;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    public static class FileTest
    {
        public static dynamic Good { get; } = new ResourceObject(nameof(Good));
        
        public static dynamic Bad { get; } = new ResourceObject(nameof(Bad));

        public static dynamic Classes { get; } = new ResourceObject(nameof(Classes));
        
        public static dynamic Detection { get; } = new ResourceObject(nameof(Detection));

        private class ResourceObject : DynamicObject
        {
            private readonly string category;

            public ResourceObject(string category)
            {
                this.category = category;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", category);
                var files = Directory.GetFiles(path, binder.Name + ".*");

                if (files.Length != 1)
                {
                    throw new FileNotFoundException($"Cannot find file: {category}/{binder.Name}");
                }

                result = new FileTestBase(Path.Combine(category, binder.Name + Path.GetExtension(files[0])));

                return true;
            }
        }
    }
}