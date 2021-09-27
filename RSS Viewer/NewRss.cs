using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace RSS_Viewer
{
    class NewRss : SerializableDataSaver
    {
        
        public List<Item> Items { get; }
        public NewRss(string URL)
        {
            Items = new List<Item>();
           
            try
            {
                XmlTextReader read = new XmlTextReader(URL);
                XmlDocument doc = new XmlDocument();
                doc.Load(read);
                read.Close();
                XmlNode channelXmlNode = doc.GetElementsByTagName("channel")[0];
                if (channelXmlNode != null)
                {
                    foreach (XmlNode channelNode in channelXmlNode.ChildNodes)
                    {
                        switch (channelNode.Name)
                        {
                            case "item":
                                {
                                    Item channelItem = new Item(channelNode);
                                    Items.Add(channelItem);
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка в XML. Описание канала не найдено!",
                            "Ошибка!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                }
                Save();
            }
            catch (SystemException)
            {
                MessageBox.Show("Неверно введена ссылка, попробуйте снова",
                            "Ошибка!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                return;
            }
        }
        public NewRss(bool IsOld)
        {
            if (IsOld)
            {
                Items = GetItemsData();
            }
        }
        private void Save()
        {
            Save(Items);
        }
        private List<Item> GetItemsData()
        {
            return Load<Item>()?? new List<Item>();

        }
    }
}
