using System.Globalization;
using System.Text;

namespace ShopOnline.Helper
{
    public class Utilities
    {
        public static string GenerateAlias(string Name)
        {
            if (string.IsNullOrEmpty(Name))
                return string.Empty;
            // Bỏ dấu tiếng Việt
            string normalized = Name.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char c in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            string name = sb.ToString().Normalize(NormalizationForm.FormC);

            // Chuyển về chữ thường
            name = name.ToLowerInvariant();
            // Thay thế các ký tự đặc biệt bằng dấu gạch ngang
            name = System.Text.RegularExpressions.Regex.Replace(name, @"[^a-z0-9\s-]", "-");
            // Thay thế khoảng trắng và dấu gạch ngang liên tiếp thành một dấu gạch ngang duy nhất
            name = System.Text.RegularExpressions.Regex.Replace(name, @"[\s-]+", "-");
            // Cắt bỏ dấu gạch ngang ở đầu và cuối
            name = name.Trim('-');
            return name;
        }
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalized = text.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }

    }
}
