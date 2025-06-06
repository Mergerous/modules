using System;
using System.Text.RegularExpressions;

namespace Modules.Common.Extensions
{
    public enum CaseType
    {
        None = 0,
        Camel = 1,
        Snake = 2,
        Kebab = 3,
        Pascal = 4,
    }
    
    public static class TextExtensions
    {
        public static string ToCase(this string source, CaseType type) => type switch 
        {
            CaseType.Camel => source.ToCamelCase(),
            CaseType.Kebab => source.ToKebabCase(),
            CaseType.Pascal => source.ToPascalCase(),
            CaseType.Snake => source.ToSnakeCase(),
            CaseType.None => source
        };
        
        private static string ToCamelCase(this string source)
        {
            string camelCase = Regex.Replace(source, @"(?:^|[_\s])(\w)", match => match.Groups[1].Value.ToUpper());
            camelCase = char.ToLower(camelCase[0]) + camelCase.Substring(1);
            return camelCase;
        }
        
        private static string ToKebabCase(this string source) => Regex.Replace(source, @"([a-z])([A-Z])", "$1-$2").ToLower();

        private static string ToPascalCase(this string source) => Regex.Replace(source, @"(?:^|[_\s])(\w)", match => match.Groups[1].Value.ToUpper());

        private static string ToSnakeCase(this string source) => Regex.Replace(source, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", "_$1").ToLower();
        
        public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);
    }
}
