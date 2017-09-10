using System.Collections.Generic;

namespace Utils.CollectionUtils {
    public static class CollectionUtils {
        public static string Combine(IEnumerable<string> iEnumerable, string split) {
            string result = "";
            foreach (var a in iEnumerable) {
                result += a;
            }
            return result;
        }
    }
}
