using System.Text.RegularExpressions;

namespace Models
{
    public class Topic
    {
        public string Name { get; }
        public Topic(string name)
        {
            ValidateName(name);
            Name = name;
        }

        private static void ValidateName(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name), "Topic name is not set.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The topic name cannot be empty or consist of whitespaces.", nameof(name));
            }

            if (name.Any(character => char.IsWhiteSpace(character)))
            {
                throw new ArgumentException("The topic name cannot contain whitespaces.", nameof(name));
            }

            if (name.Length > MAX_TOPIC_NAME_LENGTH)
            {
                throw new ArgumentException("The name of a topic is too long.", nameof(name));
            }

            if (!_topicNameCharacters.IsMatch(name))
            {
                throw new ArgumentException("Incorrect topic name. The topic name may consist of characters 'a' to 'z', 'A' to 'Z', digits, and minus signs.", nameof(name));
            }
        }

        private static readonly Regex _topicNameCharacters = new(
            "^[a-zA-Z0-9\\-]*$",
            RegexOptions.Compiled);

        private const int MAX_TOPIC_NAME_LENGTH = 249;
    }
}
