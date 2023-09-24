using System;
namespace EnigmaShared.Models
{
	public class SwaggerConfig
	{
		public string MyProperty { get; set; } = "";

        public string Name { get; set; } = "MyAPI v1";
        public string Version { get; set; } = "v1";
        public string Title { get; set; } = "Api";
        public string JSONPath { get; set; } = "";
    }
}

