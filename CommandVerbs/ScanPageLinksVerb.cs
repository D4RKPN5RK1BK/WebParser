using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandVerbs
{
    [Verb("scanlinks", HelpText = "Сканирует страницы на сайте и заносит их в базу данных")]
    internal class ScanPageLinksVerb
    {
        [Option('l', "link", Required = false, Default = null, HelpText = "Ссылка на страницу в которой будет произведен поиск ссылок на вложенные страницы")]
        public string Link { get; set; }

        [Option('a', "advanced", Default = false, HelpText = "Продвинутое сканирование (ищет ссылки в контенте страницы)")]
        public bool Advanced { get; set; }

        [Option('s', "save", Default = false, HelpText = "Сохраняет уже полученную информацию с базе данных.")]
        public bool Save { get; set; }
      
    }
}
