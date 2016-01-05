using System.Linq;
using System.Text.RegularExpressions;
using Odin.Parsing;

namespace Odin.Configuration
{
    /// <summary>
    /// Parser implementation for arguments of the form /name:value
    /// </summary>
    public class SlashColonParser : IParser
    {
        private readonly ParameterValue _parameter;

        public SlashColonParser(ParameterValue parameter)
        {
            _parameter = parameter;
        }

        private static bool IsArgumentName(string token)
        {
            return Regex.IsMatch(token, @"/\w+");
        }

        private static bool IsNameValuePair(string token)
        {
            return Regex.IsMatch(token, @"/\w+:\w+");
        }

        /// <summary>
        /// Returns a <see cref="ParseResult"/> given a position in a list of tokens.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public ParseResult Parse(string[] tokens, int i)
        {
            var token = tokens[i];
            if (IsNameValuePair(token))
            {
                var value = token.Split(':').Skip(1).First();
                return new ParseResult()
                {
                    Value = _parameter.Coerce(value),
                    TokensProcessed = 1,
                };
            }

            if (!IsArgumentName(token))
                return new ParseResult()
                {
                    Value = _parameter.Coerce(token),
                    TokensProcessed = 1,
                };

            if (_parameter.IsBoolean())
            {
                var value = !_parameter.Conventions.IsNegatedLongOptionName(_parameter.Name, token);
                return new ParseResult()
                {
                    Value = value,
                    TokensProcessed = 1,
                };
            }

            return new ParseResult()
            {
                Value = _parameter.Coerce(token),
                TokensProcessed = 1,
            };
        }
    }
}