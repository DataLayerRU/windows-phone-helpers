using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace WPDevelopment.classes
{
    public class OFileWork
    {
        public static bool CreateFile(string _file_path)
        {
            bool result = false;

            var file = IsolatedStorageFile.GetUserStoreForApplication();
            if (!file.FileExists(_file_path))
            {
                file.CreateFile(_file_path);
            }

            return result;
        }

        public static void RemoveFile(string _file_name)
        {
            var file = IsolatedStorageFile.GetUserStoreForApplication();
            file.DeleteFile(_file_name);
        }

        public static bool WriteToFile(string _file_path, string _data)
        {
            bool result = false;

            var file = IsolatedStorageFile.GetUserStoreForApplication();

            using (var stream = new IsolatedStorageFileStream(_file_path, System.IO.FileMode.OpenOrCreate, file))
            {
                var fileWriter = new StreamWriter(stream);
                fileWriter.Write(_data);
                fileWriter.Close();

                result = true;
            }

            return result;
        }

        public static string ReadFile(string _file_path)
        {
            string result = "";

            var file = IsolatedStorageFile.GetUserStoreForApplication();
            if (file.FileExists(_file_path))
            {
                using (var stream = new IsolatedStorageFileStream(_file_path, System.IO.FileMode.Open, file))
                {
                    var fileReader = new StreamReader(stream);
                    result = fileReader.ReadToEnd();
                }
            }

            return result;
        }

        public static bool IsFileExist(string _file_name)
        {
            bool result = false;

            var file = IsolatedStorageFile.GetUserStoreForApplication();
            result = file.FileExists(_file_name);

            return result;
        }

        public static String GetXmlFieldFromString(String _xml, String _field)
        {


            XDocument doc = XDocument.Parse(_xml);
            //doc.XPathSelectElements();
            //IEnumerable<XElement> Elements = doc.Root.Descendants("AddressLine");

            //foreach (XElement el in Elements)
            //{
            //    Console.WriteLine(el.Name.ToString());
            //}

            String result = GetXmlField(doc.Root, _field);
            //using (XmlReader reader = XmlReader.Create(new StringReader(_xml)))
            //{
            //    reader.MoveToContent();
            //    while (reader.Read())
            //    {
            //        if (reader.NodeType == XmlNodeType.Element)
            //        {
            //            if (reader.Name == _field)
            //            {
            //                XElement el = XNode.ReadFrom(reader) as XElement;
            //                if (el != null)
            //                {
            //                    result = el.Value;
            //                }
            //            }
            //        }
            //    }
            //}
            return result;
        }

        private static String GetXmlField(XElement _el, String _field)
        {
            IEnumerable<XElement> elements = _el.Elements();

            foreach (XElement el in elements)
            {
                if (el.Name.LocalName == _field)
                {
                    return el.Value;
                }
                else
                {
                    string result = GetXmlField(el, _field);
                    if (result != "")
                    {
                        return result;
                    }
                }
            }

            return "";
        }
    }
}
