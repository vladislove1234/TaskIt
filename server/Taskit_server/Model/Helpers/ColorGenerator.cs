using System;
namespace Taskit_server.Model.Helpers
{
    public static class ColorGenerator
    {
        public static string GenerateColor()
        {
            var random = new Random();
            return String.Format("#{0:X6}", random.Next(0x1000000));
        }
    }
}
