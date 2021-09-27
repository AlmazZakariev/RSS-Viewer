using System;
using System.Collections;
using System.Xml;

namespace RSS_Viewer
{
    [Serializable]
    class Item
    {
        public string title { get; } // заголовок записи
        public string pubDate { get; }
        public string description { get; } // описание записи
        public string link { get; }
        public Item(XmlNode Item)
        {
            foreach (XmlNode tag in Item.ChildNodes)
            {
                switch (tag.Name)
                {
                    case "title":
                        {
                            title = tag.InnerText;
                            break;
                        }

                    case "description":
                        {
                            description = tag.InnerText;
                            break;
                        }
                    case "link":
                        {
                            link = tag.InnerText;
                            break;
                        }
                    case "pubDate":
                        {
                            pubDate = tag.InnerText;
                            break;
                        }
                }
            }
        }

       
    }
}
