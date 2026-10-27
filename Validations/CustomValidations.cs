using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace para.Validations
{
    // 1. Проверка на то, что текст начинается с заглавной буквы
    public class StartsWithCapitalLetterAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string str && !string.IsNullOrWhiteSpace(str))
            {
                return char.IsUpper(str[0]);
            }
            return true; 
        }
    }

    // 2. Проверка цены: цена должна быть кратна определенному числу (например, 10 или 50 руб)
    public class PriceMultipleOfAttribute : ValidationAttribute
    {
        private readonly int _divisor;
        public PriceMultipleOfAttribute(int divisor)
        {
            _divisor = divisor;
        }

        public override bool IsValid(object? value)
        {
            if (value is decimal price)
            {
                return price % _divisor == 0;
            }
            return true;
        }
    }

    // 3. Проверка на максимальное количество СЛОВ (а не символов)
    public class MaxWordCountAttribute : ValidationAttribute
    {
        private readonly int _maxWords;
        public MaxWordCountAttribute(int maxWords)
        {
            _maxWords = maxWords;
        }

        public override bool IsValid(object? value)
        {
            if (value is string str && !string.IsNullOrWhiteSpace(str))
            {
                var words = str.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                return words.Length <= _maxWords;
            }
            return true;
        }
    }

    // 4. Проверка на наличие запрещенных слов (спам-фильтр)
    public class NoForbiddenWordsAttribute : ValidationAttribute
    {
        private readonly string[] _words;
        public NoForbiddenWordsAttribute(params string[] words)
        {
            _words = words;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string str)
            {
                foreach (var word in _words)
                {
                    if (str.Contains(word, StringComparison.OrdinalIgnoreCase))
                    {
                        return new ValidationResult(ErrorMessage ?? $"Слово '{word}' запрещено к использованию.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }

    // 5. Кросс-валидация: поле не должно полностью совпадать с другим полем (например, Описание не равно Названию)
    public class NotSameAsPropertyAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;
        public NotSameAsPropertyAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_otherProperty);
            if (property != null)
            {
                var otherValue = property.GetValue(validationContext.ObjectInstance);
                if (value != null && value.Equals(otherValue))
                {
                    return new ValidationResult(ErrorMessage ?? $"Значение не может быть точной копией поля {_otherProperty}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}