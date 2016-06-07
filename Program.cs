using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Sample_XmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            // This method returns an XElement, send it to the console to display it
            var sampleOutput = XmlWithXElements();
            Console.WriteLine(sampleOutput);

            // This method returns a string of Xml, send it to the console to display it
            var documentOutput = XmlWithXmlDocument();
            Console.WriteLine(documentOutput);

            // This method returns an Xml Writer object, send it to the console to display it
            var writerOutput = XmlWithDOM();
            Console.WriteLine(writerOutput);

            // Serializer has an output to the console, just call it
            XmlWithSerializer();

            //Console.WriteLine();

            while (Console.Read() != 'q');
        }

        // Use for very small bit of data needing to be formatted to Xml
        public static XElement XmlWithXElements()
        {
            var testOuput = new XElement("Foo",
                new XAttribute("Bar", "test"),
                new XElement("Nested", "testData"));
            return testOuput;
        }

        // use with larger data chunks where the elements are known ahead of time
        public static string XmlWithXmlDocument()
        {
            var doc = new XmlDocument();
            var ele = (XmlElement)doc.AppendChild(doc.CreateElement("Foo"));
            ele.SetAttribute("Bar", "test");
            ele.AppendChild(doc.CreateElement("Nested")).InnerText = "testData";

            return doc.OuterXml;
        }

        // Use for larger data where memory space is important to preserve
        public static XmlWriter XmlWithDOM()
        {
            XmlWriter writer = XmlWriter.Create(Console.Out);
            writer.WriteStartElement("Foo");
            writer.WriteAttributeString("Bar", "test");
            writer.WriteElementString("Nested", "testData");
            writer.WriteEndElement();
            return writer;
        }

        // Serialize the data with the following if you have an object that should be mirrored
        public static void XmlWithSerializer()
        {
            var foo = new Foo
            {
                Bar = "test",
                Nested = "testData"
            };
            new XmlSerializer(typeof(Foo)).Serialize(Console.Out, foo);
        }
    }

    [Serializable]
    public class Foo
    {
        [XmlAttribute]
        public string Bar { get; set; }
        [XmlElement]
        public string Nested { get; set; }
    }
}
