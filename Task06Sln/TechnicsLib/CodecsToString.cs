using System.Collections.Generic;
using System.Text;

namespace TechnicsLib
{
    public class CodecsToString
    {
        public string Process(ICollection<VideoPlayer.VideoCodecs> codecses)
        {
            var builder = new StringBuilder();
            foreach (var videoCodec in codecses) builder.Append($"{videoCodec},");

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        public string Process(ICollection<VideoPlayer.AudioCodecs> codecses)
        {
            var builder = new StringBuilder();
            foreach (var videoCodec in codecses) builder.Append($"{videoCodec},");

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
    }
}