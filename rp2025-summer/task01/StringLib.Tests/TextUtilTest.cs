using StringLib;

namespace StringLib.Tests;

public class TextUtilTest
{
    [Theory]
    [MemberData(nameof(SplitIntoWordParams))]
    public void Can_split_into_words(string input, string[] expected)
    {
        List<string> result = TextUtil.SplitIntoWords(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(CountSentencesParams))]
        public void Can_count_sentences(string input, int expected)
    {
        int result = TextUtil.CountSentences(input);
        Assert.Equal(expected, result);
    }

    public static TheoryData<string, string[]> SplitIntoWordParams()
    {
        return new TheoryData<string, string[]>
        {
                      // Апостроф считается частью слова
            { "Can't do that", ["Can't", "do", "that"] },

            // Буква "Ё" считается частью слова
            { "Ёжик в тумане", ["Ёжик", "в", "тумане"] },
            { "Уж замуж невтерпёж", ["Уж", "замуж", "невтерпёж"] },

            // Дефис в середине считается частью слова
            { "Что-нибудь хорошее", ["Что-нибудь", "хорошее"] },
            { "mother-in-law's", ["mother-in-law's"] },
            { "up-to-date", ["up-to-date"] },
            { "Привет-пока", ["Привет-пока"] },

            // Слова из одной буквы допускаются
            { "Ну и о чём речь?", ["Ну", "и", "о", "чём", "речь"] },

            // Смена регистра не мешает разделению на слова
            { "HeLLo WoRLd", ["HeLLo", "WoRLd"] },
            { "UpperCamelCase or lowerCamelCase?", ["UpperCamelCase", "or", "lowerCamelCase"] },

            // Цифры не считаются частью слова
            { "word123", ["word"] },
            { "123word", ["word"] },
            { "word123abc", ["word", "abc"] },

            // Знаки препинания не считаются частью слова
            { "C# is awesome", ["C", "is", "awesome"] },
            { "Hello, мир!", ["Hello", "мир"] },
            { "Много   пробелов", ["Много", "пробелов"] },

            // Пустые строки, пробелы, знаки препинания
            { null!, [] },
            { "", [] },
            { "   \t\n", [] },
            { "!@#$%^&*() 12345", [] },
            { "\"", [] },

            // Пограничные случаи с апострофами и дефисами
            { "-привет", ["привет"] },
            { "привет-", ["привет"] },
            { "'hello", ["hello"] },
            { "hello'", ["hello"] },
            { "--привет--", ["привет"] },
            { "''hello''", ["hello"] },
            { "'a-b'", ["a-b"] },
            { "--", [] },
            { "'", [] },
        };
    }

    public static TheoryData<string, int> CountSentencesParams()
    {
        return new TheoryData<string, int>
        {
            // Простые случаи
            { "Hello! Are you OK?", 2 },
            { "Съешь же ещё этих мягких французских булок, да выпей чаю.", 1 },
            { "Смеркалось...", 1 }, // Многоточие считается как один конец предложения
            { "Статья 1.2.1 пункт 8.", 2 }, // Считает "1.2.1" и "8." как отдельные предложения, что соответствует условию

            // Случаи с разными знаками препинания
            { "Это первое предложение. Это второе! Это третье?", 3 },
            { "Много пробелов между   предложениями.", 1 },

            // Пустые строки и строки без слов
            { null!, 0 },
            { "", 0 },
            { "   \t\n", 0 },
            { "!@#$%^&*()", 0 }, // Без слов
            { "Просто текст", 0 },
            { "Hello world", 0 },
            { "This is a test... This is another test...", 2 },
            { "Wow!!!", 1 },  // Множественные восклицательные знаки считаются как один конец предложения
        
            // Смешанные языки
            { "Привет! Hello? こんにちは.", 3 },
            { "東京とМосква... Да!", 2 },
        
            // Аббревиатуры и сокращения
            { "Это тест. Т.е. проверка.", 2 },  // "Т.е." считается как два предложения

            // Комбинированные случаи
            { "Привет! Как дела? Всё хорошо.", 3 },
            { "Один. Два! Три? Четыре.", 4 },
            { "Что-то пошло не так...", 1 },
            { "Это первое. Это второе. Это третье. ", 3 }
        };
    }
}