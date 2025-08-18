using System.Text.RegularExpressions;

namespace StringLib;

public static class TextUtil
{
    public static List<string> SplitIntoWords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return [];
        }

        // Регулярное выражение для поиска слов:
        // - Слово начинается и заканчивается на букву.
        // - Может содержать апострофы и дефисы внутри.
        // - Не содержит чисел или знаков препинания.
        const string pattern = @"\p{L}+(?:[\-\']\p{L}+)*";
        Regex regex = new(pattern, RegexOptions.Compiled);

        return regex.Matches(text)
            .Select(match => match.Value)
            .ToList();
    }

    // Подсчитывает количество предложений в тексте.
    // Предложением считается фрагмент текста, содержащий минимум одно слово и заканчивающийся одним из следующих символов: ., !, ?.
    // Поддерживаются английский и русский языки.

    public static int CountSentences(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }

        // Регулярное выражение для поиска предложений:
        // \p{L} - любая буква (независимо от языка)
        // \s* - ноль или более пробельных символов
        // (?:[^.!?]+) - группа, которая не захватывается, содержащая один или более символов, отличных от ., !, ?
        // [\.!?]+ - один или более символов '.', '!', '?'
        // В целом, ищем последовательности, которые содержат буквы и заканчиваются на один или несколько знаков конца предложения.
        // Добавляем проверку на наличие слов внутри предложения, чтобы исключить случаи с только знаками препинания.
        const string sentencePattern = @"(\p{L}\s*)+[^.!?\s]+[\.!?]+";
        Regex regex = new(sentencePattern, RegexOptions.Compiled | RegexOptions.Multiline);

        MatchCollection matches = regex.Matches(text);

        return matches.Count;
    }
}