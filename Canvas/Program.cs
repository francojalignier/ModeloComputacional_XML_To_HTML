using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Canvas
{
    class Program
    {
        static string xmlFullPath = "";
        static string xmlName = "canvas_original";

        static void Main(string[] args)
        {

            //styles
            Style style_face = new Style() { FillColor = "#ffdab7", StrokeColor = "#000000", StrokeWidth = 2 };
            Style style_glasses = new Style() { StrokeColor = "#000000", StrokeWidth = 10 , FillOpacity = 1f };
            Style style_mouth = new Style() { FillColor = "#fca95a", StrokeColor = "#fc6c59", StrokeWidth = 6, StrokeOpacity = 0.5f };
            Style style_teeth = new Style() { FillColor = "white", StrokeColor = "black", StrokeWidth = 2 };

            //Init canvas

            Canvas canvas = new Canvas();
            //canvas.Add(new Circle(new Point(450, 450), 400, style_face));

            //canvas.Add(new Circle(new Point(300, 300), 120, style_glasses));
            //canvas.Add(new Circle(new Point(600, 300), 120, style_glasses));
            //canvas.Add(new Line(103, 250, 200, 300, style_glasses));
            //canvas.Add(new Line(797, 250, 700, 300, style_glasses));
            //canvas.Add(new Line(400, 300, 500, 300, style_glasses));

            //canvas.Add(new Rectangle(100, 50, 700, 70, style_glasses));
            //canvas.Add(new Rectangle(200, 10, 500, 70, style_glasses));

            //canvas.Add(new Rectangle(250, 500, 400, 100, style_mouth));
            //canvas.Add(new Rectangle(260, 504, 40, 30, style_teeth));
            //canvas.Add(new Rectangle(450, 504, 40, 30, style_teeth));
            //canvas.Add(new Rectangle(550, 504, 40, 30, style_teeth));

            //canvas.Add(new Rectangle(400, 568, 40, 30, style_teeth));
            //canvas.Add(new Point(300, 300, style_glasses));

            
            canvas.Add(new Point(500, 500));
            canvas.Add(new Point(400, 400));
            canvas.Add(new Point(300, 300));
            canvas.Add(new Line(new Point(1, 1), new Point(150, 120)));
            canvas.Add(new Line(400, 700, 800, 35));
            canvas.Add(new Rectangle(200, 90, 400, 20));
            canvas.Add(new Circle(new Point(50, 50), 30));
            canvas.Add(new Circle(new Point(200, 180), 70));

            //Estrella
            canvas.Add(new Line(750, 700, 721, 790));
            canvas.Add(new Line(721, 790, 798, 735));
            canvas.Add(new Line(798, 735, 702, 735));
            canvas.Add(new Line(702, 735, 779, 799));
            canvas.Add(new Line(779, 799, 750, 700));

            canvas.Draw();

            //Serialize canvas
            CanvasSerializer serializer = new CanvasSerializer();

            var serialization = serializer.Serialize(canvas, xmlName);

            Console.WriteLine("\nSerializando documento...");

            Console.ReadKey();

            if (serialization.hasError)
                Console.WriteLine("-Error al serializar el documento :: " + serialization.error);
            else
            {
                Console.WriteLine("-Documento serializado correctamente en: " + serialization.path);
                xmlFullPath = serialization.path;
            }

            

            //Deserialize canvas
            var deserialization = serializer.Deserialize(xmlName);
            Console.WriteLine("\n\nDeserializando documento...");

            if (deserialization.hasError)
                Console.WriteLine("-Error al deserializar el documento :: " + deserialization.error);
            else
                Console.WriteLine("-Documento deserializador correctamente");

            deserialization.canvas.Draw();

            Console.ReadKey();
            //Convert to HTML

            ConvertXmlToHtml();
        }


        static void ConvertXmlToHtml()
        {
            try
            {
                string xslt_path = "../../canvas_template.xslt";
                string html_path = Path.Combine(CanvasSerializer.GetRootDirectory(), string.Format("{0}.html", xmlName));

                XPathDocument x_path_doc = new XPathDocument(xmlFullPath);
                XslCompiledTransform xsl_compiled_transform = new XslCompiledTransform();

                xsl_compiled_transform.Load(xslt_path);

                using (XmlTextWriter xml_writer = new XmlTextWriter(html_path, null))
                {
                    xsl_compiled_transform.Transform(x_path_doc, null, xml_writer);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public class Actions
        {




            /// Muestra en pantalla la descripción de los objetos
            /// </summary>
            public void DrawPresentation()
            {
                try
                {
                    Random r = new Random();
                    Console.ForegroundColor = (ConsoleColor)r.Next(6, 16);

                    string line;
                    StreamReader file = new StreamReader(Path.Combine(@"../../description.txt"));
                    while ((line = file.ReadLine()) != null)
                    {
                        System.Console.WriteLine(line);
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            /// <summary>
            /// Transforma es archivo XML -> HTML mediante XSLT
            /// </summary>

        }
    }
}


