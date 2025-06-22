namespace ShopOnline.Helper
{
    public class Utilities
    {
        public static string GenerateAlias(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
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
    }
}
