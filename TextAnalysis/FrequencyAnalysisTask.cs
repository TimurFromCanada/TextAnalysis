using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, Dictionary<string, int>> GetBigramms(List<List<string>> list, Dictionary<string, Dictionary<string, int>> unSort)
        {
            foreach (var e in list)
                for (var i = 0; i < e.Count; i++)
                {
                    if (i == e.Count - 1)
                        continue;
                    else
                    {
                        if (unSort.ContainsKey(e[i]))
                            if (unSort[e[i]].ContainsKey(e[i + 1]))
                                unSort[e[i]][e[i + 1]]++;
                            else
                                unSort[e[i]][e[i + 1]] = 1;
                        else
                            unSort[e[i]] = new Dictionary<string, int> { { e[i + 1], 1 } };
                    }
                    if (e.Count >= 3)
                    {
                        if (i == e.Count - 2)
                            continue;
                        else
                        {
                            if (unSort.ContainsKey(e[i] + " " + e[i + 1]))
                                if (unSort[e[i] + " " + e[i + 1]].ContainsKey(e[i + 2]))
                                    unSort[e[i] + " " + e[i + 1]][e[i + 2]]++;
                                else
                                    unSort[e[i] + " " + e[i + 1]][e[i + 2]] = 1;
                            else
                                unSort[e[i] + " " + e[i + 1]] = new Dictionary<string, int> { { e[i + 2], 1 } };
                        }
                    }
                }
            return unSort;
        }

        public static Dictionary<string, string> GetSortDictionaryMain(Dictionary<string, Dictionary<string, int>> dictionary, Dictionary<string, string> result)
        {
            foreach (var e in dictionary.Keys)
            {
                if (dictionary[e].Count == 1)
                {
                    foreach (var r in dictionary[e].Keys)
                    {
                        result.Add(e, r);
                        break;
                    }
                }
                else
                    result.Add(e, GetSortDictionary(dictionary[e]));
            }
            return result;
        }

        public static string GetSortDictionary(Dictionary<string, int> keyValues)
        {
            var maxValue = 0;
            var endWord = "";
            foreach (var pair in keyValues)
            {
                if (pair.Value > maxValue)
                {
                    maxValue = pair.Value;
                    endWord = pair.Key;
                }
                else
                {
                    if (pair.Value == maxValue)
                        if (string.CompareOrdinal(pair.Key, endWord) < 0)
                            endWord = pair.Key;
                }
            }
            return endWord;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var dictionaryNotSort = new Dictionary<string, Dictionary<string, int>>();
            var result = new Dictionary<string, string>();
            return GetSortDictionaryMain(GetBigramms(text, dictionaryNotSort), result);
        }
    }
}