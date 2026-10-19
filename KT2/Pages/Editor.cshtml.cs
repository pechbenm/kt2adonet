using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace KT2.Pages
{
    public class EditorModel : PageModel
    {
        private readonly string _filePath;

        public EditorModel(IWebHostEnvironment env)
        {
            var dir = Path.Combine(env.ContentRootPath, "App_Data");
            Directory.CreateDirectory(dir);
            _filePath = Path.Combine(dir, "text.txt");
        }

        [BindProperty]
        public string? Content { get; set; }

        public string PreviewHtml
        {
            get
            {
                // Сначала экранируем всё, затем возвращаем только <b>, <i>, <u>
                var html = WebUtility.HtmlEncode(Content ?? "");
                foreach (var tag in new[] { "b", "i", "u" })
                {
                    html = html.Replace($"&lt;{tag}&gt;", $"<{tag}>")
                               .Replace($"&lt;/{tag}&gt;", $"</{tag}>");
                }
                return html;
            }
        }

        public void OnGet()
        {
            if (System.IO.File.Exists(_filePath))
            {
                Content = System.IO.File.ReadAllText(_filePath);
            }
        }

        public IActionResult OnPost()
        {
            System.IO.File.WriteAllText(_filePath, Content ?? "");
            TempData["Status"] = "Текст сохранён";
            return RedirectToPage();
        }
    }
}