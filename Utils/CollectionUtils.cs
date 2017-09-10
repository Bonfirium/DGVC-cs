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

        public static bool ContainsSubstring(IEnumerable<string> iEnumerable, string str) {
            foreach (var a in iEnumerable) {
                if (a.Contains(str)) {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsSuperstring(IEnumerable<string> iEnumerable, string str) {
            foreach (var a in iEnumerable) {
                if (str.Contains(a)) {
                    return true;
                }
            }
            return false;
        }
    }
}
