using System.Collections;
using System.Transactions;

namespace Calculator {
    class Start {
        // Beware, do a \* when writing *, bash does not like it otherwise
        static void Main(string[] args) {
            List<List<string>> parsed = ParseArguments(args);
            
        }

        // these can be used in expessions, maybe I'll add variables later
        // static char[] tokenList = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '*', '*', '^', '%', '(', ')', '[', ']', '=', ','];

        static List<List<string>> ParseArguments(string[] args) {
            List<List<string>> parsed = [];

            // first split all the args into lists of tokens
            foreach (string current in args) {
                List<string> split = SplitArgument(current);
                if (split.Count > 0) {
                    if (parsed.Count == 0) {
                        parsed.Add(new List<string>());
                    }
                    foreach (string str in split) {
                        if (str == ",") {
                            parsed.Add(new List<string>());
                        }
                        else {
                            parsed.Last().Add(str);
                        }
                    } 
                }
            }

            if (parsed.Count > 0 && parsed.Last().Count == 0) {
                parsed.RemoveAt(parsed.Count - 1);
            }


            return parsed;
        }

        static List<string> SplitArgument(string arg) {
            List<string> result = [];

            for (int n = 0; n < arg.Length - 1; n++) {
                if ((arg[n] >= '0' && arg[0] <= '9') || arg[n] == '.') {
                    // current is a number
                    if ((arg[n + 1] < '0' || arg[n + 1] > '9') && arg[n + 1] != '.') {
                        // next is not a number, needs a space before
                        arg = arg.Insert(n + 1, " ");
                        n++;
                    }
                }
                else {
                    // it's a token then, needs a space after
                    arg = arg.Insert(n + 1, " ");
                    n++;
                }
            }

            int start = 0;
            for (int n = 0; n < arg.Length; n++) {
                if (arg[n] == ' ') {
                    if (start != n) {
                        // there's something to put into the list since start isn't equal to current
                        result.Add(arg.Substring(start, n - start));
                    }
                    start = n + 1;
                }
            }

            if (start != arg.Length) {
                result.Add(arg.Substring(start, arg.Length - start));
            }

            return result;
        }
    }
}