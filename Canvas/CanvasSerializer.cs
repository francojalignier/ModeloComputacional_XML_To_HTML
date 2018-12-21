using System;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Canvas
{
    public class CanvasSerializer
    {

        public class SerializationResult
        {
            public string error;
            public string path;
            public bool hasError { get { return !String.IsNullOrEmpty(error); } }
        }

        public class DeserializationResult : SerializationResult
        {

            public Canvas canvas;
        }

        //Clase dummy que permite etiquerar al canvas como un array y guardar metadata para la serializacion y deserializacion
        //para entender de donde salen estos atributos : https://docs.microsoft.com/en-us/dotnet/standard/serialization/controlling-xml-serialization-using-attributes
        [XmlRoot("CanvasContainer")]
        public class CanvasContainer
        {
            [XmlArray]
            public Canvas canvas;

            [XmlElement("Metadata")]
            public CanvasMetadata meta;

        }

        //clase que contiene metadata para serializar y deserializar el canvas, por ahora solo tiene los tipos de figuras que contiene
        //con esos tipos serializados podemos deserializar sin andar hardcodeando los tipos de figuras.
        [XmlType("CanvasMetadata")]
        public class CanvasMetadata
        {
            [XmlArray("types")]
            [XmlArrayItem("type")]
            public String[] types;
        }

        //Folder donde se van a guardar todos los xmls por defecto (Se puede cambiar dinamicamente llamando a SetRootDirectory(path))
        static string RootDirectory = Path.Combine(
            System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "CanvasDocuments"
        );


        public CanvasSerializer()
        {
            CreateDirectoryIfNotExist(RootDirectory);
        }


        //CORE// //SERIALIZADOR//
        //serializa un objeto de tipo canvas y lo escribe en disco//
        //el archivo final generado se va a encontrar en la ruta {RootDirectory}//
        //metodo de escritura sincrona.
        public SerializationResult Serialize(Canvas canvas, string name)
        {

            var types = GetTypesFromCanvas(canvas);

            CanvasContainer container = new CanvasContainer()
            {
                canvas = canvas,
                meta = GenerateCanvasMetadata(types)
            };

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanvasContainer), types);

            string path = Path.Combine(RootDirectory, NormalizedName(name));

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    xmlSerializer.Serialize(writer, container);
                    return new SerializationResult() { path = path };
                }
            }
            catch (Exception e)
            {
                return new SerializationResult() { error = e.ToString() };
            }
        }

        //CORE// //DESERIALIZADOR//
        //deserializa un xml y trata de generar un canvas//
        //si se pasa como parametro el nombre del file, va a intentar buscarlo dentro de RootDirectory//
        //si se pasa como parametro el path completo, intenta deserializar desde ahi
        public DeserializationResult Deserialize(string pathOrName)
        {
            pathOrName = NormalizedName(pathOrName);

            string path = "";
            string internal_path = Path.Combine(RootDirectory, pathOrName);

            if (File.Exists(pathOrName))
                path = pathOrName;
            else if (File.Exists(internal_path))
                path = internal_path;
            else
                return new DeserializationResult() { error = "File not found!! :: " + pathOrName };


            try
            {

                FileStream xml_file = new FileStream(path, FileMode.Open);
                var types = GetTypesFromXml(xml_file);
                xml_file.Close();



                if (types == null)
                    return new DeserializationResult() { error = "Document error :: Metadata not found! " + pathOrName };

                using (FileStream fs = new FileStream(path, FileMode.Open))
                {

                    XmlSerializer serializer = new XmlSerializer(typeof(CanvasContainer), types.ToArray());

                    CanvasContainer container = (CanvasContainer)serializer.Deserialize(fs);

                    return new DeserializationResult() { canvas = container.canvas };
                }
            }
            catch (Exception e)
            {
                return new DeserializationResult() { error = e.ToString() };
            }

        }

        //helper method// convierte una coleccion de Type en una coleccion de strings para generar metadata para la deserializacion
        CanvasMetadata GenerateCanvasMetadata(Type[] types)
        {

            var sTypes = new String[types.Length];
            for (int i = 0; i < types.Length; i++)
            {
                sTypes[i] = types[i].ToString();
            }

            return new CanvasMetadata() { types = sTypes };
        }

        //Extrae el nodo Metadata del documento xml para determinar los tipos de figuras que tiene que usar el deserializador
        //podriamos recorrer el documento entero y sacarle el atributo "xsi:type" a cada figura, pero eso seria muy lento en un documento grande (esta comentado el ejemplo abajo)
        //para optimizar esto, guardamos un array de strings en CanvasMetadata con los tipos de figuras usados en ese canvas, esos tipos son determinados en el momento de la serializacion
        Type[] GetTypesFromXml(Stream stream)
        {

            XmlDocument document = new XmlDocument();
            document.Load(stream);

            XmlNodeList types = document.SelectNodes("//Metadata//types//type");

            if (types == null)
            {
                Console.WriteLine("Document metadata <types> not found!");
                return null;
            }

            //en este punto tenemos todos los nodos bajo la etiqueta <types>, que hacen referencia a nuestras clases en c# | ejemplo  <type>Canvas.Circle</type>

            List<Type> identifiedTypes = new List<Type>();

            foreach (XmlNode type in types)
            {
                //Con el nombre de las clases generamos el tipo por reflexion y lo agregamos a la lista para retornarlos a todos.
                identifiedTypes.Add( Type.GetType(type.InnerText) );
            }

            return identifiedTypes.ToArray();

        }

        //obtiene todos los diferentes tipos de figuras dentro del canvas
        Type[] GetTypesFromCanvas(Canvas canvas)
        {
            //obtenemos un elemento de cada tipo dentro del canvas//
            var figures = canvas
                .GroupBy(f => f.GetType())
                .Select(g => g.First())
                .ToArray();

            List<Type> types = new List<Type>();

            //le pedimos el tipo a cada uno
            for (int i = 0; i < figures.Length; i++)
            {
                types.Add(figures[i].GetType());
            }

            
            //types.Add(typeof(Style));

            return types.ToArray();
        }


        //Asegura que la extension del file sea .xml
        string NormalizedName(string name)
        {

            return Path.GetExtension(name).Equals(".xml") ? name : name + ".xml";
        }

        //Permite cambiar la carpeta donde se van a guardar todos los archivos generados//
        public void SetRootDirectory(string path)
        {
            CreateDirectoryIfNotExist(path);

            RootDirectory = path;
        }

        public static string GetRootDirectory() {
            return RootDirectory;
        }

        //Crea el directorio donde se van a guardar los archivos (si no existe)
        void CreateDirectoryIfNotExist(string path)
        {

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Nuevo directorio creado : " + path);

            }
        }
    }
}
