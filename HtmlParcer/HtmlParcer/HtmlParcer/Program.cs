using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Xml.Serialization;

namespace HtmlParcer
{
    class Program
    {
        static void Main(string[] args)
        {
            var parcer = new WebParcer();

            // Колбасы, сардельки, сосиски
            //parcer.ParcePage("Колбасы вареные");
            //parcer.ParcePage("Колбасы сырокопченые, копченые");
            //parcer.ParcePage("Колбасы из мясопродуктов, паштеты");
            //parcer.ParcePage("Сардельки");
            //parcer.ParcePage("Сосиски");

            // Говядина
            //parcer.ParcePage("Копчености из говядины");

            // Птица
            //parcer.ParcePage("Копчености из мяса птицы");

            //Свинина
            //parcer.ParcePage("Копчености из свинины");
            parcer.ParcePage("Сыровяленые, соленые изделия из свинины");

        }
    }
}
